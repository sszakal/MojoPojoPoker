using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI.Poker
{
    public abstract class PokerGameSimulatorBase : IPokerGameSimulator
    {
        protected IServiceContainer ServiceProvider { get; set; }
        public PokerGameSimulatorBase(IServiceContainer serviceProvider)
        {
            this.ServiceProvider = serviceProvider;
            this.Players = new List<IPlayer>();
            this.Deck = (IDeck)serviceProvider.GetService<IDeck>();
            this.Table = (ITable)serviceProvider.GetService<ITable>();
            this.Calculator = (IPokerHandCalculator)serviceProvider.GetService<IPokerHandCalculator>();
            this.Ranking = new SortedList<IPokerHand, IPlayer>(PokerHandComparer.Instance);
            this.Winners = new List<IPlayer>();
        }
        public ITable Table { get; private set; }
        public IDeck Deck { get; private set; }
        public IList<IPlayer> Players { get; private set; }
        public SortedList<IPokerHand, IPlayer> Ranking { get; private set; }
        public IList<IPlayer> Winners { get; private set; }
        public IPokerHandCalculator Calculator { get; private set; }
        public abstract int PlayerNumberOfCards();
        protected void DealCardsToPlayers()
        {
            foreach (var player in this.Players)
            {
                foreach (var card in Deck.GetCards(this.PlayerNumberOfCards()))
                {
                    player.Hand.Cards.Add(card);
                }
            }
        }
        /// <summary>
        /// Used by subclasses to hook inside the template method.
        /// </summary>
        protected virtual void TableSetup() { }
        /// <summary>
        /// Used by subclasses to hook inside the template method.
        /// </summary>
        protected virtual void PlayRounds() { }
        /// <summary>
        /// Calculates the winner(s) of the game.
        /// </summary>
        protected virtual void CalculateWinners()
        {
            foreach (var player in this.Players)
            {
                this.Ranking.Add(this.Calculator.GetPokerHand(player.Hand, player.Hand.GetValue()), player);
            }
            var min = this.Ranking.Min(r => r.Key.PokerHandValue);
            foreach (var player in this.Ranking.Where(r => r.Key.PokerHandValue == min))
            {
                this.Winners.Add(player.Value);
            }
        }

        /// <summary>
        /// Resets the simulator, to play again.
        /// </summary>
        public void Reset()
        {
            foreach (var player in this.Players)
            {
                player.Hand.Clear();
            }
            this.Deck.Reset();
            this.Table.Clear();
        }

        /// <summary>
        /// Implementation of the Template Method pattern.
        /// </summary>
        public void PlayGame()
        {
            if (this.Players.Count() < 2)
                throw new InvalidOperationException("Minimum of two players are required");
            this.DealCardsToPlayers();
            this.TableSetup();
            this.PlayRounds();
            this.CalculateWinners();
        }
    }
}
