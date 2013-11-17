using System;
namespace MojoPojoPoker.CLI.Poker
{
    /// <summary>
    /// Implementation of the Chain of Responsability design pattern.
    /// </summary>
    public interface IPokerHandCalculator
    {
        /// <summary>
        /// Returns the poker rank of the hand.
        /// </summary>
        /// <param name="hand">The player's hand</param>
        /// <param name="pokerHandValue">The Value Index provided by Cactus Kev's algorithm.</param>
        /// <returns></returns>
        IPokerHand GetPokerHand(IHand hand, int pokerHandValue);
        /// <summary>
        /// Reference to the next ranker in the chain
        /// </summary>
        IPokerHandCalculator Next { get; set; }
        /// <summary>
        /// Hand probability. Used by the system to construct the chain in the right direction.
        /// </summary>
        double HandProbability { get; }
    }
}
