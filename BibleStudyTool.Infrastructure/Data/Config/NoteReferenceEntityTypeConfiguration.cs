using System;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.Data.Config
{
    public class NoteReferenceEntityTypeConfiguration : IEntityTypeConfiguration<NoteReference>
    {
        public void Configure(EntityTypeBuilder<NoteReference> builder)
        {
            builder.HasKey(noteReference => new { noteReference.OwningNoteId, noteReference.ReferenceId });

            builder.Property(noteReference => noteReference.NoteReferenceType)
                   .IsRequired();

            builder.HasOne<Note>(noteReference => noteReference.OwningNote)
                   .WithMany(note => note.ReferencedIn)
                   .HasForeignKey(noteReference => noteReference.OwningNoteId)
                   .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne<Note>(noteReference => noteReference.ReferencedNote)
                   .WithMany(note => note.ReferencedNotes)
                   .HasForeignKey(noteReference => noteReference.ReferenceId)
                   .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne<BibleVerse>(noteReference => noteReference.ReferencedBibleVerse)
                   .WithMany(bibleVerse => bibleVerse.NoteReferences)
                   .HasForeignKey(noteReference => noteReference.ReferenceId)
                   .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
