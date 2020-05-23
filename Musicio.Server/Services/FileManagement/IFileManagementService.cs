using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musicio.Core.Enums;

namespace Musicio.Server.Services.FileManagement
{
    public interface IFileManagementService
    {
        string SavePlaylistImage(string image, string fileExtension);
        string CreateBase64String(string image, ImageType type);
    }
}
