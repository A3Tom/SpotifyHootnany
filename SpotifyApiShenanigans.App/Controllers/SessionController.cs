using Microsoft.AspNetCore.Mvc;
using SpotifyHootnanyServices.Interfaces;
using System.Threading.Tasks;

namespace SpotifyHootnany.App.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SessionController
    {
        private readonly IAuthService _urrrghhhhhh;
        public SessionController(IAuthService urrrghhhhhh)
        {
            _urrrghhhhhh = urrrghhhhhh;
        }

        public async Task<string> SlapMeUpANewYinBigBoy()
        {
            var result = await _urrrghhhhhh.ReturnAuthTokenString();

            return result;
        }
    }
}
