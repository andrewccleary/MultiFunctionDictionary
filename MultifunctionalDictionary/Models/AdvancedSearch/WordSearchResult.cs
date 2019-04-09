using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultifunctionalDictionary.Models
{
    class WordSearchResult
    {
        public String word { get; set; }
        public int referencenumber { get; set; }
        public int booknumber { get; set; }
        public String book { get; set; }
        public int chapter { get; set; }
        public int versenumber { get; set; }
        public String verse { get; set; }

        public WordSearchResult(String word, int referenceNum, int bookNum, String book, int chapter, int verseNum, String verse)
        {
            this.word = word;
            this.referencenumber = referenceNum;
            this.booknumber = bookNum;
            this.book = book;
            this.chapter = chapter;
            this.versenumber = verseNum;
            this.verse = verse;
        }

                
    }
}

