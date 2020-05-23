using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Musicio.Core;

namespace Musicio.Client.Album
{
    public class AlbumService : IAlbumService
    {
        private readonly HttpClient _httpClient;
        private const string Path = "api/album";
        public AlbumService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Core.Models.Album> GetAlbumWithSongs(int albumId)
        {
            var result = await _httpClient.GetJsonAsync<ApiResult>(Path + "/" + albumId + "/songs");

            return result.GetData<Core.Models.Album>();
        }
    }
}
