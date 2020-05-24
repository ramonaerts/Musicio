using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Musicio.Client.Playlist
{
    public interface IPlaylistService
    {
        Task<bool> CreatePlaylist(string name, string description, string image, string fileExtension);
        Task<List<Core.Models.Playlist>> GetUserPlaylists(int userId);
        Task<Core.Models.Playlist> GetPlaylistSongs(int playlistId);
        Task<List<Core.Models.Playlist>> GetUserPlaylistNames(int userId);
        Task<bool> AddSongToPlaylist(int playlistId, int songId);
    }
}
