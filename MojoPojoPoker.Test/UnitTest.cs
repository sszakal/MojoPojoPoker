using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MojoPojoPoker;
using MojoPojoPoker.CLI;
using MojoPojoPoker.CLI.Poker;
using System.ComponentModel.Design;
using System.Reflection;
using System.Linq;
using Moq;
using System.Collections.Generic;

namespace MojoPojoPoker.Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void CommandParser_GetCommandByName()
        {
            //Arange 
            var iCommand1 = new Mock<ICommand>().Object;
            var iCommand2 = new Mock<ICommand>().Object;
            var iCommandFactory1 = new Mock<ICommandFactory>();
            iCommandFactory1.Setup(x => x.CommandName).Returns("command1");
            iCommandFactory1.Setup(x => x.MakeCommand(It.IsAny<string[]>())).Returns(iCommand1);
            var iCommandFactory2 = new Mock<ICommandFactory>();
            iCommandFactory2.Setup(x => x.CommandName).Returns("command2");
            iCommandFactory2.Setup(x => x.MakeCommand(It.IsAny<string[]>())).Returns(iCommand2);
            var availableCommands = new ICommandFactory[] { iCommandFactory1.Object, iCommandFactory2.Object }.AsEnumerable();
            var parser = new CommandParser(availableCommands);
            //Act
            var command1 = parser.ParseCommand(new string[] { "command1" });
            var command2 = parser.ParseCommand(new string[] { "command2" });
            var command3 = parser.ParseCommand(new string[] { "not found" });
            //Assert
            Assert.AreSame(iCommand1, command1);
            Assert.AreSame(iCommand2, command2);
            Assert.AreSame(command3.GetType(), typeof(NotFoundCommand));
        }
        [TestMethod]
        public void PokerHandEvaluator_EvaluateAllPossiblePokerHand()
        {
            //Arange 
            var pokerHandEvaluator = (IPokerHandCalculator)Program.IoC.GetService<IPokerHandCalculator>();
            var frequency = new Dictionary<Type, int>();
            var tempPokerHandRanker = pokerHandEvaluator;
            do
            {
                frequency.Add(tempPokerHandRanker.GetType(), 0);
                tempPokerHandRanker = tempPokerHandRanker.Next;
            } while (tempPokerHandRanker.Next != null);
            frequency.Add(typeof(StraightFlushRank), 0);
            //Act
            var all5HandCombinations = new Deck().GetCards(52).ToList().Combinations(5);
            
            foreach (var combination in all5HandCombinations)
            {
                var hand = new Hand();
                foreach (var card in combination)
                    hand.Cards.Add(card);
                var handValue = combination.GetValue();
                var pokerHand = pokerHandEvaluator.GetPokerHand(hand, handValue);
                frequency[pokerHand.GetType()]++;
            }
            //Assert
            Assert.AreEqual(2598960, all5HandCombinations.Count());
            Assert.AreEqual(9,frequency.Count);
            Assert.AreEqual(1302540, frequency[typeof(HighCardRank)]);
            Assert.AreEqual(1098240, frequency[typeof(OnePairRank)]);
            Assert.AreEqual(123552, frequency[typeof(TwoPairRank)]);
            Assert.AreEqual(54912, frequency[typeof(ThreeOfAKindRank)]);
            Assert.AreEqual(10200, frequency[typeof(StraightRank)]);
            Assert.AreEqual(5108, frequency[typeof(FlushRank)]);
            Assert.AreEqual(3744, frequency[typeof(FullHouseRank)]);
            Assert.AreEqual(624, frequency[typeof(PokerRank)]);
            Assert.AreEqual(40, frequency[typeof(StraightFlushRank)]);
        }
        [TestMethod]
        public void PokerGameSimulator()
        {
            //Arange 
            var IoC = new ServiceContainer();
            #region Setup Ranking Chain
            IoC.AddService<IPokerHandCalculator>(s =>
            {
                return Program.IoC.GetService<IPokerHandCalculator>();
            });
            #endregion
            var deck = new Deck();
            var table = new Table();
            IoC.AddService<IDeck>(s => deck);
            IoC.AddService<ITable>(s => table);

            var hand1 = new Mock<IHand>();
            var hand2 = new Mock<IHand>();
            var hand1Cards = new SortedSet<Card>(CardComparer.Instance);
            var hand2Cards = new SortedSet<Card>(CardComparer.Instance);
            hand1.Setup(x => x.Cards).Returns(hand1Cards);
            hand2.Setup(x => x.Cards).Returns(hand2Cards);
            var iHandFactory = new Mock<IFactory<IHand>>();
            var numberOfHandInstancesCreated = 0;
            iHandFactory.Setup(x => x.GetInstance()).Returns(() =>
                {
                    if (numberOfHandInstancesCreated == 0)
                    {
                        numberOfHandInstancesCreated++;
                        return hand1.Object;
                    }
                    else if (numberOfHandInstancesCreated == 1)
                    {
                        numberOfHandInstancesCreated++;
                        return hand2.Object;
                    }
                    numberOfHandInstancesCreated++;
                    return new Hand();
                });
            IoC.AddService<IFactory<IHand>>(s => iHandFactory.Object);

            var simulator = new PokerGameSimulator(IoC);
            var player1 = new Player("Player One", IoC);
            var player2 = new Player("Player Tne", IoC);
            simulator.Players.Add(player1);
            simulator.Players.Add(player2);
            //Act
            simulator.PlayGame();
            //Assert
            Assert.AreEqual(2, numberOfHandInstancesCreated);
            Assert.AreEqual(5, hand1Cards.Count());
            Assert.AreEqual(5, hand2Cards.Count());
            Assert.AreEqual(0, table.Cards.Count());
            Assert.IsNotNull(simulator.Players);
            Assert.AreEqual(2, simulator.Players.Count());
            Assert.IsNotNull(simulator.Ranking);
            Assert.AreEqual(2, simulator.Ranking.Count());
            Assert.IsNotNull(simulator.Winners);
        }
        [TestMethod]
        public void HoldemGameSimulator()
        {
            //Arange 
            var IoC = new ServiceContainer();
            #region Setup Ranking Chain
            IoC.AddService<IPokerHandCalculator>(s =>
            {
                return Program.IoC.GetService<IPokerHandCalculator>();
            });
            #endregion
            var deck = new Deck();
            var table = new Table();
            IoC.AddService<IDeck>(s => deck);
            IoC.AddService<ITable>(s => table);

            var hand1 = new Mock<IHand>();
            var hand2 = new Mock<IHand>();
            var hand1Cards = new SortedSet<Card>(CardComparer.Instance);
            var hand2Cards = new SortedSet<Card>(CardComparer.Instance);
            hand1.Setup(x => x.Cards).Returns(hand1Cards);
            hand2.Setup(x => x.Cards).Returns(hand2Cards);
            var iHandFactory = new Mock<IFactory<IHand>>();
            var numberOfHandInstancesCreated = 0;
            iHandFactory.Setup(x => x.GetInstance()).Returns(() =>
            {
                if (numberOfHandInstancesCreated == 0)
                {
                    numberOfHandInstancesCreated++;
                    return hand1.Object;
                }
                else if (numberOfHandInstancesCreated == 1)
                {
                    numberOfHandInstancesCreated++;
                    return hand2.Object;
                }
                numberOfHandInstancesCreated++;
                return new Hand();
            });
            IoC.AddService<IFactory<IHand>>(s => iHandFactory.Object);

            var simulator = new HoldemGameSimulator(IoC);
            var player1 = new Player("Player One", IoC);
            var player2 = new Player("Player Tne", IoC);
            simulator.Players.Add(player1);
            simulator.Players.Add(player2);
            //Act
            simulator.PlayGame();
            //Assert
            Assert.AreEqual(4, numberOfHandInstancesCreated);
            Assert.AreEqual(2, hand1Cards.Count());
            Assert.AreEqual(2, hand2Cards.Count());
            Assert.AreEqual(5, table.Cards.Count());
            Assert.IsNotNull(simulator.Players);
            Assert.AreEqual(2, simulator.Players.Count());
            Assert.IsNotNull(simulator.Ranking);
            Assert.AreEqual(2, simulator.Ranking.Count());
            Assert.IsNotNull(simulator.Winners);
        }
    }
}
