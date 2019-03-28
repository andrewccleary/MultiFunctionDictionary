using MultifunctionalDictionary.Helper;
using MultifunctionalDictionary.Models;
using MultifunctionalDictionary.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MultifunctionalDictionary
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
        DatabaseHelper dh;
        List<SearchResult> results = new List<SearchResult>();

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

            searchContext.IsEditable = true;
            searchContext.IsReadOnly = true;
            searchContext.IsEnabled = false;
            searchContext.Text = "Context";

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
            homeSearchBox.Clear();

            bookSelector.Text = "Book";
            chapterSelector.Text = "Chapter";
            verseSelector.Text = "Verse";

            verseBlock.Text = "";

            chapterSelector.IsEnabled = false;
            verseSelector.IsEnabled = false;
            goButton.IsEnabled = false;
            clearButton.IsEnabled = false;

            searchContext.IsEnabled = false;
        }

        private void FontSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            verseBlock.FontSize = FontSlider.Value;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchHelper sh = new SearchHelper(dh.GetConnection());
            String searchTerm = homeSearchBox.Text;
            results.Clear();

            if (searchTerm == String.Empty)
            {
                MessageBox.Show("Please enter a valid search term.", "Invalid Search", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (bookSelector.Text != "Book" && chapterSelector.Text == "Chapter" && verseSelector.Text == "Verse")
                {
                    results = sh.searchWordByBook(searchTerm, bookSelector.SelectedIndex + 1);
                }
                else if (bookSelector.Text != "Book" && chapterSelector.Text != "Chapter" && verseSelector.Text == "Verse")
                {
                    results = sh.searchWordByBookChapter(searchTerm, bookSelector.SelectedIndex + 1, chapterSelector.SelectedIndex + 1);
                }
                else if (bookSelector.Text != "Book" && chapterSelector.Text != "Chapter" && verseSelector.Text != "Verse")
                {
                    results = sh.searchWordByBookChapterVerse(searchTerm, bookSelector.SelectedIndex + 1, chapterSelector.SelectedIndex + 1, verseSelector.SelectedIndex + 1);
                }
                else
                {
                    MessageBox.Show("Please select a Book, Chapter, or Verse in order to search.", "Selection Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

                if(results.Count != 0)
                {
                    searchWordTextBox.Text = results.ElementAt<SearchResult>(0).GetWord();
                    searchReferenceNumTextBox.Text = results.ElementAt<SearchResult>(0).GetReferenceNum().ToString();
                    searchHebrewWordTextBox.Text = results.ElementAt<SearchResult>(0).GetHebrewWord();
                    searchHebrewTranslationTextBox.Text = results.ElementAt<SearchResult>(0).GetHebrewTranslation();
                    searchPronunciationTextBox.Text = results.ElementAt<SearchResult>(0).GetPronunciation();
                    searchDefinitionTextBox.Text = results.ElementAt<SearchResult>(0).GetDefinition();

                    searchContext.Text = "Context";
                    searchContext.Items.Clear();
                    searchContext.IsEnabled = true;

                    for (int i = 0; i < results.Count; i++)
                    {
                        String context = results.ElementAt<SearchResult>(i).GetBook() + " " +
                                           results.ElementAt<SearchResult>(i).GetChapter() + ":" +
                                             results.ElementAt<SearchResult>(i).GetVerseNum();
                        searchContext.Items.Insert(i, context);
                    }
                    
                } else if (results.Count == 0)
                {
                    MessageBox.Show("No result found for this word.", "No Results", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void searchContext_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine("Context Changed!");
            int index = searchContext.SelectedIndex;
            searchWordTextBox.Text = results.ElementAt<SearchResult>(index).GetWord();
            searchReferenceNumTextBox.Text = results.ElementAt<SearchResult>(index).GetReferenceNum().ToString();
            searchHebrewWordTextBox.Text = results.ElementAt<SearchResult>(index).GetHebrewWord();
            searchHebrewTranslationTextBox.Text = results.ElementAt<SearchResult>(index).GetHebrewTranslation();
            searchPronunciationTextBox.Text = results.ElementAt<SearchResult>(index).GetPronunciation();
            searchDefinitionTextBox.Text = results.ElementAt<SearchResult>(index).GetDefinition();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void AddRootReferenceButton_Click(object sender, RoutedEventArgs e)
        {
            RootReferenceWindow subwindow = new RootReferenceWindow();
            subwindow.Show();
        }

        private void AddTranslationButton_Click(object sender, RoutedEventArgs e)
        {
            TranslationWindow subwindow = new TranslationWindow();
            subwindow.Show();
        }

        private void AddWordMapButton_Click_1(object sender, RoutedEventArgs e)
        {
            WordMapWindow subwindow = new WordMapWindow();
            subwindow.Show();
        }
    }
}
