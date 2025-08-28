using GithubInsight.Application.Mappers;
using GithubInsight.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GithubInsight.Application.Receivers.LanguageReceiver
{
    public class ReadLanguage
    {
        private readonly ILanguagesRepository _languagesRepository;

        public ReadLanguage(ILanguagesRepository languagesRepository)
        {
            _languagesRepository = languagesRepository;
        }

        public async Task<State> ExecuteAsync(string userLogin)
        {
            var languages = await _languagesRepository.GetAllLanguagesByUser(userLogin);

            var response = LanguageMapper.ToDto(languages);

            if (languages == null)
            {
                return new State(404, "Nenhuma Linguagem encontrada para o usuário especificado", null);
            }

            return new State(200, "Linguagens recuperadas com sucesso", response);
        }
    }
}
