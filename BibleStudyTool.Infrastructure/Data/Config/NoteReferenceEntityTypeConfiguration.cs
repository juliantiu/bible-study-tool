﻿using System;
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
            builder.HasKey(noteReference => noteReference.NoteReferenceSurrogateKey);

            builder.HasOne<Note>(noteReference => noteReference.Note)
                   .WithMany(note => note.ReferencedIn)
                   .HasForeignKey(noteReference => noteReference.NoteId)
                   .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne<Note>(noteReference => noteReference.ReferencedNote)
                   .WithMany(note => note.ReferencedNotes)
                   .HasForeignKey(noteReference => noteReference.ReferencedNoteId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne<BibleVerse>(noteReference => noteReference.ReferencedBibleVerse)
                   .WithMany(bibleVerse => bibleVerse.ReferencedNotes)
                   .HasForeignKey(noteReference => noteReference.ReferencedNoteId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
