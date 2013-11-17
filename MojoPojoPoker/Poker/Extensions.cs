using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI.Poker
{
    public static partial class Extensions
    {
        /// <summary>
        /// Generates all combinations possible of lenght K from an group of elemeents. Implemented using recursion.
        /// </summary>
        /// <typeparam name="T">The type of the element, can be reference type or value type.</typeparam>
        /// <param name="elements">the elememnts of the group</param>
        /// <param name="k">The lenght of a combination.</param>
        /// <returns>Returns a sequence of sequences of elements of type T</returns>
        public static IEnumerable<IEnumerable<T>> Combinations<T>(this IEnumerable<T> elements, int k)
        {
            return k == 0 ? new[] { new T[0] } :
              elements.SelectMany((e, i) =>
                elements.Skip(i + 1).Combinations(k - 1).Select(c => (new[] { e }).Concat(c)));
        }

        public static IEnumerable<Card> GetCards(this IDeck deck, int numberOfCards)
        {
            return Enumerable.Range(0, numberOfCards).Select(i => deck.GetCard());
        }

        private static Dictionary<Suits, int> SuitIndex = new Dictionary<Suits, int>() { { Suits.Spades, 0x1000 }, 
                                                                                        { Suits.Hearts, 0x2000 },
                                                                                        { Suits.Diamonds, 0x4000 },
                                                                                        { Suits.Clubs, 0x8000 }};
        public static int GetIndex(this Suits suit)
        {
            return Extensions.SuitIndex[suit];
        }

        private static Dictionary<Values, int> ValueIndex = new Dictionary<Values, int>() { { Values.Two, 0 }, 
                                                                                            { Values.Three, 1 },
                                                                                            { Values.Four, 2 },
                                                                                            { Values.Five, 3 },
                                                                                            { Values.Six, 4 },
                                                                                            { Values.Seven, 5 },
                                                                                            { Values.Eight, 6 },
                                                                                            { Values.Nine, 7 },
                                                                                            { Values.Ten, 8 },
                                                                                            { Values.Jack, 9 },
                                                                                            { Values.Queen, 10 },
                                                                                            { Values.King, 11 },
                                                                                            { Values.Ace, 12 },
                                                                                          };

        public static int GetIndex(this Values value)
        {
            return Extensions.ValueIndex[value];
        }

        private static int[] Primes = { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41 };

        public static int CardIndex(this Card card)
        {
            return Extensions.Primes[card.Value.GetIndex()] | (card.Value.GetIndex() << 8) | card.Suit.GetIndex() | (1 << (16 + card.Value.GetIndex()));
        }

        private static uint FindFast(uint u)
        {
            uint a, b, r;
            u += 0xe91aaa35;
            u ^= u >> 16;
            u += u << 8;
            u ^= u >> 4;
            b = (u >> 8) & 0x1ff;
            a = (u + (u << 2)) >> 19;
            r = a ^ Extensions.HashAdjust[b];
            return r;
        }

        /// <summary>
        /// Cactus Kev algorithm with Paul Senzee Optimization (using a precomputed perfect hash function )
        /// </summary>
        public static int GetValue(this IHand hand)
        {
            return hand.Cards.GetValue();
        }
        public static int GetValue(this IEnumerable<Card> cards)
        {
            if (cards == null)
                throw new NullReferenceException("Empty Hand. Cards instance is null.");
            if (cards.Count() != 5)
                throw new InvalidOperationException("This algorithm works only with 5 card hands");

            var cardIndex = cards.Select(c => c.CardIndex());

            var q = (uint)cardIndex.Aggregate((x, y) => x | y) >> 16;

            /* check for Flushes and StraightFlushes */
            if ((cardIndex.Aggregate((x, y) => x & y) & 0xF000) > 0)
            {
                return Extensions.Flushes[q];
            }

            /* check for Straights and HighCard hands
            */
            var s = Extensions.Unique5[q];
            if (s > 0)
                return (s);

            /* let's do it the hard way
            */
            q = (uint)cardIndex.Select(i => i & 0xFF).Aggregate((x, y) => x * y);
            q = Extensions.FindFast(q);

            return Extensions.HashValues[q];
        }
    }
}
