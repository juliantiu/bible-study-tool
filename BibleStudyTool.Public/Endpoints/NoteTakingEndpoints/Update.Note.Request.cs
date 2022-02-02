using System;
using System.Collections.Generic;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.NoteTakingEndpoints
{
    public class UpdateNoteRequest
    {
        public IEnumerable<NoteVerseReferenceDto> NoteVerseReferences { get; set; }
        public IEnumerable<int> NoteReferenceIds { get; set; }
        public IEnumerable<int> TagIds { get; set; }
        public IEnumerable<TagDto> NewTags { get; set; }

        public int NoteId { get; set; }

        public string Summary { get; set; }
        public string Text { get; set; }
    }
}
