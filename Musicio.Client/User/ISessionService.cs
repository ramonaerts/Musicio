using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Musicio.Client.User
{
    public interface ISessionService
    {
        Task SetCookie(string name, string token);
        Task<string> GetCookie();
        Task DeleteCookie();
    }
}
