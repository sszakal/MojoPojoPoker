using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI.Poker
{
    /// <summary>
    /// Used in the chain of responsability to check if the users hand is a high card hand.
    /// A high-card or no-pair hand is a poker hand made of any five cards not meeting 
    /// any requirments of any higher rank.
    /// Its the lowest rank in poker and is used a starting point 
    /// of the chain.
    /// </summary>
    public sealed class HighCardRank : PokerHandCalculator, IPokerHand
    {
        IHand IPokerHand.Hand { get; set; }
        int IPokerHand.PokerHandValue { get; set; }
        public override IPokerHand GetPokerHand(IHand hand, int pokerHandValue)
        {
            if (pokerHandValue > 6185)
            {
                var pokerHand = (IPokerHand)this;
                pokerHand.Hand = hand;
                pokerHand.PokerHandValue = pokerHandValue;
                return (IPokerHand)this.MemberwiseClone();
            }
            return this.Next.GetPokerHand(hand, pokerHandValue);
        }

        public override double HandProbability { get { return 50.11; } }

        public string Name
        {
            get { return "High Card"; }
        }
    }
}
