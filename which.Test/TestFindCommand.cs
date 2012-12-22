using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace which.Test
{
    [TestFixture]
    public class TestFindCommand
    {
        [Test]
        public void TestFind(string command, IEnumerable<string> directories, 
            IEnumerable<string> extensions, IEnumerable<string> existingFiles)
        {
            new CommandFinder().Find(command, () => directories, () => extensions, existingFiles.Contains);
        }
    }
}
