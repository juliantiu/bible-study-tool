﻿using BibleStudyTool.Core.Entities.BibleVerse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleStudyTool.Infrastructure.DAL.EF.Configs
{
    internal class BibleVerseEntityTypeConfiguration : IEntityTypeConfiguration<BibleVerse>
    {
        public void Configure(EntityTypeBuilder<BibleVerse> builder)
        {

            builder.HasKey 
                (bv => new
                    { 
                        bv.Language,
                        bv.VersionAbbreviation,
                        bv.BookKey,
                        bv.ChapterNumber,
                        bv.VerseNumber
                    }
                );

            builder.Property
                (bv => bv.Language)
                .HasColumnType("character (3)")
                .HasColumnName("language");

            builder.Property
                (bv => bv.VersionAbbreviation)
                .HasColumnType("character varying (256)")
                .HasColumnName("version_abbreviation");

            builder.Property
                (bv => bv.BookKey)
                .HasColumnType("character varying (256)")
                .HasColumnName("book_id");

            builder.Property
                (bv => bv.ChapterNumber)
                .HasColumnType("smallint")
                .HasColumnName("chapter_number");

            builder.Property
                (bv => bv.VerseNumber)
                .HasColumnType("smallint")
                .HasColumnName("verse_number");

            builder.Property
                (bv => bv.VerseText)
                .HasColumnType("text")
                .HasColumnName("verse_text")
                .IsRequired();
        }
    }
}
