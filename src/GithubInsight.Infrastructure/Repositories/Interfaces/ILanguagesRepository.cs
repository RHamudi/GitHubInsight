using GithubInsight.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubInsight.Infrastructure.Repositories.Interfaces
{
    public interface ILanguagesRepository
    {
        Task<List<LanguageStat>> GetAllLanguagesByUser(string userLogin);
    }
}
