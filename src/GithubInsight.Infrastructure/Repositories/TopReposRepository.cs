using GithubInsight.Domain.Entities;
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
    public class TopReposRepository : ITopReposRepository
    {
        private readonly GithubInsightContext _context;
        public TopReposRepository(GithubInsightContext context)
        {
            _context = context;
        }

        public async Task<List<TopRepo>> GetTopReposAsync(string userLogin)
        {
            return await _context.TopRepos
                .Where(tr => tr.Stats.User.Login == userLogin)
                .OrderByDescending(tr => tr.Stars)
                .ToListAsync();
        }
    }
}
