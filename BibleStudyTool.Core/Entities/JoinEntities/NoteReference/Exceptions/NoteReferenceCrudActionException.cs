using System;
using BibleStudyTool.Core.Exceptions;

namespace BibleStudyTool.Core.Entities.JoinEntities
{
    public class NoteReferenceCrudActionException : EntityCrudActionException
    {
        public NoteReferenceCrudActionException() => Timestamp = DateTime.UtcNow;

        public NoteReferenceCrudActionException(string message)
            : base(message) => Timestamp = DateTime.UtcNow;

        public NoteReferenceCrudActionException(string message, Exception inner)
            : base(message, inner) => Timestamp = DateTime.UtcNow;
    }
}
