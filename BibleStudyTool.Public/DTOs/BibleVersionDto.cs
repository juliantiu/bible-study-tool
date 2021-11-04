using System;
namespace BibleStudyTool.Public.DTOs
{
    public class BibleVersionDto
    {
        public int BibleVersionId { get; }
        public string DefaultName { get; set; }
        public string DefaultAbbreviation { get; set; }
        public string LanguageCode { get; }
        public string Name { get; }
        public string Abbreviation { get; }
    }
}
