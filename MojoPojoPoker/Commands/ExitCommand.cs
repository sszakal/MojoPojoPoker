using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI
{
    public class ExitCommand : CommandFactoryBase, ICommand
    {
        public void Execute(IServiceContainer serviceProvider)
        {
            Console.WriteLine("Hope to see you again. Byeee");
            Task.Factory.StartNew(() => Thread.Sleep(5000)).ContinueWith((t) => Environment.Exit(0), TaskScheduler.Default);
        }
        public override string CommandName { get { return "Exit"; } }
        public override string Description { get { return this.Order + ". " + this.CommandName + " - Use this command to close the program"; } }
        public override int Order { get { return 3; } }
    }
}
