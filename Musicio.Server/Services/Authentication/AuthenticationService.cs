using Musicio.Core.Domain;
using Musicio.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musicio.Core.Data;

namespace Musicio.Server.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository<User> _userRepository;
        public AuthenticationService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> LoginUser(LoginMessage message)
        {
            User user = _userRepository.Table.SingleOrDefault(a => a.Username == message.Username);
            return user;
        }
    }
}
