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
        string CreateToken(int userId);
        Task<bool> RegisterUser(RegisterMessage message);
        Core.Domain.User GetUserById(int userId);
        bool ChangeUserInfo(ChangeUserInfoMessage message, int userId);
    }
}
