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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MultifunctionalDictionary.Helper;

namespace MultifunctionalDictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            bookSelector.IsEditable = true;
            bookSelector.IsReadOnly = true;
            bookSelector.Text = "Book";

            chapterSelector.IsEditable = true;
            chapterSelector.IsReadOnly = true;
            chapterSelector.Text = "Chapter";

            verseSelector.IsEditable = true;
            verseSelector.IsReadOnly = true;
            verseSelector.Text = "Verse";

            DatabaseHelper dh = new DatabaseHelper("localhost", "5432", "postgres", "postgres", "MFD");
            //dh.AcquireConnection();
        }

        private void BookSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
