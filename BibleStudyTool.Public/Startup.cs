﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BibleStudyTool.Infrastructure.Data;
using System.IO;
using Microsoft.EntityFrameworkCore;
using BibleStudyTool.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;

namespace BibleStudyTool.Public
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var cnxstr = File.ReadAllText("./testdbcnxstr.txt");
            services.AddDbContext<BibleReadingDbContext>(options =>
                options.UseNpgsql(cnxstr, db => db.MigrationsAssembly("BibleStudyTool.Infrastructure")));

            services.AddIdentity<BibleReader, IdentityRole>()
                    .AddEntityFrameworkStores<BibleReadingDbContext>()
                    .AddDefaultTokenProviders();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
