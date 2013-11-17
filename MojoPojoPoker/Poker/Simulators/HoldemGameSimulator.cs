using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI.Poker
{
    public class HoldemGameSimulator : PokerGameSimulatorBase
    {
        public HoldemGameSimulator(IServiceContainer serviceProvider)
            : base(serviceProvider)
        {
        }
        public override int PlayerNumberOfCards()
        {
            return 2;
        }

        protected override void TableSetup() {
            foreach (var card in Deck.GetCards(3))
            {
                this.Table.Cards.Add(card);
            }
        }

        protected override void PlayRounds()
        { 
            //round 1
            this.Table.Cards.Add(this.Deck.GetCard());
            //round 2
            this.Table.Cards.Add(this.Deck.GetCard());
        }

        protected override void CalculateWinners()
        {
            var handFactory = this.ServiceProvider.GetService<IFactory<IHand>>();
            foreach (var player in this.Players)
            {
                //get best hand of player
                var allHands = this.Table.Cards.Union(player.Hand.Cards).Combinations(5).Select(c => new { Cards = c, Value = c.GetValue() });
                var bestIndex = allHands.Min(c => c.Value);
                var bestCards = allHands.First(c => c.Value == bestIndex).Cards;
                IHand bestHand = (IHand)handFactory.GetInstance();
                foreach (var card in bestCards)
                {
                    bestHand.Cards.Add(card);
                }
                this.Ranking.Add(this.Calculator.GetPokerHand(bestHand, bestIndex), player);
            }
            var min = this.Ranking.Min(r => r.Key.PokerHandValue);
            foreach (var player in this.Ranking.Where(r => r.Key.PokerHandValue == min))
            {
                this.Winners.Add(player.Value);
            }
        }
    }
}
