using System;
using BibleStudyTool.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.DAL.Config
{
    public class BibleBookEntityTypeConfiguration : IEntityTypeConfiguration<BibleBook>
    {
        public void Configure(EntityTypeBuilder<BibleBook> builder)
        {
            builder.HasKey(bibleBook => bibleBook.BibleBookId);

            builder.Property(bibleBook => bibleBook.DefaultName)
                   .HasColumnName("BibleBookDefaultName")
                   .HasMaxLength(30)
                   .IsRequired();
        }
    }
}
