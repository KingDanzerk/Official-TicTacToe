using System;

namespace TicTacToe
{
class Program
{
    static void Main(string[] args)
    {
        Player player1 = new Player(new Guid(), 'X');
        Player player2 = new Player(new Guid(), 'Z');
        BoardLogic business = new BoardLogic(player1, player2);
        UI userInterface = new UI();
        userInterface.PrintBoard();
            
        while (business.CheckGameStatus() == GameStatus.Contiue)
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
    }
}
}