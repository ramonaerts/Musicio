using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Musicio.Core;

namespace Musicio.Client.Artist
{
    public class ArtistService : IArtistService
    {
        private readonly HttpClient _httpClient;
        private const string Path = "api/artist";
        public ArtistService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Core.Models.Artist>> GetArtists()
        {
            var result = await _httpClient.GetJsonAsync<ApiResult>(Path);

            return result.GetData<List<Core.Models.Artist>>();
        }

        public async Task<Core.Models.Artist> GetArtistById(int artistId)
        {
            var result = await _httpClient.GetJsonAsync<ApiResult>(Path + "/" + artistId);

            return result.GetData<Core.Models.Artist>();
        }
    }
}
