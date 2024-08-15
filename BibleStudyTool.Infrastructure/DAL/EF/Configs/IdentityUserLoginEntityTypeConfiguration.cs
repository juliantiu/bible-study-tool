using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleStudyTool.Infrastructure.DAL.EF.Configs
{
    internal class IdentityUserLoginEntityTypeConfiguration : IEntityTypeConfiguration<IdentityUserLogin<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserLogin<string>> builder)
        {
            builder.Property
                (br => br.LoginProvider)
                .HasColumnName("login_provider");

            builder.Property
                (br => br.ProviderKey)
                .HasColumnName("provider_key");

            builder.Property
                (br => br.ProviderDisplayName)
                .HasColumnName("provider_display_name");

            builder.Property
                (br => br.UserId)
                .HasColumnName("user_id");
        }
    }
}
