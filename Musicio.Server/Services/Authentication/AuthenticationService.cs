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
            var user = _userRepository.Table.SingleOrDefault(a => a.Mail == message.Mail);
            if (user == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(message.Password, user.Password)) return null;
            return user;
        }

        public async Task<bool> RegisterUser(RegisterMessage message)
        {
            if (_userRepository.Table.SingleOrDefault(a => a.Mail == message.Mail) != null) return false;

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(message.Password);

            var newUser = new User()
            {
                Username = message.Username,
                Password = hashedPassword,
                Mail = message.Mail,
                Role = 3
            };

            _userRepository.Insert(newUser);

            return true;
        }
    }
}
