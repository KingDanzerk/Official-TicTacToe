using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace TicTacToe
{
class Program
{
    static void Main(string[] args)
    {

        GameModes gameMode = new GameModes();
        Displays displays = new Displays();
        UI userInterface = new UI();

        gameMode.DisplayGameModes();
        BoardLogic business = gameMode.GameMode();
        Console.Clear();
        userInterface.PrintBoard();
        
        while (true)
        {

            if (business.CheckGameStatus() != GameStatus.Contiue)
            {
                Console.Clear();
                ConsoleKey AskInput = displays.AskToRestartOrEnd();

                if (AskInput == ConsoleKey.R)
                {
                    Console.Clear();
                    userInterface.PrintBoard();
                    business.ResetGame();
                }

                if (AskInput == ConsoleKey.Enter)
                {
                    Console.WriteLine("Press Enter Again to end!");
                    break;
                }
                
            }

            displays.SetBoardWindowSize();
            displays.DisplayWinsandDraws(business.Player1, business.Player2, business.Draws);
            
            if (!(business.CurrentPlayer is AI))
            {
                userInterface.MoveCursor();

                if (!business.SpaceFilled(userInterface.RealRow, userInterface.RealColumn))
                {
                    userInterface.AddSymbol(business.CurrentPlayer);
                    business.AddSpace(business.CurrentPlayer, userInterface.RealRow, userInterface.RealColumn);
                    business.SwitchPlayer();
                }

                userInterface.resetCoordinates();
            }

            else
            {
                Thread.Sleep(500);
                (int x, int y) = business.AIPlayer.Brain.BestMoveAI(business.returnBoardData(),business.FirstPlayer, business.Pieces, business.ReturnPlayerHistory(business.OppositePlayerOfCurrent));
                userInterface.AddSymbol(business.AIPlayer, x, y);
                business.AddSpace(business.AIPlayer, x, y);
                business.AIPlayer.Brain.SaveLastMoves(x, y);
                business.SwitchPlayer();
            }
            
        }

            Console.Read();
    }
}
}