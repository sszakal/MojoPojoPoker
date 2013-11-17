using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI
{
    /// <summary>
    /// Implementation of the Null Object pattern.
    /// </summary>
    public class NotFoundCommand : ICommand
    {
        public string Name { get; set; }
        public void Execute(IServiceContainer serviceProvider)
        {
            Console.WriteLine("Couldn't find command: " + Name);
        }
    }
}
