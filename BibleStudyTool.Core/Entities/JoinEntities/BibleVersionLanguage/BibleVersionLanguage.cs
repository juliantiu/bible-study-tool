using System;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class BibleVersionLanguage : BaseEntity
    {
        public int BibleVersionId { get; }
        public BibleVersion BibleVersion { get; }

        public string LanguageCode { get; }
        public Language Language { get; }

        public string BibleVersionName { get; }
        public string BibleVersionAbbreviation { get; }

        public BibleVersionLanguage()
        {
        }
    }
}
