﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Musicio.Core.Models;

namespace Musicio.Client.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> Login(string mail, string password);

        Task<bool> Register(string mail, string username, string password);
    }
}
