using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultifunctionalDictionary.Models;
using Npgsql;

namespace MultifunctionalDictionary.Helper
{
    class TranslationHelper
    {
        private NpgsqlConnection connection;

        public TranslationHelper(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        
        public void insertTranslation(Translation translation)
        {

            //using (NpgsqlCommand cmd = new NpgsqlCommand(String.Format("SELECT verseid, language, testamentnum, testament, booknum, book, chapter, versenum, verse FROM getVersesByBook({0})", bookNumber.ToString()), connection))
            using (NpgsqlCommand cmd = new NpgsqlCommand(String.Format("INSERT INTO translation(referencenum, hebrewword, hebrewtranslation, pronunciation, definition) VALUES ({0}, '{1}', '{2}', '{3}', '{4}')",
                  translation.GetReferenceNum(), translation.GetHebrewWord(), translation.GetHebrewTranslation(), translation.GetPronunciation(), translation.GetDefinition()), connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

    }
}
