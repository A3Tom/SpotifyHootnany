using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyHootnany.App.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CallbackController : Controller
    {
        private readonly ILogger _logger;

        public CallbackController(ILogger<CallbackController> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //public void Auth([FromBody] dynamic payload) // access_token, string token_type, string expires_in, string state)
        //{
        //    HttpRequest httpRequest;

        //    SpotifyWebAPI api = new SpotifyWebAPI() { TokenType = payload.TokenType, AccessToken = payload.AccessToken };
        //    // Do requests with API client


        //    //_logger.LogInformation($"{access_token} | {token_type} | {expires_in} | {state}");
        //}


        [HttpGet]
        public void Auth(string access_token, string token_type, string expires_in, string state) // access_token, string token_type, string expires_in, string state)
        {
            HttpRequest httpRequest;

            //SpotifyWebAPI api = new SpotifyWebAPI() { TokenType = payload.TokenType, AccessToken = payload.AccessToken };

            SpotifyWebAPI webApi = new SpotifyWebAPI() { AccessToken = access_token, TokenType = token_type };
            // Do requests with API client


            //_logger.LogInformation($"{access_token} | {token_type} | {expires_in} | {state}");
        }

    }
}
