using System;
using System.Collections.Generic;
using System.Text;

namespace Musicio.Core.Messages
{
    public class ChangeUserInfoMessage
    {
        public string Mail { get; set; }
        public string Username { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }

        public ChangeUserInfoMessage(string mail, string username, string newPassword, string oldPassword)
        {
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
