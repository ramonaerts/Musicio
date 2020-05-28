using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Musicio.Core;
using Musicio.Core.Messages;

namespace Musicio.Client.Playlist
{
    public class PlaylistService : IPlaylistService
    {
        private readonly HttpClient _httpClient;
        private const string Path = "api/playlist";
        public PlaylistService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreatePlaylist(string name, string description, string image, string fileExtension)
        {
            var result = await _httpClient.PostJsonAsync<ApiResult>(Path, new PlaylistCreationMessage(name, description, image, fileExtension));

            return result.IsSuccess;
        }

        public async Task<List<Core.Models.Playlist>> GetUserPlaylists()
        {
            var result = await _httpClient.GetJsonAsync<ApiResult>(Path + "/@me");

            return result.GetData<List<Core.Models.Playlist>>();
        }

        public async Task<Core.Models.Playlist> GetPlaylistSongs(int playlistId)
        {
            var result = await _httpClient.GetJsonAsync<ApiResult>(Path + "/" + playlistId + "/songs");

            return result.GetData<Core.Models.Playlist>();
        }

        public async Task<List<Core.Models.Playlist>> GetUserPlaylistNames()
        {
            var result = await _httpClient.GetJsonAsync<ApiResult>(Path + "/@me/names");

            return result.GetData<List<Core.Models.Playlist>>();
        }

        public async Task<bool> AddSongToPlaylist(int playlistId, int songId)
        {
            var result = await _httpClient.PostJsonAsync<ApiResult>(Path + "/" + playlistId + "/songs/" + songId, new
            {
                playlistId,
                songId
            });

            return result.GetData<bool>();
        }
    }
}
