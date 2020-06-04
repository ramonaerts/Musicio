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

        public bool GetContentOfType(string dataString, DataType type, out byte[] bytes)
        {
            bytes = new byte[0];
            var path = GetPath(type);

            if (!File.Exists(Path.Combine(Environment.CurrentDirectory + path + dataString))) return false;

            bytes = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory + path + dataString));

            return true;
        }

        private static string GetPath(DataType type)
        {
            switch (type)
            {
                case DataType.Playlist:
                    return "\\Content\\Playlistimages\\";
                case DataType.Album:
                    return "\\Content\\Musicimages\\";
                case DataType.Artist:
                    return "\\Content\\Artistimages\\";
                case DataType.Song:
                    return "\\Content\\Musicaudio\\";
                default:
                    return null;
            }
        }
    }
}
