using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultifunctionalDictionary.Models
{
    public class WordMap
    {
        String word;
        int verseId;
        int referenceNum;

        public WordMap(String word, int verseId, int referenceNum)
        {
            this.word = word;
            this.verseId = verseId;
            this.referenceNum = referenceNum;
        }
    }
}
