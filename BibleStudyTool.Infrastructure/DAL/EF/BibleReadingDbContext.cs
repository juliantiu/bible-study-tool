/*
 * * EF core migration and update commands from cli
 * ** dotnet ef migrations add MyFirstMigration --project BibleStudyTool.Infrastructure --startup-project BibleStudyTool.Public --context BibleReadingDbContext --output-dir ./DAL/EF/Migrations
 * ** dotnet ef database update --project BibleStudyTool.Infrastructure --startup-project BibleStudyTool.Public
 * * Clearing All DB tables
 * 
 *  DROP SCHEMA public CASCADE;
 *  CREATE SCHEMA public;
 *  
 * Microsoft Identity with EF core
 *   - https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-5.0&tabs=visual-studio
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibleStudyTool.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BibleStudyTool.Infrastructure.DAL.EF
{
    public class BibleReadingDbContext : IdentityDbContext<BibleReader>
    {
        public BibleReadingDbContext(DbContextOptions<BibleReadingDbContext> options) : base(options)
        {
        }

        public BibleReadingDbContext()
        {
        }
    }
}
