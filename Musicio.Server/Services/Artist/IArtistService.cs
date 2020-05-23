using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musicio.Server.Services.Artist
{
    public interface IArtistService
    {
        List<Core.Domain.Artist> GetArtists();
        Core.Domain.Artist GetArtistById(int artistId);
        Core.Domain.Artist GetArtistWithAlbums(int artistId);
    }
}
