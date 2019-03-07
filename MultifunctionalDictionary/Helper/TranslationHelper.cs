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
    class TranslationHelper
    {
        private NpgsqlConnection connection;

        public TranslationHelper(NpgsqlConnection connection)
        {
            this.connection = connection;
        }

        //Return 0 if pass
        //Return 1 if entry is already in database
        public int insertTranslation(Translation translation)
        {

            using (NpgsqlCommand cmd = new NpgsqlCommand(String.Format("INSERT INTO translation(referencenum, hebrewword, hebrewtranslation, pronunciation, definition) VALUES ({0}, '{1}', '{2}', '{3}', '{4}')",
                  translation.GetReferenceNum(), translation.GetHebrewWord(), translation.GetHebrewTranslation(), translation.GetPronunciation(), translation.GetDefinition()), connection))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                } catch(NpgsqlException e)
                {
                    Debug.WriteLine(e.ErrorCode);
                    Debug.WriteLine(e.Message);

                    if (e.ErrorCode == -2147467259)
                    {
                        Debug.WriteLine("Trying to handle duplicate entry");
                        return 1;
                    }

                }
                
            }
            return 0;
        }

        public void updateTranslation(Translation translation)
        {
            

            using (NpgsqlCommand cmd = new NpgsqlCommand(String.Format("UPDATE translation SET hebrewword = '{1}', hebrewtranslation = '{2}', pronunciation = '{3}', definition = '{4}' WHERE referencenum = '{0}'",
                  translation.GetReferenceNum(), translation.GetHebrewWord(), translation.GetHebrewTranslation(), translation.GetPronunciation(), translation.GetDefinition()), connection))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (NpgsqlException e)
                {
                    Debug.WriteLine(e.ErrorCode);
                    Debug.WriteLine(e.Message);
                    
                }

            }
        }

    }
}
