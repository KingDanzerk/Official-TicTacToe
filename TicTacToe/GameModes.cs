using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    class GameModes
    {
        public BoardLogic GameMode()
        {
            Player player1 = new Player(new Guid(), 'X');
            Player player2 = new Player(new Guid(), 'Y');
            AI robot = new AI(new Guid(), 'R', player1);

            string input = Console.ReadLine();

            while (input != "1" && input != "2")
            {
                Console.WriteLine("Enter a gamemode!" );
                input = Console.ReadLine();
            }

            if (input == "1")
            {
               
                char player1Symb = AskForSymbol("Player 1", player1);
                player1 = new Player(new Guid(), player1Symb);
                AskForName("Player 1", player1);
               
                char player2Symb = AskForSymbol("Player 2", player2);
                player2 = new Player(new Guid(), player2Symb);
                AskForName("Player 2", player2);

                BoardLogic gameMode1 = new BoardLogic(player1, player2);

                return gameMode1;
            }

            if (input == "2")
            {
                
                char player1Symb = AskForSymbol("Player 1", player1);
                player1 = new Player(new Guid(), player1Symb);
                AskForName("Player 1", player1);

                robot = new AI(new Guid(), 'R', player1);

            }

            Console.Clear();
            
            BoardLogic gameMode2 = new BoardLogic(player1, robot);
            return gameMode2;
        }

        public void AskForName(string playerName, User Player)
        {
            Console.WriteLine($"Enter a name for {playerName}");
            string input = Console.ReadLine();
            Player.Name = input;
            
        }

        public char AskForSymbol(string playerName, User Player)
        {
            Console.WriteLine($"Enter a symbol for {playerName}");
            string input = Console.ReadLine();
            char symbol;

            while (char.TryParse(input, out symbol) == false)
            {
                Console.WriteLine("Enter a actual symbol, one character!");
                input = Console.ReadLine();
            }

            return symbol;
        }
    }
}
