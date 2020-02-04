using System.Threading.Tasks;

namespace SpotifyHootnanyServices.Interfaces
{
    public interface IAuthService
    {
        Task<string> ReturnAuthTokenString();
    }
}
