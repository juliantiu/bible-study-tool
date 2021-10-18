using System;
using System.Collections.Generic;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    public class CreateNoteRequest
    {
        public IList<int> ExistingTags { get; set; }
        public IList<TagDto> NewTags { get; set; }
        public IList<int> BibleVerseReferences { get; set; }
        public IList<int> NoteReferences { get; set; }
        public string Summary { get; set; }
        public string Text { get; set; }
    }
}
