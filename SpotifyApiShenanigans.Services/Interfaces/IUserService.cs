using System.Collections.Generic;
using System.Threading.Tasks;

namespace SpotifyHootnany.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<string>> GetProfileDisplayName();
    }
}
