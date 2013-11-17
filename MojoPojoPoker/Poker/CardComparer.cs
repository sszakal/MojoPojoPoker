using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI.Poker
{
    /// <summary>
    /// Class used by other components to sort Cards. The class implements the Lazy-Singleton pattern.
    /// </summary>
    public class CardComparer : IComparer<Card>
    {
        /// <summary>
        /// Private constructor used to force user to get an instance of this class using the static property.
        /// </summary>
        private CardComparer()
        {
        }

        /// <summary>
        /// The one and only instance of this class.
        /// </summary>
        public static CardComparer Instance
        {
            get { return Nested.instance; }
        }

        /// <summary>
        /// Hidden class, gets initiallized when its first time accessed. (Lazy Loading) 
        /// </summary>
        private class Nested
        {
            static Nested()
            {
            }

            internal static readonly CardComparer instance = new CardComparer();
        }

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than,
        /// equal to, or greater than the other.
        /// </summary>
        /// <param name="x">The first object to compare.</param>
        /// <param name="y">The second object to compare.</param>
        /// <returns></returns>
        public int Compare(Card x, Card y)
        {
            var valX = (int)x.Value * 4 + (int)x.Suit;
            var valY = (int)y.Value * 4 + (int)y.Suit;
            return valX - valY;
        }
    }
}
