using System;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class BibleBookAbbreviationLanguage : BaseEntity
    {
        public int BibleBookId { get; }
        public BibleBook BibleBook { get; }

        public string LanguageCode { get; }
        public Language Language { get; }

        public string BibleBookAbbreviation { get; }

        public BibleBookAbbreviationLanguage()
        {
        }
    }
}
