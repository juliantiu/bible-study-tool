using System;
using System.Collections.Generic;

namespace BibleStudyTool.Core.Entities.BibleAggregate
{
    public class Language : BaseEntity, IAggregateRoot
    {
        public string LanguageId { get; private set; }

        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Endonym { get; private set; }

        public IList<BibleVersion> BibleVersions { get; private set; }

        public Language() { }

        public Language(string code, string name, string endonym)
        {
            Code = code;
            Name = name;
            Endonym = endonym;
        }
    }
}
