using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TicTacToe
{
    public class Menu
    {
        string[] gameModes = new string[2] { "Player vs Player", "Player vs AI" };

        public void PrintMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to TicTacToe!");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("Choose a gamemode!");
            Console.WriteLine("\n");
            Thread.Sleep(1000);
            Console.WriteLine("....");
            Thread.Sleep(1000);
        }
    }
}
