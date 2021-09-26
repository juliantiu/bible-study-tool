using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Public.Endpoints.NoteReferenceEndpoints
{
    public class CreateNoteReferenceRequestObject
    {
        public int OwningNoteId { get; set; }
        public int ReferenceId { get; set; }
        public NoteReferenceType NoteReferenceType;
    }

    public class CreateNoteReferenceRequest
    {
        public IList<CreateNoteReferenceRequestObject> NoteReferences;

        public CreateNoteReferenceRequest()
        {
        }
    }
}
