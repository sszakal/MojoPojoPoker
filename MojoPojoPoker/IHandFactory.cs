using MojoPojoPoker.CLI.Poker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI
{
    /// <summary>
    /// Used as workaround to the limitations of the ServiceContainer.
    /// </summary>
    public class IHandFactory: IFactory<IHand>
    {
        public object GetInstance()
        {
            return new Hand();
        }
    }
}
