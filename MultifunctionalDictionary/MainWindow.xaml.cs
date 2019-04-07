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
        List<ReferenceNumberSearchResult> referenceNumberResults = new List<ReferenceNumberSearchResult>();
        List<int> children = new List<int>();
        List<WordSearchResult> WordResults = new List<WordSearchResult>();
        List<ContextSearchResult> ContextResults = new List<ContextSearchResult>();

        //Global variable to keep track of which advanced search radio button is selected
        //0 = none
        //1 = Search for Reference Number
        //2 = Search for Word
        //3 = Search for Word w/ Context

        int advancedSearchType = 0;

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

            bookSelector2.IsEditable = true;
            bookSelector2.IsReadOnly = true;
            bookSelector2.Text = "Book";

            chapterSelector2.IsEditable = true;
            chapterSelector2.IsReadOnly = true;
            chapterSelector2.IsEnabled = false;
            chapterSelector2.Text = "Chapter";

            verseSelector2.IsEditable = true;
            verseSelector2.IsReadOnly = true;
            verseSelector2.IsEnabled = false;
            verseSelector2.Text = "Verse";

            bookSelector2.Visibility = Visibility.Hidden;
            chapterSelector2.Visibility = Visibility.Hidden;
            verseSelector2.Visibility = Visibility.Hidden;

            goButton.IsEnabled = false;
            clearButton.IsEnabled = false;
            FontSlider.IsEnabled = false;

            searchContext.IsEditable = true;
            searchContext.IsReadOnly = true;
            searchContext.IsEnabled = false;
            searchContext.Text = "Context";

            childReferenceComboBox.IsEditable = true;
            childReferenceComboBox.IsReadOnly = true;
            childReferenceComboBox.Text = "Related Reference Numbers";
            childReferenceComboBox.Visibility = Visibility.Hidden;

            dh = new DatabaseHelper("localhost", "5432", "postgres", "postgres", "MFD");
            dh.AcquireConnection();
            SelectionHelper sh = new SelectionHelper(dh.GetConnection());

            Dictionary<int, String> booksList = sh.GetBooksList();

            foreach(KeyValuePair<int, String> book in booksList)
            {
                bookSelector.Items.Insert(book.Key-1, book.Value);
                bookSelector2.Items.Insert(book.Key - 1, book.Value);
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

        private void bookSelector2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionHelper sh = new SelectionHelper(dh.GetConnection());
            List<int> chapters = sh.GetChapterList(bookSelector2.SelectedIndex + 1);

            chapterSelector2.Items.Clear();

            foreach (int chapter in chapters)
            {
                chapterSelector2.Items.Insert(chapter - 1, chapter);
            }

            chapterSelector2.Text = "Chapter";
            chapterSelector2.IsEnabled = true;

            verseSelector2.Text = "Verse";
            verseSelector2.IsEnabled = false;
        }

        private void chapterSelector2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectionHelper sh = new SelectionHelper(dh.GetConnection());
            List<int> verses = sh.GetVerseList(bookSelector2.SelectedIndex + 1, chapterSelector2.SelectedIndex + 1);

            verseSelector2.Items.Clear();

            foreach (int verse in verses)
            {
                verseSelector2.Items.Insert(verse - 1, verse);
            }

            verseSelector2.Text = "Verse";
            verseSelector2.IsEnabled = true;
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
            searchWordTextBox.Text = "";
            searchReferenceNumTextBox.Text = "";
            searchHebrewWordTextBox.Text = "";
            searchHebrewTranslationTextBox.Text = "";
            searchPronunciationTextBox.Text = "";
            searchDefinitionTextBox.Text = "";
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
            searchContext.Items.Clear();

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
            
            if(index != -1)
            {
                searchWordTextBox.Text = results.ElementAt<SearchResult>(index).GetWord();
                searchReferenceNumTextBox.Text = results.ElementAt<SearchResult>(index).GetReferenceNum().ToString();
                searchHebrewWordTextBox.Text = results.ElementAt<SearchResult>(index).GetHebrewWord();
                searchHebrewTranslationTextBox.Text = results.ElementAt<SearchResult>(index).GetHebrewTranslation();
                searchPronunciationTextBox.Text = results.ElementAt<SearchResult>(index).GetPronunciation();
                searchDefinitionTextBox.Text = results.ElementAt<SearchResult>(index).GetDefinition();
            }
            
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

        private void AdvancedSearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchHelper sh = new SearchHelper(dh.GetConnection());
            String searchTerm = advancedSearch.Text;
            referenceNumberResults.Clear();
            WordResults.Clear();
            ContextResults.Clear();
            dataGrid.ItemsSource = null;
            dataGrid.Items.Refresh();

            if (searchTerm == String.Empty && contextRadioButton.IsChecked == false)
            {
                MessageBox.Show("Please enter a valid search term.", "Invalid Search", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            switch (advancedSearchType)
            {
                case 0:
                    MessageBox.Show("Please select an advanced search type.", "Invalid Search", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case 1:
                    int searchTermInt = 0;
                    try
                    {
                        searchTermInt = int.Parse(searchTerm);
                    }
                    catch
                    {
                        MessageBox.Show("Please enter a number", "Invalid Search", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    }

                    if(searchTermInt <= 0)
                    {
                        MessageBox.Show("Please enter positive number", "Invalid Search", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    }

                    referenceNumberResults = sh.getReferenceNumbers(searchTermInt);
                    
                    if (referenceNumberResults.Count != 0)
                    {
                        //Debug.WriteLine(referenceNumberResults[0].verse);
                        dataGrid.ItemsSource = referenceNumberResults;
                        dataGrid.Items.Refresh();
                        childReferenceComboBox.Visibility = Visibility.Visible;
                        children.Clear();
                        children = sh.getChildReferenceNumbers(searchTermInt);

                        childReferenceComboBox.Items.Clear();
                        childReferenceComboBox.Text = "Related Reference Numbers";
                        for (int i = 0; i < children.Count; i++)
                        {
                            
                            childReferenceComboBox.Items.Insert(i, children[i]);
                        }

                    }
                    else if (referenceNumberResults.Count == 0)
                    {
                        MessageBox.Show("No result found for this Reference Number.", "No Results", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                    break;
                case 2:
                    WordResults = sh.getWords(searchTerm);

                    if (WordResults.Count != 0)
                    {
                        //Debug.WriteLine(referenceNumberResults[0].verse);
                        dataGrid.ItemsSource = WordResults;
                        dataGrid.Items.Refresh();
                    }
                    else if (WordResults.Count == 0)
                    {
                        MessageBox.Show("No result found for this Word.", "No Results", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    break;
                case 3:

                    if (bookSelector2.Text != "Book" && chapterSelector2.Text != "Chapter" && verseSelector2.Text != "Verse")
                    {
                        ContextResults = sh.searchByContext(bookSelector2.SelectedIndex + 1, chapterSelector2.SelectedIndex + 1, verseSelector2.SelectedIndex + 1);
                    }
                    else
                    {
                        MessageBox.Show("Please select a Book, Chapter, and Verse in order to search.", "Selection Required", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    
                    if (ContextResults.Count != 0)
                    {
                        //Debug.WriteLine(referenceNumberResults[0].verse);
                        verseTextBlock.Text = ContextResults[0].GetVerse();
                        dataGrid.ItemsSource = ContextResults;
                        dataGrid.Items.Refresh();
                    }
                    else if (ContextResults.Count == 0)
                    {
                        MessageBox.Show("No result found", "No Results", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    break;
            }
            
        }

        private void referenceNumberRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            advancedSearchType = 1;
            bookSelector2.Visibility = Visibility.Hidden;
            chapterSelector2.Visibility = Visibility.Hidden;
            verseSelector2.Visibility = Visibility.Hidden;
            verseTextBlock.Text = "";
        }

        private void wordRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            advancedSearchType = 2;
            childReferenceComboBox.Visibility = Visibility.Hidden;
            bookSelector2.Visibility = Visibility.Hidden;
            chapterSelector2.Visibility = Visibility.Hidden;
            verseSelector2.Visibility = Visibility.Hidden;
            verseTextBlock.Text = "";
        }

        private void contextRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            advancedSearchType = 3;
            childReferenceComboBox.Visibility = Visibility.Hidden;
            bookSelector2.Visibility = Visibility.Visible;
            chapterSelector2.Visibility = Visibility.Visible;
            verseSelector2.Visibility = Visibility.Visible;
        }

        private void childReferenceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = childReferenceComboBox.SelectedIndex;
            if (index != -1)
            {
                SearchHelper sh = new SearchHelper(dh.GetConnection());
                
                dataGrid.ItemsSource = null;
                dataGrid.Items.Refresh();

                Debug.WriteLine(index);
                int child = 0;
                child = children[index];

                Debug.WriteLine(child);
                referenceNumberResults = sh.getReferenceNumbers(child);

                if (referenceNumberResults.Count != 0)
                {
                    //Debug.WriteLine(referenceNumberResults[0].verse);
                    dataGrid.ItemsSource = referenceNumberResults;
                    dataGrid.Items.Refresh();
                    childReferenceComboBox.Visibility = Visibility.Visible;
                    children.Clear();
                    children = sh.getChildReferenceNumbers(child);
                    childReferenceComboBox.Items.Clear();
                    childReferenceComboBox.Text = "Related Reference Numbers";
                    for (int i = 0; i < children.Count; i++)
                    {
                        searchContext.Items.Insert(i, children[i]);
                    }

                    
                }
                else if (referenceNumberResults.Count == 0)
                {
                    MessageBox.Show("No result found for this Reference Number.", "No Results", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        
    }
}
