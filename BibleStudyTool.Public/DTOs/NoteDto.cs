using System;
namespace BibleStudyTool.Public.DTOs
{
    public class NoteDto
    {
        public int NoteId { get; set; }

        public string Uid { get; set; }

        public string Summary { get; set; }
        public string Text { get; set; }
    }
}
