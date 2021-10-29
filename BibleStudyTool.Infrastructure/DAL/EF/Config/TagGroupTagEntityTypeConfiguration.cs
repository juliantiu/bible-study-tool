﻿using System;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.DAL.EF.Config
{
    public class TagGroupTagEntityTypeConfiguration : IEntityTypeConfiguration<TagGroupTag>
    {
        public void Configure(EntityTypeBuilder<TagGroupTag> builder)
        {
            builder.HasKey(tagGroupTag => new { tagGroupTag.TagGroupId, tagGroupTag.TagId });

            builder.HasOne<TagGroup>(tagGroupTag => tagGroupTag.TagGroup)
                   .WithMany(tagGroup => tagGroup.TagGroupTags)
                   .HasForeignKey(tagGroupTag => tagGroupTag.TagGroupId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Tag>(tagGroupTag => tagGroupTag.Tag)
                   .WithMany(tag => tag.TagGroupTags)
                   .HasForeignKey(tagGroupTag => tagGroupTag.TagId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
