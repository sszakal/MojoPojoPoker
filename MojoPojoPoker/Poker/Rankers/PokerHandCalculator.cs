using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI.Poker
{
    /// <summary>
    /// Implementation of the Chain of Responsability design pattern.
    /// </summary>
    public abstract class PokerHandCalculator : IPokerHandCalculator
    {
        public abstract IPokerHand GetPokerHand(IHand hand, int pokerHandValue);
        public IPokerHandCalculator Next { get; set; }
        public abstract double HandProbability { get; }
    }
}
