using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musicio.Core.Domain;
using Musicio.Core.Messages;

namespace Musicio.Server.Services.User
{
    public interface IUserService
    {
        Task<Core.Domain.User> LoginUser(LoginMessage message);

        Task<bool> RegisterUser(RegisterMessage message);
    }
}
