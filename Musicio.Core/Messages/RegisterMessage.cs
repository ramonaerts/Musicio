using System;
using System.Collections.Generic;
using System.Text;

namespace Musicio.Core.Messages
{
    public class RegisterMessage
    {
        public string Mail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public RegisterMessage(string mail, string username, string password)
        {
            Mail = mail;
            Username = username;
            Password = password;
        }

        public RegisterMessage()
        {

        }
    }
}
