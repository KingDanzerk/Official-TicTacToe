using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    public class Displays
    {
        public void DisplayWinsandDraws(User player1, User player2, int draws)
        {
            Console.SetCursorPosition(16, 0);
            Console.WriteLine($"{player1.Name} <{player1.PlayerSymbol}> Wins: {player1.Wins} - Draws: {draws} - {player2.Name} <{player2.PlayerSymbol}> Wins: {player2.Wins}");
        }

        public ConsoleKey AskToRestartOrEnd()
        {
            Console.WriteLine("If youd like to restart match press R, if youd like to end press ENTER");
            ConsoleKeyInfo input = Console.ReadKey();
            return input.Key;
        }
    }
}
