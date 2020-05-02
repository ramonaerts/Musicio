using Musicio.Core.Domain;
using Musicio.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musicio.Server.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public AuthenticationService()
        {
            
        }

        public Task<User> LoginUser(LoginMessage message)
        {
            throw new NotImplementedException();
        }
    }
}
