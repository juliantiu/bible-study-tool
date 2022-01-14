using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Entities
{
    public class Language : BaseEntity, IAggregateRoot
    {
        public string Code { get; private set; }

        public string Name { get; private set; }
        public string Endonym { get; private set; }

        public IList<BibleVersionLanguage> BibleVersionLanguages { get; }
        public IList<BibleVerseBibleVersionLanguage> BibleVerseBibleVersionLanguages { get; }
        public IList<BibleBookAbbreviationLanguage> BibleBookAbbreviationLanguages { get; }
        public IList<BibleBookLanguage> BibleBookLanguages { get; }

        public Language() { }

        public Language(string code, string name, string endonym)
        {
            Code = code;
            Name = name;
            Endonym = endonym;
        }
    }
}
