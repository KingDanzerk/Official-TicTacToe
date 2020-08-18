using System;

namespace TicTacToe
{
public class User //Base Class for a User
{
    public string Name { get; set; }
    public int Wins { get; set; }
    public Guid PlayerID { get; protected set; }
    public char PlayerSymbol { get; protected set; }

    public User(Guid guid, char symbol)
    {
        Wins = 0;
        PlayerID = guid;
        PlayerSymbol = symbol;
    }
}
}
