using SpotifyAPI.Web;
using SpotifyAPI.Web.Models;
using SpotifyHootnany.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyHootnany.Services.Classes
{
    public class UserService : IUserService
    {
        const string TOKEN_TYPE = "Bearer";
        const string ACCESS_TOKEN = "BQBQPR1928TnRNO-6mGo14q8_s7iJ0qKU_YNiv4M0JUyB4x5xCiMvj2lXvJPre-nL_21yUneC41pgBB68sIrEy6evzAvtRe3hZE8PNN8Nc4X6ydM13TsJHifGv1h_ZzQBZLmceiFhHYPsTOhLC5GlgvCIsdMTScDbmf9Hul3IWWCD000PV1TNR8XQuJBpK6YcmMuzw";

        private SpotifyWebAPI _spotifyAPI;

        public UserService()
        {
            SpotifyWebBuilder test = new SpotifyWebBuilder();

            _spotifyAPI = new SpotifyWebAPI
            {
                AccessToken = ACCESS_TOKEN,
                TokenType = TOKEN_TYPE
            };
        }

        public async Task<List<string>> GetProfileDisplayName()
        {
            var result = new List<string>();

            PrivateProfile profile = await _spotifyAPI.GetPrivateProfileAsync();

            result.Add($"Account Display Name : {profile.DisplayName}");

            var tet = await GetFrigateTrackBreakdownByUser();

            return result;
        }

        public async Task<Dictionary<string, int>> GetFrigateTrackBreakdownByUser()
        {
            PrivateProfile profile = await _spotifyAPI.GetPrivateProfileAsync();

            var Playlists = await _spotifyAPI.GetUserPlaylistsAsync(profile.Id, 250, 0);

            var frigateFMID = Playlists.Items.SingleOrDefault(p => p.Name == "Frigate FM" & p.Collaborative).Id;

            var frigateTracks = await _spotifyAPI.GetPlaylistTracksAsync(frigateFMID, "", 1, 0, "");

            List<Task<Paging<PlaylistTrack>>> tasks = new List<Task<Paging<PlaylistTrack>>>();

            for (int i = 0; i < frigateTracks.Total + 1; i += 100)
            {
                tasks.Add(_spotifyAPI.GetPlaylistTracksAsync(frigateFMID, "", i + 100, i, ""));
            }

            var hings = await Task.WhenAll(tasks);

            var testing = hings.GroupBy(g => g.Items.Select(i => i.AddedBy.DisplayName));

            var frigateFMTest = await _spotifyAPI.GetPlaylistTracksAsync(frigateFMID, "", 1000, 0, "");

            return new Dictionary<string, int>();
        }
    }
}
