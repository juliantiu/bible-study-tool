using System;
namespace BibleStudyTool.Public.DTOs
{
    public class BibleBookDto
    {
        public int BibleBookId { get; set; }
        public string DefaultName { get; set; }
        public string Name { get; set; }
        public string DefaultAbbreviation { get; set; }
        public string Abbreviation { get; set; }
        public string Style { get; set; }
    }
}
