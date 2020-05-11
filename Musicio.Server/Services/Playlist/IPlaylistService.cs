using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musicio.Core.Messages;

namespace Musicio.Server.Services.Playlist
{
    public interface IPlaylistService
    {
        Task<bool> CreatePlaylist(PlaylistCreationMessage message);
    }
}
