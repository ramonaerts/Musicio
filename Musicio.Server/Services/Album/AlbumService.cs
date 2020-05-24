using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Musicio.Core.Data;
using Musicio.Server.Services.FileManagement;

namespace Musicio.Server.Services.Album
{
    public class AlbumService : IAlbumService
    {
        private readonly IRepository<Core.Domain.Album> _albumRepository;
        private readonly IFileManagementService _fileManagementService;
        public AlbumService(IRepository<Core.Domain.Album> albumRepository, IFileManagementService fileManagementService)
        {
            _albumRepository = albumRepository;
            _fileManagementService = fileManagementService;
        }

        public List<Core.Domain.Album> GetArtistAlbums(int artistId)
        {
            return _albumRepository.TableNoTracking.Where(e => e.ArtistId == artistId).ToList();
        }

        public Core.Domain.Album GetAlbumWithSongs(int albumId)
        {
            return _albumRepository.TableNoTracking.Include(e => e.AlbumSongs).ThenInclude(e => e.Song)
                .SingleOrDefault(e => e.Id == albumId);
        }
    }
}
