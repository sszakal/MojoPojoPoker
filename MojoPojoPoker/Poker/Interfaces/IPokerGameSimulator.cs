using System;
using System.Collections.Generic;
namespace MojoPojoPoker.CLI.Poker
{
    /// <summary>
    /// Interfaced by all poker game simulators.
    /// </summary>
    interface IPokerGameSimulator
    {
        /// <summary>
        /// The deck of cards used in the simulation.
        /// </summary>
        IDeck Deck { get;}
        /// <summary>
        /// The Poker Hand Evaluator. 
        /// Organizes poker hands by rank and determins the winners.
        /// </summary>
        IPokerHandCalculator Calculator { get; }
        /// <summary>
        /// The table cards.
        /// </summary>
        ITable Table { get; }
        /// <summary>
        /// A list of all the players in the game.
        /// </summary>
        IList<IPlayer> Players { get; }
        /// <summary>
        /// A sorted list of players and associated poker hand ordered by poker hand rank.
        /// The list is null, gets initialized only after the games is player.
        /// </summary>
        SortedList<IPokerHand, IPlayer> Ranking { get; }
        /// <summary>
        /// A list with the player that won the current game.
        /// </summary>
        IList<IPlayer> Winners { get; }
        /// <summary>
        /// Plays one game. Follows the template method patterns.
        /// </summary>
        void PlayGame();
        /// <summary>
        /// Clears all player hands, the table and rests and shufles the deck.
        /// </summary>
        void Reset();
    }
}
