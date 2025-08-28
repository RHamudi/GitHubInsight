using System;
using System.Collections.Generic;

namespace GithubInsight.Domain.Entities;

public partial class LanguageStat
{
    public int Id { get; set; }

    public int StatsId { get; set; }

    public string Language { get; set; } = null!;

    public int Count { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Stat Stats { get; set; } = null!;
}
