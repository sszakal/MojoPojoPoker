using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI.Poker
{
    /// <summary>
    /// Used in the chain of responsability to check if the users hand has a Full House.
    /// Full House is a hand that contains three matching cards of one rank 
    /// and two matching cards of another rank.
    /// </summary>
    public sealed class FullHouseRank : PokerHandCalculator, IPokerHand
    {
        IHand IPokerHand.Hand { get; set; }
        int IPokerHand.PokerHandValue { get; set; }
        public override IPokerHand GetPokerHand(IHand hand, int pokerHandValue)
        {
            if (pokerHandValue > 166)
            {
                var pokerHand = (IPokerHand)this;
                pokerHand.Hand = hand;
                pokerHand.PokerHandValue = pokerHandValue;
                return (IPokerHand)this.MemberwiseClone();
            }
            return this.Next.GetPokerHand(hand, pokerHandValue);
        }
        public override double HandProbability { get { return 0.1441; } }
        string IPokerHand.Name
        {
            get { return "Full House"; }
        }
    }
}
