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
    public class UserRepository : IUserRepository
    {
        private readonly GithubInsightContext _context;
        public UserRepository(GithubInsightContext context)
        {
            _context = context;
        }
        public async Task<User> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _context.Users.AnyAsync(u => u.Login == username);
        }
    }
}
