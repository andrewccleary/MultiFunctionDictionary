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
using MultifunctionalDictionary.Helper;

namespace MultifunctionalDictionary.Windows
{
    /// <summary>
    /// Interaction logic for RootReferenceWindow.xaml
    /// </summary>
    public partial class RootReferenceWindow : Window
    {
        DatabaseHelper dh;

        public RootReferenceWindow()
        {
            InitializeComponent();

            dh = new DatabaseHelper("localhost", "5432", "postgres", "postgres", "MFD");
            dh.AcquireConnection();

            clearButton.IsEnabled = false;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            rootReferenceNumberTextBox.Clear();
            childReferenceNumberTextBox.Clear();
            clearButton.IsEnabled = false;
        }

        private void RootReferenceNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            clearButton.IsEnabled = true;
        }

        private void ChildReferenceNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            clearButton.IsEnabled = true;
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            RootReferenceHelper rrh = new RootReferenceHelper(dh.GetConnection());
            String result;

            result = rrh.importRootReference(Convert.ToInt32(rootReferenceNumberTextBox.Text), Convert.ToInt32(childReferenceNumberTextBox.Text));

            MessageBox.Show(result, "Database Result", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
