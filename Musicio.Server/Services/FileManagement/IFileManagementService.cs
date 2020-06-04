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
        bool GetContentOfType(string contentString, DataType type, out byte[] imageBytes);
    }
}
