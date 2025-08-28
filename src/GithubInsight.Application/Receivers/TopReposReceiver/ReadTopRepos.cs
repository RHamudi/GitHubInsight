using GithubInsight.Application.Mappers;
using GithubInsight.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubInsight.Application.Receivers.TopReposReceiver
{
    public class ReadTopRepos
    { 
        private readonly ITopReposRepository _topReposRepository;

        public ReadTopRepos(ITopReposRepository topReposRepository)
        {
            _topReposRepository = topReposRepository;
        }

        public async Task<State> ExecuteAsync(string userLogin)
        {
            try
            {
                var topRepos = TopRepoMapper.ToDto(await _topReposRepository.GetTopReposAsync(userLogin));


                if (topRepos == null || !topRepos.Any())
                    return new State(404, "Top repositórios não encontrados para o usuário especificado", null);

                return new State(200, "Top repositórios recuperados com sucesso", topRepos);
            }
            catch (Exception ex)
            {
                return new State(500, ex.Message, ex);
            }

        }
    }
}
