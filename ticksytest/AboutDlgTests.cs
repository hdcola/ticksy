using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ticksy.Dialogs;

namespace ticksytest
{
    [TestClass]
    public class AboutDlgTests
    {
        [TestMethod]
        public void BtnTestDatabaseConn_OnClick_ShouldOpenDialog()
        {
            // Arrange
            //var app = new App();
            var mainWindow = new AboutDlg();
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
    }
}
