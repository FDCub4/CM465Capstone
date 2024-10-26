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
//using ScottPlot.WinForms;
using ScottPlot.WPF;
//using MathNet.Numerics;
using System.Numerics;
using Manufaktura.Controls.WPF;
using System.Windows.Media;
using System.Drawing;



namespace TestingMasuka
{
    public partial class MainWindow : Window
    {
        private WaveViewer waveViewer;
        private string selectedMp3FilePath;
        public MainWindow()
        {
            InitializeComponent();

            var viewModel = new TestData();
            DataContext = viewModel;
            

            //viewModel.LoadTestData();

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

        private BitmapSource GetPlotBitmap(WpfPlot plot)
        {
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap(
                (int)plot.ActualWidth,
                (int)plot.ActualHeight,
                96d,
                96d,
                PixelFormats.Pbgra32);

            renderTargetBitmap.Render(plot);
            return renderTargetBitmap;
        }
        private Bitmap RenderNoteViewerToImage(NoteViewer noteViewer)
        {

            noteViewer.Measure(new System.Windows.Size(Double.PositiveInfinity, Double.PositiveInfinity));
            noteViewer.Arrange(new Rect(0, 0, noteViewer.DesiredSize.Width, noteViewer.DesiredSize.Height));
            noteViewer.UpdateLayout();


            var renderBitmap = new RenderTargetBitmap(
                (int)noteViewer.ActualWidth,
                (int)noteViewer.ActualHeight,
                96d,
                96d,
                System.Windows.Media.PixelFormats.Pbgra32);


            renderBitmap.Render(noteViewer);


            var bitmap = new System.Drawing.Bitmap(renderBitmap.PixelWidth, renderBitmap.PixelHeight);
            var bitmapData = bitmap.LockBits(new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            renderBitmap.CopyPixels(Int32Rect.Empty, bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);
            bitmap.UnlockBits(bitmapData);

            return bitmap;
        }



        private void btnExportPDF_click(object sender, RoutedEventArgs e)
        {

            var noteViewer = NoteViewer1;
            string notesOutput = noteViewer?.ScoreSource.ToString() ?? "No notes available";



            Bitmap noteViewerImage = RenderNoteViewerToImage(noteViewer);


            PdfDocument document = new PdfDocument();
            document.Info.Title = "Musical Notes PDF";


            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 20);





            if (noteViewerImage != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    noteViewerImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    stream.Position = 0;
                    XImage xImage = XImage.FromStream(stream);


                    int originalWidth = noteViewerImage.Width;
                    int originalHeight = noteViewerImage.Height;

                    double aspectRatio = (double)originalHeight / originalWidth;


                    double desiredWidth = page.Width;

                    double desiredHeight = desiredWidth * aspectRatio;


                    gfx.DrawImage(xImage, 0, 0, desiredWidth, desiredHeight);
                }
            }


            string pdfFileName = "MusicalNotes.pdf";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), pdfFileName);


            try
            {
                document.Save(filePath);
                MessageBox.Show($"PDF file '{filePath}' has been created successfully!");
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

        


        public async void btnRecord_Click(object sender, RoutedEventArgs e)
        {
            PlayAudio();

            // Await the asynchronous conversion process
            await ConvertMp3ToFrequencies(selectedMp3FilePath);

            // Load test data after processing is complete
            var viewModel = (TestData)DataContext;
            viewModel.LoadTestData();
        }


        //private void btnLoadNotes_click(object sender, RoutedEventArgs e)
        //{
        //    var viewModel = (TestData)DataContext;
        //    viewModel.LoadTestData();
        //}
        //private void btnConvert_click(object sender, RoutedEventArgs e)
        //{
        //    ConvertMp3ToFrequencies(selectedMp3FilePath);
        //}


        public async Task ConvertMp3ToFrequencies(string mp3FilePath)
        {
            var progress = new Progress<int>(value =>
            {
                LoadingProgressBar.Value = value; // Update progress bar value
            });

            AudioFilePeakLogger readFile = new AudioFilePeakLogger(mp3FilePath);

            await Task.Run(() => readFile.ProcessFile(@"C:\Users\treya\OneDrive\Documents\GitHub\CM465Capstone\Trey\TestingMasuka (2)\TestingMasuka\TestingMasuka\TestingMasuka\bin\Debug\net8.0-windows\frequencies.txt", progress));

            MessageBox.Show("Completed Processing");
        }

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


        private IWavePlayer outputDevice; // Class-level variable to hold the output device

        public void PlayAudio()
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
                    outputDevice = new WaveOutEvent();
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    MessageBox.Show("File is playing. Click 'Stop' to stop playback.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred, please try a different file: " + ex.Message);
            }
        }

        private void btnStopPlayingAudio_Click(object sender, RoutedEventArgs e)
        {
            if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
            {
                outputDevice.Stop();
                MessageBox.Show("Playback stopped.");
            }
        }
    }

}
