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
        public string Email { get; set; }
        public UserRole Role { get; set; }
        //public List<Playlist> Playlists { get; set; }

        public User(int id, string username, string email, int roleId)
        {
            Id = id;
            Username = username;
            Email = email;
            SetUserRole(roleId);
        }

        private void SetUserRole(int roleId)
        {
            switch (roleId)
            {
                case 1:
                    Role = UserRole.Admin;
                    break;
                case 2:
                    Role = UserRole.Artist;
                    break;
                case 3:
                    Role = UserRole.User;
                    break;
            }
        }
    }
}
