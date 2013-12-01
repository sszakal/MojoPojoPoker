using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI
{
    /// <summary>
    /// Implementation of the command pattern.
    /// Makes use of the factory pattern to create new instances of ICommand. 
    /// Used as a base class for all ICommand concrete implementations.
    /// </summary>
    public abstract class CommandFactoryBase : ICommandFactory
    {
        /// <summary>
        /// The usage descrition of the command and what it does.
        /// </summary>
        public abstract string CommandName { get; }
        /// <summary>
        /// Description for the command. Usage information for the CLI.
        /// </summary>
        public abstract string Description { get; }
        /// <summary>
        /// An index for the command.
        /// It is used by the CLI to order the available commands and
        /// display them to the user.
        /// </summary>
        public abstract int Order { get; }
        /// <summary>
        /// The command factory.
        /// </summary>
        /// <param name="arguments">An array of strings. Provided by the CLI</param>
        /// <returns>An ICommand contrete type.</returns>
        public ICommand MakeCommand(string[] arguments)
        {
            return (ICommand)Activator.CreateInstance(this.GetType());
        }
    }
}
