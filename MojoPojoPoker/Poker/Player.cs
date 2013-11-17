using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;

namespace MojoPojoPoker.CLI.Poker
{
    public class Player: IPlayer
    {
        public Player(string name, IServiceContainer serviceProvider)
        {
            this.Name = name;
            var handFactory = serviceProvider.GetService<IFactory<IHand>>();
            this.Hand = (IHand)handFactory.GetInstance();
        }
        public string Name { get; private set; }
        public IHand Hand { get; private set; }

    }
}
