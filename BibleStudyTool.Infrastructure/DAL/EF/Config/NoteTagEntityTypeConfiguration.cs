using System;
using BibleStudyTool.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.DAL.EF.Config
{
    public class NoteTagEntityTypeConfiguration : IEntityTypeConfiguration<NoteTag>
    {
        public void Configure(EntityTypeBuilder<NoteTag> builder)
        {
            builder.HasKey(tagNote => new { tagNote.TagId, tagNote.NoteId });

            builder.HasOne<Tag>(tagNote => tagNote.Tag)
                   .WithMany(tag => tag.NoteTags)
                   .HasForeignKey(tagNote => tagNote.TagId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Note>(tagNote => tagNote.Note)
                   .WithMany(note => note.NoteTags)
                   .HasForeignKey(tagNote => tagNote.NoteId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
