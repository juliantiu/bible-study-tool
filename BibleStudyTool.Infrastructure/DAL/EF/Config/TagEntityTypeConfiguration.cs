using System;
using BibleStudyTool.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.DAL.EF.Config
{
    public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(tag => tag.TagId);

            builder.Property(tag => tag.Uid)
                   .HasColumnName("TagUid")
                   .IsRequired();

            builder.Property(tag => tag.Label)
                   .HasColumnName("TagLabel")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(tag => tag.Color)
                   .HasColumnName("TagColor")
                   .HasMaxLength(25); // Supports: RGB(255,255,255) , CMYK(100%,100%,100%,100%) , RGBA(255,255,255,0.000) , #FFFFFF , HSLA(359,100%,100%) , HSLA(359,100%,100%,0.000) 
        }
    }
}
