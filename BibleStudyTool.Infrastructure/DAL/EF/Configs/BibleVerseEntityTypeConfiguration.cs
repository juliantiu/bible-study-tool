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
                        bv.VerseReferenceKey,
                        bv.Language,
                        bv.Version 
                    }
                );

            builder.Property
                (bv => bv.BookKey)
                .IsRequired();

            builder.Property
                (bv => bv.ChapterNumber)
                .IsRequired();

            builder.Property
                (bv => bv.VerseNumber)
                .IsRequired();

            builder.Property
                (bv => bv.Text)
                .IsRequired();
        }
    }
}
