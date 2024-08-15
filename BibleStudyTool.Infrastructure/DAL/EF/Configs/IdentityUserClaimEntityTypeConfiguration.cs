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
    internal class IdentityUserClaimEntityTypeConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
        {
            builder.Property
                (br => br.Id)
                .HasColumnName("id");

            builder.Property
                (br => br.UserId)
                .HasColumnName("user_id");

            builder.Property
                (br => br.ClaimType)
                .HasColumnName("claim_type");

            builder.Property
                (br => br.ClaimValue)
                .HasColumnName("claim_value");
        }
    }
}
