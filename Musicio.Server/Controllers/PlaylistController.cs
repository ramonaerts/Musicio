using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Musicio.Core.Enums;
using Musicio.Server.Services.FileManagement;

namespace Musicio.Server.Controllers
{
    /*[Authorize]*/
    [Controller]
    [Route("[controller]")]
    public class PlaylistController : ControllerBase
    {
        private readonly IFileManagementService _fileManagementService;

        public PlaylistController(IFileManagementService fileManagementService)
        {
            _fileManagementService = fileManagementService;
        }

        [HttpGet]
        [Route("images/{playlistImage}")]
        public IActionResult GetPlaylistImage(string playlistImage)
        {
            if (_fileManagementService.GetContentOfType(playlistImage, DataType.Playlist, out var imageBytes))
            {
                if (!new FileExtensionContentTypeProvider().TryGetContentType(playlistImage, out var contentType))
                    contentType = "application/octet-stream";

                return new FileContentResult(imageBytes, contentType);
            }

            return new NotFoundResult();
        }
    }
}
