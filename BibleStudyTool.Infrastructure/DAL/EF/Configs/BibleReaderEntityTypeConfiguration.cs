using BibleStudyTool.Core.Entities.BibleVerse;
using BibleStudyTool.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleStudyTool.Infrastructure.DAL.EF.Configs
{
    public class BibleReaderEntityTypeConfiguration : IEntityTypeConfiguration<BibleReader>
    {
        public void Configure(EntityTypeBuilder<BibleReader> builder)
        {
            builder.Property
                (br => br.Id)
                .HasColumnName("id");
            
            builder.Property
                (br => br.UserName)
                .HasColumnName("username");

            builder.Property
                (br => br.NormalizedUserName)
                .HasColumnName("normalized_username");

            builder.Property
                (br => br.Email)
                .HasColumnName("email");

            builder.Property
                (br => br.NormalizedEmail)
                .HasColumnName("normalized_email");

            builder.Property
                (br => br.EmailConfirmed)
                .HasColumnName("email_confirmed");

            builder.Property
                (br => br.PasswordHash)
                .HasColumnName("password_hash");

            builder.Property
                (br => br.SecurityStamp)
                .HasColumnName("security_stamp");

            builder.Property
                (br => br.ConcurrencyStamp)
                .HasColumnName("concurrency_setup");

            builder.Property
                (br => br.PhoneNumber)
                .HasColumnName("phone_number");

            builder.Property
                (br => br.PhoneNumberConfirmed)
                .HasColumnName("phone_number_confirmed");

            builder.Property
                (br => br.TwoFactorEnabled)
                .HasColumnName("two_factor_enabled");

            builder.Property
                (br => br.LockoutEnd)
                .HasColumnName("lockout_end");

            builder.Property
                (br => br.LockoutEnabled)
                .HasColumnName("lockout_enabled");

            builder.Property
                (br => br.AccessFailedCount)
                .HasColumnName("access_failed_count");
        }
    }
}
