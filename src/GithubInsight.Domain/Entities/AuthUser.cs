﻿using System;
using System.Collections.Generic;

namespace GithubInsight.Domain.Entities;

public partial class AuthUser
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
