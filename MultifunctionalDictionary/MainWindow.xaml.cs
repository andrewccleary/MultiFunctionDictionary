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
            chapterSelector.IsEnabled = false;
            chapterSelector.Text = "Chapter";

            verseSelector.IsEditable = true;
            verseSelector.IsReadOnly = true;
            verseSelector.IsEnabled = false;
            verseSelector.Text = "Verse";

            DatabaseHelper dh = new DatabaseHelper("localhost", "5432", "postgres", "postgres", "MFD");
            dh.AcquireConnection();

            Dictionary<int, String> booksList = dh.GetBooksList();

            foreach(KeyValuePair<int, String> book in booksList)
            {
                bookSelector.Items.Insert(book.Key-1, book.Value);
            }
        }

        private void BookSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DatabaseHelper dh = new DatabaseHelper("localhost", "5432", "postgres", "postgres", "MFD");
            dh.AcquireConnection();

            List<int> chapters = dh.GetChapterList(bookSelector.SelectedIndex+1);

            chapterSelector.Items.Clear();

            foreach(int chapter in chapters)
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
            DatabaseHelper dh = new DatabaseHelper("localhost", "5432", "postgres", "postgres", "MFD");
            dh.AcquireConnection();

            List<int> verses = dh.GetVerseList(bookSelector.SelectedIndex+1, chapterSelector.SelectedIndex+1);

            verseSelector.Items.Clear();

            foreach (int verse in verses)
            {
                verseSelector.Items.Insert(verse - 1, verse);
            }

            verseSelector.Text = "Verse";
            verseSelector.IsEnabled = true;
        }

        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            int chapter = chapterSelector.SelectedIndex + 1;
            int verseNum = verseSelector.SelectedIndex + 1;

            //TODO: Need logic to determine which verse selection method to use
        }
    }
}
