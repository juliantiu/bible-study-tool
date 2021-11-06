using System;
using System.Collections.Generic;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.NoteTakingEndpoints
{
    public class CreateNoteRequest
    {
        public IEnumerable<int> BibleVerseReferences { get; set; }
        public IEnumerable<int> ExistingTags { get; set; }
        public IEnumerable<int> NoteReferences { get; set; }
        public IEnumerable<TagDto> NewTags { get; set; }

        public string Summary { get; set; }
        public string Text { get; set; }
    }
}
