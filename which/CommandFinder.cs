﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace which
{
    /// <summary>
    /// Find a command in a 
    /// </summary>
    public class CommandFinder
    {
        /// <summary>
        /// Find the full path of the command executed when t
        /// </summary>
        /// <returns>
        /// The full path to the executed command or null if it is not found.
        /// </returns>
        /// <param name="command">
        /// The name of the executable, batch file, script, etc to look for.
        /// This cannot be null, empty or whitespace.
        /// </param>
        /// <param name="directories">
        /// A list of directories to search.
        /// </param>
        /// <param name="extensions">
        /// A list of extensions to search.
        /// </param>
        /// <param name="fileExists">
        /// Check whether the given file exists.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="command"/> is empty or whitespace.
        /// </exception>
        public string Find(string command, Func<IEnumerable<string>> directories,
            Func<IEnumerable<string>> extensions, Predicate<string> fileExists)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (string.IsNullOrWhiteSpace(command))
            {
                throw new ArgumentException("command cannot be empty or whitespace", "command");
            }
            if (directories == null)
            {
                throw new ArgumentNullException("directories");
            }
            if (extensions == null)
            {
                throw new ArgumentNullException("extensions");
            }
            if (fileExists == null)
            {
                throw new ArgumentNullException("fileExists");
            }

            string commandPath;

            // The windows command parser appears to look for the current file
            // first then iterate thhough the extensions.

            //foreach (string directory in directories())
            //{
            //    commandPath = Path.Combine(directory, command);
            //    if (fileExists(commandPath))
            //    {
            //        return commandPath;
            //    }
            //}

            foreach (string extension in extensions())
            {
                foreach (string directory in directories())
                {
                    commandPath = Path.Combine(Path.Combine(directory, command), extension);
                    if (fileExists(commandPath))
                    {
                        return commandPath;
                    }
                }
            }
            return null;
        }
    }
}
