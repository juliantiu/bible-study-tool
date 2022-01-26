using BibleStudyTool.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleStudyTool.Infrastructure.DAL.EF.Config
{
    public class NoteVerseReferenceEntityTypeConfiguration : IEntityTypeConfiguration<NoteVerseReference>
    {
        public void Configure(EntityTypeBuilder<NoteVerseReference> builder)
        {
            builder.HasKey(noteVerseReference => noteVerseReference.Id);

            builder.Property(noteVerseReference => noteVerseReference.NoteId)
                   .IsRequired();

            builder.Property(noteVerseReference => noteVerseReference.BibleBook)
                   .IsRequired();

            builder.Property(noteVerseReference => noteVerseReference.BookChapter)
                   .IsRequired();

            builder.Property(noteVerseReference => noteVerseReference.ChapterVerseNumber)
                   .IsRequired();

            builder.HasOne<Note>(noteReference => noteReference.Note)
                   .WithMany(note => note.NoteVerseReferences)
                   .HasForeignKey(noteReference => noteReference.NoteId)
                   .IsRequired(false)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
