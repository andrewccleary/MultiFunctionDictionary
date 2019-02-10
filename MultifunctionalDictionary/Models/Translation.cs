using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultifunctionalDictionary.Models
{
    public class Translation
    {
        int referenceNum;
        String HebrewWord;
        String HebrewTranslation;
        String pronunciation;
        String definition;

        public Translation(int referenceNum, String HebrewWord, String HebrewTranslation, String pronunciation, String definition)
        {
            this.referenceNum = referenceNum;
            this.HebrewWord = HebrewWord;
            this.HebrewTranslation = HebrewTranslation;
            this.pronunciation = pronunciation;
            this.definition = definition;
        }
    }
}
