using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.Entities
{
    public class BibleBook : BaseEntity, IAggregateRoot
    {
        public bool IsNewTestament { get; private set; }
        public int BibleBookId { get; private set; }
        public int Order { get; private set; }
        public string DefaultAbbreviation { get; private set; }
        public string DefaultName { get; private set; }

        public IList<BibleVerse> BibleVerses { get; }
        public IList<BibleBookAbbreviationLanguage> BibleBookAbbreviationLanguages { get; }
        public IList<BibleBookLanguage> BibleBookLanguages { get; }

        public BibleBook()
        {
        }

        public BibleBook
            (int bibleBookId, string defaultAbbreviation, string defaultName,
            int order, bool isNewTestament)
        {
            BibleBookId = bibleBookId;
            DefaultAbbreviation = defaultAbbreviation;
            DefaultName = defaultName;
            Order = order;
            IsNewTestament = isNewTestament;
        }

        public BibleBook(string defaultName)
        {
            DefaultName = defaultName;
        }
    }
}
