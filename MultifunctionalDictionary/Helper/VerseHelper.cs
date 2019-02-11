using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultifunctionalDictionary.Models;
using Npgsql;

namespace MultifunctionalDictionary.Helper
{
    public class VerseHelper
    {

        private NpgsqlConnection connection;

        public VerseHelper(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Get list of verses by book number
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="bookNumber"></param>
        /// <returns></returns>
        public List<Verse> GetVersesByBook(int bookNumber)
        {
            List<Verse> verses = new List<Verse>();

            using (NpgsqlCommand cmd = new NpgsqlCommand(String.Format("SELECT verseid, language, testamentnum, testament, booknum, book, chapter, versenum, verse FROM getVersesByBook({0})", bookNumber.ToString()), connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Verse verse = new Verse(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetString(8));
                        verses.Add(verse);
                    }
                    return verses;
                }
            }
        }

        /// <summary>
        /// Get list of verses by book number and chapter
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="bookNumber"></param>
        /// <param name="chapter"></param>
        /// <returns></returns>
        public List<Verse> GetVersesByBookChapter(int bookNumber, int chapter)
        {
            List<Verse> verses = new List<Verse>();

            using (NpgsqlCommand cmd = new NpgsqlCommand(String.Format("SELECT verseid, language, testamentnum, testament, booknum, book, chapter, versenum, verse FROM getVersesByBookChapter({0},{1})", bookNumber.ToString(), chapter.ToString()), connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Verse verse = new Verse(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetString(8));
                        verses.Add(verse);
                    }
                    return verses;
                }
            }
        }

        /// <summary>
        /// Get verse by book number, chapter, and verse number
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="bookNumber"></param>
        /// <param name="chapter"></param>
        /// <param name="verseNumber"></param>
        /// <returns></returns>
        public List<Verse> GetVerseByBookChapterVerse(int bookNumber, int chapter, int verseNumber)
        {
            List<Verse> verses = new List<Verse>();

            using (NpgsqlCommand cmd = new NpgsqlCommand(String.Format("SELECT verseid, language, testamentnum, testament, booknum, book, chapter, versenum, verse FROM getVerseByBookChapterVerse({0},{1},{2})", bookNumber.ToString(), chapter.ToString(), verseNumber.ToString()), connection))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Verse verse = new Verse(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetString(8));
                        verses.Add(verse);
                    }
                    return verses;
                }
            }
        }
    }
}
