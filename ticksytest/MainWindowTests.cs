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
        [TestMethod]
        public void BtnTestDatabaseConn_OnClick_ShouldOpenDialog()
        {
            //// Arrange
            //var app = new App();
            //var mainWindow = new MainWindow();
            //mainWindow.Show();

            //// Get test components
            //var button = (Button)mainWindow.FindName("btnAdd");
            //Assert.IsNotNull(button, "add button was not found in the XAML.");
            //var text = (TextBlock)mainWindow.FindName("txtCount");
            //Assert.IsNotNull(text, "count text block was not found in the XAML.");

            //// Act
            //button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            //// Assert that the dialog is open
            //Assert.AreEqual("1", text.Text);

            //// Act
            //button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            //// Assert that the dialog is open
            //Assert.AreEqual("2", text.Text);

            //// Cleanup
            //mainWindow.Close();
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
        public void ValidateInput_ShouldFail_WhenInputIsEmpty()
        {
            string input = "";
            string fieldName = "First name";
            int minLength = 5;
            int maxLength = 50;

            // Act
            var result = Validator.ValidateInput(input, fieldName, minLength, maxLength, out string errorMessage);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("First name cannot be empty.", errorMessage);
        }

        [TestMethod]
        public void ValidateInput_ShouldFail_WhenInputIsTooShort()
        {
            // Arrange
            string input = "Phil";
            string fieldName = "First name";
            int minLength = 5;
            int maxLength = 50;

            // Act
            var result = Validator.ValidateInput(input, fieldName, minLength, maxLength, out string errorMessage);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual("First name must be between 5 and 50 characters.", errorMessage);
        }



        [TestMethod]
        public void ValidateInput_ShouldFail_WhenInputIsTooLong()
        {
            // Arrange
            string input = "wetAAAA15243573489236492AAAsdhdogjfgjdgjogdisdhsfdfdfd";
            string fieldName = "First name";
            int minLength = 5;
            int maxLength = 50;

            // Act
            var result = Validator.ValidateInput(input, fieldName, minLength, maxLength, out string errorMessage);

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


    }
}
