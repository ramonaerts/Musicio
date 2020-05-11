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

        public PlaylistService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreatePlaylist(string name, string description)
        {
            var result = await _httpClient.PostJsonAsync<ApiResult>("api/Playlist/create", new PlaylistCreationMessage(name, description));

            return result.IsSuccess;
        }
    }
}
