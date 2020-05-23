using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Musicio.Client.Album
{
    public interface IAlbumService
    {
        Task<Core.Models.Album> GetAlbumWithSongs(int albumId);
    }
}
