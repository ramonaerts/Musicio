using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Musicio.Core;
using Musicio.Core.Domain;
using Musicio.Core.Enums;
using Musicio.Server.Services.Artist;
using Musicio.Server.Services.FileManagement;
using Album = Musicio.Core.Models.Album;

namespace Musicio.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IFileManagementService _fileManagementService;
        private readonly IMapper _mapper;
        public ArtistController(IArtistService artistService ,IMapper mapper, IFileManagementService fileManagementService)
        {
            _artistService = artistService;
            _fileManagementService = fileManagementService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public ApiResult GetArtists()
        {
            List<Artist> artistEntities = _artistService.GetArtists();

            var artistModels = _mapper.Map<List<Core.Models.Artist>>(artistEntities);

            foreach (var a in artistModels)
            {
                if (a.Image != null) a.Image = _fileManagementService.CreateBase64String(a.Image, ImageType.Artist);
            }

            return ApiResult.Success(artistModels);
        }

        [HttpGet]
        [Route("{artistId}")]
        public ApiResult GetArtistWithAlbums(int artistId)
        {
            Artist artist = _artistService.GetArtistWithAlbums(artistId);

            var artistModel = _mapper.Map<Core.Models.Artist>(artist);

            artistModel.Albums = new List<Album>();
            foreach (var album in artist.Albums)
            {
                if (album.Image != null) album.Image = _fileManagementService.CreateBase64String(album.Image, ImageType.Album);
                var albumModel = _mapper.Map<Album>(album);
                artistModel.Albums.Add(albumModel);
            }

            if (artistModel.Image != null) artistModel.Image = _fileManagementService.CreateBase64String(artistModel.Image, ImageType.Artist);

            return ApiResult.Success(artistModel);
        }
    }
}
