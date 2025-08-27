using GithubInsight.Application.DTO;
using GithubInsight.Application.Mappers;
using GithubInsight.Application.Services.APIGithub.Interfaces;
using GithubInsight.Application.Services.Stats;
using GithubInsight.Infrastructure.Entities;
using GithubInsight.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubInsight.Application.Receivers.RepoReceiver
{
    public class InsertStats
    {
        private readonly IGithubService _githubService;
        private readonly IStatsRepository _statsRepository;
        public InsertStats(IGithubService githubService, IStatsRepository statsRepository)
        {
            _githubService = githubService;
            _statsRepository = statsRepository;
        }

        public async Task<Stat> ExecuteAsync(string username, int userId)
        {
            var repos = await _githubService.GetReposUserAsync(username);
            if (repos == null || !repos.Any())
                throw new ArgumentException("Nenhum repositório encontrado para o usuário.");

            var stat = StatsProcessor.Process(repos, userId);

            await _statsRepository.AddStatsAsync(stat);

            return stat;
        }
    }
}
