using GithubInsight.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubInsight.Application.DTO
{
    internal class UserDTO
    {
        public string Login { get; set; } = null!;

        public string? Name { get; set; }

        public string? Location { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public virtual StatDto? Stat { get; set; }
    }
}
