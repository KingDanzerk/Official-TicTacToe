using System;


namespace TicTacToe
{
	public class AI : User 
	{
        public AIBrain Brain { get; }
        public AI(Guid guid, char symbol, Player enemy ) : base(guid, symbol)
        {
            Brain = new AIBrain(this, enemy);
            Wins = 0;
            PlayerID = guid;
            PlayerSymbol = symbol;
        }
    }
}
