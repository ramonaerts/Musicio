using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Musicio.Core.Data;
using Musicio.Server.Services.Album;
using Musicio.Server.Services.FileManagement;

namespace Musicio.Server.Services.Artist
{
    public class ArtistService : IArtistService
    {
        private readonly IRepository<Core.Domain.Artist> _artistRepository;
        private readonly IFileManagementService _fileManagementService;
        private readonly IAlbumService _albumService;
        public ArtistService(IRepository<Core.Domain.Artist> artistRepository, IFileManagementService fileManagementService, IAlbumService albumService)
        {
            _artistRepository = artistRepository;
            _fileManagementService = fileManagementService;
            _albumService = albumService;
        }

        public Core.Domain.Artist GetArtistById(int artistId)
        {
            return _artistRepository.TableNoTracking.SingleOrDefault(e => e.Id == artistId);
        }

        public Core.Domain.Artist GetArtistWithAlbums(int artistId)
        {
            var artist = GetArtistById(artistId);

            artist.Albums = _albumService.GetArtistAlbums(artistId);

            return artist;
        }

        public List<Core.Domain.Artist> GetArtists()
        {
            return _artistRepository.TableNoTracking.ToList();
        }
    }
}
