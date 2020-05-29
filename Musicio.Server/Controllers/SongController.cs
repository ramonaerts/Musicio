using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Musicio.Core.Enums;
using Musicio.Server.Services.FileManagement;

namespace Musicio.Server.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class SongController : ControllerBase
    {
        private readonly IFileManagementService _fileManagementService;

        public SongController(IFileManagementService fileManagementService)
        {
            _fileManagementService = fileManagementService;
        }

        [HttpGet]
        [Route("songs/{songFile}")]
        public IActionResult GetSongFile(string songFile)
        {
            if (_fileManagementService.GetImageOfType(songFile, ImageType.Song, out var songBytes))
            {
                if (!new FileExtensionContentTypeProvider().TryGetContentType(songFile, out var contentType))
                    contentType = "application/octet-stream";

                return new FileContentResult(songBytes, contentType);
            }

            return new NotFoundResult();
        }
    }
}
