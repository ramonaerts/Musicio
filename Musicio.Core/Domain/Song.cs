using Musicio.Core.Data;
using Musicio.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Musicio.Core.Domain
{
    public class Song : BaseEntity
    {
        public string Artist { get; set; }
        public SongType Type { get; set; }
        public int? AlbumId { get; set; }
        public string SongTitle { get; set; }
        public SongGenre Genre { get; set; }
        public string SongFile { get; set; }
        public TimeSpan SongDuration { get; set;}       
    }
}
