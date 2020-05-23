using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Musicio.Core.Models
{
    [AutoMap(typeof(Domain.Artist))]
    public class Artist
    {
        public int Id { get; set; }
        public string ArtistName { get; set; }
        public string Image { get; set; }
        public List<Album> Albums { get; set; }
    }
}
