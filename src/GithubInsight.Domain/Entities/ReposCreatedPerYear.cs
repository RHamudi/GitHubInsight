using System;
using System.Collections.Generic;

namespace GithubInsight.Domain.Entities;

public partial class ReposCreatedPerYear
{
    public int Id { get; set; }

    public int StatsId { get; set; }

    public int Year { get; set; }

    public int Count { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Stat Stats { get; set; } = null!;
}
