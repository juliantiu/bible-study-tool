using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Public.Endpoints.NoteReferenceEndpoints
{
    public class CreateNoteReferenceRequestObject
    {
        public int OwningNoteId { get; set; }
        public int? ReferencedNoteId { get; set; }
        public int? ReferencedBibleVerseId { get; set; }
    }

    public class CreateNoteReferenceRequest
    {
        public IList<CreateNoteReferenceRequestObject> NoteReferences;

        public CreateNoteReferenceRequest()
        {
        }
    }
}
