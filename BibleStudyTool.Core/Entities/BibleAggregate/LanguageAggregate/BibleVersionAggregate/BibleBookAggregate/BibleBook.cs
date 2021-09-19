using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities.BibleAggregate
{
    public class BibleBook : BaseEntity, IAggregateRoot
    {
        public string BibleBookId { get; private set; }

        public string BibleVersionId { get; private set; }
        public BibleVersion BibleVersion { get; private set; }

        public string Name { get; private set; }

        public IList<BibleBookAbbreviation> BibleBookAbbreviations { get; set; }
        public IList<BibleVerse> BibleBookVerses { get; set; }

        public BibleBook() { }

        public BibleBook(string versionId, BibleVersion bibleVersion, string name)
        {
            BibleVersionId = versionId;
            BibleVersion = bibleVersion;
            Name = name;
        }
    }
}
