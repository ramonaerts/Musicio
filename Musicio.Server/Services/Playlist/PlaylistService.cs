using Musicio.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musicio.Core.Data;

namespace Musicio.Server.Services.Playlist
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IRepository<Core.Domain.Playlist> _playlistRepository;
        public PlaylistService(IRepository<Core.Domain.Playlist> playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        public async Task<bool> CreatePlaylist(PlaylistCreationMessage message)
        {
            var newPlaylist = new Core.Domain.Playlist()
            {
                Title = message.Name,
                Description = message.Description,
                CreationDate = DateTime.Now,
                UserId = 1
            };

            _playlistRepository.Insert(newPlaylist);

            return true;
        }
    }
}
