using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultifunctionalDictionary.Models;
using Npgsql;
using System.Diagnostics;

namespace MultifunctionalDictionary.Helper
{
    class SearchHelper
    {
        private NpgsqlConnection connection;

        public SearchHelper(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        public List<SearchResult> searchWordByBookChapterVerse(String word, int bookNumber, int chapterNumber, int verseNumber)
        {
            List<SearchResult> results = new List<SearchResult>();

            using (NpgsqlCommand cmd = new NpgsqlCommand(String.Format("SELECT word, testament, book, chapter, versenum, referencenum, hebrewword, hebrewtranslation, pronunciation, definition FROM searchWordByBookChapterVerse('{0}',{1},{2},{3})", word, bookNumber, chapterNumber, verseNumber), connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SearchResult result = new SearchResult(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                        results.Add(result);
                    }

                    return results;
                }
            }
        }

        public List<SearchResult> searchWordByBookChapter(String word, int bookNumber, int chapterNumber)
        {
            List<SearchResult> results = new List<SearchResult>();

            using (NpgsqlCommand cmd = new NpgsqlCommand(String.Format("SELECT word, testament, book, chapter, versenum, referencenum, hebrewword, hebrewtranslation, pronunciation, definition FROM searchWordByBookChapter('{0}',{1},{2})", word, bookNumber, chapterNumber), connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SearchResult result = new SearchResult(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                        results.Add(result);
                    }

                    return results;
                }
            }
        }

        public List<SearchResult> searchWordByBook(String word, int bookNumber)
        {
            List<SearchResult> results = new List<SearchResult>();

            using (NpgsqlCommand cmd = new NpgsqlCommand(String.Format("SELECT word, testament, book, chapter, versenum, referencenum, hebrewword, hebrewtranslation, pronunciation, definition FROM searchWordByBook('{0}',{1})", word, bookNumber), connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        SearchResult result = new SearchResult(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                        results.Add(result);
                    }

                    return results;
                }
            }
        }

        public List<ReferenceNumberSearchResult> getReferenceNumbers(int referenceNumber)
        {
            List<ReferenceNumberSearchResult> results = new List<ReferenceNumberSearchResult>();

            using (NpgsqlCommand cmd = new NpgsqlCommand(String.Format("SELECT referencenum, word, booknum, book, chapter, versenum, verse FROM getReferenceNumbers('{0}')", referenceNumber), connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ReferenceNumberSearchResult result = new ReferenceNumberSearchResult(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetString(6));
                        results.Add(result);
                    }

                    return results;
                }
            }
        }

        public List<WordSearchResult> getWords(String searchedWord)
        {
            List<WordSearchResult> results = new List<WordSearchResult>();

            using (NpgsqlCommand cmd = new NpgsqlCommand(String.Format("SELECT word, referencenum, booknum, book, chapter, versenum, verse FROM getWords('{0}')", searchedWord), connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        WordSearchResult result = new WordSearchResult(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetString(6));
                        results.Add(result);
                    }

                    return results;
                }
            }
        }

        public List<int> getChildReferenceNumbers(int referenceNumber)
        {
            List<int> results = new List<int>();

            using (NpgsqlCommand cmd = new NpgsqlCommand(String.Format("SELECT referencenum FROM getChildReferenceNumbers('{0}')", referenceNumber), connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int result = reader.GetInt32(0);
                        results.Add(result);
                    }

                    return results;
                }
            }
        }

        public List<ContextSearchResult> searchByContext(int booknumber, int chapternumber, int versenumber)
        {
            List<ContextSearchResult> results = new List<ContextSearchResult>();

            using (NpgsqlCommand cmd = new NpgsqlCommand(String.Format("SELECT booknum, chapter, versenum, word, referencenum, hebrewword, hebrewtranslation, pronunciation, definition, verse FROM searchByContext('{0}', '{1}', '{2}')", booknumber, chapternumber, versenumber), connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ContextSearchResult result = new ContextSearchResult(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                        results.Add(result);
                    }

                    return results;
                }
            }
        }

        public List<ContextSearchResult> searchByContextWithWord(String searchTerm, int booknumber, int chapternumber, int versenumber)
        {
            List<ContextSearchResult> results = new List<ContextSearchResult>();

            using (NpgsqlCommand cmd = new NpgsqlCommand(String.Format("SELECT booknum, chapter, versenum, word, referencenum, hebrewword, hebrewtranslation, pronunciation, definition, verse FROM searchByContextWithWord('{0}', '{1}', '{2}', '{3}')", searchTerm, booknumber, chapternumber, versenumber), connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ContextSearchResult result = new ContextSearchResult(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5), reader.GetString(6), reader.GetString(7), reader.GetString(8), reader.GetString(9));
                        results.Add(result);
                    }

                    return results;
                }
            }
        }
    }
}
