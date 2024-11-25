using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using System.Windows.Controls;
using ticksy;
using ticksy.Dialogs;
using ticksy.Helpers;

namespace ticksytest
{
    [TestClass]
    public class MainWindowTests
    {


        /*
        [TestMethod]
        public void BtnCreateProject_OnClick_ShouldOpenDialog()
        {
            // Arrange
            var app = new App();
            var mainWindow = new MainWindow();
            mainWindow.Show();

            // Get test components
            var button = (Button)mainWindow.FindName("btnCreateProject");
            Assert.IsNotNull(button, "button btnCreateProject was not found in the XAML.");

            // Cleanup
            mainWindow.Close();
        }
        */


        // ticksy.Helpers Validator
        [TestMethod]
        public void ValidateInput_ShouldFail_WhenInputIsEmpty()
        {
            string input = "";
            string fieldName = "Username";
            int minLength = 5;
            int maxLength = 50;

            // Act
            var result = Validator.ValidateInput(input, fieldName, minLength, maxLength, out string errorMessage);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Username cannot be empty.", errorMessage);
        }

        [TestMethod]
        public void ValidateInput_ShouldFail_WhenInputIsTooShort()
        {
            // Arrange
            string input = "Den";
            string fieldName = "Username";
            int minLength = 5;
            int maxLength = 50;

            // Act
            var result = Validator.ValidateInput(input, fieldName, minLength, maxLength, out string errorMessage);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Username must be between 5 and 50 characters.", errorMessage);
        }



        [TestMethod]
        public void ValidateInput_ShouldFail_WhenInputIsTooLong()
        {
            // Arrange
            string input = "wetAAAA15243573489236492AAAsdhdogjfgjdgjogdisdhsfdfdfd";
            string fieldName = "Username";
            int minLength = 5;
            int maxLength = 50;

            // Act
            var result = Validator.ValidateInput(input, fieldName, minLength, maxLength, out string errorMessage);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Username must be between 5 and 50 characters.", errorMessage);
        }

        [TestMethod]
        public void ValidateEmail_ShouldFail_WhenEmpty()
        {
            // Arrange
            string email = "";

            // Act
            var result = Validator.ValidateEmail(email, out string errorMessage);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Email cannot be empty.", errorMessage);
        }


        [TestMethod]
        public void ValidateEmail_ShouldFail_WhenNotValidEmail()
        {
            // Arrange
            string email = "aaa@gmail";

            // Act
            var result = Validator.ValidateEmail(email, out string errorMessage);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("Please enter a valid email address.", errorMessage);

        }


    }
}
