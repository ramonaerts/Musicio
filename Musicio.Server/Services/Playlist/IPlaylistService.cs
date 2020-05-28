using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musicio.Core.Messages;

namespace Musicio.Server.Services.Playlist
{
    public interface IPlaylistService
    {
        bool CreatePlaylist(PlaylistCreationMessage message, int userId);
        List<Core.Domain.Playlist> GetUserPlaylists(int userId);
        Core.Domain.Playlist GetPlaylistById(int playlistId);
        Core.Domain.Playlist GetPlaylistSongs(int playlistId);
        List<Core.Domain.Playlist> GetPlaylistNameAndId(int userId);
        bool PlaylistExists(int playlistId);
        void AddSongToPlaylist(int playlistId, int songId);
        int GetSongCountInPlaylist(int playlistId);
        int GetPlaylistCount(int userId);
    }
}
