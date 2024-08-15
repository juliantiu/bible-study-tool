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
    internal class IdentityRoleEntityTypeConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.Property
                (br => br.Id)
                .HasColumnName("id");

            builder.Property
                (br => br.Name)
                .HasColumnName("name");

            builder.Property
                (br => br.NormalizedName)
                .HasColumnName("normalized_name");

            builder.Property
                (br => br.ConcurrencyStamp)
                .HasColumnName("concurrency_stamp");
        }
    }
}
