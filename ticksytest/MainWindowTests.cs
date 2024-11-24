using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;
using System.Windows.Controls;
using ticksy;
using ticksy.Helpers;

namespace ticksytest
{
    [TestClass]
    public class MainWindowTests
    {
        [TestMethod]
        public void BtnTestDatabaseConn_OnClick_ShouldOpenDialog()
        {
            // Arrange
            var app = new App();
            var mainWindow = new MainWindow();
            mainWindow.Show();

            // Get test components
            var button = (Button)mainWindow.FindName("btnAdd");
            Assert.IsNotNull(button, "add button was not found in the XAML.");
            var text = (TextBlock)mainWindow.FindName("txtCount");
            Assert.IsNotNull(text, "count text block was not found in the XAML.");

            // Act
            button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            // Assert that the dialog is open
            Assert.AreEqual("1", text.Text);

            // Act
            button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            // Assert that the dialog is open
            Assert.AreEqual("2", text.Text);

            // Cleanup
            mainWindow.Close();
        }

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
        public void ValidateString_ShouldFail_WhenInputIsEmpty()
        {
            string input = "";
            string fieldName = "First name";
            int minLength = 5;
            int maxLength = 50;

            // Act
            var result = Validator.ValidateString(input, fieldName, minLength, maxLength, out string errorMessage);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("First name cannot be empty.", errorMessage);
        }

        [TestMethod]
        public void ValidateString_ShouldFail_WhenInputIsTooShort()
        {
            // Arrange
            string input = "Phil";
            string fieldName = "First name";
            int minLength = 5;
            int maxLength = 50;

            // Act
            var result = Validator.ValidateString(input, fieldName, minLength, maxLength, out string errorMessage);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("First name must be between 5 and 50 characters.", errorMessage);
        }



        [TestMethod]
        public void ValidateString_ShouldFail_WhenInputIsTooLong()
        {
            // Arrange
            string input = "wetAAAA15243573489236492AAAsdhdogjfgjdgjogdisdhsfdfdfd";
            string fieldName = "First name";
            int minLength = 5;
            int maxLength = 50;

            // Act
            var result = Validator.ValidateString(input, fieldName, minLength, maxLength, out string errorMessage);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("First name must be between 5 and 50 characters.", errorMessage);
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

       /* public static bool ValidateEmail(string email, string fieldName, out string errorMessage)
        {        errorMessage = string.Empty;
            if (string.IsNullOrWhiteSpace(email))            {
                errorMessage = $"{fieldName} cannot be empty or whitespace.";
                return false;
            }
                   // regex for email validation
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
                  if (!emailRegex.IsMatch(email))          {
                errorMessage = $"{fieldName} must be a valid email address.";
                return false;          }
            return true;
        }*/


    }
}
