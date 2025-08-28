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
    public class LanguagesRepository : ILanguagesRepository
    {
        private readonly GithubInsightContext _context;

        public LanguagesRepository(GithubInsightContext context)
        {
            _context = context;
        }

        public async Task<List<LanguageStat>> GetAllLanguagesByUser(string userLogin)
        {
            return await _context.LanguageStats
                .Where(ls => ls.Stats.User.Login == userLogin)
                .OrderByDescending(tr => tr.Count)
                .ToListAsync();
        }
    }
}
