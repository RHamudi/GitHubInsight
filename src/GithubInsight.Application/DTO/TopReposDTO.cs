using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubInsight.Application.DTO
{
    internal class TopReposDTO
    {
        public string RepoName { get; set; } = null!;

        public int Stars { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
