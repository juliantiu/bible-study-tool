using System;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Public.DTOs
{
    public class BibleBookDto
    {
        public int BibleBookId { get; set; }
        public int Order { get; set; }
        public string DefaultName { get; set; }
        public string Name { get; set; }
        public string DefaultAbbreviation { get; set; }
        public string Abbreviation { get; set; }
        public string Style { get; set; }
        public string LanguageCode { get; set; }

        public BibleBookDto
            ((BibleBook bb, BibleBookLanguage bbl,
            BibleBookAbbreviationLanguage bbal)
            book)
        {
            BibleBookId = book.bb.BibleBookId;
            DefaultName = book.bb.DefaultName;
            DefaultAbbreviation = book.bb.DefaultAbbreviation;
            LanguageCode = book.bbl.LanguageCode;
            Abbreviation = book.bbal.Abbreviation;
            Style = book.bbl.Name;
            Name = book.bbl.Name;
            Order = book.bb.Order;
        }
    }
}
