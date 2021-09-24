using System;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class BibleBookAbbreviationLanguage : BaseEntity
    {
        public int BibleBookId { get; }
        public BibleBook BibleBook { get; }

        public string LanguageCode { get; }
        public Language Language { get; }

        public string Abbreviation { get; }

        public BibleBookAbbreviationLanguage()
        {
        }
    }
}
