using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace which
{
    /// <summary>
    /// The source of possible command extensions, specifically the PATHEXT environment variable.
    /// </summary>
    public class ExtensionSource
    {
        /// <summary>
        /// Default PATH environment variable directory separator.
        /// </summary>
        internal static readonly char PathExtensionsSeparator = ';';

        /// <summary>
        /// The file extensions to try (in order).
        /// </summary>
        /// <returns>
        /// The extensions to search.
        /// </returns>
        public IEnumerable<string> GetExtenions()
        {
            return new[] { string.Empty }.Concat(
                GetExtensions(Environment.GetEnvironmentVariable("PATHEXT"), PathExtensionsSeparator));
        }

        /// <summary>
        /// The file extensions to try (in order).
        /// </summary>
        /// <param name="pathExtensionsEnvironmentVariable">
        /// The PATHEXT environment variable. This can be null.
        /// </param>
        /// <param name="separator">
        /// The separator for the PATHEXT environment variable.
        /// </param>
        /// <returns>
        /// The extensions to search.
        /// </returns>
        internal IEnumerable<string> GetExtensions(string pathExtensionsEnvironmentVariable, char separator)
        {
            if (pathExtensionsEnvironmentVariable != null)
            {
                foreach (string extension in
                    pathExtensionsEnvironmentVariable.Split(new [] { separator }, StringSplitOptions.RemoveEmptyEntries))
                {
                    yield return extension;
                }
            }
        }
    }
}
