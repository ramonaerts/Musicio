using System;
using System.Collections.Generic;
using System.Text;

namespace Musicio.Client.Validation
{
    public class ValidationService : IValidationService
    {
        public bool ValidateLoginInput(string mail, string password)
        {
            if (string.IsNullOrEmpty(mail))
                return false;
            return !string.IsNullOrEmpty(password);
        }


        public bool ValidateRegisterInput(string username, string password, string passwordConfirm, string email)
        {
            if (string.IsNullOrEmpty(username))
                return false;
            if (string.IsNullOrEmpty(password))
                return false;
            if (string.IsNullOrEmpty(passwordConfirm))
                return false;
            return !string.IsNullOrEmpty(email);
        }
    }
}
