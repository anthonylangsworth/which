using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace which.Test
{
    /// <summary>
    /// Test the <see cref="CommandFinder"/> class.
    /// </summary>
    [TestFixture]
    public class TestFindCommand
    {
        [Test]
        public void TestFind_NullCommand()
        {
            Assert.That(
                () => new CommandFinder().Find(null, () => new string[0], () => new string[0], new string[0].Contains),
                Throws.InstanceOf<ArgumentNullException>().And.Property("ParamName").EqualTo("command"));
        }

        [Test]
        public void TestFind_EmptyCommand()
        {
            Assert.That(
                () => new CommandFinder().Find("", () => new string[0], () => new string[0], new string[0].Contains),
                Throws.InstanceOf<ArgumentException>().And.Property("ParamName").EqualTo("command"));
        }

        [Test]
        public void TestFind_WhitespaceCommand()
        {
            Assert.That(
                () => new CommandFinder().Find(" ", () => new string[0], () => new string[0], new string[0].Contains),
                Throws.InstanceOf<ArgumentException>().And.Property("ParamName").EqualTo("command"));
        }

        [Test]
        public void TestFind_NullGetDirectories()
        {
            Assert.That(
                () => new CommandFinder().Find("a", null, () => new string[0], new string[0].Contains),
                Throws.InstanceOf<ArgumentException>().And.Property("ParamName").EqualTo("getDirectories"));
        }

        [Test]
        public void TestFind_NullGetExtensions()
        {
            Assert.That(
                () => new CommandFinder().Find("a", () => new string[0], null, new string[0].Contains),
                Throws.InstanceOf<ArgumentException>().And.Property("ParamName").EqualTo("getExtensions"));
        }

        [Test]
        public void TestFind_NullFileExists()
        {
            Assert.That(
                () => new CommandFinder().Find("a", () => new string[0], () => new string[0], null),
                Throws.InstanceOf<ArgumentException>().And.Property("ParamName").EqualTo("fileExists"));
        }

        /// <summary>
        /// Test the <see cref="CommandFinder.Find"/> method.
        /// </summary>
        /// <param name="command"></param>
        /// <param name="directories"></param>
        /// <param name="extensions"></param>
        /// <param name="existingFiles"></param>
        /// <param name="expectedResult"></param>
        // Check edge cases
        [TestCase("a.exe", new string[0], new string[0], new string[0], null)]
        [TestCase("a.exe", new[] { "" }, new string[0], new string[0], null)]
        [TestCase("a.exe", new string[0], new[] { "" }, new string[0], null)]
        [TestCase("a.exe", new string[0], new string[0], new[] { "a.exe" }, null)]
        [TestCase("a.exe", new[] { "" }, new[] { "" }, new[] { "a.exe" }, "a.exe")]
        [TestCase("b.exe", new[] { "" }, new[] { "" }, new[] { "a.exe" }, null)]
        [TestCase("a", new[] { "c:\\", "c:\\windows" }, new[] { "exe", "bat" }, new[] { "c:\\a" }, "c:\\a")]
        [TestCase("a.ps1", new[] { "c:\\", "c:\\windows" }, new[] { "exe", "bat" }, new[] { "c:\\a" }, null)]
        // Check all the possibilities are checked (possible duplicate of above)
        [TestCase("a", new[] { "c:\\", "c:\\windows" }, new[] { "exe", "bat" }, new[] { "c:\\a" }, "c:\\a")]
        [TestCase("a", new[] { "c:\\", "c:\\windows" }, new[] { "exe", "bat" }, new[] { "c:\\a.exe" }, "c:\\a.exe")]
        [TestCase("a", new[] { "c:\\", "c:\\windows" }, new[] { "exe", "bat" }, new[] { "c:\\a.bat" }, "c:\\a.bat")]
        [TestCase("a", new[] { "c:\\", "c:\\windows" }, new[] { "exe", "bat" }, new[] { "c:\\windows\\a" }, "c:\\windows\\a")]
        [TestCase("a", new[] { "c:\\", "c:\\windows" }, new[] { "exe", "bat" }, new[] { "c:\\windows\\a.exe" }, "c:\\windows\\a.exe")]
        [TestCase("a", new[] { "c:\\", "c:\\windows" }, new[] { "exe", "bat" }, new[] { "c:\\windows\\a.bat" }, "c:\\windows\\a.bat")]
        // Check non-matching combinations fail
        [TestCase("b", new[] { "c:\\", "c:\\windows" }, new[] { "exe", "bat" }, new[] { "c:\\a" }, null)]
        [TestCase("b", new[] { "c:\\", "c:\\windows" }, new[] { "exe", "bat" }, new[] { "c:\\a.exe" }, null)]
        // Check directory ordering
        [TestCase("a", new[] { "c:\\", "c:\\windows" }, new[] { "exe", "bat" }, new[] { "c:\\a", "c:\\windows\\a" }, "c:\\a")]
        [TestCase("a", new[] { "c:\\windows", "c:\\" }, new[] { "exe", "bat" }, new[] { "c:\\a", "c:\\windows\\a" }, "c:\\windows\\a")]
        // Check extension ordering
        [TestCase("a", new[] { "c:\\", "c:\\windows" }, new[] { "exe", "bat" }, new[] { "c:\\a.exe", "c:\\a.bat" }, "c:\\a.exe")]
        [TestCase("a", new[] { "c:\\", "c:\\windows" }, new[] { "bat", "exe" }, new[] { "c:\\a.exe", "c:\\a.bat" }, "c:\\a.bat")]
        [TestCase("a", new[] { "c:\\", "c:\\windows" }, new[] { "exe", "bat" }, new[] { "c:\\windows\\a.exe", "c:\\windows\\a.bat" }, "c:\\windows\\a.exe")]
        [TestCase("a", new[] { "c:\\", "c:\\windows" }, new[] { "bat", "exe" }, new[] { "c:\\windows\\a.exe", "c:\\windows\\a.bat" }, "c:\\windows\\a.bat")]
        public void TestFind(string command, IEnumerable<string> directories, 
            IEnumerable<string> extensions, IEnumerable<string> existingFiles,
            string expectedResult)
        {
            Assert.That(
                new CommandFinder().Find(command, () => directories, () => extensions, existingFiles.Contains),
                Is.EqualTo(expectedResult));
        }
    }
}
