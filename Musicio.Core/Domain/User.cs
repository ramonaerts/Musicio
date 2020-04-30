using System;
using System.Collections.Generic;
using System.Text;
using Musicio.Core.Data;
using Musicio.Core.Enums;

namespace Musicio.Core.Domain
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public UserRole Role { get; set; }
        public List<Playlist> Playlists { get; set; }
    }
}
