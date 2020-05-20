using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Musicio.Core.Enums;

namespace Musicio.Core.Models
{
    [AutoMap(typeof(Domain.User))]
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Mail { get; set; }
        public int Role { get; set; }
        public string Token { get; set; }
        //public List<Playlist> Playlists { get; set; }

        public User(int id, string username, string email, int roleId, string token)
        {
            Id = id;
            Username = username;
            Mail = email;
            Role = roleId;
            Token = token;
        }

        public User()
        {
            
        }
    }
}
