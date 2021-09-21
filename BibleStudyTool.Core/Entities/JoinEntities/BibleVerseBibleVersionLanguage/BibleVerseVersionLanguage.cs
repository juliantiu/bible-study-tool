using System;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class BibleVerseBibleVersionLanguage : BaseEntity
    {
        public int BibleVerseId { get; }
        public BibleVerse BibleVerse { get; }

        public int BibleVersionId { get; }
        public BibleVersion BibleVersion { get; }

        public string LanguageCode { get; }
        public Language Language { get; }

        public string Text { get; }

        public BibleVerseBibleVersionLanguage()
        {
        }
    }
}
