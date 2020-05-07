using System;
using System.Collections.Generic;
using System.Text;

namespace Musicio.Core.Messages
{
    public class LoginMessage
    {
        public string Mail { get; set; }
        public string Password { get; set; }

        public LoginMessage(string mail, string password)
        {
            Mail = mail;
            Password = password;
        }

        public LoginMessage()
        {
            
        }
    }
}
