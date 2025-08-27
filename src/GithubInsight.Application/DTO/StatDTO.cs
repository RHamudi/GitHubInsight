using GithubInsight.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubInsight.Application.DTO
{
    public class StatDto
    {
        public int UserId { get; set; }
        public int TotalRepos { get; set; }
        public int Stars { get; set; }
        public string MostUsedLanguage { get; set; }
        public List<LanguageStatDto> LanguageStats { get; set; }
        public List<ReposCreatedPerYearDto> ReposCreatedPerYears { get; set; }
        public List<TopRepoDto> TopRepos { get; set; }
    }

    public class LanguageStatDto
    {
        public string Language { get; set; }
        public int Count { get; set; }
        public DateTime? CreatedAt { get; set; }
    }

    public class ReposCreatedPerYearDto
    {
        public int Year { get; set; }

        public int Count { get; set; }

        public DateTime? CreatedAt { get; set; }
    }

    public partial class TopRepoDto
    {

        public string RepoName { get; set; } = null!;

        public int Stars { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
