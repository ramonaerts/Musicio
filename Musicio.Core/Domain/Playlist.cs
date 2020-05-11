using Musicio.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Musicio.Core.Domain
{
    public class Playlist : BaseEntity
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string ImagePath { get; set; }
        public List<Song> PlaylistSongs { get; set; }
    }
}
