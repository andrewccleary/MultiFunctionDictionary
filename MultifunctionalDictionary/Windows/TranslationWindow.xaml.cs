using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Configuration;
using MultifunctionalDictionary.Helper;

namespace MultifunctionalDictionary.Windows
{
    /// <summary>
    /// Interaction logic for TranslationWindow.xaml
    /// </summary>
    public partial class TranslationWindow : Window
    {
        DatabaseHelper dh;

        public TranslationWindow()
        {
            InitializeComponent();

            string server = ConfigurationManager.AppSettings["server"];
            string port = ConfigurationManager.AppSettings["port"];
            string user = ConfigurationManager.AppSettings["user"];
            string password = ConfigurationManager.AppSettings["password"];
            string database = ConfigurationManager.AppSettings["database"];

            dh = new DatabaseHelper(server, port, user, password, database);
            dh.AcquireConnection();

            clearButton.IsEnabled = false;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            referenceNumberTextBox.Clear();
            hebrewWordTextBox.Clear();
            hebrewTranslationTextBox.Clear();
            pronunciationTextBox.Clear();
            definitionTextBox.Clear();
        }

        private void ReferenceNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            clearButton.IsEnabled = true;
        }

        private void HebrewWordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            clearButton.IsEnabled = true;
        }

        private void HebrewTranslationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            clearButton.IsEnabled = true;
        }

        private void PronunciationTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            clearButton.IsEnabled = true;
        }

        private void DefinitionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            clearButton.IsEnabled = true;
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            TranslationHelper th = new TranslationHelper(dh.GetConnection());
            String result;

            result = th.importTranslation(Convert.ToInt32(referenceNumberTextBox.Text), hebrewWordTextBox.Text, hebrewTranslationTextBox.Text, 
                    pronunciationTextBox.Text, definitionTextBox.Text);

            MessageBox.Show(result, "Database Result", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
