using System;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.BibleAggregate;
using BibleStudyTool.Core.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.Data.Config
{
    public class BibleVerseNoteEntityTypeConfiguration: IEntityTypeConfiguration<BibleVerseNote>
    {
        public void Configure(EntityTypeBuilder<BibleVerseNote> builder)
        {
            builder.HasKey(bibleVerseNote => bibleVerseNote.BibleVerseNoteId);

            builder.HasOne<BibleVerse>(bibleVerseNote => bibleVerseNote.BibleVerse)
                   .WithMany(bibleVerse => bibleVerse.BibleVerseNotes)
                   .HasForeignKey(bibleVerseNotes => bibleVerseNotes.BibleVerseId)
                   .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne<Note>(bibleVerseNote => bibleVerseNote.Note)
                   .WithMany(note => note.BibleVerseNotes)
                   .HasForeignKey(bibleVerseNote => bibleVerseNote.NoteId)
                   .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
