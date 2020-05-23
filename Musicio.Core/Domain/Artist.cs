﻿using Musicio.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Musicio.Core.Domain
{
    public class Artist : BaseEntity
    {
        public string ArtistName { get; set; }
        public string Image { get; set; }
        public List<Album> Albums { get; set; }
        //public List<Song> ArtistSingles { get; set; }
    }
}
