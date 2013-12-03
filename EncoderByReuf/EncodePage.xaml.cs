using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for EncodePage.xaml
    /// </summary>
    public partial class EncodePage : UserControl
    {
        public EncodePage()
        {
            InitializeComponent();
            this.inputBox.Focus();
        }

        private void encodeButton_Click(object sender, RoutedEventArgs e)
        {
            PowerEncoding powerEncoding = new PowerEncoding();
            resultBox.Text = powerEncoding.Encode(inputBox.Text);
            if(resultBox.Text.Length/4 != inputBox.Text.Length)
            {
                ModernDialog.ShowMessage("Some special characters may have been skipped due to the PowerEncoding.", "Warning", MessageBoxButton.OK);
            }
            hashBox.Text = CryptoRepository.CalculateHash(
                inputBox.Text,
                HashAlgorithm.Create((hashComboBox.SelectedValue as ComboBoxItem).Content.ToString()),
                (bool)base64RadioButton.IsChecked);
        }

        private void decodeButton_Click(object sender, RoutedEventArgs e)
        {
            PowerEncoding powerEncoding = new PowerEncoding();
            try
            {
                resultBox.Text = powerEncoding.Decode(inputBox.Text);
                hashBox.Text = CryptoRepository.CalculateHash(
                    resultBox.Text,
                    HashAlgorithm.Create((hashComboBox.SelectedValue as ComboBoxItem).Content.ToString()),
                    (bool)base64RadioButton.IsChecked);
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message, "Error", MessageBoxButton.OK);
            }
        }

        private void copyFromResultButton_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Text = resultBox.Text;
            resultBox.Text = "";
        }

    }
}
