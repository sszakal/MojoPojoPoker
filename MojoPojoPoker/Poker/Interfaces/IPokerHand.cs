using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MojoPojoPoker.CLI.Poker
{
    /// <summary>
    /// Interface impemented by poker hand rankers 
    /// But used only when associated with a player's hand (IHand)
    /// </summary>
    public interface IPokerHand
    {
        /// <summary>
        /// The Value Index (calculated using Cactus Kev Algorithm 
        /// (with perfect hash function optimization)
        /// </summary>
        int PokerHandValue { get; set; }
        /// <summary>
        /// The associated player hand.
        /// </summary>
        IHand Hand { get; set; }
        /// <summary>
        /// The name of the Poker hand (flush, straight, two pairs, etc)
        /// </summary>
        string Name { get; }
    }
}