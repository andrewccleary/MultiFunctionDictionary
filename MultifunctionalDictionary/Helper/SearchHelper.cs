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
    }
}
