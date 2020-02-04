using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpotifyHootnany.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpotifyHootnany.App.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController
    {
        private readonly ILogger _logger;
        private readonly IUserService _userService;

        public AccountController(ILogger<AccountController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService; 
        }

        public async Task<List<string>> GetName()
        {
            _logger.LogDebug("GetName Method hit");

            var result = await _userService.GetProfileDisplayName();

            return result;
        }
    }
}
