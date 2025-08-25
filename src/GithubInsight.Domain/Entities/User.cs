using System;
using System.Collections.Generic;

namespace GithubInsight.Infrastructure.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Login { get; set; } = null!;

    public string? Name { get; set; }

    public string? Location { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Stat? Stat { get; set; }
}
