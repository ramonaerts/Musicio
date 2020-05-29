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
using Musicio.Core.Messages;
using Musicio.Server.Services.FileManagement;
using Musicio.Server.Services.Playlist;
using NAudio.Utils;
using Song = Musicio.Core.Models.Song;

namespace Musicio.Server.Controllers.Api
{
    [Authorize]
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
        [Route("")]
        public ApiResult CreatePlaylist(PlaylistCreationMessage message)
        {
            var id = User.Claims.First(c => c.Type == "UserId").Value;

            var success = _playlistService.CreatePlaylist(message, Convert.ToInt32(id));

            return success ? ApiResult.Success(success) : ApiResult.BadRequest();
        }

        [HttpGet]
        [Route("me")]
        public ApiResult GetUserPlaylists()
        {
            var id = User.Claims.First(c => c.Type == "UserId").Value;

            List<Playlist> playlistsEntities = _playlistService.GetUserPlaylists(Convert.ToInt32(id));

            var playlistsModels = _mapper.Map<List<Core.Models.Playlist>>(playlistsEntities);

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

            return ApiResult.Success(playlistModel);
        }

        [HttpGet]
        [Route("me/names")]
        public ApiResult GetUserPlaylistsNames()
        {
            var id = User.Claims.First(c => c.Type == "UserId").Value;

            List<Playlist> playlistsEntities = _playlistService.GetPlaylistNameAndId(Convert.ToInt32(id));

            var playlistsModels = _mapper.Map<List<Core.Models.Playlist>>(playlistsEntities);

            return ApiResult.Success(playlistsModels);
        }

        [HttpPost]
        [Route("{playlistId}/songs/{songId}")]
        public ApiResult AddSongToPlaylist(int playlistId, int songId)
        {
            bool playlistExists = _playlistService.PlaylistExists(playlistId);

            if(!playlistExists) return ApiResult.BadRequest();

            //TODO: add check if song exists

            _playlistService.AddSongToPlaylist(playlistId, songId);

            return ApiResult.Success(true);
        }
    }
}
