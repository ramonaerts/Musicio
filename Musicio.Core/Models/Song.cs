using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Musicio.Core.Enums;

namespace Musicio.Core.Models
{
    [AutoMap(typeof(Domain.Song))]
    public class Song
    {
        public int Id { get; set; }
        public int CollectionId { get; set; }
        public string Artist { get; set; }
        //public SongType Type { get; set; }
        //public int? AlbumId { get; set; }
        public string SongTitle { get; set; }
        //public SongGenre Genre { get; set; }
        public string SongFile { get; set; }
        public TimeSpan SongDuration { get; set; }
    }
}
