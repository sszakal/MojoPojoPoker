using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MojoPojoPoker.CLI.Poker
{
    public class PokerHandComparer : IComparer<IPokerHand>
    {
        /// <summary>
        /// Private constructor used to force user to get an instance of this class using the static property.
        /// </summary>
        private PokerHandComparer()
        {
        }

        /// <summary>
        /// The one and only instance of this class.
        /// </summary>
        public static PokerHandComparer Instance
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

            internal static readonly PokerHandComparer instance = new PokerHandComparer();
        }
        public int Compare(IPokerHand x, IPokerHand y)
        {
            return x.PokerHandValue - y.PokerHandValue;
        }
    }
}
