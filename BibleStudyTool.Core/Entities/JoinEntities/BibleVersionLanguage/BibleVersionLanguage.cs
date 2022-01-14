using System;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class BibleVersionLanguage : BaseEntity
    {
        public int BibleVersionId { get; }
        public BibleVersion BibleVersion { get; }

        public string LanguageCode { get; }
        public Language Language { get; }

        public string Name { get; }
        public string Abbreviation { get; }

        public BibleVersionLanguage()
        {
        }

        public BibleVersionLanguage
            (int bibleVersionId, string languageCode, string name, string abbrv)
        {
            BibleVersionId = bibleVersionId;
            LanguageCode = languageCode;
            Name = name;
            Abbreviation = abbrv;
        }
    }
}
