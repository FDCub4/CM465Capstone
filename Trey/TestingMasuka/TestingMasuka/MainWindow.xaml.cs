using Manufaktura.Controls.Model;
using Manufaktura.Music.Model.MajorAndMinor;
using Manufaktura.Music.Model;
using System.Windows;
using static System.Formats.Asn1.AsnWriter;
using NAudio.Wave;
using NAudio.Gui;
using NAudio.CoreAudioApi;
using System;
using System.Windows;
using PdfSharp;
using System.Reflection.Metadata;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using Microsoft.VisualBasic.ApplicationServices;
using System.IO;
using Microsoft.Win32; // For SaveFileDialog and OpenFileDialog
using System;
using System.IO; // For File and Directory operations
using System.Windows;
using System.Windows.Media.Imaging;
using NAudio.CoreAudioApi;
using ScottPlot;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using ScottPlot.WinForms;
using ScottPlot.WPF;



namespace TestingMasuka
{
    public partial class MainWindow : Window
    {
        private WaveViewer waveViewer;

        public MainWindow()
        {
            InitializeComponent();

            var viewModel = new TestDataViewModel();
            DataContext = viewModel;

            viewModel.LoadTestData();


        }
        private void btnMenu_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Open clicked");
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Music Files (*.mp3;*.wav)|*.mp3;*.wav|All Files (*.*)|*.*",
                Title = "Save Music File"
            };

            if (saveFileDialog.ShowDialog() == true)
            {

                MessageBox.Show("File saved: " + saveFileDialog.FileName);
            }
        }


        private void btnExportPDF_click(object sender, RoutedEventArgs e)
        {

            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";


            PdfPage page = document.AddPage();


            XGraphics gfx = XGraphics.FromPdfPage(page);


            XFont font = new XFont("Verdana", 20);


            gfx.DrawString(selectedMp3FilePath, font, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height),
            XStringFormats.Center);



            string pdfFileName = Path.GetFileNameWithoutExtension(selectedMp3FilePath);

            string filename = @$"C:\Users\treya\Downloads\{pdfFileName}.pdf";

            try
            {
                document.Save(filename);
                MessageBox.Show($"PDF file '{filename}' has been created successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to save PDF file. Error: {ex.Message}");
            }

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUndo_Click(object sender, RoutedEventArgs e)
        {
            txtFileSelected.Clear();

            MessageBox.Show("Undo clicked");
        }

        private void btnRedo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Redo clicked");
        }

        private string selectedMp3FilePath;

        private string LoadFiles()
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {

                Filter = "MP3 files (*.mp3)|*.mp3",
                Multiselect = false
            };

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string filePath = openFileDialog.FileName;

                return filePath;
            }

            return null;
        }

        private void btnLoadFile_Click(object sender, RoutedEventArgs e)
        {
            selectedMp3FilePath = LoadFiles();

            if (!string.IsNullOrEmpty(selectedMp3FilePath))
            {
                txtFileSelected.Text = Path.GetFileName(selectedMp3FilePath);
                MessageBox.Show($"MP3 file '{Path.GetFileName(selectedMp3FilePath)}' selected.");
            }
            else
            {
                MessageBox.Show("No file selected.");
            }
        }



        private void btnRecord_Click(object sender, RoutedEventArgs e)
        {

            string nameOfMP3File = selectedMp3FilePath;
            if (string.IsNullOrEmpty(nameOfMP3File))
            {
                MessageBox.Show("Please enter a valid mp3 file location");
                return;
            }
            try
            {
                using (var audioFile = new AudioFileReader(nameOfMP3File))
                {
                    using (var outputDevice = new WaveOutEvent())
                    {
                        outputDevice.Init(audioFile);
                        outputDevice.Play();

                        MessageBox.Show("File is playing, press any key to stop.");
                        Console.ReadKey();

                        outputDevice.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured, please try a different file" + ex.Message);
            }

        }

        private void txtFileSelected_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {


        }

        private void btnExit_click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnHelp_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This is an application that transcribes audio files into " +
                "sheet music.  This application has the ability to save transcribed files into PDF " +
                "forms, and allow for saving to personal devices.");
        }


        readonly double[] AudioValues;
        readonly WasapiCapture AudioDevice;

        
        private void AudioMonitorForm_FormClosed(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Closing audio device: {AudioDevice}");
            AudioDevice.StopRecording();
            AudioDevice.Dispose();
        }
        
        private void WaveIn_DataAvailable(object? sender, WaveInEventArgs e)
        {
            int bytesPerSamplePerChannel = AudioDevice.WaveFormat.BitsPerSample / 8;
            int bytesPerSample = bytesPerSamplePerChannel * AudioDevice.WaveFormat.Channels;
            int bufferSampleCount = e.Buffer.Length / bytesPerSample;
            if (bufferSampleCount >= AudioValues.Length)
            {
                bufferSampleCount = AudioValues.Length;
            }
            if (bytesPerSamplePerChannel == 2 && AudioDevice.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
            {
                for (int i = 0; i < bufferSampleCount; i++)
                    AudioValues[i] = BitConverter.ToInt16(e.Buffer, i * bytesPerSample);
            }
            else if (bytesPerSamplePerChannel == 4 && AudioDevice.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
            {
                for (int i = 0; i < bufferSampleCount; i++)
                    AudioValues[i] = BitConverter.ToInt32(e.Buffer, i * bytesPerSample);
            }
            else if (bytesPerSamplePerChannel == 4 && AudioDevice.WaveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
            {
                for (int i = 0; i < bufferSampleCount; i++)
                    AudioValues[i] = BitConverter.ToSingle(e.Buffer, i * bytesPerSample);
            }
            else
            {
                throw new NotSupportedException(AudioDevice.WaveFormat.ToString());
            }
        }

        private void noteViewer1_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }

    public class TestDataViewModel : ViewModel
    {
        private Score data;
        public Score Data
        {
            get { return data; }
            set { data = value; OnPropertyChanged(() => Data); }
        }

        readonly double[] AudioValues;
        readonly WasapiCapture AudioDevice;
        readonly double[] FftValues;
        
        private void FftMonitorForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Closing audio device: {AudioDevice}");
            AudioDevice.StopRecording();
            AudioDevice.Dispose();
        }
        private void WaveIn_DataAvailable(object? sender, WaveInEventArgs e)
        {
            int bytesPerSamplePerChannel = AudioDevice.WaveFormat.BitsPerSample / 8;
            int bytesPerSample = bytesPerSamplePerChannel * AudioDevice.WaveFormat.Channels;
            int bufferSampleCount = e.Buffer.Length / bytesPerSample;
            if (bufferSampleCount >= AudioValues.Length)
            {
                bufferSampleCount = AudioValues.Length;
            }
            if (bytesPerSamplePerChannel == 2 && AudioDevice.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
            {
                for (int i = 0; i < bufferSampleCount; i++)
                    AudioValues[i] = BitConverter.ToInt16(e.Buffer, i * bytesPerSample);
            }
            else if (bytesPerSamplePerChannel == 4 && AudioDevice.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
            {
                for (int i = 0; i < bufferSampleCount; i++)
                    AudioValues[i] = BitConverter.ToInt32(e.Buffer, i * bytesPerSample);
            }
            else if (bytesPerSamplePerChannel == 4 && AudioDevice.WaveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
            {
                for (int i = 0; i < bufferSampleCount; i++)
                    AudioValues[i] = BitConverter.ToSingle(e.Buffer, i * bytesPerSample);
            }
            else
            {
                throw new NotSupportedException(AudioDevice.WaveFormat.ToString());
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            WpfPlot wpfPlot = new WpfPlot();
            double[] paddedAudio = FftSharp.Pad.ZeroPad(AudioValues);
            double[] fftMag = FftSharp.Transform.FFTmagnitude(paddedAudio);
            Array.Copy(fftMag, FftValues, fftMag.Length);
            // find the frequency peak
            int peakIndex = 0;
            for (int i = 0; i < fftMag.Length; i++)
            {
                if (fftMag[i] > fftMag[peakIndex])
                    peakIndex = i;
            }
            double fftPeriod = FftSharp.Transform.FFTfreqPeriod(AudioDevice.WaveFormat.SampleRate, fftMag.Length);
            double peakFrequency = fftPeriod * peakIndex;
            var (note, octave) = FreqToNote(peakFrequency);
            string logFilePath = "frequencies.txt";
            string logMessage = $"Peak Frequency: {peakFrequency:N2} Hz, Note: {note}, Octave: {octave}, at {DateTime.Now}{Environment.NewLine}";
            File.AppendAllText(logFilePath, logMessage);  // Write the log message with note and octave
                                                          //label1.Text = $"Peak Frequency: {peakFrequency:N0} Hz";
                                                          // request a redraw using a non-blocking render queue
            wpfPlot.Refresh();
        }
        //Use this method to return the frequency, note, and octave

       // public (double, string, int) doFFT()
       public (string, int)doFFT()
        {
            double[] paddedAudio = FftSharp.Pad.ZeroPad(AudioValues);
            double[] fftMag = FftSharp.Transform.FFTmagnitude(paddedAudio);
            Array.Copy(fftMag, FftValues, fftMag.Length);
            // find the frequency peak
            int peakIndex = 0;
            for (int i = 0; i < fftMag.Length; i++)
            {
                if (fftMag[i] > fftMag[peakIndex])
                    peakIndex = i;
            }
            double fftPeriod = FftSharp.Transform.FFTfreqPeriod(AudioDevice.WaveFormat.SampleRate, fftMag.Length);
            double peakFrequency = fftPeriod * peakIndex;
            var (note, octave) = FreqToNote(peakFrequency);
            // return (peakFrequency, note, octave);
            return (note, octave);
        }


        public void LoadTestData()
        {

            double freq = 440;
            double noteNumber = 12 * Math.Log2(freq / 440) + 49;
            var (note, octave) = FreqToNote(freq);
            Pitch pitch =  MapNoteToPitch(note, octave);

            // Map note to Manufaktura Pitch
            var score = Score.CreateOneStaffScore(Clef.Treble, new MajorScale(Step.C, false));
            score.FirstStaff.Elements.Add(new Note(pitch, RhythmicDuration.Quarter));
            Data = score;
            score.Staves[0].Elements.Add(new Note(pitch, RhythmicDuration.Half));


            //var score = Score.CreateOneStaffScore(Clef.Treble, new MajorScale(Step.C, false));
            //score.FirstStaff.Elements.Add(new Note(Pitch.ASharp4, RhythmicDuration.Quarter));
            //score.FirstStaff.Elements.Add(new Note(Pitch.B4, RhythmicDuration.Quarter));
            //score.FirstStaff.Elements.Add(new Note(Pitch.C5, RhythmicDuration.Half));
            //score.FirstStaff.Elements.Add(new Barline());
            //Data = score;

            //var secondStaff = new Staff();
            //secondStaff.Elements.Add(Clef.Treble);
            //secondStaff.Elements.Add(new Manufaktura.Controls.Model.Key(0));
            //secondStaff.Elements.Add(new Note(Pitch.G4, RhythmicDuration.Whole));
            //secondStaff.Elements.Add(new Barline());
            //score.Staves.Add(secondStaff);
            //score.Staves.Add(new Staff());

            //score.ThirdStaff.Elements.Add(Clef.Tenor);
            //score.ThirdStaff.Elements.Add(new Manufaktura.Controls.Model.Key(0));
            //score.ThirdStaff.Elements.Add(new Note(Pitch.D4, RhythmicDuration.Half));
            //score.ThirdStaff.Elements.Add(new Note(Pitch.E4, RhythmicDuration.Half));
            //score.ThirdStaff.Elements.Add(new Barline());
            //score.Staves.Add(new Staff());

            //score.Staves[3].Elements.Add(Clef.Bass);    //0-based index
            //score.Staves[3].Elements.Add(new Manufaktura.Controls.Model.Key(0));
            //score.Staves[3].Elements.Add(new Note(Pitch.G3, RhythmicDuration.Half));
            //score.Staves[3].Elements.Add(new Note(Pitch.C3, RhythmicDuration.Half));
            //score.Staves[3].Elements.Add(new Barline());
            //for(int i =1; i<10; i++)
            //{
            //    var score = Score.CreateOneStaffScore(Clef.Treble, new MajorScale(Step.C, false));
            //    score.FirstStaff.Elements.Add(new Note(Step.i.toString(), RhythmicDuration.Quarter));
            //}
            //might have to recreate a class within our own project to handle pitch, see website:
            //https://bitbucket.org/Ajcek/manufakturalibraries/src/master/Manufaktura.Music/Model/Pitch.cs
            //this shows how they determine pitch, so for us, we will need to recreate the library/dictionary
            //for our own inputs, after data analysis has occured to find frequency/pitch
            //var score = Score.CreateOneStaffScore(Clef.Treble, new MajorScale(Step.C, false));

            //score.FirstStaff.Elements.Add(new Note(Pitch.C1, RhythmicDuration.Quarter));
            //Data = score;
            //score.FirstStaff.Elements.Add(new Barline());




        }
        public static (string, int) FreqToNote(double freq)
        {
            string[] notes = { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };
            // Calculate the note number
            double noteNumber = 12 * Math.Log2(freq / 440) + 49;
            int roundedNoteNumber = (int)Math.Round(noteNumber);
            // Ensure noteIndex is within the bounds of the array
            int noteIndex = (roundedNoteNumber - 1) % notes.Length;
            if (noteIndex < 0)
                noteIndex += notes.Length;  // Fix for negative modulo result
            string note = notes[noteIndex];
            // Determine the octave
            int octave = (roundedNoteNumber + 8) / notes.Length;
            return (note, octave);
        }


        public static Pitch MapNoteToPitch(string note, int octave)
        {
            switch (note)
            {
                case "A": return Pitch.A.ToPitch(octave);
                case "A#": return Pitch.ASharp.ToPitch(octave);
                case "B": return Pitch.B.ToPitch(octave);
                case "C": return Pitch.C.ToPitch(octave);
                case "C#": return Pitch.CSharp.ToPitch(octave);
                case "D": return Pitch.D.ToPitch(octave);
                case "D#": return Pitch.DSharp.ToPitch(octave);
                case "E": return Pitch.E.ToPitch(octave);
                case "F": return Pitch.F.ToPitch(octave);
                case "F#": return Pitch.FSharp.ToPitch(octave);
                case "G": return Pitch.G.ToPitch(octave);
                case "G#": return Pitch.GSharp.ToPitch(octave);
                default: throw new ArgumentException("Invalid note name");
            }
        }

        public partial class AudioMonitorForm : Form
        {
            private ScottPlot.WPF.WpfPlot wpfPlot;
            readonly double[] AudioValues;
            readonly WasapiCapture AudioDevice;
            public AudioMonitorForm(WasapiCapture captureDevice)
            {
                
                AudioDevice = captureDevice;
                WaveFormat fmt = captureDevice.WaveFormat;
                AudioValues = new double[fmt.SampleRate * 10 / 1000]; // 10 milliseconds
                wpfPlot.Plot.Add.Signal(AudioValues, fmt.SampleRate / 1000);
                wpfPlot.Plot.Add.Signal(AudioValues, fmt.SampleRate / 1000);
                wpfPlot.Plot.YLabel("Level");
                wpfPlot.Plot.XLabel("Time (milliseconds)");
                wpfPlot.Plot.Title($"{fmt.Encoding} ({fmt.BitsPerSample}-bit) {fmt.SampleRate} KHz");
                wpfPlot.Plot.Axes.SetLimits(-.5, .5);
                wpfPlot.Refresh();
                AudioDevice.DataAvailable += WaveIn_DataAvailable;
                AudioDevice.StartRecording();
            }
            private void AudioMonitorForm_FormClosed(object sender, FormClosedEventArgs e)
            {
                System.Diagnostics.Debug.WriteLine($"Closing audio device: {AudioDevice}");
                AudioDevice.StopRecording();
                AudioDevice.Dispose();
            }
            private void timer1_Tick(object sender, EventArgs e)
            {
                wpfPlot.Refresh();
            }
            private void WaveIn_DataAvailable(object? sender, WaveInEventArgs e)
            {
                int bytesPerSamplePerChannel = AudioDevice.WaveFormat.BitsPerSample / 8;
                int bytesPerSample = bytesPerSamplePerChannel * AudioDevice.WaveFormat.Channels;
                int bufferSampleCount = e.Buffer.Length / bytesPerSample;
                if (bufferSampleCount >= AudioValues.Length)
                {
                    bufferSampleCount = AudioValues.Length;
                }
                if (bytesPerSamplePerChannel == 2 && AudioDevice.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
                {
                    for (int i = 0; i < bufferSampleCount; i++)
                        AudioValues[i] = BitConverter.ToInt16(e.Buffer, i * bytesPerSample);
                }
                else if (bytesPerSamplePerChannel == 4 && AudioDevice.WaveFormat.Encoding == WaveFormatEncoding.Pcm)
                {
                    for (int i = 0; i < bufferSampleCount; i++)
                        AudioValues[i] = BitConverter.ToInt32(e.Buffer, i * bytesPerSample);
                }
                else if (bytesPerSamplePerChannel == 4 && AudioDevice.WaveFormat.Encoding == WaveFormatEncoding.IeeeFloat)
                {
                    for (int i = 0; i < bufferSampleCount; i++)
                        AudioValues[i] = BitConverter.ToSingle(e.Buffer, i * bytesPerSample);
                }
                else
                {
                    throw new NotSupportedException(AudioDevice.WaveFormat.ToString());
                }
            }
        }

    }
}
