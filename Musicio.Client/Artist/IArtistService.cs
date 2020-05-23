using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Musicio.Client.Artist
{
    public interface IArtistService
    {
        Task<List<Core.Models.Artist>> GetArtists();
    }
}
