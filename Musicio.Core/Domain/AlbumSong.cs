using System;
using System.Collections.Generic;
using System.Text;
using Musicio.Core.Data;

namespace Musicio.Core.Domain
{
    public class AlbumSong : BaseEntity
    {
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
    }
}
