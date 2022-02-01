using System;

namespace BibleStudyTool.Public.DTOs
{
    public class NoteReferenceDto
    {
        public int NoteId { get; set; }
        public int? ReferencedNoteId { get; set; }
        public int? ReferencedBibleVerseId { get; set; }
    }
}
