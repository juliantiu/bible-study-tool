using System;
namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    public class UpdateNoteRequest
    {
        public int NoteId { get; set; }
        public string Summary { get; set; }
        public string Text { get; set; }

        public UpdateNoteRequest()
        {
        }
    }
}
