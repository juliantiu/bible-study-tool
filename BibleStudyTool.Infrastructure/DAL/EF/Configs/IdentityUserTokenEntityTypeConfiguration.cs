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
    internal class IdentityUserTokenEntityTypeConfiguration : IEntityTypeConfiguration<IdentityUserToken<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
        {
            builder.Property
                (br => br.UserId)
                .HasColumnName("user_id");

            builder.Property
                (br => br.LoginProvider)
                .HasColumnName("login_provider");

            builder.Property
                (br => br.Name)
                .HasColumnName("name");

            builder.Property
                (br => br.Value)
                .HasColumnName("value");
        }
    }
}
