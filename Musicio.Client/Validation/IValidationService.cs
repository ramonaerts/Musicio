using System;
using System.Collections.Generic;
using System.Text;

namespace Musicio.Client.Validation
{
    public interface IValidationService
    {
        bool ValidateLoginInput(string mail, string password);
        bool ValidateRegisterInput(string username, string password, string passwordConfirm, string email);
        bool ValidateSingleInput(string value);
    }
}
