using GithubInsight.Infrastructure.Entities;
using GithubInsight.Infrastructure.Repositories.Interfaces;
using GithubInsight.Infrastructure.Shared.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubInsight.Infrastructure.Repositories
{
    public class StatsRepository : IStatsRepository
    {
        private readonly GithubInsightContext _context;
        public StatsRepository(GithubInsightContext context)
        {
            _context = context;
        }

        public async Task AddStatsAsync(Stat stat)
        {   
            await _context.Stats.AddAsync(stat);
            await _context.SaveChangesAsync();
        }
    }
}
