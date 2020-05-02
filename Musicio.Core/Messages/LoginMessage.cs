using System;
using System.Collections.Generic;
using System.Text;

namespace Musicio.Core.Messages
{
    public class LoginMessage
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginMessage(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public LoginMessage()
        {
            
        }
    }
}
