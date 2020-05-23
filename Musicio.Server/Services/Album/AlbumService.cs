using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            return _albumRepository.Table.Where(e => e.ArtistId == artistId).ToList();
        }
    }
}
