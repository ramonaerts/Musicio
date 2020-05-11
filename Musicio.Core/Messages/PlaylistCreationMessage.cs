using System;
using System.Collections.Generic;
using System.Text;

namespace Musicio.Core.Messages
{
    public class PlaylistCreationMessage
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public PlaylistCreationMessage(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public PlaylistCreationMessage()
        {
            
        }
    }
}
