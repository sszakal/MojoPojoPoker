using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI.Poker
{
    /// <summary>
    /// Used in the chain of responsability to check if the users hand has a straigh flush.
    /// A straight flush is a hand that contains five cards in sequence, all of the same suit
    /// </summary>
    public sealed class StraightFlushRank : PokerHandCalculator, IPokerHand
    {
        IHand IPokerHand.Hand { get; set; }
        int IPokerHand.PokerHandValue { get; set; }
        public override IPokerHand GetPokerHand(IHand hand, int pokerHandValue)
        {
            var pokerHand = (IPokerHand)this;
            pokerHand.Hand = hand;
            pokerHand.PokerHandValue = pokerHandValue;
            return (IPokerHand)this.MemberwiseClone();
        }
        public override double HandProbability { get { return 0.0015; } }
        string IPokerHand.Name
        {
            get { return "Straight Flush"; }
        }
    }
}
