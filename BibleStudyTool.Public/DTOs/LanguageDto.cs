using System;
using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Public.DTOs
{
    public class LanguageDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Endonym { get; set; }

        public LanguageDto() { }

        public LanguageDto(Language language)
        {
            Code = language.Code;
            Name = language.Name;
            Endonym = language.Endonym;
        }
    }
}
