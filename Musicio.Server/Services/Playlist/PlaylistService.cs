using Musicio.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Musicio.Core.Data;
using Musicio.Server.Services.FileManagement;

namespace Musicio.Server.Services.Playlist
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IRepository<Core.Domain.Playlist> _playlistRepository;
        private readonly IFileManagementService _fileManagementService;
        public PlaylistService(IRepository<Core.Domain.Playlist> playlistRepository, IFileManagementService fileManagementService)
        {
            _playlistRepository = playlistRepository;
            _fileManagementService = fileManagementService;
        }

        public async Task<bool> CreatePlaylist(PlaylistCreationMessage message)
        {
            string imageName = null;
            if (message.Image != null)
            {
                imageName = _fileManagementService.SavePlaylistImage(message.Image, message.FileExtension);
            }

            var newPlaylist = new Core.Domain.Playlist()
            {
                Title = message.Name,
                Description = message.Description,
                CreationDate = DateTime.Now,
                UserId = 1,
                Image = imageName
            };

            _playlistRepository.Insert(newPlaylist);

            return true;
        }

        public async Task<List<Core.Domain.Playlist>> GetUserPlaylists(int userId)
        {
            return _playlistRepository.Table.Where(e => e.UserId == userId).ToList();
        }

        public Core.Domain.Playlist GetPlaylistSongs(int playlistId)
        {
            return _playlistRepository.Table.Include(e => e.PlaylistSongs).ThenInclude(e => e.Song)
                .SingleOrDefault(e => e.Id == playlistId);
        }

        public Core.Domain.Playlist GetPlaylistById(int playlistId)
        {
            return _playlistRepository.Table.SingleOrDefault(e => e.Id == playlistId);
        }

        public List<Core.Domain.Playlist> GetPlaylistNameAndId(int userId)
        {
            return _playlistRepository.Table.Where(e => e.UserId == userId).Select(p => new Core.Domain.Playlist
            {
                Id = p.Id, 
                Title = p.Title
            }).ToList();
        }
    }
}
