using System;
using System.Collections.Generic;
using System.Text;

namespace Musicio.Core.Messages
{
    public class PlaylistCreationMessage
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string FileExtension { get; set; }

        public PlaylistCreationMessage(string name, string description, string image, string fileExtension)
        {
            Name = name;
            Description = description;
            Image = image;
            FileExtension = fileExtension;
        }

        public PlaylistCreationMessage()
        {
            
        }
    }
}
