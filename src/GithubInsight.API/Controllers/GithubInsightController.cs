using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GithubInsight.Application.DTO;
using GithubInsight.Application.Mappers;
using GithubInsight.Application.Receivers.LanguageReceiver;
using GithubInsight.Application.Receivers.StatsReceiver;
using GithubInsight.Application.Receivers.TopReposReceiver;
using GithubInsight.Application.Receivers.UserReceiver;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GithubInsight.API.Controllers
{
    /// <summary>
    /// Controller para opera��es de autentica��o
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class GithubInsightController : ControllerBase
    {
        private readonly InsertUser _insertReceiver;
        private readonly ReadStats _readStatsReceiver;
        private readonly ReadTopRepos _readTopReposReceiver;
        private readonly ReadLanguage _readLanguage;

        public GithubInsightController(InsertUser insertReceiver,
            ReadStats readStatsReceiver,
            ReadTopRepos readTopRepos,
            ReadLanguage readLanguage)
        {
            _insertReceiver = insertReceiver;
            _readStatsReceiver = readStatsReceiver;
            _readTopReposReceiver = readTopRepos;
            _readLanguage = readLanguage;
        }

        /// <summary>
        /// Obtem o token de authentica��o para utilizar a API
        /// </summary>
        /// <param name="username">Nome de usu�rio do GitHub</param>
        /// <returns>Token para utilizar na authentica��o da API</returns>
        /// <response code="200">Estat�sticas retornadas com sucesso</response>
        /// <response code="404">Usu�rio n�o encontrado</response>
        /// <response code="401">N�o autorizado - token inv�lido</response>
        [HttpPost("AuthUser/{username}")]
        public async Task<IActionResult> LoginUser(string username)
        {
            try
            {
                var authUser = await _insertReceiver.LoginUser(username);
                if (authUser.StatusCode != 200)
                    return BadRequest(authUser);

                return Ok(authUser);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Obtem Usuario do github e atualiza o banco de dados
        /// </summary>
        /// <param name="username">Nome de usu�rio do GitHub</param>
        /// <returns>Todos os dados processados do usuario</returns>
        /// <response code="200">Estat�sticas retornadas com sucesso</response>
        /// <response code="404">Usu�rio n�o encontrado no GitHub</response>
        /// <response code="401">N�o autorizado - token inv�lido</response>
        [Authorize]
        [Authorize]
        [HttpGet("sync-user/{username}")]
        public async Task<IActionResult> SyncUser(string username)
        {
            try
            {
                var stateResponse = await _insertReceiver.ExecuteAsync(username);

                if (stateResponse.StatusCode == 200)
                {
                    return Ok(new
                    {
                        message = stateResponse.Message,
                        user = stateResponse.Data
                    });
                }

                return StatusCode(stateResponse.StatusCode, new
                {
                    message = stateResponse.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// Obt�m estat�sticas de um usu�rio do GitHub
        /// </summary>
        /// <param name="username">Nome de usu�rio do GitHub</param>
        /// <returns>Estat�sticas e m�tricas do usu�rio</returns>
        /// <response code="200">Estat�sticas retornadas com sucesso</response>
        /// <response code="404">Usu�rio n�o encontrado</response>
        /// <response code="401">N�o autorizado - token inv�lido</response>
        [Authorize]
        [HttpGet("stats/{username}")]
        public IActionResult GetStat(string username)
        {
            try
            {
                var stat = _readStatsReceiver.ExecuteAsync(username).Result;

                if (stat.StatusCode == 200)
                {
                    return Ok(new
                    {
                        message = stat.Message,
                        stats = stat.Data
                    });
                }

                return StatusCode(stat.StatusCode, new
                {
                    message = stat.Message,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// Obtem os 5 reposit�rios mais populares de um usu�rio do GitHub
        /// </summary>
        /// <param name="username">Nome de usu�rio do GitHub</param>
        /// <returns>Repositorios e suas quantidades de estrela</returns>
        /// <response code="200">Repositorios retornados com sucesso</response>
        /// <response code="404">Usu�rio n�o encontrado</response>
        /// <response code="401">N�o autorizado - token inv�lido</response>
        [Authorize]
        [HttpGet("TopRepos/{username}")]
        public IActionResult GetTopRepos(string username)
        {
            try
            {
                var topRepos = _readTopReposReceiver.ExecuteAsync(username).Result;

                if (topRepos.StatusCode == 200)
                {
                    return Ok(topRepos);

                }

                return StatusCode(topRepos.StatusCode, new
                {
                    message = topRepos.Message,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        /// <summary>
        /// Obtem as linguagens mais utilizadas de um usu�rio do GitHub
        /// </summary>
        /// <param name="username">Nome de usu�rio do GitHub</param>
        /// <returns>Retorna a linguagem e a quantidade de repositorios com essa linguagem</returns>
        /// <response code="200">Quantidade de linguagem retornados com sucesso</response>
        /// <response code="404">Usu�rio n�o encontrado</response>
        /// <response code="401">N�o autorizado - token inv�lido</response>
        [Authorize]
        [HttpGet("Languages/{username}")]
        public IActionResult GetLanguages(string username)
        {
            try
            {
                var languages = _readLanguage.ExecuteAsync(username).Result;

                if (languages.StatusCode == 200)
                {
                    return Ok(languages);
                }

                return StatusCode(languages.StatusCode, new
                {
                    message = languages.Message,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }
    }
}