using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ticksy.Dialogs;

namespace ticksytest
{
    [TestClass]
    public class LoginWithEmailDlgTests
    {

        [TestMethod]
        public void IsLoginDataValid_WithValidData_ReturnsTrue()
        {
            // Arrange
            var loginWithEmailDlg = new LoginWithEmailDlg
            {
                EmailTest = "valid.email@example.com",
                PasswordTest = "Password123"
            };

            // Act
            bool result = loginWithEmailDlg.isLoginDataValid(out string errorsString);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(string.Empty, errorsString);
        }



        [TestMethod]
        public void IsLoginDataValid_WithEmptyData_ReturnsFalse()
        {
            // Arrange
            var loginWithEmailDlg = new LoginWithEmailDlg
            {
                EmailTest = "",
                PasswordTest = ""
            };

            // Act
            bool result = loginWithEmailDlg.isLoginDataValid(out string errorsString);

            // Assert
            Assert.IsFalse(result, "Validation should fail with invalid data.");
            Assert.IsTrue(errorsString.Contains("Email cannot be empty."), "Email has validation error.");
            Assert.IsTrue(errorsString.Contains("Password cannot be empty."), "Password has validation error.");

        }




        [TestMethod]
        public void IsLoginDataValid_WithInvalidData_ReturnsFalse()
        {
            // Arrange
            var registerDlg = new RegisterDlg
            {
                EmailTest = "invalid.email@example",
                PasswordTest = "123"
            };

            // Act
            bool result = registerDlg.isRegisterDataValid(out string errorsString);

            // Assert
            Assert.IsFalse(result, "Validation should fail with invalid data.");
            Assert.IsTrue(errorsString.Contains("Please enter a valid email address"), "Email validation error is incorrect.");

        }
    }
}
