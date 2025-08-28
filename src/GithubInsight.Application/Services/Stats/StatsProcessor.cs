using GithubInsight.Application.DTO;
using GithubInsight.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubInsight.Application.Services.Stats
{
    internal class StatsProcessor
    {
        public static Stat Process(List<GithubUserRepoResponse> response, int userId)
        {
            var totalRepos = response.Count;
            var totalStars = response.Sum(r => r.Star);

            var mostUsedLanguage = response
                    .Where(r => !string.IsNullOrEmpty(r.Language))
                    .GroupBy(r => r.Language)
                    .OrderByDescending(g => g.Count())
                    .FirstOrDefault()?.Key;

            var LanguageStats = response
                .Where(r => !string.IsNullOrEmpty(r.Language))
                .GroupBy(r => r.Language)
                .Select(g => new LanguageStat
                {
                    Language = g.Key!,
                    Count = g.Count(),
                    CreatedAt = DateTime.UtcNow
                }).ToList();

            var reposPerYear = response
                    .GroupBy(r => r.CreatedAt.Year)
                    .Select(g => new ReposCreatedPerYear
                    {
                        Year = g.Key,
                        Count = g.Count(),
                        CreatedAt = DateTime.UtcNow
                    }).ToList();

            var TopRepos = response
                .OrderByDescending(r => r.Star)
                .Take(5)
                .Select(r => new TopRepo
                {
                    RepoName = r.Name,
                    Stars = r.Star,
                    CreatedAt = DateTime.UtcNow
                }).ToList();

            return new Stat
            {
                UserId = userId,
                TotalRepos = totalRepos,
                Stars = totalStars,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                MostUsedLanguage = mostUsedLanguage,
                LanguageStats = LanguageStats,
                ReposCreatedPerYears = reposPerYear,
                TopRepos = TopRepos,
            };
        }
    }
}
