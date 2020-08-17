using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    public class Player : User
    {
        public Player(Guid guid, char symbol) : base(guid, symbol)
        {
            Wins = 0;
            PlayerID = guid;
            PlayerSymbol = symbol;
        }
    }
}
