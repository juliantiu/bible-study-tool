using System;
using BibleStudyTool.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.DAL.Config
{
    public class LanguageEntityTypeConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(language => language.Code); // ISO-639-3

            builder.Property(language => language.Name)
                   .HasColumnName("LanguageName")
                   .HasMaxLength(70)
                   .IsRequired();

            builder.Property(language => language.Endonym)
                   .HasMaxLength(70);
        }
    }
}
