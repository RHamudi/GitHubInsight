using GithubInsight.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubInsight.Infrastructure.Repositories.Interfaces
{
    public interface IStatsRepository
    {
        Task AddStatsAsync(Stat stat);
        Task<Stat> GetStatByUserAsync(string userLogin);
    }
}
