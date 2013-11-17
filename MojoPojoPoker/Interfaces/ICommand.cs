using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI
{
    /// <summary>
    /// Implementation of the command patterns
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// This method when called executes the command.
        /// </summary>
        /// <param name="serviceProvider">An IoC container -> dependency injection.</param>
        void Execute(IServiceContainer serviceProvider);
    }
}
