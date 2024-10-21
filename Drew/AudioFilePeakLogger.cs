﻿using NAudio.Wave;
using System;

namespace AudioMonitor
{
    public class AudioFilePeakLogger
    {
        readonly double[] AudioValues;
        readonly double[] FftValues;
        readonly int sampleRate;
        readonly int bytesPerSample;
        readonly WaveFormat waveFormat;

        readonly string Filepath = "";
        
        public AudioFilePeakLogger(string filePath)
        {
            // Read the audio file
            Filepath = filePath;
            using var reader = new AudioFileReader(Filepath);
            waveFormat = reader.WaveFormat;
            sampleRate = waveFormat.SampleRate;
            bytesPerSample = waveFormat.BitsPerSample / 8;

            //Prep work for processing using FFT
            AudioValues = new double[sampleRate / 100];  // 10ms chunks
            double[] paddedAudio = FftSharp.Pad.ZeroPad(AudioValues);
            double[] fftMag = FftSharp.Transform.FFTpower(paddedAudio);
            FftValues = new double[fftMag.Length];
        }

        public void ProcessFile(string outputLogFile)
        {
            using var reader = new AudioFileReader(Filepath);
            string logFilePath = outputLogFile;

            // Process the audio file in chunks
            byte[] buffer = new byte[AudioValues.Length * bytesPerSample]; // buffer size based on the chunk size
            int bytesRead;
            int sampleIndex = 0;

            while ((bytesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
            {
                int bufferSampleCount = bytesRead / bytesPerSample;
                ProcessChunk(buffer, bufferSampleCount, sampleIndex, logFilePath);
                sampleIndex += bufferSampleCount;
            }
        }

        private void ProcessChunk(byte[] buffer, int bufferSampleCount, int sampleIndex, string logFilePath)
        {
            // Convert the buffer data into AudioValues
            if (waveFormat.Encoding == WaveFormatEncoding.Pcm)
            {
                if (bytesPerSample == 2) // 16-bit PCM
                {
                    for (int i = 0; i < bufferSampleCount; i++)
                        AudioValues[i] = BitConverter.ToInt16(buffer, i * bytesPerSample);
                }
                else if (bytesPerSample == 4) // 32-bit PCM
                {
                    for (int i = 0; i < bufferSampleCount; i++)
                        AudioValues[i] = BitConverter.ToInt32(buffer, i * bytesPerSample);
                }
            }
            else if (waveFormat.Encoding == WaveFormatEncoding.IeeeFloat && bytesPerSample == 4) // 32-bit float
            {
                for (int i = 0; i < bufferSampleCount; i++)
                    AudioValues[i] = BitConverter.ToSingle(buffer, i * bytesPerSample);
            }
            else
            {
                throw new NotSupportedException(waveFormat.ToString());
            }

            // Perform FFT
            double[] paddedAudio = FftSharp.Pad.ZeroPad(AudioValues);
            double[] fftMag = FftSharp.Transform.FFTmagnitude(paddedAudio);
            Array.Copy(fftMag, FftValues, fftMag.Length);

            // Find the peak frequency
            int peakIndex = 0;
            for (int i = 0; i < fftMag.Length; i++)
            {
                if (fftMag[i] > fftMag[peakIndex])
                    peakIndex = i;
            }

            double fftPeriod = FftSharp.Transform.FFTfreqPeriod(sampleRate, fftMag.Length);
            double peakFrequency = fftPeriod * peakIndex;

            // Calculate the time in the audio file
            double currentTime = (double)sampleIndex / sampleRate;

            // Convert frequency to note and octave
            var (note, octave) = FreqToNote(peakFrequency);

            // Log the peak frequency, note, octave, and timestamp
            if (peakFrequency > 0)
            {
                string logMessage = $"Timestamp: {currentTime:N2} sec, Peak Frequency: {peakFrequency:N2} Hz, Note: {note}, Octave: {octave}{Environment.NewLine}";
                File.AppendAllText(logFilePath, logMessage);
            }

        }

        static (string, int) FreqToNote(double freq)
        {
            string[] notes = { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };

            // Calculate the note
            double noteNumber = 12 * Math.Log2(freq / 440) + 49;
            int roundedNoteNumber = (int)Math.Round(noteNumber);
            int noteIndex = (roundedNoteNumber - 1) % notes.Length;
            if (noteIndex < 0) noteIndex += notes.Length;  //negative modulo result fix

            string note = notes[noteIndex];
            int octave = (roundedNoteNumber + 8) / notes.Length;

            return (note, octave);
        }
    }
}