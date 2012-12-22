using System;
using System.Collections.Generic;
using System.Linq;

namespace which
{
    /// <summary>
    /// The source of directories to search, specifically the PATH environment variable.
    /// </summary>
    public class DirectorySource
    {
        /// <summary>
        /// Default PATH environment variable directory separator.
        /// </summary>
        internal static readonly char PathSeparator = ';';

        /// <summary>
        /// The directories to check (in order).
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerable{String}"/> of directories to check.
        /// </returns>
        public IEnumerable<string> GetDirectories()
        {
            return GetCurrentDirectory().Concat(
                GetPathDirectories(Environment.GetEnvironmentVariable("PATH"), PathSeparator));
        }

        /// <summary>
        /// The directories to check (in order).
        /// </summary>
        /// <param name="pathEnvironmentVariable">
        /// The contents of the PATH environment variable. This may be null.
        /// </param>
        /// <param name="separator">
        /// The PATH environment separator.s
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{String}"/> of directories to check.
        /// </returns>
        internal IEnumerable<string> GetPathDirectories(string pathEnvironmentVariable, char separator)
        {
            if (pathEnvironmentVariable != null)
            {
                foreach (string directory in
                    pathEnvironmentVariable.Split(new char[] { separator }, StringSplitOptions.RemoveEmptyEntries))
                {
                    yield return directory;
                }
            }
        }

        /// <summary>
        /// The current directory.
        /// </summary>
        /// <returns>
        /// An enumeration containing the current directory (only).
        /// </returns>
        internal IEnumerable<string> GetCurrentDirectory()
        {
            yield return Environment.CurrentDirectory;
        }
    }
}
