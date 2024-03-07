using Background_Working_Startup_Toast;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

[TestClass]
public class ThisAddInTests
{
    [TestMethod]
    public void ShowPopupMessage_ValidInput_DoesNotThrowException()
    {
        // Arrange
        string message = new string('a', 500);
        int duration = 10;

        // Act and Assert
        ThisAddIn.ShowPopupMessage(message, duration);
    }

    [TestMethod]
    public void ShowPopupMessage_MessageTooLong_ThrowsException()
    {
        // Arrange
        string message = new string('a', 501);
        int duration = 10;

        // Act and Assert
        Assert.ThrowsException<ArgumentException>(() => ThisAddIn.ShowPopupMessage(message, duration));
    }

    [TestMethod]
    public void ShowPopupMessage_DurationTooLong_ThrowsException()
    {
        // Arrange
        string message = new string('a', 500);
        int duration = 11;

        // Act and Assert
        Assert.ThrowsException<ArgumentException>(() => ThisAddIn.ShowPopupMessage(message, duration));
    }
}
