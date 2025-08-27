using GithubInsight.Application.DTO;
using GithubInsight.Application.Receivers;
using GithubInsight.Application.Services.APIGithub.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GithubInsight.Application.Services.APIGithub
{
    public class GithubService : IGithubService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GithubService> _logger;

        public GithubService(HttpClient httpClient, ILogger<GithubService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;

            // Configurações padrão para a API do GitHub
            _httpClient.BaseAddress = new Uri("https://api.github.com/");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "GithubInsight");
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
        }

        public async Task<List<GithubUserRepoResponse>> GetReposUserAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                _logger.LogWarning("Tentativa de busca com nome de usuário vazio ou nulo.");
                throw new ArgumentException("O nome de usuário não pode ser vazio ou nulo.", nameof(username));
            }

            try
            {
                var allRepos = new List<GithubUserRepoResponse>();
                int page = 1;
                const int perPage = 100;

                while (true)
                {
                    _logger.LogInformation($"Iniciando requisição para os repositórios do usuário: {username}");
                    var response = await _httpClient.GetAsync($"users/{username}/repos?per_page={perPage}&page={page}");

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogWarning($"Falha na requisição para os repositórios do usuário {username}. StatusCode: {response.StatusCode}");
                        throw new HttpRequestException($"Falha na requisição para os repositórios do usuário {username}. StatusCode: {response.StatusCode}");
                    }

                    var json = await response.Content.ReadAsStringAsync();
                    var pages = JsonSerializer.Deserialize<List<GithubUserRepoResponse>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                        ReferenceHandler = ReferenceHandler.Preserve
                    });

                    if (pages == null || pages.Count == 0)
                        break;

                    allRepos.AddRange(pages);

                    page++;
                }
                
                //var content = await response.Content.ReadAsStringAsync();
                //var repos = JsonSerializer.Deserialize<List<GithubUserRepoResponse>>(content, new JsonSerializerOptions
                //{
                //    PropertyNameCaseInsensitive = true,
                //    ReferenceHandler = ReferenceHandler.Preserve
                //});

                //if (repos == null || !repos.Any())
                //{
                //    _logger.LogWarning($"Nenhum repositório encontrado para o usuário {username}.");
                //    return new List<GithubUserRepoResponse>(); // Retorna uma lista vazia em vez de lançar exceção
                //}

                _logger.LogInformation($"Repositórios do usuário {username} coletados com sucesso. Total: {allRepos.Count}");
                return allRepos;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, $"Erro ao se comunicar com a API do GitHub para o usuário {username}.");
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, $"Erro ao desserializar a resposta da API para os repositórios do usuário {username}.");
                throw new InvalidOperationException("Erro ao processar a resposta da API.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro inesperado ao buscar os repositórios do usuário {username}.");
                throw new ApplicationException("Erro interno ao buscar os repositórios do usuário.", ex);
            }
        }

        public async Task<GitHubUserResponse> GetUserAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                _logger.LogWarning("Tentativa de busca com nome de usuário vazio ou nulo.");
                throw new ArgumentException("O nome de usuário não pode ser vazio ou nulo.", nameof(username));
            }

            try
            {
                _logger.LogInformation($"Iniciando requisição para o usuário: {username}");
                var response = await _httpClient.GetAsync($"users/{username}");

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning($"Falha na requisição para o usuário {username}. StatusCode: {response.StatusCode}");
                    throw new HttpRequestException($"Falha na requisição para o usuário {username}. StatusCode: {response.StatusCode}");
                }

                var content = await response.Content.ReadAsStringAsync();
                var user = JsonSerializer.Deserialize<GitHubUserResponse>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (user == null)
                {
                    _logger.LogWarning($"Usuário {username} não encontrado.");
                    throw new InvalidOperationException($"Usuário {username} não encontrado.");
                }

                _logger.LogInformation($"Usuário {username} coletado com sucesso.");
                return user;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Erro ao se comunicar com a API do GitHub.");
                throw;
            }
            catch (JsonException ex)
            {
                _logger.LogError(ex, "Erro ao desserializar a resposta da API.");
                throw new InvalidOperationException("Erro ao processar a resposta da API.", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro interno do servidor.");
                throw new ApplicationException("Erro interno ao buscar o usuário.", ex);
            }
        }
    }
}
