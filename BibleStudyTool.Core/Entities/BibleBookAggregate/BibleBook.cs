using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities
{
    public class BibleBook : BaseEntity, IAggregateRoot
    {
        public int BibleBookId { get; }
        public string BibleBookDefaultName { get; private set; }

        public IList<BibleVerse> BibleVerses { get; }
        public IList<BibleBookAbbreviationLanguage> BibleBookAbbreviationLanguages { get; }
        public IList<BibleBookLanguage> BibleBookLanguages { get; }

        public BibleBook()
        {
        }

        public BibleBook(string bibleBookDefaultName)
        {
            BibleBookDefaultName = bibleBookDefaultName;
        }
    }
}
