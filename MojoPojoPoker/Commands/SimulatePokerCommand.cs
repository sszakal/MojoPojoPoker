using MojoPojoPoker.CLI.Poker;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI
{
    /// <summary>
    /// Command used to run a simulated 5 poker card game. (classic poker)
    /// </summary>
    public class SimulaterPokerCommand : CommandFactoryBase, ICommand
    {
        public void Execute(IServiceContainer serviceProvider)
        {
            Console.Clear();
            Console.WriteLine("Simulate a 5 cards poker game");
            Console.Write("Initializing Game Simulator...");
            IPokerGameSimulator simulator = new PokerGameSimulator(serviceProvider);
            simulator.Players.Add(new Player("Player One", serviceProvider));
            simulator.Players.Add(new Player("Player Two", serviceProvider));
            Console.WriteLine("Done");
            
            Console.WriteLine();
            Console.WriteLine();

            simulator.PlayGame();
            foreach(var player in simulator.Players)
            {
                Console.WriteLine();
                Console.WriteLine(player.Name +" :");
                Console.WriteLine(string.Join(", ", player.Hand.Cards.OrderByDescending(c => (int)c.Value).Select(c => c.Value + " of " + c.Suit)));
                //Console.WriteLine(string.Join(", ", player.Hand.Cards.OrderByDescending(c => (int)c.Value).Select(c => c.Suit.ToString().First() + c.Value.ToString())));
            }

            Console.WriteLine();
            Console.WriteLine("Ranking: ");

            foreach(var place in simulator.Ranking)
            {
                Console.WriteLine();
                Console.WriteLine(place.Value.Name + " - " + place.Key.Name + (simulator.Winners.Contains(place.Value) ? " (Won)" : ""));
            }

            simulator.Reset();
        }
        public override string CommandName { get { return "SimPoker"; } }
        public override string Description { get { return this.Order + ". " + this.CommandName + " - Simulate a 5 cards poker game"; } }
        public override int Order { get { return 1; } }
    }
}
