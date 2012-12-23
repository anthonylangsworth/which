using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CommandLine;

namespace which
{
    /// <summary>
    /// 
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Standard entry point.
        /// </summary>
        /// <param name="args">
        /// Command line args.
        /// </param>
        /// <returns>
        /// The number of commands not found.
        /// </returns>
        public static int Main(string[] args)
        {
            WhichCommandLineArguments arguments;
            CommandFinder commandFinder;
            DirectorySource directorySource;
            ExtensionSource extensionSource;
            FileChecker fileChecker;
            int returnValue;

            arguments = new WhichCommandLineArguments();
            if (CommandLineParser.Default.ParseArguments(args, arguments, Console.Error))
            {
                commandFinder = new CommandFinder();
                directorySource = new DirectorySource();
                extensionSource = new ExtensionSource();
                fileChecker = new FileChecker();

                IList<string> paths = new List<string>(arguments.Commands.Select(
                    x => commandFinder.Find(x, directorySource.GetDirectories,
                    extensionSource.GetExtenions, fileChecker.FileExists)));
                foreach (string path in paths.Where(x => x != null))
                {
                    Console.Out.WriteLine(path);
                }

                returnValue = paths.Count(x => x == null);
            }
            else
            {
                returnValue = 1;
            }

            return returnValue;
        }
    }
}
