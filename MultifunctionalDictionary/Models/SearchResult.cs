using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultifunctionalDictionary.Models
{
    class SearchResult
    {
        private String word;
        private String testament;
        private String book;
        private int chapter;
        private int verseNum;
        private int referenceNum;
        private String hebrewWord;
        private String hebrewTranslation;
        private String pronunciation;
        private String definition;

        public SearchResult(String word, String testament, String book, int chapter, int verseNum, int referenceNum, String hebrewWord, String hebrewTranslation, String pronunciation, String definition)
        {
            this.word = word;
            this.testament = testament;
            this.book = book;
            this.chapter = chapter;
            this.verseNum = verseNum;
            this.referenceNum = referenceNum;
            this.hebrewWord = hebrewWord;
            this.hebrewTranslation = hebrewTranslation;
            this.pronunciation = pronunciation;
            this.definition = definition;
        }

        public String GetWord() { return this.word; }
        public String GetTestament() { return this.testament; }
        public String GetBook() { return this.book; }
        public int GetChapter() { return this.chapter; }
        public int GetVerseNum() { return this.verseNum; }
        public int GetReferenceNum() { return this.referenceNum; }
        public String GetHebrewWord() { return this.hebrewWord; }
        public String GetHebrewTranslation() { return this.hebrewTranslation; }
        public String GetPronunciation() { return this.pronunciation; }
        public String GetDefinition() { return this.definition; }
    }
}
