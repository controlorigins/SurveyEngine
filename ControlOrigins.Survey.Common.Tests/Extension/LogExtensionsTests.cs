using ControlOrigins.Survey.Common.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ControlOrigins.Survey.Common.Tests.Extension
{
    [TestClass]
    public class LogExtensionsTests
    {
        [TestMethod]
        public void IsSimpleType_StateUnderTest_ExpectedBehaviorTrue()
        {
            // Arrange
            string type = "test";

            // Act
            var result = LogExtensions.IsSimpleType(
                type.GetType());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result);
        }
    }
}