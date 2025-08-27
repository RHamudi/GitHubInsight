using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GithubInsight.Application.Receivers.UserReceiver;
using Microsoft.AspNetCore.Mvc;

namespace GithubInsight.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GithubInsightController : ControllerBase
    {
        private readonly InsertUser _insertReceiver;

        public GithubInsightController(InsertUser insertReceiver)
        {
            _insertReceiver = insertReceiver;
        }

        [HttpGet("sync-user/{username}")]
        public IActionResult GetStatus(string username)
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
    }
}