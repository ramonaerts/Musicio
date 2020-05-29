using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Musicio.Core.Enums;

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

        public string CreateBase64String(string image, ImageType type)
        {
            string path = GetPath(type);

            byte[] bytes = File.ReadAllBytes(Environment.CurrentDirectory + path + image);

            return Convert.ToBase64String(bytes, 0, bytes.Length);
        }

        public bool GetImageOfType(string imageString, ImageType type, out byte[] imageBytes)
        {
            imageBytes = new byte[0];
            var path = GetPath(type);

            if (!File.Exists(Path.Combine(Environment.CurrentDirectory + path + imageString))) return false;

            imageBytes = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory + path + imageString));

            return true;

        }

        private static string GetPath(ImageType type)
        {
            switch (type)
            {
                case ImageType.Playlist:
                    return "\\Content\\Playlistimages\\";
                case ImageType.Album:
                    return "\\Content\\Musicimages\\";
                case ImageType.Artist:
                    return "\\Content\\Artistimages\\";
                default:
                    return null;
            }
        }
    }
}
