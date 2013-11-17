using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI.Poker
{
    public class PokerGameSimulator : PokerGameSimulatorBase
    {
        public PokerGameSimulator(IServiceContainer serviceProvider)
            : base(serviceProvider)
        {
        }
        public override int PlayerNumberOfCards()
        {
            return 5; 
        }
    }
}
