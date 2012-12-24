using System.Collections.Generic;
using CommandLine;
using CommandLine.Text;

namespace Which
{
    /// <summary>
    /// Command line argument container.
    /// </summary>
    internal class WhichCommandLineArguments
    {
        /// <summary>
        /// Commands to find.
        /// </summary>
        [ValueList(typeof(List<string>))]
        public List<string> Commands { get; set; }

        /// <summary>
        /// Show help.
        /// </summary>
        [HelpOption()]
        public string ShowHelp()
        {
            HelpText helpText;
            
            helpText = new HelpText
            {
                AdditionalNewLineAfterOption = true,
                AddDashesToOption = true
            };
            helpText.AddPreOptionsLine(
                "Iterates the current PATH environment variable and outputs the path to the executable, batch file or script "
                + "(based on the PATHEXT variable) that would be executed for each command. Nothing is output if a command is not found.");
            helpText.AddPreOptionsLine(string.Empty);
            helpText.AddPreOptionsLine("Usage: which command [command 2] [command 3] ... [command n]");
            helpText.AddOptions(this);
            return helpText;
        }
    }
}
