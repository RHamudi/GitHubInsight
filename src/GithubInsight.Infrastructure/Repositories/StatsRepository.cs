using GithubInsight.Infrastructure.Entities;
using GithubInsight.Infrastructure.Repositories.Interfaces;
using GithubInsight.Infrastructure.Shared.Context;
using Microsoft.EntityFrameworkCore;
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

        public async Task<Stat> GetStatByUserAsync(string userLogin)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Login == userLogin) ?? throw new Exception("User not found");
            var stat = await _context.Stats
                .Include(s => s.LanguageStats)
                .Include(s => s.ReposCreatedPerYears)
                .Include(s => s.TopRepos)
                .FirstOrDefaultAsync(s => s.UserId == user.Id);

            return stat;
        }
    }
}
