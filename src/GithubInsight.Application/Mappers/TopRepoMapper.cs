using GithubInsight.Application.DTO;
using GithubInsight.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubInsight.Application.Mappers
{
    internal class TopRepoMapper
    {
        public static List<TopReposDTO> ToDto(List<TopRepo> entity)
        {
            return entity.Select(topRepo => new TopReposDTO
            {
                CreatedAt = topRepo.CreatedAt,
                RepoName = topRepo.RepoName,
                Stars = topRepo.Stars,
            }).ToList();
        }
    }
}
