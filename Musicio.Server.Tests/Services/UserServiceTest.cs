using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Extensions.Options;
using Moq;
using Musicio.Core.Domain;
using Musicio.Core.Messages;
using Musicio.Server.Data;
using Musicio.Server.Services.User;
using Xunit;

namespace Musicio.Server.Tests.Services
{
    public class UserServiceTest
    {
        private readonly Mock<Repository<User>> _userRepositoryMock;
        private readonly Mock<IOptions<AppSettings>> _appSettingsMock;

        public UserServiceTest()
        {
            _userRepositoryMock = new Mock<Repository<User>>();
            _appSettingsMock = new Mock<IOptions<AppSettings>>();
        }

        [Fact]
        public void RegisterUserTest()
        {
            var user1 = new User { Id = 1, Mail = "TestMail@test.com", Password = "UnencryptedPass", Role = 3 };
            var user2 = new User { Id = 2, Mail = "TestMail@test.com", Password = "UnencryptedPass", Role = 3 };
            var user3 = new User { Id = 3, Mail = "TestMail@test.com", Password = "UnencryptedPass", Role = 3 };

            _userRepositoryMock.SetupRepositoryMock(options =>
            {
                options.Insert(user1);
                options.Insert(user2);
                options.Insert(user3);
            });

            var userService = new UserService(_userRepositoryMock.Object, _appSettingsMock.Object);

            RegisterMessage message = new RegisterMessage
            {
                Mail = "NewUserMail@gmail.com",
                Password = "TestPassword",
                Username = "NewUsername"
            };

            var success = userService.RegisterUser(message);
            var newUser = userService.GetUserById(4);

            Assert.True(success);
            Assert.NotNull(newUser);
            Assert.Equal(message.Mail, newUser.Mail);
            Assert.NotEqual(message.Password, newUser.Password);
            Assert.Equal(60, newUser.Password.Length);
        }

        [Fact]
        public void RegisterUserWithDoubleMailTest()
        {
            var user1 = new User { Id = 1, Mail = "TestMail@test.com", Password = "UnencryptedPass", Role = 3 };
            var user2 = new User { Id = 2, Mail = "TestMail@test.com", Password = "UnencryptedPass", Role = 3 };
            var user3 = new User { Id = 3, Mail = "TestMail@test.com", Password = "UnencryptedPass", Role = 3 };

            _userRepositoryMock.SetupRepositoryMock(options =>
            {
                options.Insert(user1);
                options.Insert(user2);
                options.Insert(user3);
            });

            var userService = new UserService(_userRepositoryMock.Object, _appSettingsMock.Object);

            RegisterMessage message = new RegisterMessage
            {
                Mail = "TestMail@test.com",
                Password = "TestPassword",
                Username = "NewUsername"
            };

            var success = userService.RegisterUser(message);

            Assert.False(success);
        }

        [Fact]
        public void MailExistTrueTest()
        {
            var user1 = new User { Id = 1, Mail = "TestMail@test.com", Password = "UnencryptedPass", Role = 3 };

            _userRepositoryMock.SetupRepositoryMock(options =>
            {
                options.Insert(user1);
            });

            var userService = new UserService(_userRepositoryMock.Object, _appSettingsMock.Object);

            var exists = userService.MailExists("TestMail@test.com");

            Assert.True(exists);
        }

        [Fact]
        public void MailExistFalseTest()
        {
            var user1 = new User { Id = 1, Mail = "TestMail@test.com", Password = "UnencryptedPass", Role = 3 };

            _userRepositoryMock.SetupRepositoryMock(options =>
            {
                options.Insert(user1);
            });

            var userService = new UserService(_userRepositoryMock.Object, _appSettingsMock.Object);

            var exists = userService.MailExists("UniqueMail@test.com");

            Assert.False(exists);
        }

        [Fact]
        public void LoginSuccessTest()
        {
            var user1 = new User { Id = 1, Mail = "TestMail@test.com", Password = "UnencryptedPass", Role = 3 };

            _userRepositoryMock.SetupRepositoryMock(options =>
            {
                options.Insert(user1);
            });

            var userService = new UserService(_userRepositoryMock.Object, _appSettingsMock.Object);

            var registerSuccess = userService.RegisterUser(new RegisterMessage("mail@test.com", "testUsername", "pass"));

            LoginMessage message = new LoginMessage
            {
                Mail = "mail@test.com",
                Password = "pass"
            };

            var user = userService.LoginUser(message);

            Assert.True(registerSuccess);
            Assert.NotNull(user);
            Assert.Equal(message.Mail, user.Mail);
            Assert.NotEqual(message.Password, user.Password);
        }

        [Fact]
        public void LoginWrongPasswordTest()
        {
            var user1 = new User { Id = 1, Mail = "TestMail@test.com", Password = "UnencryptedPass", Role = 3 };

            _userRepositoryMock.SetupRepositoryMock(options =>
            {
                options.Insert(user1);
            });

            var userService = new UserService(_userRepositoryMock.Object, _appSettingsMock.Object);

            var registerSuccess = userService.RegisterUser(new RegisterMessage("mail@test.com", "testUsername", "pass"));

            LoginMessage message = new LoginMessage
            {
                Mail = "mail@test.com",
                Password = "wrongPass"
            };

            var user = userService.LoginUser(message);

            Assert.True(registerSuccess);
            Assert.Null(user);
        }

        [Fact]
        public void LoginWrongMailTest()
        {
            var user1 = new User { Id = 1, Mail = "TestMail@test.com", Password = "UnencryptedPass", Role = 3 };

            _userRepositoryMock.SetupRepositoryMock(options =>
            {
                options.Insert(user1);
            });

            var userService = new UserService(_userRepositoryMock.Object, _appSettingsMock.Object);

            var registerSuccess = userService.RegisterUser(new RegisterMessage("mail@test.com", "testUsername", "pass"));

            LoginMessage message = new LoginMessage
            {
                Mail = "wrongMail@test.com",
                Password = "pass"
            };

            var user = userService.LoginUser(message);

            Assert.True(registerSuccess);
            Assert.Null(user);
        }

        [Fact]
        public void LoginWrongMailAndPasswordTest()
        {
            var user1 = new User { Id = 1, Mail = "TestMail@test.com", Password = "UnencryptedPass", Role = 3 };

            _userRepositoryMock.SetupRepositoryMock(options =>
            {
                options.Insert(user1);
            });

            var userService = new UserService(_userRepositoryMock.Object, _appSettingsMock.Object);

            var registerSuccess = userService.RegisterUser(new RegisterMessage("mail@test.com", "testUsername", "pass"));

            LoginMessage message = new LoginMessage
            {
                Mail = "wrongMail@test.com",
                Password = "wrongPass"
            };

            var user = userService.LoginUser(message);

            Assert.True(registerSuccess);
            Assert.Null(user);
        }
    }
}
