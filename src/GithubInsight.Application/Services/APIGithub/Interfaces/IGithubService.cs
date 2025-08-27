using GithubInsight.Application.DTO;
using GithubInsight.Application.Receivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubInsight.Application.Services.APIGithub.Interfaces
{
    public interface IGithubService
    {
        Task<GitHubUserResponse> GetUserAsync(string username);
        Task<List<GithubUserRepoResponse>> GetReposUserAsync(string username);
    }
}
