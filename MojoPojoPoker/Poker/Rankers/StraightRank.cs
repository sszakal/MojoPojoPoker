using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI.Poker
{
    /// <summary>
    /// Used in the chain of responsability to check if the users hand has a straigh.
    /// A straight is a poker hand that contains five cards of sequential rank in at least two different suits.
    /// </summary>
    public sealed class StraightRank : PokerHandCalculator, IPokerHand
    {
        IHand IPokerHand.Hand { get; set; }
        int IPokerHand.PokerHandValue { get; set; }
        public override IPokerHand GetPokerHand(IHand hand, int pokerHandValue)
        {
            if (pokerHandValue > 1599)
            {
                var pokerHand = (IPokerHand)this;
                pokerHand.Hand = hand;
                pokerHand.PokerHandValue = pokerHandValue;
                return (IPokerHand)this.MemberwiseClone();
            }
            return this.Next.GetPokerHand(hand, pokerHandValue);
        }
        public override double HandProbability { get { return 0.39; } }

        public string Name
        {
            get { return "Straight"; }
        }
    }
}
