using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musicio.Server.Services.Album
{
    public interface IAlbumService
    {
        List<Core.Domain.Album> GetArtistAlbums(int artistId);
        Core.Domain.Album GetAlbumWithSongs(int albumId);
    }
}
