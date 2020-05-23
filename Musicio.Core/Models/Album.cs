using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Musicio.Core.Models
{
    [AutoMap(typeof(Domain.Album))]
    public class Album
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string AlbumTitle { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Image { get; set; }
        public List<Song> AlbumSongs { get; set; }
    }
}
