using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Musicio.Core.Models;

namespace Musicio.Client.User
{
    public interface IUserService
    {
        Task<bool> Login(string mail, string password);
        Task<bool> Register(string mail, string username, string password);
        Task<Core.Models.User> GetUserInfo();
        Task<bool> ChangeUserInfo(string mail, string username, string newPassword, string oldPassword);
    }
}
