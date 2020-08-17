using System;

namespace TicTacToe
{
public class UI
{
    private int _row = 0;
    private int _column = 0;
    public int RealColumn
    {
        get
        {
            if (_column != 0)
            {
                return _column / 5;
            }

            else
            {
                return 0;
            }
        }
    }
    public int RealRow
    {
        get
        {
            if (_row != 0)
            {
                return _row / 2;
            }

            else
            {
                return 0;
            }

        }
    } 

    public void PrintBoard()
    {
        string board =
        "   ║   ║   " + Environment.NewLine +
        "═══╬═══╬═══" + Environment.NewLine +
        "   ║   ║   " + Environment.NewLine +
        "═══╬═══╬═══" + Environment.NewLine +
        "   ║   ║   ";
        Console.WriteLine(board);
    }

    public void AddSymbol(User currentPlayer) //If this is for player
    {
        Console.SetCursorPosition(_column, _row);
        Console.Write(currentPlayer.PlayerSymbol);
    }

    public void AddSymbol(User currentPlayer, int x, int y) //If you have "RealCoordinates" for AI
    {
        Console.SetCursorPosition(y * 5, x * 2);
        Console.WriteLine(currentPlayer.PlayerSymbol);
    }

    public void MoveCursor()
    {
        Console.SetCursorPosition(_column, _row);

        while (true)
        {
            ConsoleKeyInfo input = Console.ReadKey(true);

            if (input.Key == ConsoleKey.RightArrow)
            {
                _column = _column == 10 ? 0 : _column + 5;
                Console.SetCursorPosition(_column, _row);

            }

            if (input.Key == ConsoleKey.DownArrow)
            {
                _row = _row == 4 ? 0 : _row + 2;
                Console.SetCursorPosition(_column, _row);
            }

            if (input.Key == ConsoleKey.LeftArrow)
            {
                _column = _column == 0 ? 10 : _column - 5;
                Console.SetCursorPosition(_column, _row);
            }

            if (input.Key == ConsoleKey.UpArrow)
            {
                _row = _row == 0 ? 4 : _row - 2;
                Console.SetCursorPosition(_column, _row);

            }

            if (input.Key == ConsoleKey.Enter)
            {
                break;
            }
        }
    }//Allows Movement for a Player with Keyboard

    public void resetCoordinates() //Resets the the row 
    {
        _row = 0;
        _column = 0;
    }
}
}
