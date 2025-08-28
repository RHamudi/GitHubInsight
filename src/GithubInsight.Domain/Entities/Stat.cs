using System;
using System.Collections.Generic;

namespace GithubInsight.Domain.Entities;

public partial class Stat
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int TotalRepos { get; set; }

    public int Stars { get; set; }

    public string? MostUsedLanguage { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<LanguageStat> LanguageStats { get; set; } = new List<LanguageStat>();

    public virtual ICollection<ReposCreatedPerYear> ReposCreatedPerYears { get; set; } = new List<ReposCreatedPerYear>();

    public virtual ICollection<TopRepo> TopRepos { get; set; } = new List<TopRepo>();

    public virtual User User { get; set; } = null!;
}
