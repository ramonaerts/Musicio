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
    public class AlbumController : ControllerBase
    {
        private readonly IFileManagementService _fileManagementService;

        public AlbumController(IFileManagementService fileManagementService)
        {
            _fileManagementService = fileManagementService;
        }

        [HttpGet]
        [Route("images/{albumImage}")]
        public IActionResult GetAlbumImage(string albumImage)
        {
            if (_fileManagementService.GetContentOfType(albumImage, DataType.Album, out var imageBytes))
            {
                if (!new FileExtensionContentTypeProvider().TryGetContentType(albumImage, out var contentType))
                    contentType = "application/octet-stream";

                return new FileContentResult(imageBytes, contentType);
            }

            return new NotFoundResult();
        }
    }
}
