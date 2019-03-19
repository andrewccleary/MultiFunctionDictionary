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
    /// Interaction logic for RootReferenceWindow.xaml
    /// </summary>
    public partial class RootReferenceWindow : Window
    {
        public RootReferenceWindow()
        {
            InitializeComponent();
            this.Title = "Import Root Reference";

            clearButton.IsEnabled = false;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            rootReferenceNumberTextBox.Clear();
            childReferenceNumberTextBox.Clear();
            clearButton.IsEnabled = false;
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            //Validate Fields & Push To Database
            this.Close();
        }

        private void RootReferenceNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            clearButton.IsEnabled = true;
        }

        private void ChildReferenceNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            clearButton.IsEnabled = true;
        }
    }
}
