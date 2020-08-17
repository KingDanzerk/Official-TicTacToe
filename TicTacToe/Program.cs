using Microsoft.VisualBasic.CompilerServices;
using System;

namespace TicTacToe
{
class Program
{
    static void Main(string[] args)
    {
        Player player1 = new Player(new Guid(), 'X');
        Player player2 = new Player(new Guid(), 'O');
        AI robot = new AI(new Guid(), 'R');

        BoardLogic business = new BoardLogic(robot, player1);
        UI userInterface = new UI();
        AIBrain aiBrain = new AIBrain(business.FirstPlayer, robot, player1);
        userInterface.PrintBoard();

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
                (int x, int y) = aiBrain.BestMoveAI(business.returnBoardData(), business.Pieces);
                userInterface.AddSymbol(robot, x, y);
                business.AddSpace(robot, x, y);
                business.SwitchPlayer();
            }
        }
    }
}
}