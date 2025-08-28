using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubInsight.Application.DTO
{
    internal class LanguageDTO
    {
        public string Language { get; set; } = null!;

        public int Count { get; set; }

        public DateTime? CreatedAt { get; set; }
    }
}
