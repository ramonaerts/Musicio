using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Musicio.Core.Models;

namespace Musicio.Client.Authentication
{
    public interface IAuthenticationService
    {
        Task<User> Login(string username, string password);
    }
}
