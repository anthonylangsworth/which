using System;
using System.Collections.Generic;
using System.IO;

namespace which
{
    class Program
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
        static int Main(string[] args)
        {
            int returnValue;
            string commandPath;
            CommandFinder commandFinder;

            // Not worth a dedicated command line parser
            returnValue = 0;
            if (args.Length == 1 && (args[0] == "-?" || args[0] == "/?" 
                || args[0].Equals("-h", StringComparison.CurrentCultureIgnoreCase)
                || args[0].Equals("--help", StringComparison.CurrentCultureIgnoreCase)
                || args[0].Equals("/h", StringComparison.CurrentCultureIgnoreCase)))
            {
                ShowHelp();
            }
            else
            {
                commandFinder = new CommandFinder();

                for (int i = 0; i < args.Length; i++)
                {
                    commandPath = commandFinder.Find(args[i], new DirectorySource().GetDirectories,
                                                     new ExtensionSource().GetExtenions,
                                                     new FileChecker().FileExists);
                    if (commandPath != null)
                    {
                        Console.Out.WriteLine(commandPath);
                    }
                    else
                    {
                        returnValue++;
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Show a help message.
        /// </summary>
        private static void ShowHelp()
        {
            Console.Error.WriteLine(
@"Iterates the current PATH environment variable and outputs the path to the 
executable, batch file or script (based on the PATHEXT variable) that would be
executed for each command. Nothing is output if a command is not found.

WHICH [command 1] [command 2] [command 3] ... [command n]

'which' sets the error level to the number of commands that were not found.

The source to 'which' is available on github at 
https://github.com/anthonylangsworth/which.
");
        }
    }
}
