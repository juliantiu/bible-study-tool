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
    internal class IdentityRoleClaimEntityTypeConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
        {
            builder.Property
                (br => br.Id)
                .HasColumnName("id");

            builder.Property
                (br => br.RoleId)
                .HasColumnName("role_id");

            builder.Property
                (br => br.ClaimType)
                .HasColumnName("claim_type");

            builder.Property
                (br => br.ClaimValue)
                .HasColumnName("claim_value");
        }
    }
}
