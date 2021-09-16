using System;
using BibleStudyTool.Core.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.Data.Config
{
    public class NoteReferenceEntityTypeConfiguration : IEntityTypeConfiguration<NoteReference>
    {
        public void Configure(EntityTypeBuilder<NoteReference> builder)
        {
            builder.HasKey(noteReference => noteReference.NoteReferenceId);

            builder.HasOne(note => note.Note)
                   .WithMany(note => note.Notes)
                   .HasForeignKey(note => note.NoteId)
                   .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(reference => reference.Reference)
                   .WithMany(note => note.NoteReferences)
                   .HasForeignKey(reference => reference.NoteReferenceId)
                   .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
