using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultifunctionalDictionary.Models;
using Npgsql;

namespace MultifunctionalDictionary.Helper
{
    public class SelectionHelper
    {
        private NpgsqlConnection connection;

        public SelectionHelper(NpgsqlConnection connection)
        {
            this.connection = connection;
        }
        /// <summary>
        /// Get the entire list of books
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, String> GetBooksList()
        {
            Dictionary<int, String> books = new Dictionary<int, string>(); ;

            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT booknum, book FROM getBooks()", connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        books.Add(reader.GetInt32(0), reader.GetString(1));
                    }

                    return books;
                }
            }
        }

        /// <summary>
        /// Get the list of chapters for a particular book
        /// </summary>
        /// <param name="bookNumber"></param>
        /// <returns></returns>
        public List<int> GetChapterList(int bookNumber)
        {
            List<int> chapters = new List<int>();

            using (NpgsqlCommand cmd = new NpgsqlCommand(String.Format("SELECT chapter FROM getChapters({0})", bookNumber.ToString()), connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        chapters.Add(reader.GetInt32(0));
                    }
                    return chapters;
                }
            }
        }

        /// <summary>
        /// Get the list of verse numbers for a particular book and chapter
        /// </summary>
        /// <param name="bookNumber"></param>
        /// <param name="chapter"></param>
        /// <returns></returns>
        public List<int> GetVerseList(int bookNumber, int chapter)
        {
            List<int> verses = new List<int>();

            using (NpgsqlCommand cmd = new NpgsqlCommand(String.Format("SELECT versenum FROM getVerses({0},{1})", bookNumber.ToString(), chapter.ToString()), connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        verses.Add(reader.GetInt32(0));
                    }
                    return verses;
                }
            }
        }
    }
}
