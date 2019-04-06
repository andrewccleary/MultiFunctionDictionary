using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultifunctionalDictionary.Models
{
    class ReferenceNumberSearchResult
    {
        public int referenceNum;
        public String word;
        public int bookNum;
        public String book;
        public int chapter;
        public int verseNum;
        public String verse;

        public ReferenceNumberSearchResult(int referenceNum, String word, int bookNum, String book, int chapter, int verseNum, String verse)
        {
            this.referenceNum = referenceNum;
            this.word = word;
            this.bookNum = bookNum;
            this.book = book;
            this.chapter = chapter;
            this.verseNum = verseNum;
            this.verse = verse;
        }

        public String GetWord() { return this.word; }
        public String GetBook() { return this.book; }
        public String GetVerse() { return this.verse; }
        public int GetChapter() { return this.chapter; }
        public int GetVerseNum() { return this.verseNum; }
        public int GetReferenceNum() { return this.referenceNum; }
        public int GetBookNum() { return this.bookNum; }
    }
}

