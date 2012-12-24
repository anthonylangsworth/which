using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Which.Test
{
    /// <summary>
    /// Test <see cref="ExtensionSource"/>.
    /// </summary>
    [TestFixture]
    public class TestExtensionSource
    {
        /// <summary>
        /// Test GetPathDirectories().
        /// </summary>
        /// <param name="pathExtensionsEnvironmentVariable"></param>
        /// <param name="expectedExtensions"></param>
        [TestCase("", new string[0])]
        [TestCase("a", new[] { "a" })]
        [TestCase("a;", new[] { "a" })]
        [TestCase(";a", new[] { "a" })]
        [TestCase("a;a", new[] { "a", "a" })]
        [TestCase("a;;a", new[] { "a", "a" })]
        [TestCase(";a;a", new[] { "a", "a" })]
        [TestCase("a;a;", new[] { "a", "a" })]
        [TestCase("a;b;c;d", new[] { "a", "b", "c", "d" })]
        public void TestGetPathExtensions(string pathExtensionsEnvironmentVariable, IEnumerable<string> expectedExtensions)
        {
            IEnumerable<string> result = new ExtensionSource().GetExtensions(pathExtensionsEnvironmentVariable, DirectorySource.PathSeparator);
            Assert.That(result, Is.EquivalentTo(expectedExtensions));
        }
    }
}
