using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultifunctionalDictionary.Models
{
    public class WordMap
    {
        private String word;
        private int verseId;
        private int referenceNum;

        public WordMap(String word, int verseId, int referenceNum)
        {
            this.word = word;
            this.verseId = verseId;
            this.referenceNum = referenceNum;
        }

        public String GetWord(){ return this.word; }
        public int GetVerseId(){ return this.verseId; }
        public int GetReferenceNum() { return this.referenceNum; }
    }
}
