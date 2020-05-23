using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Musicio.Core.Models
{
    [AutoMap(typeof(Domain.AlbumSong))]
    public class AlbumSong
    {
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        public int SongId { get; set; }
        public Song Song { get; set; }
    }
}
