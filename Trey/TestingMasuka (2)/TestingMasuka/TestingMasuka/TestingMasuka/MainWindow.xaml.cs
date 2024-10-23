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
            btnRecord.Click += btnRecord_Click;
            

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



        public void btnRecord_Click(object sender, RoutedEventArgs e)
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


       


        //public void ConvertMp3ToFrequencies(string mp3FilePath)
        //{

        //    AudioFilePeakLogger readFile = new AudioFilePeakLogger(mp3FilePath);

        //    readFile.ProcessFile(@"C:\Users\treya\Downloads\Capstone\TestingMasuka (2)\TestingMasuka\TestingMasuka\TestingMasuka\bin\Debug\net8.0-windows\frequencies.txt");
        //    MessageBox.Show("Completed Processing");




        //}

        private void btnConvert_click(object sender, RoutedEventArgs e)
        {
            ConvertMp3ToFrequencies(selectedMp3FilePath);
        }

        private void btnLoad_click(object sender, RoutedEventArgs e)
        {
            var viewModel = (TestData)DataContext;
            viewModel.LoadTestData();
        }


        public void ConvertMp3ToFrequencies(string mp3FilePath)
        {

            AudioFilePeakLogger readFile = new AudioFilePeakLogger(mp3FilePath);

            readFile.ProcessFile(@"C:\Users\treya\Downloads\Capstone\TestingMasuka (2)\TestingMasuka\TestingMasuka\TestingMasuka\bin\Debug\net8.0-windows\frequencies.txt");
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
    }

}
