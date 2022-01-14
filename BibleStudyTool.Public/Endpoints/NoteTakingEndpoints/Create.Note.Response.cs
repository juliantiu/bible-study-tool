using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Public.Endpoints.NoteTakingEndpoints
{
    public class CreateNoteResponse
    {
        public NoteWithTagsAndReferences Note { get; set; }

        public CreateNoteResponse() { }

        public CreateNoteResponse(NoteWithTagsAndReferences note)
        {
            Note = note;
        }
    }
}
