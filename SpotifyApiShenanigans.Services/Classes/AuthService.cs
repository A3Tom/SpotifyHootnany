using System;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web.Enums;
using SpotifyAPI.Web.Models;
using SpotifyHootnanyServices.Interfaces;

namespace SpotifyHootnanyServices.Classes
{
    public class AuthService : IAuthService
    {
        public AuthService()
        {

        }

        public async Task<string> ReturnAuthTokenString()
        {
            return Authorize();
        }

        private static string _clientId = "887ce5f05bac41869572d37f2c12206b"; //"";
        private static string _secretId = "7acfa9a55f4a46d68cf02f3548828216"; //"";

        // ReSharper disable once UnusedParameter.Local
        public string Authorize()
        {
            _clientId = string.IsNullOrEmpty(_clientId)
                ? Environment.GetEnvironmentVariable("SPOTIFY_CLIENT_ID")
                : _clientId;

            _secretId = string.IsNullOrEmpty(_secretId)
                ? Environment.GetEnvironmentVariable("SPOTIFY_SECRET_ID")
                : _secretId;

            Console.WriteLine("####### Spotify API Example #######");
            Console.WriteLine("This example uses AuthorizationCodeAuth.");
            Console.WriteLine(
                "Tip: If you want to supply your ClientID and SecretId beforehand, use env variables (SPOTIFY_CLIENT_ID and SPOTIFY_SECRET_ID)");

            ImplicitGrantAuth auth =
                new ImplicitGrantAuth(_clientId, "http://localhost:50426/callback/auth", "http://localhost:50426/callback/auth", Scope.UserReadPrivate);
            auth.AuthReceived += async (sender, payload) =>
            {
                auth.Stop(); // `sender` is also the auth instance
                SpotifyWebAPI api = new SpotifyWebAPI() { TokenType = payload.TokenType, AccessToken = payload.AccessToken };
                // Do requests with API client
            };
            auth.Start(); // Starts an internal HTTP Server
            auth.OpenBrowser();

            Console.ReadLine();
            auth.Stop(0);

            return "awd";
        }

        private static async void AuthOnAuthReceived(object sender, AuthorizationCode payload)
        {
            AuthorizationCodeAuth auth = (AuthorizationCodeAuth)sender;
            auth.Stop();

            Token token = await auth.ExchangeCode(payload.Code);
            SpotifyWebAPI api = new SpotifyWebAPI
            {
                AccessToken = token.AccessToken,
                TokenType = token.TokenType
            };
            PrintUsefulData(api);
        }

        private static async void PrintUsefulData(SpotifyWebAPI api)
        {
            PrivateProfile profile = await api.GetPrivateProfileAsync();
            string name = string.IsNullOrEmpty(profile.DisplayName) ? profile.Id : profile.DisplayName;
            Console.WriteLine($"Hello there, {name}!");

            Console.WriteLine("Your playlists:");
            Paging<SimplePlaylist> playlists = await api.GetUserPlaylistsAsync(profile.Id);
            do
            {
                playlists.Items.ForEach(playlist =>
                {
                    Console.WriteLine($"- {playlist.Name}");
                });
                playlists = await api.GetNextPageAsync(playlists);
            } while (playlists.HasNextPage());


        }

    }
}
