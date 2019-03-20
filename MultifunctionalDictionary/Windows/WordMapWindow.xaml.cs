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
    /// Interaction logic for WordMapWindow.xaml
    /// </summary>
    public partial class WordMapWindow : Window
    {
        DatabaseHelper dh;

        public WordMapWindow()
        {
            InitializeComponent();

            bookSelector.IsEditable = true;
            bookSelector.IsReadOnly = true;
            bookSelector.Text = "Book";

            chapterSelector.IsEditable = true;
            chapterSelector.IsReadOnly = true;
            chapterSelector.IsEnabled = false;
            chapterSelector.Text = "Chapter";

            verseSelector.IsEditable = true;
            verseSelector.IsReadOnly = true;
            verseSelector.IsEnabled = false;
            verseSelector.Text = "Verse";

            clearButton.IsEnabled = false;

            dh = new DatabaseHelper("localhost", "5432", "postgres", "postgres", "MFD");
            dh.AcquireConnection();
            SelectionHelper sh = new SelectionHelper(dh.GetConnection());

            Dictionary<int, String> booksList = sh.GetBooksList();

            foreach (KeyValuePair<int, String> book in booksList)
            {
                bookSelector.Items.Insert(book.Key - 1, book.Value);
            }
        }

        private void BookSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionHelper sh = new SelectionHelper(dh.GetConnection());
            List<int> chapters = sh.GetChapterList(bookSelector.SelectedIndex + 1);

            chapterSelector.Items.Clear();
            clearButton.IsEnabled = true;

            foreach (int chapter in chapters)
            {
                chapterSelector.Items.Insert(chapter - 1, chapter);
            }

            chapterSelector.Text = "Chapter";
            chapterSelector.IsEnabled = true;

            verseSelector.Text = "Verse";
            verseSelector.IsEnabled = false;
        }

        private void ChapterSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionHelper sh = new SelectionHelper(dh.GetConnection());
            List<int> verses = sh.GetVerseList(bookSelector.SelectedIndex + 1, chapterSelector.SelectedIndex + 1);

            verseSelector.Items.Clear();
            clearButton.IsEnabled = true;

            foreach (int verse in verses)
            {
                verseSelector.Items.Insert(verse - 1, verse);
            }

            verseSelector.Text = "Verse";
            verseSelector.IsEnabled = true;
        }

        private void VerseSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            clearButton.IsEnabled = true;
        }

        private void WordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            clearButton.IsEnabled = true;
        }

        private void ReferenceNumberTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            clearButton.IsEnabled = true;
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            verseSelector.Items.Clear();
            chapterSelector.Items.Clear();
            wordTextBox.Clear();
            referenceNumberTextBox.Clear();

            bookSelector.Text = "Book";
            chapterSelector.Text = "Chapter";
            verseSelector.Text = "Verse";

            chapterSelector.IsEnabled = false;
            verseSelector.IsEnabled = false;
            clearButton.IsEnabled = false;
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            WordMapHelper wmh = new WordMapHelper(dh.GetConnection());
            String result;

            result = wmh.importWordMap(wordTextBox.Text, bookSelector.Text, Convert.ToInt32(chapterSelector.Text), 
                                Convert.ToInt32(verseSelector.Text), Convert.ToInt32(referenceNumberTextBox.Text));

            MessageBox.Show(result, "Database Result", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
