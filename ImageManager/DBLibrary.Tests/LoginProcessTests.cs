using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLibrary;
using Xunit;

namespace DBLibrary.Tests
{


    public class LoginProcessTests
    {
        [Theory]
        [InlineData("testchar","test6char",true)]
        [InlineData("sgjdsgjskgjsdkgjsdkgjsdkgjsdgjsdkgjskgjdgjskg","sgdsgsdgsgesdgsdgsdesdgdsgsd",false)]
        [InlineData("","",false)]
        [InlineData("admin","password",true)]
        public void ValidateUserInput_StringsShouldVerify(string username, string password, bool expected)
        {
            //Arrange
            LoginProcess loginProcess = new LoginProcess();
            
            //Act
            bool actual = loginProcess.ValidateUserInput(username,password);

            //Assert
            Assert.Equal(expected, actual);

        }
    }
}
