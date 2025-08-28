using GithubInsight.Application.DTO;
using GithubInsight.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubInsight.Application.Mappers
{
    public class StatsMapper
    {
        public static StatDto ToDto(Stat stat)
        {
            return new StatDto
            {
                UserId = stat.UserId,
                TotalRepos = stat.TotalRepos,
                Stars = stat.Stars,
                MostUsedLanguage = stat.MostUsedLanguage,
                LanguageStats = stat.LanguageStats?.Select(ls => new LanguageStatDto
                {
                    
                    Language = ls.Language,
                    Count = ls.Count,
                    CreatedAt = ls.CreatedAt
                }).ToList() ?? [],
                ReposCreatedPerYears = stat.ReposCreatedPerYears?.Select(r => new ReposCreatedPerYearDto
                {
                    Year = r.Year,
                    Count = r.Count,
                    CreatedAt = r.CreatedAt
                }).ToList() ?? [],
                TopRepos = stat.TopRepos?.Select(tr => new TopRepoDto
                {
                    RepoName = tr.RepoName,
                    Stars = tr.Stars,
                    CreatedAt = tr.CreatedAt
                }).ToList() ?? []
            };
        }
    }
}
