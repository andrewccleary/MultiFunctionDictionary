using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultifunctionalDictionary.Models
{
    class ContextSearchResult
    {
        private int booknumber { get; set; }
        private int chapter { get; set; }
        private int versenumber { get; set; }
        public String word { get; set; }
        public int referencenumber { get; set; }
        public String hebrewword { get; set; }
        public String hebrewtranslation { get; set; }
        public String pronunciation { get; set; }
        public String definition { get; set; }
        private String verse { get; set; }

        public ContextSearchResult(int booknum, int chapter, int versenum, String word, int referencenum, String hebrewword, String hebrewtranslation, String pronunciation, String definition, String verse)
        {
            this.booknumber = booknum;
            this.chapter = chapter;
            this.versenumber = versenum;
            this.word = word;
            this.referencenumber = referencenum;
            this.hebrewword = hebrewword;
            this.hebrewtranslation = hebrewtranslation;
            this.pronunciation = pronunciation;
            this.definition = definition;
            this.verse = verse;
        }

        public String GetVerse()
        {
            return this.verse;
        }

    }
}

