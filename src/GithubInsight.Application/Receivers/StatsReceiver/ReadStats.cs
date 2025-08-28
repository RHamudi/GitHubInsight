using GithubInsight.Application.Mappers;
using GithubInsight.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubInsight.Application.Receivers.StatsReceiver
{
    public class ReadStats 
    {
        private readonly IStatsRepository _statsRepository;

        public ReadStats(IStatsRepository statsRepository)
        {
            _statsRepository = statsRepository;
        }

        public async Task<State> ExecuteAsync(string userLogin)
        {
            try
            {
                var stat = await _statsRepository.GetStatByUserAsync(userLogin);

                var response = StatsMapper.ToDto(stat);

                if (stat == null)
                    return new State(404, "Estatísticas não encontradas para o usuário especificado", null);

                return new State(200, "Estatísticas recuperadas com sucesso", response);
            }
            catch (Exception ex)
            {
                return new State(500, ex.Message, ex);
            }

        }
    }
}
