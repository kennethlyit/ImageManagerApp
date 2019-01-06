using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public class RegistrationProcess
    {
        public bool ValidateUserRegistration(string username, string password, string email)
        {
            //submitting a value of false, then applying true if it passes input checks
            bool validated = false;
            
            if (username.Length == 0 || username.Length > 30)
            {
                validated = true;
            }
            //checks each character in the username, looking for numbers and returning true
            foreach (char character in username)
            {
                if (character >= '0' && character <= '9')
                {
                    validated = true;
                }
            }
            //checking if the password length is greater then 6 and less then 30
            if (password.Length <= 6 || password.Length > 30)
            {
                validated = true;
            }
            //email check using .net library
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address == email)
                {
                    validated = true;
                }
            }
            catch
            {            
                
            }




            return validated;
        }
    }
}
