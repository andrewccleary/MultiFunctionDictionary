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
using MultifunctionalDictionary.Models;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace MultifunctionalDictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DatabaseHelper dh;

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

            goButton.IsEnabled = false;
            clearButton.IsEnabled = false;
            FontSlider.IsEnabled = false;

            dh = new DatabaseHelper("localhost", "5432", "postgres", "postgres", "MFD");
            dh.AcquireConnection();
            SelectionHelper sh = new SelectionHelper(dh.GetConnection());

            Dictionary<int, String> booksList = sh.GetBooksList();

            foreach(KeyValuePair<int, String> book in booksList)
            {
                bookSelector.Items.Insert(book.Key-1, book.Value);
            }

        }

        private void BookSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionHelper sh = new SelectionHelper(dh.GetConnection());
            List<int> chapters = sh.GetChapterList(bookSelector.SelectedIndex+1);

            chapterSelector.Items.Clear();
            goButton.IsEnabled = true;
            clearButton.IsEnabled = true;

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
            SelectionHelper sh = new SelectionHelper(dh.GetConnection());
            List<int> verses = sh.GetVerseList(bookSelector.SelectedIndex+1, chapterSelector.SelectedIndex+1);

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
            verseBlock.Text = " ";
            FontSlider.IsEnabled = true;

            int chapter = chapterSelector.SelectedIndex + 1;
            int verseNum = verseSelector.SelectedIndex + 1;

            VerseHelper vh = new VerseHelper(dh.GetConnection());
            List<Verse> verses = new List<Verse>();

            if (bookSelector.Text != "Book" && chapterSelector.Text == "Chapter")
            {
                verses = vh.GetVersesByBook(bookSelector.SelectedIndex + 1);
            }
            else if(bookSelector.Text != "Book" && chapterSelector.Text != "Chapter" && verseSelector.Text == "Verse")
            {
                verses = vh.GetVersesByBookChapter(bookSelector.SelectedIndex + 1, chapterSelector.SelectedIndex + 1);
            }
            else if (bookSelector.Text != "Book" && chapterSelector.Text != "Chapter" && verseSelector.Text != "Verse")
            {
                verses = vh.GetVerseByBookChapterVerse(bookSelector.SelectedIndex + 1, chapterSelector.SelectedIndex + 1, verseSelector.SelectedIndex + 1);
            }

            foreach (Verse verse in verses)
            {
                verseBlock.Text += verse.GetVerseNum().ToString();
                verseBlock.Text += " ";
                verseBlock.Text += verse.GetVerse();
                verseBlock.Text += " ";
            }
            
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            FontSlider.IsEnabled = false;
            verseSelector.Items.Clear();
            chapterSelector.Items.Clear();

            bookSelector.Text = "Book";
            chapterSelector.Text = "Chapter";
            verseSelector.Text = "Verse";

            verseBlock.Text = "";

            chapterSelector.IsEnabled = false;
            verseSelector.IsEnabled = false;
            goButton.IsEnabled = false;
            clearButton.IsEnabled = false;
        }

        private void FontSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            verseBlock.FontSize = FontSlider.Value;
        }
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            String searchTerm = homeSearchBox.Text;
            
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

       private bool validateEntry()
        {
            referenceNumberEntryError.Text = "";
            hebrewWordEntryError.Text = "";
            hebrewTranslationEntryError.Text = "";
            pronunciationEntryError.Text = "";

            bool flag = false;
            if(referenceNumberEntry.Text == "")
            {
                Debug.WriteLine("Reference number empty");
                referenceNumberEntryError.Text = "Reference number empty.";
                flag = true;
            }
            if (hebrewWordEntry.Text == "")
            {
                Debug.WriteLine("Hebrew Word empty");
                hebrewWordEntryError.Text = "Hebrew word empty.";
                flag = true;
            }
            if (hebrewTranslationEntry.Text == "")
            {
                Debug.WriteLine("Hebrew Translation empty");
                hebrewTranslationEntryError.Text = "Hebrew Translation empty.";
                flag = true;
            }
            if (pronunciationEntry.Text == "")
            {
                Debug.WriteLine("Pronunciation empty.");
                pronunciationEntryError.Text = "Pronunciation empty.";
                flag = true;
            }
            if (definitionEntry.Text == "")
            {
                Debug.WriteLine("Definition empty.");
                definitionEntryError.Text = "Definition empty.";
                flag = true;
            }

            return flag;
        }

        private void importButton_Click(object sender, RoutedEventArgs e)
        {
            bool pass = validateEntry();
            if (pass)
            {
                //Do import to Postgress stuff
            }
        }
    }
}
