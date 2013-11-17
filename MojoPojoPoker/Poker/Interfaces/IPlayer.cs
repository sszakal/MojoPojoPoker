using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MojoPojoPoker.CLI.Poker
{
    /// <summary>
    /// Interface implemented by types
    /// that are used to represent a player of the game.
    /// </summary>
    public interface IPlayer
    {
        /// <summary>
        /// The name of the player.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// The hand associated with the player.
        /// </summary>
        IHand Hand { get; }
    }
}
