using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI
{
    /// <summary>
    /// Helper class.
    /// </summary>
    public class CommandParser
    {
        /// <summary>
        /// An enumeration of CLI commands available in the assembly.
        /// (Classes that implement ICommand interface)
        /// </summary>
        readonly IEnumerable<ICommandFactory> availableCommands;

        public CommandParser(IEnumerable<ICommandFactory> availableCommands)
        {
            this.availableCommands = availableCommands;
        }

        /// <summary>
        /// Takes the input string from the CLI and associates it with a command in the system.
        /// Then, creates an instance of that command type and returns it.
        /// </summary>
        /// <param name="args">The input from the CLI.</param>
        /// <returns>A concrete type</returns>
        public ICommand ParseCommand(string[] args)
        {
            var requestedCommandName = args[0].Trim().ToUpper();

            var command = FindRequestedCommand(requestedCommandName);
            if (null == command)
                return new NotFoundCommand { Name = requestedCommandName };

            return command.MakeCommand(args);
        }

        /// <summary>
        /// Helper method, used to find a command by name.
        /// </summary>
        /// <param name="commandName">The commands name.</param>
        /// <returns>Returns a factory for that command.</returns>
        ICommandFactory FindRequestedCommand(string commandName)
        {
            return availableCommands
                .FirstOrDefault(cmd => cmd.CommandName.ToUpper() == commandName || cmd.Order.ToString() == commandName);
        }
    }
}
