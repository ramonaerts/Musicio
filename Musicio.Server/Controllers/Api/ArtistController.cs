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

namespace Musicio.Server.Controllers.Api
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistService _artistService;
        private readonly IMapper _mapper;
        public ArtistController(IArtistService artistService ,IMapper mapper)
        {
            _artistService = artistService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public ApiResult GetArtists()
        {
            List<Artist> artistEntities = _artistService.GetArtists();

            var artistModels = _mapper.Map<List<Core.Models.Artist>>(artistEntities);

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
                var albumModel = _mapper.Map<Album>(album);
                artistModel.Albums.Add(albumModel);
            }

            return ApiResult.Success(artistModel);
        }
    }
}
