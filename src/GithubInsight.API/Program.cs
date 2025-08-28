
using GithubInsight.Application.Receivers.LanguageReceiver;
using GithubInsight.Application.Receivers.RepoReceiver;
using GithubInsight.Application.Receivers.StatsReceiver;
using GithubInsight.Application.Receivers.TopReposReceiver;
using GithubInsight.Application.Receivers.UserReceiver;
using GithubInsight.Application.Services.APIGithub;
using GithubInsight.Application.Services.APIGithub.Interfaces;
using GithubInsight.Application.Services.JWT;
using GithubInsight.Infrastructure.Repositories;
using GithubInsight.Infrastructure.Repositories.Interfaces;
using GithubInsight.Infrastructure.Shared.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        };
    });

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "GithubInsight API", Version = "v1" });

    // Configurar segurança JWT
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT desta forma: Bearer {seu_token}"
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAuthorization();

builder.Services.AddDbContext<GithubInsightContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
//builder.Services.AddScoped<GithubInsightContext>();
builder.Services.AddScoped<IGithubService, GithubService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IStatsRepository, StatsRepository>();
builder.Services.AddScoped<ITopReposRepository, TopReposRepository>();
builder.Services.AddScoped<ILanguagesRepository,  LanguagesRepository>();
builder.Services.AddScoped<InsertUser>();
builder.Services.AddScoped<InsertStats>();
builder.Services.AddScoped<ReadStats>();
builder.Services.AddScoped<ReadTopRepos>();
builder.Services.AddScoped<ReadLanguage>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddHttpClient();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();

