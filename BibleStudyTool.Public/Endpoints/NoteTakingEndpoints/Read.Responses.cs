using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Public.Endpoints.NoteTakingEndpoints
{
    public class GetChapterNotesResponse
    {
        public IEnumerable<NoteWithTagsAndReferences> Notes { get; set; }
    }
}
