using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MojoPojoPoker.CLI.Poker
{
    /// <summary>
    /// Interface implemented by types
    /// that are used to represent a set of cards 
    /// which the users can share/have in common.
    /// </summary>
    public interface ITable: IHand
    {
        //same interface as IHand.
    }
}
