using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace Musicio.Server.Services.FileManagement
{
    public class FileManagementService : IFileManagementService
    {
        public string SavePlaylistImage(string image, string fileExtension)
        {
            var fileName = CreateGuidName(fileExtension);

            var filePath = Environment.CurrentDirectory + "\\Content\\Playlistimages\\" + fileName;
            
            File.WriteAllBytes(filePath, Convert.FromBase64String(image));

            return fileName;
        }

        private static string CreateGuidName(string fileExtension)
        {
            var uniqueFileName = Convert.ToString(Guid.NewGuid());
            var extension = fileExtension.Substring(6);

            return uniqueFileName + "." + extension;
        }

        public string CreateBase64String(string image)
        {
            byte[] bytes = File.ReadAllBytes(Environment.CurrentDirectory + "\\Content\\Playlistimages\\" + image);

            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }
    }
}
