using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI.Poker
{
    /// <summary>
    /// Used in the chain of responsability to check if the users hand has a flush.
    /// A flush is a poker hand where all five cards are of the same suit
    /// </summary>
    public sealed class FlushRank : PokerHandCalculator, IPokerHand
    {
        IHand IPokerHand.Hand { get; set; }
        int IPokerHand.PokerHandValue { get; set; }
        public override IPokerHand GetPokerHand(IHand hand, int pokerHandValue)
        {
            if (pokerHandValue > 322)
            {
                var pokerHand = (IPokerHand)this;
                pokerHand.Hand = hand;
                pokerHand.PokerHandValue = pokerHandValue;
                return (IPokerHand)this.MemberwiseClone();
            }
            return this.Next.GetPokerHand(hand, pokerHandValue);
        }
        public override double HandProbability { get { return 0.196; } }
        string IPokerHand.Name
        {
            get { return "Flush"; }
        }
    }
}
