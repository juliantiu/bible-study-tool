using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.Entities
{
    public class Note : BaseEntity
    {
        public int Id { get; private set; }

        public string Uid { get; private set; }

        public string Summary { get; private set; }
        public string Text { get; private set; }

        public IEnumerable<NoteTag> NoteTags { get; private set; }
        public IEnumerable<NoteVerseReference> NoteVerseReferences { get; private set; }
        public IEnumerable<NoteReference> NoteReferences { get; private set; }
        public IEnumerable<NoteReference> ReferencedByTheseNotes { get; private set; }

        public Note() { }

        public Note(string uid, string summary, string text)
        {
            Uid = uid;
            Summary = summary;
            Text = text;
        }

        public Note(int noteId, string uid, string summary, string text)
        {
            Id = noteId;
            Uid = uid;
            Summary = summary;
            Text = text;
        }

        public Note UpdateDetails(string summary, string text)
        {
            Summary = summary;
            Text = text;

            return this;
        }

        public void SetNoteAuthor(string uid)
        {
            Uid = uid;
        }

        public void PopulateNoteVerseReferences(IEnumerable<NoteVerseReference> noteVerseReferences)
        {
            NoteVerseReferences = noteVerseReferences;
        }

        public void PopulateNoteReferences(IEnumerable<NoteReference> noteReferences)
        {
            NoteReferences = noteReferences;
        }

        public void ReferencedBy(IEnumerable<NoteReference> referencedByTheseNotes)
        {
            ReferencedByTheseNotes = referencedByTheseNotes;
        }
    }
}
