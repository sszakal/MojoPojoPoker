using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI
{
    /// <summary>
    /// Implementation of the command pattern.
    /// Makes use if the factory pattern to create new instances of ICommand. 
    /// </summary>
    public interface ICommandFactory
    {
        /// <summary>
        /// The command name. Used in the CLI to invoke the command.
        /// </summary>
        string CommandName { get; }
        /// <summary>
        /// The usage descrition of the command and what it does.
        /// </summary>
        string Description { get; }
        /// <summary>
        /// An index for the command.
        /// It is used by the CLI to order the available commands and
        /// display them to the user.
        /// </summary>
        int Order { get; }
        /// <summary>
        /// The command factory.
        /// </summary>
        /// <param name="arguments">An array of strings. Provided by the CLI</param>
        /// <returns>An ICommand contrete type.</returns>
        ICommand MakeCommand(string[] arguments);
    }
}
