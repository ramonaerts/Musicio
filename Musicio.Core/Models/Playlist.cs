using System;
using System.Collections.Generic;
using System.Text;

namespace Musicio.Core.Models
{
    public class Playlist
    {
        public int PlaylistId { get; set; }
        public string Creator { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string Image { get; set; }
    }
}
