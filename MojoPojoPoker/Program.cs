using MojoPojoPoker.CLI.Poker;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MojoPojoPoker.CLI
{
    public class Program
    {
        /// <summary>
        /// Gets all the types that implement ICommandFactory 
        /// and returns an a set of instances, an instance of each type found.
        /// </summary>
        static IEnumerable<ICommandFactory> AvailableCommands = Assembly.GetExecutingAssembly().GetTypes().Where(t => !t.IsAbstract
                                                                                                                   && !t.IsInterface
                                                                                                                   && typeof(ICommandFactory).IsAssignableFrom(t))
                                                                                                          .Select(t => (ICommandFactory)Activator.CreateInstance(t))
                                                                                                          .OrderBy(f => f.Order);
        //A very simple IoC Container (Implementation of the Service Locator pattern)
        public static ServiceContainer IoC = new ServiceContainer();
        static Program()
        {
            //USE IFactory<T> IF YOU WANT TO AVOID THE SINGLETOR DEFAULT BEHAVIOUR OF THE ServiceContainer !!!
            Program.IoC.AddType<IDeck, Deck>();
            Program.IoC.AddService<IFactory<IHand>>((s) => new IHandFactory());
            Program.IoC.AddService<ITable>((s) => new Table());
            Program.IoC.AddService<IPokerHandCalculator>( s => {
                var rankers = Assembly.GetExecutingAssembly().GetTypes().Where(t => !t.IsAbstract
                           && !t.IsInterface
                           && typeof(IPokerHandCalculator).IsAssignableFrom(t))
                        .Select(t => (IPokerHandCalculator)Activator.CreateInstance(t)).OrderByDescending(f => f.HandProbability).ToArray();
                for (int i = 0; i < rankers.Length - 1; i++)
                {
                    rankers[i].Next = rankers[i + 1];
                }
                return rankers.First();
            });
        }
        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    Program.AvailableCommands.PrintUsage();
                    if (args.Length != 0 && !string.IsNullOrWhiteSpace(args[0]))
                    {
                        var parser = new CommandParser(Program.AvailableCommands);
                        var command = parser.ParseCommand(args);
                        command.Execute(Program.IoC);
                    }
                    args = Regex.Split(Console.ReadLine(), @"\s+", RegexOptions.Compiled);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("I'm sorry, I cannot continue because of the following error: ");
                Console.WriteLine("\t" + ex.Message);
                Console.WriteLine("Press the enter key to close the program.");
                Console.ReadKey();
            }
        }
    }
}
