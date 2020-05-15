using Musicio.Core.Domain;
using Musicio.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Musicio.Core.Data;

namespace Musicio.Server.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRepository<Core.Domain.User> _userRepository;
        public UserService(IRepository<Core.Domain.User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Core.Domain.User> LoginUser(LoginMessage message)
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

            var newUser = new Core.Domain.User()
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
