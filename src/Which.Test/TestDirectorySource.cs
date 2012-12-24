using System.Collections.Generic;
using NUnit.Framework;

namespace Which.Test
{
    /// <summary>
    /// Test <see cref="DirectorySource"/>.
    /// </summary>
    [TestFixture]
    public class TestDirectorySource
    {
        /// <summary>
        /// Test GetPathDirectories().
        /// </summary>
        /// <param name="pathEnvironmentVariable"></param>
        /// <param name="expectedDirectories"></param>
        [TestCase("", new string[0])]
        [TestCase("a", new []{ "a" })]
        [TestCase("a;", new[] { "a" })]
        [TestCase(";a", new[] { "a" })]
        [TestCase("a;a", new[] { "a", "a" })]
        [TestCase("a;;a", new[] { "a", "a" })]
        [TestCase(";a;a", new[] { "a", "a" })]
        [TestCase("a;a;", new[] { "a", "a" })]
        [TestCase("a;b;c;d", new[] { "a", "b", "c", "d" })]
        public void TestGetPathDirectories(string pathEnvironmentVariable, IEnumerable<string> expectedDirectories)
        {
            IEnumerable<string> result = new DirectorySource().GetPathDirectories(pathEnvironmentVariable, DirectorySource.PathSeparator);
            Assert.That(result, Is.EquivalentTo(expectedDirectories));
        }
    }
}
