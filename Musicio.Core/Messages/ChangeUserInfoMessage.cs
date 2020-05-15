using System;
using System.Collections.Generic;
using System.Text;

namespace Musicio.Core.Messages
{
    public class ChangeUserInfoMessage
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Username { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }

        public ChangeUserInfoMessage(int id, string mail, string username, string newPassword, string oldPassword)
        {
            Id = id;
            Mail = mail;
            Username = username;
            NewPassword = newPassword;
            OldPassword = oldPassword;
        }

        public ChangeUserInfoMessage()
        {
            
        }
    }
}
