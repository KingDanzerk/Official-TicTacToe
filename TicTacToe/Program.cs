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
        Player player1 = new Player(new Guid(), '$'); 
        Player player2 = new Player(new Guid(), '%');
        AI robot = new AI(new Guid(), 'R');

        BoardLogic business = new BoardLogic(player1, robot); //First Player is always the first argument. 
        UI userInterface = new UI();
        AIBrain aiBrain = new AIBrain(business.FirstPlayer, robot, player1); //Enter the right player if use of AI is wanted.
        userInterface.PrintBoard();
        User[,] yeet = business.boardData;

        while (business.CheckGameStatus() == GameStatus.Contiue)
        {
            if (business.CurrentPlayer != robot)
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
                (int x, int y) = aiBrain.BestMoveAI(business.returnBoardData(), business.Pieces, business.ReturnPlayerHistory(business.OppositePlayerOfCurrent));
                userInterface.AddSymbol(robot, x, y);
                business.AddSpace(robot, x, y);
                aiBrain.SaveLastMoves(x, y);
                business.SwitchPlayer();
            }
        }

            Console.Read();
    }
}
}