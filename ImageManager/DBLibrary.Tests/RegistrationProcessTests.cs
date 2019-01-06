using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DBLibrary;
using System.Windows;

namespace DBLibrary.Tests
{
    public class RegistrationProcessTests
    {
        [Theory]
        [InlineData("testchar", "test6char", "example@example.com", true)]
        [InlineData("","","",false)]
        [InlineData("", "teststst", "", false)]
        [InlineData("", "", "example@example", false)]
        [InlineData("TestUser", "ExampleUser", "", false)]
        [InlineData("sgjskgjdskgjdkhjdkfhjfdkhsgsdggsdgjdfkhjdfkh", "test", "example@gmail.com", false)]


        public void ValidateNewUserRegistration_StringsShouldVerify(string username, string password, string email, bool expected)
        {
            //Arrange
            RegistrationProcess registrationProcess = new RegistrationProcess();

            //Act
            bool actual = registrationProcess.ValidateUserRegistration(username, password, email);

            //Assert
            Assert.Equal(expected, actual);

        }
    }
}
