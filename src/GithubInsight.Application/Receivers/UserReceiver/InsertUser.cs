using GithubInsight.Application.DTO;
using GithubInsight.Application.Mappers;
using GithubInsight.Application.Receivers.RepoReceiver;
using GithubInsight.Application.Services.APIGithub.Interfaces;
using GithubInsight.Application.Services.JWT;
using GithubInsight.Domain.Entities;
using GithubInsight.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GithubInsight.Application.Receivers.UserReceiver
{
    public class InsertUser 
    {
        private readonly IGithubService _githubService;
        private readonly IUserRepository _userRepository;
        private readonly InsertStats _statsReceiver;
        private readonly TokenService _tokenService;

        public InsertUser(IGithubService githubService, IUserRepository userRepository, InsertStats statsReceiver, TokenService tokenService)
        {
            _githubService = githubService;
            _userRepository = userRepository;
            _statsReceiver = statsReceiver;
            _tokenService = tokenService;
        }

        public async Task<State> ExecuteAsync(string username)
        {
            var userExists = await _userRepository.UserExistsAsync(username);
            if (userExists)
                return new State(400, "Usuário já existe no banco de dados", null);

            try
            {
                var dto = await _githubService.GetUserAsync(username);
                var user = UserMapper.ToEntity(dto);
                var userInDb = await _userRepository.AddUserAsync(user);

                var stat = await _statsReceiver.ExecuteAsync(user.Login, userInDb.Id);

                var response = UserMapper.ToDto(userInDb, stat);

                return new State(200, "Usuário inserido com sucesso", response);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Erro de entrada: {ex.Message}");
                return new State(400, ex.Message, null);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro de comunicação: {ex.Message}");
                return new State(400, ex.Message, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                return new State(500, ex.Message, null);
            }
        }

        public async Task<State> LoginUser(string login)
        {
            try
            {
                var userExist = await _userRepository.UserExistsAsync(login);

                if (!userExist)
                    return new State(400, "Usuário não existe no banco de dados", null);

                var token = _tokenService.GenerateToken(login);
                return new State(200, "Usuario autenticado com sucesso", token);
            } catch(Exception ex)
            {
                return new State(500, "erro servidor", ex);
            }
            
        }
    }
}
