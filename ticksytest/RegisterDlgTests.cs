using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ticksy.Dialogs;

namespace ticksytest
{
    [TestClass]
    public class RegisterDlgTests
    {
        [TestMethod]
        public void IsRegisterDataValid_WithValidData_ReturnsTrue()
        {
            // Arrange
            var registerDlg = new RegisterDlg
            {
                UsernameTest = "ValidUsername",
                EmailTest = "valid.email@example.com",
                PasswordTest = "Password123",
                ConfirmPasswordTest = "Password123"
            };

            // Act
            bool result = registerDlg.isRegisterDataValid(out string errorsString);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(string.Empty, errorsString);
        }

        [TestMethod]
        public void IsRegisterDataValid_WithInvalidData_ReturnsFalse()
        {
            // Arrange
            var registerDlg = new RegisterDlg
            {
                UsernameTest = "A",
                EmailTest = "invalid.email@example",
                PasswordTest = "123",
                ConfirmPasswordTest = "456"
            };

            // Act
            bool result = registerDlg.isRegisterDataValid(out string errorsString);

            // Assert
            Assert.IsFalse(result, "Validation should fail with invalid data.");
            Assert.IsTrue(errorsString.Contains("Username"), "Username must be between 5 and 50 characters.");
            Assert.IsTrue(errorsString.Contains("Please enter a valid email address"), "Email validation error is incorrect.");
            Assert.IsTrue(errorsString.Contains("Password"), "Password must be between 6 and 100 characters.");
            Assert.IsTrue(errorsString.Contains("Passwords"), "Passwords do not match");

        }
    }
}
