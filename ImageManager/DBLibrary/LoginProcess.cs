using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBLibrary
{
    public class LoginProcess
    {
        /// <summary>
        /// Checks Users Inputs, makes sure that password and username fields are populated
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ValidateUserInput(string username, string password)
        {
            //submitting a value of true, then applying false if it fails input checks
            bool validated = true;
            if (username.Length == 0 || username.Length > 30)
            {
                validated = false;
            }
            //checks each character in the username, looking for numbers and returning false
            foreach (char character in username)
            {
                if (character >= '0' && character <= '9')
                {
                    validated = false;
                }
            }
            //checking if the password length is greater then 6 and less then 30
            if (password.Length <= 6 || password.Length > 30)
            {
                validated = false;
            }
            return validated;
        }
    }
}
