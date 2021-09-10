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
            builder.HasKey(tag => tag.Id);

            builder.Property(tag => tag.Uid)
                   .IsRequired();

            builder.Property(tag => tag.Label)
                   .HasMaxLength(100)
                   .IsRequired();
        }
    }
}
