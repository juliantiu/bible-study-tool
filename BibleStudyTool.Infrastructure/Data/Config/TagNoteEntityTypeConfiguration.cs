using System;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.Data.Config
{
    public class TagNoteEntityTypeConfiguration : IEntityTypeConfiguration<TagNote>
    {
        public void Configure(EntityTypeBuilder<TagNote> builder)
        {
            builder.HasKey(tagNote => tagNote.TagNoteId);

            builder.HasOne<Tag>(tagNote => tagNote.Tag)
                   .WithMany(tag => tag.TagNotes)
                   .HasForeignKey(tagNote => tagNote.TagId)
                   .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne<Note>(tagNote => tagNote.Note)
                   .WithMany(note => note.TagNotes)
                   .HasForeignKey(tagNote => tagNote.NoteId)
                   .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
