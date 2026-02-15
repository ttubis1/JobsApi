using JobsApi.Data;
using JobsApi.Data.Repositories;
using JobsApi.Models;
using JobsApi.Profile;
using JobsApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var useSqlite = builder.Configuration.GetValue<bool>("UseSqlite");

if (useSqlite)
{
    // Register SQLite DbContext
    builder.Services.AddDbContext<IJobsDbContext, SqliteAppDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("SqliteConnection")));

}
else
{
    // Register SQL Server DbContext
    builder.Services.AddDbContext<IJobsDbContext, JobsDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

}




// Add services to the container.


builder.Services.AddAutoMapper(typeof(JobProfile));


builder.Services.AddScoped<JobService>();


builder.Services.AddControllers();
// Configure JWT Authentication
var InMemory = builder.Configuration["MySettings:InMemory"];
if (bool.Parse(InMemory))
    builder.Services.AddSingleton<IJobRepository, JobsRepositoryMem>();
else
    builder.Services.AddScoped<IJobRepository, JobsRepositoryDb>();

    var startupTime = DateTime.UtcNow;


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Add custom middleware before MVC

using (var scope = app.Services.CreateScope())
{
    if (useSqlite)
    {
        var db = scope.ServiceProvider.GetRequiredService<SqliteAppDbContext>();
        db.Database.Migrate();   // <-- This applies all pending migrations
    }
    else
    {
        var db = scope.ServiceProvider.GetRequiredService<JobsDbContext>();
        db.Database.Migrate();   // <-- This applies all pending migrations
    }
    
}

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
