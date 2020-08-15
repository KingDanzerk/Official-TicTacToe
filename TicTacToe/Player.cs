using System;

namespace TicTacToe
{
public class Player
{
    public string Name { get; set; }
    public int Wins { get; set; }
    public Guid PlayerID { get; }
    public char PlayerSymbol { get; }

    public Player(Guid guid, char symbol)
    {
        Wins = 0;
        PlayerID = guid;
        PlayerSymbol = symbol;
    }
}
}
