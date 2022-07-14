using ControlOrigins.Survey.Common.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace ControlOrigins.Survey.Common.Tests.Models
{
    [TestClass]
    public class BuildVersionTests
    {
        [TestMethod]
        public void BuildVersionToString_ExpectedBehavior()
        {
            // Arrange
            var buildVersion = new BuildVersion(Assembly.GetExecutingAssembly()?.GetName().Version);

            // Act
            var result = buildVersion.ToString();

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
