using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Musicio.Core;
using Musicio.Core.Messages;
using Musicio.Server.Services.Playlist;

namespace Musicio.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;
        private readonly IMapper _mapper;

        public PlaylistController(IMapper mapper, IPlaylistService playlistService)
        {
            _mapper = mapper;
            _playlistService = playlistService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ApiResult> CreatePlaylist(PlaylistCreationMessage message)
        {
            var success = await _playlistService.CreatePlaylist(message);

            return success ? ApiResult.Success(success) : ApiResult.BadRequest();
        }
    }
}
