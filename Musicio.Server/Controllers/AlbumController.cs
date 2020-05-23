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
using Musicio.Server.Services.Album;
using Musicio.Server.Services.FileManagement;

namespace Musicio.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly IFileManagementService _fileManagementService;
        private readonly IMapper _mapper;

        public AlbumController(IAlbumService albumService, IFileManagementService fileManagementService, IMapper mapper)
        {
            _albumService = albumService;
            _fileManagementService = fileManagementService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{albumId}/songs")]
        public ApiResult GetAlbum(int albumId)
        {
            Album album = _albumService.GetAlbumWithSongs(albumId);

            var albumModel = _mapper.Map<Core.Models.Album>(album);
            albumModel.Songs = new List<Core.Models.Song>();

            foreach (var song in album.AlbumSongs)
            {
                var songModel = _mapper.Map<Core.Models.Song>(song.Song);
                albumModel.Songs.Add(songModel);
            }

            if (albumModel.Image != null) albumModel.Image = _fileManagementService.CreateBase64String(albumModel.Image, ImageType.Album);

            return ApiResult.Success(albumModel);
        }
    }
}
