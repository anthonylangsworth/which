using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Which
{
    /// <summary>
    /// Check whether a file exists. (There is probably a better name)
    /// </summary>
    public class FileChecker
    {
        /// <summary>
        /// Does the given file <paramref name="path"/> exist?
        /// </summary>
        /// <param name="path">
        /// The path to check.
        /// </param>
        /// <returns>
        /// True if the file exists, false otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// No argument can be null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <paramref name="path"/> is empty or whitespace.
        /// </exception>
        public bool FileExists(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("path");
            }
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("path cannot be empty or whitespace", "path");
            }

            return File.Exists(path);
        }
    }
}
