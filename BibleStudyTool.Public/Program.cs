using BibleStudyTool.Infrastructure.DAL.EF;
using BibleStudyTool.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BibleReadingDbContext>(
    options => options.UseNpgsql
        (builder.Configuration.GetConnectionString
            ("TestDbLocal"))
);

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<BibleReader>()
    .AddEntityFrameworkStores<BibleReadingDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<BibleReader>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
