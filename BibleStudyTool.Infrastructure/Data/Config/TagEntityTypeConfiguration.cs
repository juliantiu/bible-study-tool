using System;
using BibleStudyTool.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.Data.Config
{
    public class TagEntityTypeConfiguration: IEntityTypeConfiguration<Tag>
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
        }
    }
}
