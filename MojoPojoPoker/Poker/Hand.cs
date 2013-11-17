using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI.Poker
{
    /// <summary>
    /// Class used to represent a set of Cards that a Player would receive within a card game.
    /// </summary>
    public class Hand : IHand
    {
       public SortedSet<Card> Cards { get; private set; }
       public Hand()
       {
           this.Cards = new SortedSet<Card>(CardComparer.Instance);
       }
       public void Clear()
       {
           this.Cards = new SortedSet<Card>(CardComparer.Instance);
       }
    }
}
