using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Musicio.Client.User
{
    public interface ISessionService
    {
        Task SetCookie(string name, string token);
        Task<string> GetJwtCookie();
        int GetIdFromToken();
        Task<bool> CheckIfTokenIsAvailable();
        Task RemoveCookies();
    }
}
