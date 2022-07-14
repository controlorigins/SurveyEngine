using ControlOrigins.Survey.Common.Extension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel.DataAnnotations;

namespace ControlOrigins.Survey.Common.Tests.Extension
{
    /// <summary>
    /// Enum TestEnum
    /// </summary>
    public enum TestEnum
    {
        /// <summary>
        /// The unknown
        /// </summary>
        [Display(Name = "Unknown", Description = "Unknown")]
        Unknown = 0,

        /// <summary>
        /// The first
        /// </summary>
        [Display(Name = "1st", Description = "The First One")]
        First = 1,

        /// <summary>
        /// The second
        /// </summary>
        Second = 2
    }

    /// <summary>
    /// Defines test class EnumExtensionTests.
    /// </summary>
    [TestClass]
    public class EnumExtensionTests
    {
        /// <summary>
        /// Defines the test method EnumGetDisplayNameExpectedBehavior.
        /// </summary>
        [TestMethod]
        public void EnumGetDisplayNameExpectedBehavior()
        {
            // Arrange
            var myTest = TestEnum.First;

            // Act
            var result = myTest.GetDisplayName();

            // Assert
            Assert.AreEqual(result, "1st");
        }

        /// <summary>
        /// Defines the test method EnumGetDisplayNameMissingDisplayName.
        /// </summary>
        [TestMethod]
        public void EnumGetDisplayNameMissingDisplayName()
        {
            // Arrange
            var myTest = TestEnum.Second;

            // Act
            var result = myTest.GetDisplayName();

            // Assert
            Assert.AreEqual(result, "Second");
        }

        /// <summary>
        /// Defines the test method EnumToDictionaryExpectedBehavior.
        /// </summary>
        [TestMethod]
        public void EnumToDictionaryExpectedBehavior()
        {
            // Arrange
            var myTest = TestEnum.First;

            // Act
            var result = myTest.ToDictionary();

            // Assert
            Assert.AreEqual(result.Count, 3);
        }
    }
}