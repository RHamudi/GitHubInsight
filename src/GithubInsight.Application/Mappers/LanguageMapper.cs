using GithubInsight.Application.DTO;
using GithubInsight.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubInsight.Application.Mappers
{
    internal class LanguageMapper
    {
        public static List<LanguageDTO> ToDto(List<LanguageStat> languages)
        {
            return languages.Select(lang => new LanguageDTO
            {
                Count = lang.Count,
                CreatedAt = lang.CreatedAt,
                Language = lang.Language
            }).ToList();
        }
    }
}
