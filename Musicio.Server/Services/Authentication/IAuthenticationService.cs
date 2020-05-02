using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musicio.Core.Domain;
using Musicio.Core.Messages;

namespace Musicio.Server.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<User> LoginUser(LoginMessage message);
    }
}
