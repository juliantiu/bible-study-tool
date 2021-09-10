using System;
using System.Collections.Generic;

namespace BibleStudyTool.Core.Entities.BibleAggregate
{
    public class BibleBook: BaseEntity, IAggregateRoot
    {
        public int BibleVersionId { get; private set; }
        public BibleVersion BibleVersion { get; private set; }

        public string Name { get; private set; }

        public IList<BibleBookAbbreviation> BibleBookAbbreviations { get; set; }

        public BibleBook(int versionId, BibleVersion bibleVersion, string name)
        {
            BibleVersionId = versionId;
            BibleVersion = bibleVersion;
            Name = name;
        }
    }
}
