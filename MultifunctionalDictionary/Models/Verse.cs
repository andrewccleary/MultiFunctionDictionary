using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace MultifunctionalDictionary.Models
{
    public class Verse
    {
        private int verseId;
        private String language;
        private int testamentNum;
        private String testament;
        private int bookNum;
        private String book;
        private int chapter;
        private int verseNum;
        public String verse;

        public Verse(int verseId, String language, int testamentNum, String testament, int bookNum,
            String book, int chapter, int verseNum, String verse)
        {
            this.verseId = verseId;
            this.language = language;
            this.testamentNum = testamentNum;
            this.testament = testament;
            this.bookNum = bookNum;
            this.book = book;
            this.chapter = chapter;
            this.verseNum = verseNum;
            this.verse = verse;
        }

        public int GetVerseId(){ return this.verseId; }
        public String GetLanguage() { return this.language; }
        public int GetTestamentNum() { return this.testamentNum; }
        public String GetTestament() { return this.testament; }
        public int GetBookNum() { return this.bookNum; }
        public String GetBook() { return this.book; }
        public int GetChapter() { return this.chapter; }
        public int GetVerseNum() { return this.verseNum; }
        public String GetVerse() { return this.verse; }
    }
}
