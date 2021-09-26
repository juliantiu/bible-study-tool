using System.Collections.Generic;

namespace BibleStudyTool.Public.Endpoints.TagGroupNoteEndpoints
{
    public class DeleteTagGroupNoteRequestObject
    {
        public int TagGroupId { get; set; }
        public int NoteId { get; set; }
    }

    public class DeleteTagGroupNoteRequest
    {
        public IList<DeleteTagGroupNoteRequestObject> TagGroupNotes { get; set; }

        public DeleteTagGroupNoteRequest()
        {
        }
    }
}
