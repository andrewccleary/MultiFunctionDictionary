using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultifunctionalDictionary.Models
{
    public class Translation
    {
        private int referenceNum;
        private String HebrewWord;
        private String HebrewTranslation;
        private String pronunciation;
        private String definition;

        public Translation(int referenceNum, String HebrewWord, String HebrewTranslation, String pronunciation, String definition)
        {
            this.referenceNum = referenceNum;
            this.HebrewWord = HebrewWord;
            this.HebrewTranslation = HebrewTranslation;
            this.pronunciation = pronunciation;
            this.definition = definition;
        }

        public int GetReferenceNum() { return this.referenceNum; }
        public String GetHebrewWord() { return this.HebrewWord; }
        public String GetHebrewTranslation() { return this.HebrewTranslation; }
        public String GetPronunciation() { return this.pronunciation; }
        public String GetDefinition() { return this.definition; }

    }
}
