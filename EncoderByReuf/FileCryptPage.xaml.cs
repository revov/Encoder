using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommonCryptography;

namespace EncoderByReuf
{
    /// <summary>
    /// Interaction logic for FileCryptPage.xaml
    /// </summary>
    public partial class FileCryptPage : UserControl
    {
        private readonly AESCrypter _AESProvider = new AESCrypter(
            ConfigurationManager.AppSettings["AesKey"],
            ConfigurationManager.AppSettings["AesIv"],
            ConfigurationManager.AppSettings["AesSalt"]);

        public FileCryptPage()
        {
            InitializeComponent();
        }

        private void EncryptFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
                {
                    Title = "Select a file to encrypt.",
                    Multiselect = false
                };
            fileDialog.ShowDialog();
            if(!string.IsNullOrEmpty(fileDialog.FileName))
            {
                BackgroundWorker backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += backgroundWorker_EncryptFile;
                backgroundWorker.RunWorkerCompleted += backgroundWorker_WorkerCompleted;
                CryptoProgressBar.IsIndeterminate = true;
                backgroundWorker.RunWorkerAsync(fileDialog);
            }
        }

        private void DecryptFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Title = "Select a file to decrypt.",
                Multiselect = false,
                Filter = "Encrypted files (*.cdn)|*.cdn"
            };
            fileDialog.ShowDialog();
            if (!string.IsNullOrEmpty(fileDialog.FileName))
            {
                BackgroundWorker backgroundWorker = new BackgroundWorker();
                backgroundWorker.DoWork += backgroundWorker_DecryptFile;
                backgroundWorker.RunWorkerCompleted += backgroundWorker_WorkerCompleted;
                CryptoProgressBar.IsIndeterminate = true;
                backgroundWorker.RunWorkerAsync(fileDialog);
            }
        }

        private void backgroundWorker_EncryptFile(object sender, DoWorkEventArgs e)
        {            
            BackgroundWorker backgroundWorker = sender as BackgroundWorker;
            OpenFileDialog fileDialog = e.Argument as OpenFileDialog;

            byte[] inputFile = File.ReadAllBytes(fileDialog.FileName);
            string logMessage = fileDialog.SafeFileName + " original: "
                + CryptoRepository.CalculateHMACSHA384(inputFile)
                + Environment.NewLine;
            LogBox.Dispatcher.BeginInvoke(new Action(() => LogBox.AppendText(logMessage)));

            byte[] encryptedFile = _AESProvider.Encrypt(inputFile);
            logMessage = fileDialog.SafeFileName + " encrypted: "
                + CryptoRepository.CalculateHMACSHA384(encryptedFile)
                + Environment.NewLine;
            LogBox.Dispatcher.BeginInvoke(new Action(() => LogBox.AppendText(logMessage)));

            byte[] compressedFile = CryptoRepository.Compress(encryptedFile);
            logMessage = fileDialog.SafeFileName + " compressed: "
                + CryptoRepository.CalculateHMACSHA384(compressedFile)
                + Environment.NewLine;
            LogBox.Dispatcher.BeginInvoke(new Action(() => LogBox.AppendText(logMessage)));

            File.WriteAllBytes(fileDialog.FileName + ".cdn", compressedFile);
            
            e.Result = fileDialog.FileName + " was encrypted successfully!";
            
        }

        private void backgroundWorker_DecryptFile(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker backgroundWorker = sender as BackgroundWorker;
            OpenFileDialog fileDialog = e.Argument as OpenFileDialog;

            byte[] inputFile = File.ReadAllBytes(fileDialog.FileName);
            string logMessage = fileDialog.SafeFileName + " compressed: "
                + CryptoRepository.CalculateHMACSHA384(inputFile)
                + Environment.NewLine;
            LogBox.Dispatcher.BeginInvoke(new Action(() => LogBox.AppendText(logMessage)));

            byte[] decompressedFile = CryptoRepository.Decompress(inputFile);
            logMessage = fileDialog.SafeFileName + " encrypted: "
                + CryptoRepository.CalculateHMACSHA384(decompressedFile)
                + Environment.NewLine;
            LogBox.Dispatcher.BeginInvoke(new Action(() => LogBox.AppendText(logMessage)));

            byte[] decryptedFile = _AESProvider.Decrypt(decompressedFile);
            logMessage = fileDialog.SafeFileName + " original: "
                + CryptoRepository.CalculateHMACSHA384(decryptedFile)
                + Environment.NewLine;
            LogBox.Dispatcher.BeginInvoke(new Action(() => LogBox.AppendText(logMessage)));

            File.WriteAllBytes(fileDialog.FileName.Replace(".cdn", ""), decryptedFile);

            e.Result = fileDialog.FileName + " was decrypted successfully!";

        }

        private void backgroundWorker_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CryptoProgressBar.IsIndeterminate = false;
            ModernDialog.ShowMessage(e.Result as string, "Completed!", MessageBoxButton.OK);
            LogBox.AppendText(e.Result + "\n------------------------------\n");
            GC.Collect();
        }

    }
}
