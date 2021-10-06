using System;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Public.DTOs
{
    public class NoteReferenceDto
    {
        public int NoteId { get; set; }
        public int ReferenceId { get; set; }
        public NoteReferenceType NoteReferenceType { get; set; }
    }
}
