using System;
namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    public class CreateNoteRequest
    {
        public string Summary { get; set; }
        public string Text { get; set; }

        public CreateNoteRequest()
        {
        }
    }
}
