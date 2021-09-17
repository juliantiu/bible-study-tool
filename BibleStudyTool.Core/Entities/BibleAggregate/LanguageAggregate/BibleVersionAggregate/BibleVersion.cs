using System;
using System.Collections.Generic;

namespace BibleStudyTool.Core.Entities.BibleAggregate
{
    public class BibleVersion : BaseEntity, IAggregateRoot
    {
        public string BibleVersionId { get; private set; }

        public string Name { get; private set; }
        public string Abbreviation { get; private set; }

        public string LanguageId { get; private set; }
        public Language Language { get; private set; }

        public IList<BibleBook> BibleBooks { get; set; }

        public BibleVersion() { }

        public BibleVersion(string name, string abbreviation)
        {
            Name = name;
            Abbreviation = abbreviation;
        }
    }
}
