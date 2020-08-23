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
        BoardLogic business = gameMode.GameMode();
        UI userInterface = new UI();
        userInterface.PrintBoard();

        while (true)
        {

            if (business.CheckGameStatus() != GameStatus.Contiue)
            {
                Console.Clear();
                userInterface.PrintBoard();
                business.ResetGame();
            }


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
                Thread.Sleep(1000);
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