using System;
using BibleStudyTool.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.DAL.EF.Config
{
    public class BibleBookEntityTypeConfiguration : IEntityTypeConfiguration<BibleBook>
    {
        public void Configure(EntityTypeBuilder<BibleBook> builder)
        {
            builder.HasKey(bibleBook => bibleBook.BibleBookId);

            builder.Property(bibleBook => bibleBook.DefaultName)
                   .HasColumnName("BibleBookDefaultName")
                   .IsRequired();

            builder.Property(bibleBook => bibleBook.DefaultAbbreviation)
                   .HasColumnName("BibleBookDefaultAbbreviation")
                   .IsRequired();

            builder.Property(bibleBook => bibleBook.Order)
                   .HasColumnName("BibleBookOrder")
                   .IsRequired();

            builder.Property(bibleBook => bibleBook.IsNewTestament)
                   .IsRequired();
        }
    }
}
