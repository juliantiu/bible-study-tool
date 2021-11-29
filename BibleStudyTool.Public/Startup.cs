using System;
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
using BibleStudyTool.Infrastructure.DAL.EF;
using System.IO;
using Microsoft.EntityFrameworkCore;
using BibleStudyTool.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using BibleStudyTool.Core.Interfaces;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BibleStudyTool.Core.NonEntityTypes;
using BibleStudyTool.Infrastructure.ServiceLayer;
using BibleStudyTool.Infrastructure.DAL.Npgsql;

namespace BibleStudyTool.Public
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime.
        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Entity Framework core configuration
            var cnxstr = File.ReadAllText("./testdbcnxstr.txt");
            services.AddDbContext<BibleReadingDbContext>
                (options => options.UseNpgsql
                    (cnxstr,
                    db => db.MigrationsAssembly
                        ("BibleStudyTool.Infrastructure")));

            // Microsoft Identity with EF core configuration
            services.AddIdentity<BibleReader, IdentityRole>
                (options =>
                {
                    options.SignIn.RequireConfirmedEmail = false;
                    // todo configure to true in production 
                })
                .AddEntityFrameworkStores<BibleReadingDbContext>()
                .AddDefaultTokenProviders();

            // Scoped services
            services.AddScoped
                <ITokenClaimsService, IdentityTokenClaimService>();

            services.AddScoped
                (typeof(IAsyncRepository<>),
                typeof(BibleReadingEntityRepository<>));

            services.AddScoped
                (typeof(IEntityCrudActionExceptionFactory),
                typeof(EntityCrudActionExceptionFactory));

            // Entity query services
            services.AddScoped
                (typeof(LanguageQueries), _ => new LanguageQueries(cnxstr));

            services.AddScoped
                (typeof(BibleVersionLanguageQueries),
                _ => new BibleVersionLanguageQueries(cnxstr));

            services.AddScoped
                (typeof(BibleBookLanguageQueries),
                _ => new BibleBookLanguageQueries(cnxstr));

            services.AddScoped
                (typeof(NoteQueries), _ => new NoteQueries(cnxstr));

            services.AddScoped
                (typeof(NoteReferenceQueries),
                _ => new NoteReferenceQueries(cnxstr));

            services.AddScoped(typeof(TagQueries), _ => new TagQueries(cnxstr));

            services.AddScoped
                (typeof(TagGroupQueries), _ => new TagGroupQueries(cnxstr));

            services.AddScoped
                (typeof(TagNoteQueries), _ => new TagNoteQueries(cnxstr));

            // Entity services
            services.AddScoped
                (typeof(IBibleVersionLanguageService),
                typeof(BibleVersionLanguageService));

            services.AddScoped
                (typeof(ILanguageService), typeof(LanguageQueries));

            services.AddScoped
                (typeof(IBibleBookLanguageService),
                typeof(BibleBookLanguageService));

            services.AddScoped
                (typeof(ITagService), typeof(TagService));

            services.AddScoped
                (typeof(ITagGroupTagService), typeof(TagGroupTagService));

            services.AddScoped
                (typeof(ITagGroupService), typeof(TagGroupService));

            services.AddScoped
                (typeof(ITagNoteService), typeof(TagNoteService));

            services.AddScoped
                (typeof(INoteService), typeof(NoteService));

            services.AddScoped
                (typeof(INoteReferenceService), typeof(NoteReferenceService));

            /* Authentication & JWT configuration
             * Resources:
             *   - https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.authentication.jwtbearer.jwtbeareroptions?view=aspnetcore-5.0
             *   - https://www.youtube.com/watch?app=desktop&v=Lh82WlOvyQk
             * */
            var key = Encoding.ASCII.GetBytes("P@ssw0rd");
            // (AuthorizationConstants.JWT_SECRET_KEY);
            // todo configure in environment variable

            services.AddAuthentication
                (config =>
                {
                    config.DefaultScheme =
                        JwtBearerDefaults.AuthenticationScheme;

                    config.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                })
            .AddJwtBearer(config =>
            {
                config.Events = new JwtBearerEvents()
                {
                    OnTokenValidated = context =>
                    {
                        var userCntx = context.HttpContext
                                              .RequestServices
                                              .GetRequiredService
                                                <UserManager<BibleReader>>();

                        var user = userCntx.GetUserAsync
                            (context.HttpContext.User);

                        if (user == null)
                        {
                            context.Fail("Unauthorized.");
                        }

                        return Task.CompletedTask;
                    }
                };
                config.RequireHttpsMetadata = false;
                // todo should be true in productiom

                config.SaveToken = true;
                config.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddControllers();
        }

        // This method gets called by the runtime.
        // Use this method to configure the HTTP request pipeline.
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
