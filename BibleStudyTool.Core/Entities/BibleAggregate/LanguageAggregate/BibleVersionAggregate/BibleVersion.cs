using System;
using System.Collections.Generic;

namespace BibleStudyTool.Core.Entities.BibleAggregate
{
    public class BibleVersion : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Abbreviation { get; private set; }
        public string Text { get; private set; }

        public int LanguageId { get; private set; }
        public Language Language { get; private set; }

        public IList<BibleBook> BibleBooks { get; set; }

        public BibleVersion(string name, string abbreviation, string text)
        {
            Name = name;
            Abbreviation = abbreviation;
            Text = text; 
        }
    }
}
