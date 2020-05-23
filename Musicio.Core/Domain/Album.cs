using System;
using System.Collections.Generic;
using System.Text;
using Musicio.Core.Data;

namespace Musicio.Core.Domain
{
    public class Album : BaseEntity
    {
        public int ArtistId { get; set; }
        public string AlbumTitle { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Image { get; set; }
        public List<AlbumSong> AlbumSongs { get; set; }
        
    }
}
