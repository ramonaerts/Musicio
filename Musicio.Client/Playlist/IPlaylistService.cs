﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Musicio.Client.Playlist
{
    public interface IPlaylistService
    {
        Task<bool> CreatePlaylist(string name, string description, string image, string fileExtension);
        Task<List<Core.Models.Playlist>> GetUserPlaylists();
        Task<Core.Models.Playlist> GetPlaylistSongs(int playlistId);
        Task<List<Core.Models.Playlist>> GetUserPlaylistNames();
        Task<bool> AddSongToPlaylist(int playlistId, int songId);
        Task<bool> RemovePlaylistSong(int playlistId, int playlistSongId);
    }
}
