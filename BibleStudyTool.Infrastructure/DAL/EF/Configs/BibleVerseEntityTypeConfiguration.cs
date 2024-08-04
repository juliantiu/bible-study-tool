using BibleStudyTool.Core.Entities.BibleVerse;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleStudyTool.Infrastructure.DAL.EF.Configs
{
    public class BibleVerseEntityTypeConfiguration : IEntityTypeConfiguration<BibleVerse>
    {
        public void Configure(EntityTypeBuilder<BibleVerse> builder)
        {

            builder.HasKey 
                (bv => new
                    { 
                        bv.Language,
                        bv.VersionAbbr,
                        bv.BookId,
                        bv.ChapterNumber,
                        bv.VerseNumber
                    }
                );

            builder.Property
                (bv => bv.Version)
                .IsRequired();

            builder.Property
                (bv => bv.BookAbbr)
                .IsRequired();

            builder.Property
                (bv => bv.BookName)
                .IsRequired();

            builder.Property
                (bv => bv.Text)
                .IsRequired();
        }
    }
}
