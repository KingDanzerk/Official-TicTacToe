using System;


namespace TicTacToe
{
	public class AI : User 
	{
        public AI(Guid guid, char symbol) : base(guid, symbol)
        {
            Wins = 0;
            PlayerID = guid;
            PlayerSymbol = symbol;
        }
    }
}
