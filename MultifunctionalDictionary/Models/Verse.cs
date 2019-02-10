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
        int verseId;
        String language;
        int testamentNum;
        String testament;
        int bookNum;
        String book;
        int chapter;
        int verseNum;
        String verse;

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
        
    }
}
