using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Musicio.Core;
using Musicio.Core.Domain;
using Musicio.Core.Messages;
using Musicio.Server.Services.FileManagement;
using Musicio.Server.Services.Playlist;
using Song = Musicio.Core.Models.Song;

namespace Musicio.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;
        private readonly IFileManagementService _fileManagementService;
        private readonly IMapper _mapper;

        public PlaylistController(IMapper mapper, IPlaylistService playlistService, IFileManagementService fileManagementService)
        {
            _mapper = mapper;
            _playlistService = playlistService;
            _fileManagementService = fileManagementService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ApiResult> CreatePlaylist(PlaylistCreationMessage message)
        {
            var success = await _playlistService.CreatePlaylist(message);

            return success ? ApiResult.Success(success) : ApiResult.BadRequest();
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<ApiResult> GetUserPlaylists(int userId)
        {
            List<Playlist> playlistsEntities = await _playlistService.GetUserPlaylists(userId);

            var playlistsModels = _mapper.Map<List<Core.Models.Playlist>>(playlistsEntities);

            foreach (var p in playlistsModels)
            {
                if (p.Image != null) p.Image = _fileManagementService.CreateBase64String(p.Image);
            }

            return ApiResult.Success(playlistsModels);
        }

        [HttpGet]
        [Route("{playlistId}/songs")]
        public ApiResult GetPlaylistSongs(int playlistId)
        {
            Playlist playlist = _playlistService.GetPlaylistSongs(playlistId);

            var playlistModel = _mapper.Map<Core.Models.Playlist>(playlist);
            playlistModel.Songs = new List<Song>();

            foreach (var song in playlist.PlaylistSongs)
            {
                var songModel = _mapper.Map<Song>(song.Song);
                playlistModel.Songs.Add(songModel);
            }

            if (playlistModel.Image != null) playlistModel.Image = _fileManagementService.CreateBase64String(playlistModel.Image);

            return ApiResult.Success(playlistModel);
        }
    }
}
