using GithubInsight.Application.Receivers.RepoReceiver;
using GithubInsight.Application.Receivers.UserReceiver;
using GithubInsight.Application.Services.APIGithub;
using GithubInsight.Application.Services.APIGithub.Interfaces;
using GithubInsight.Infrastructure.Repositories;
using GithubInsight.Infrastructure.Repositories.Interfaces;
using GithubInsight.Infrastructure.Shared.Context;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<GithubInsightContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
builder.Services.AddScoped<GithubInsightContext>();
builder.Services.AddScoped<IGithubService, GithubService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStatsRepository, StatsRepository>();
builder.Services.AddScoped<InsertUser>();
builder.Services.AddScoped<InsertStats>();
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

