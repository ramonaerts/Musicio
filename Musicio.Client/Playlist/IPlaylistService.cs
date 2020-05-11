using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Musicio.Client.Playlist
{
    public interface IPlaylistService
    {
        Task<bool> CreatePlaylist(string name, string description);
        Task<List<Core.Models.Playlist>> GetUserPlaylists(int userId);
    }
}
