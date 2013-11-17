using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI.Poker
{
    /// <summary>
    /// Used in the chain of responsability to check if the users hand has a three of a kind hand.
    /// Three of a kind, also called trips or a set, that contains three cards of the same rank, 
    /// plus two cards which are not of this rank nor the same as each other. 
    /// </summary>
    public sealed class ThreeOfAKindRank : PokerHandCalculator, IPokerHand
    {
        IHand IPokerHand.Hand { get; set; }
        int IPokerHand.PokerHandValue { get; set; }
        public override IPokerHand GetPokerHand(IHand hand, int pokerHandValue)
        {
            if (pokerHandValue > 1609)
            {
                var pokerHand = (IPokerHand)this;
                pokerHand.Hand = hand;
                pokerHand.PokerHandValue = pokerHandValue;
                return (IPokerHand)this.MemberwiseClone();
            }
            return this.Next.GetPokerHand(hand, pokerHandValue);
        }
        public override double HandProbability { get { return 2.11; } }
        string IPokerHand.Name
        {
            get { return "Three Of A Kind"; }
        }
    }
}
