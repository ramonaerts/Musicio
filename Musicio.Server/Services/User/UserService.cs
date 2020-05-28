using Musicio.Core.Domain;
using Musicio.Core.Messages;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Musicio.Core.Data;

namespace Musicio.Server.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRepository<Core.Domain.User> _userRepository;
        private readonly AppSettings _appSettings;

        public UserService(IRepository<Core.Domain.User> userRepository, IOptions<AppSettings> appSettings)
        {
            _userRepository = userRepository;
            _appSettings = appSettings.Value;
        }

        public async Task<Core.Domain.User> LoginUser(LoginMessage message)
        {
            var user = _userRepository.TableNoTracking.SingleOrDefault(a => a.Mail == message.Mail);
            if (user == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(message.Password, user.Password)) return null;
            return user;
        }

        public string CreateToken(int userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<bool> RegisterUser(RegisterMessage message)
        {
            if (MailExists(message.Mail)) return false;

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

        public Core.Domain.User GetUserById(int userId)
        {
            return _userRepository.TableNoTracking.SingleOrDefault(a => a.Id == userId);
        }

        public bool ChangeUserInfo(ChangeUserInfoMessage message, int userId)
        {
            if (MailExists(message.Mail)) return false;

            var user = GetUserById(userId);
            if (!BCrypt.Net.BCrypt.Verify(message.OldPassword, user.Password)) return false;

            user.Username = message.Username;
            user.Mail = message.Mail;
            user.Password = BCrypt.Net.BCrypt.HashPassword(message.NewPassword);

            _userRepository.Update(user);

            return true;
        }

        public bool MailExists(string mail)
        {
            return _userRepository.TableNoTracking.Any(e => e.Mail == mail);
        }
    }
}
