using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI
{
    /// <summary>
    /// Helper Methods
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Prints all available commands and the description for each.
        /// </summary>
        public static void PrintUsage(this IEnumerable<ICommandFactory> availableCommands)
        {
            Console.WriteLine("Mojo Pojo Poker Usage: CommandName Arguments");
            Console.WriteLine("Commands:");
            foreach (var command in availableCommands)
                Console.WriteLine("  {0}", command.Description);
            Console.WriteLine("Hint: You can also use the command number instead of the command name.");
        }
    }
}
