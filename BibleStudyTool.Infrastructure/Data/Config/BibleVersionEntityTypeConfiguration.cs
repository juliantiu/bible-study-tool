using System;
using BibleStudyTool.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.Data.Config
{
    public class BibleVersionEntityTypeConfiguration : IEntityTypeConfiguration<BibleVersion>
    {
        public void Configure(EntityTypeBuilder<BibleVersion> builder)
        {
            builder.HasKey(bibleVersion => bibleVersion.BibleVersionId);

            builder.Property(bibleVersion => bibleVersion.DefaultName)
                   .HasColumnName("BibleVersionDefaultName")
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(bibleVersion => bibleVersion.DefaultAbbreviation)
                   .HasColumnName("BibleVersionDefaultAbbreviation")
                   .HasMaxLength(20);
        }
    }
}
