using System;
using System.Collections.Generic;
using System.Text;
using Musicio.Core.Data;

namespace Musicio.Core.Domain
{
    public class PlaylistSong : BaseEntity
    {
        public int PlaylistId { get; set; }
        public Playlist Playlist { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
    }
}
