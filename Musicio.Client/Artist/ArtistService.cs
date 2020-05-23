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

        public ArtistService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Core.Models.Artist>> GetArtists()
        {
            var result = await _httpClient.GetJsonAsync<ApiResult>("api/Artist");

            return result.GetData<List<Core.Models.Artist>>();
        }
    }
}
