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
    class WordMapHelper
    {
        private NpgsqlConnection connection;

        public WordMapHelper(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        public String importWordMap(String word, String book, int chapter, int verseNum, int referenceNum)
        {
            String result;

            using (NpgsqlCommand cmd = new NpgsqlCommand(String.Format("SELECT importWordMap FROM importWordMap('{0}','{1}',{2},{3},{4})", word, book, chapter, verseNum, referenceNum), connection))
            {
                result = (String)cmd.ExecuteScalar();
            }

            return result;
        }
    }
}