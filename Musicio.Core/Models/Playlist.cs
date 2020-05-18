using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Musicio.Core.Models
{
    [AutoMap(typeof(Domain.Playlist))]
    public class Playlist
    {
        public int Id { get; set; }
        public string Creator { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string Image { get; set; }
        public List<Song> Songs { get; set; }

        public Playlist()
        {
            
        }
    }
}
