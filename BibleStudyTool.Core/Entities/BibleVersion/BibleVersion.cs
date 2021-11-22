using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.Entities
{
    public class BibleVersion : BaseEntity
    {
        public int BibleVersionId { get; }

        public string DefaultName { get; private set; }
        public string DefaultAbbreviation { get; private set; }

        public IList<BibleVersionLanguage> BibleVersionLanguages { get; }
        public IList<BibleVerseBibleVersionLanguage> BibleVerseBibleVersionLanguages { get; }

        public BibleVersion()
        {
        }

        public BibleVersion
            (int bibleVersonId, string defaultName, string defaultAbbreviation)
        {
            BibleVersionId = bibleVersonId;
            DefaultName = defaultName;
            DefaultAbbreviation = defaultAbbreviation;
        }

        public BibleVersion(string defaultName, string defaultAbbreviation)
        {
            DefaultName = defaultName;
            DefaultAbbreviation = defaultAbbreviation;
        }
    }
}
