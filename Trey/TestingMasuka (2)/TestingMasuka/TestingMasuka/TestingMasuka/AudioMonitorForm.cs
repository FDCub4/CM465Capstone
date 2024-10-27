using NAudio.CoreAudioApi;
using NAudio.Wave;
using System.Windows.Forms;

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

    
