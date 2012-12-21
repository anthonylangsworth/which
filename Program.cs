using System;
using System.Collections.Generic;
using System.IO;

namespace which
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1 || (args.Length == 1 && args[0] == "-?" || args[0] == "/?"))
            {
                ShowHelp();
            }
            else
            {
                Console.Out.WriteLine(FindCommand(args[0]) ?? string.Empty);
            }
        }

        /// <summary>
        /// Show a help message.
        /// </summary>
        private static void ShowHelp()
        {
            Console.Error.WriteLine("Usage: which <command name>");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command">
        /// The name of the executable, batch file, script, etc to look for.
        /// This cannot be null, empty or whitespace.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="command"/> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="command"/> is empty or whitespace.
        /// </exception>
        private static string FindCommand(string command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (string.IsNullOrWhiteSpace(command))
            {
                throw new ArgumentException("command cannot be empty or whitespace", "command");
            }

            string commandPath;

            foreach (string directory in GetDirectories())
            {
                foreach (string extension in GetExtenions())
                {
                    commandPath = Path.ChangeExtension(Path.Combine(directory, command), extension);
                    if (File.Exists(commandPath))
                    {
                        return commandPath;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// The directories to check (in order).
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{String}"/> of directories to check.
        /// </returns>
        public static IEnumerable<string> GetDirectories()
        {
            string path;

            yield return Environment.CurrentDirectory;

            path = Environment.GetEnvironmentVariable("PATH");
            if (path != null)
            {
                foreach (string directory in 
                    path.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    yield return directory;
                }
            }
        }

        /// <summary>
        /// The file extensions to try (in order).
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<string> GetExtenions()
        {
            string pathExtensions;

            yield return string.Empty;

            pathExtensions = Environment.GetEnvironmentVariable("PATHEXT");
            if (pathExtensions != null)
            {
                foreach (string extension in 
                    pathExtensions.Split(new char[]{';'}, StringSplitOptions.RemoveEmptyEntries))
                {
                    yield return extension;
                }
            }
        }
    }
}
