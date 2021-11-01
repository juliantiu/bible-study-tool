using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    public class GetChapterNotesResponse
    {
        public IEnumerable<NoteWithTagsAndReferences> Notes { get; set; }
    }
}
