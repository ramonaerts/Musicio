using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musicio.Core.Data;
using Musicio.Server.Services.FileManagement;

namespace Musicio.Server.Services.Artist
{
    public class ArtistService : IArtistService
    {
        private readonly IRepository<Core.Domain.Artist> _artistRepository;
        private readonly IFileManagementService _fileManagementService;
        public ArtistService(IRepository<Core.Domain.Artist> artistRepository, IFileManagementService fileManagementService)
        {
            _artistRepository = artistRepository;
            _fileManagementService = fileManagementService;
        }

        public Core.Domain.Artist GetArtistById(int artistId)
        {
            return _artistRepository.Table.SingleOrDefault(e => e.Id == artistId);
        }

        public List<Core.Domain.Artist> GetArtists()
        {
            return _artistRepository.TableNoTracking.ToList();
        }
    }
}
