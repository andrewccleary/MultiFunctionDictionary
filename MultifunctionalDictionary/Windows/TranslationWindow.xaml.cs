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

namespace MultifunctionalDictionary.Windows
{
    /// <summary>
    /// Interaction logic for TranslationWindow.xaml
    /// </summary>
    public partial class TranslationWindow : Window
    {
        public TranslationWindow()
        {
            InitializeComponent();

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
            //Validate Fields & Push To Database
            this.Close();
        }
    }
}
