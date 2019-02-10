using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace MultifunctionalDictionary.Helper
{
    class DatabaseHelper
    {
        private String server;
        private String port;
        private String user;
        private String password;
        private String database;

        private NpgsqlConnection connection;

        public DatabaseHelper(String server, String port, String user, String password, String database)
        {
            this.server = server;
            this.port = port;
            this.user = user;
            this.password = password;
            this.database = database;
        }

        /// <summary>
        /// Acquire Connection to PostgreSQL Database
        /// </summary>
        public void AcquireConnection()
        {
            string connectionString = String.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};", server, port, user, password, database);
            connection = new NpgsqlConnection(connectionString);
            connection.Open();
        }

        #region Populate Home Selection Options
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
        #endregion
    }
}
