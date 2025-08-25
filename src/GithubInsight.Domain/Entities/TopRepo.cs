using System;
using System.Collections.Generic;

namespace GithubInsight.Infrastructure.Entities;

public partial class TopRepo
{
    public int Id { get; set; }

    public int StatsId { get; set; }

    public string RepoName { get; set; } = null!;

    public int Stars { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Stat Stats { get; set; } = null!;
}
