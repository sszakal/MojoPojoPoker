using System;
using System.Collections.Generic;
namespace MojoPojoPoker.CLI.Poker
{
    /// <summary>
    /// Interface implemented by types
    /// that are used to represent a set of cards 
    /// which the user receives during game play.
    /// </summary>
    public interface IHand
    {
        /// <summary>
        /// The set of card the user posseses
        /// </summary>
        SortedSet<Card> Cards { get; }
        /// <summary>
        /// Removes all the cards from the user's hand
        /// </summary>
        void Clear();
    }
}
