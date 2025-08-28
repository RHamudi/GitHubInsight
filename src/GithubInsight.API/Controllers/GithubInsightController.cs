using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GithubInsight.Application.Mappers;
using GithubInsight.Application.Receivers.LanguageReceiver;
using GithubInsight.Application.Receivers.StatsReceiver;
using GithubInsight.Application.Receivers.TopReposReceiver;
using GithubInsight.Application.Receivers.UserReceiver;
using Microsoft.AspNetCore.Mvc;

namespace GithubInsight.API.Controllers
{
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

        [HttpGet("sync-user/{username}")]
        public IActionResult SyncUser(string username)
        {
            try
            {
                var stateResponse = _insertReceiver.ExecuteAsync(username).Result;

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