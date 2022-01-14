using System;
using BibleStudyTool.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.DAL.EF.Config
{
    public class NoteEntityTypeConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(note => note.NoteId);

            builder.Property(note => note.Uid)
                   .HasColumnName("NoteUid")
                   .IsRequired();
            
			builder.Property(note => note.Summary)
                   .HasColumnName("NoteSummary")
                   .IsRequired();

            builder.Property(note => note.Text)
                   .HasColumnName("NoteText");
        }
    }
}
