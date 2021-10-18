using System;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Public.DTOs
{
    public class NoteReferenceDto
    {
        public int NoteId { get; set; }
        public int? ReferenceNoteId { get; set; }
        public int? ReferencedBibleVerseId { get; set; }
    }
}
