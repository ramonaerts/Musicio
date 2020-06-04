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
    public class ArtistController : ControllerBase
    {
        private readonly IFileManagementService _fileManagementService;

        public ArtistController(IFileManagementService fileManagementService)
        {
            _fileManagementService = fileManagementService;
        }

        [HttpGet]
        [Route("images/{artistImage}")]
        public IActionResult GetArtistImage(string artistImage)
        {
            if (_fileManagementService.GetContentOfType(artistImage, DataType.Artist, out var imageBytes))
            {
                if (!new FileExtensionContentTypeProvider().TryGetContentType(artistImage, out var contentType))
                    contentType = "application/octet-stream";

                return new FileContentResult(imageBytes, contentType);
            }

            return new NotFoundResult();
        }
    }
}
