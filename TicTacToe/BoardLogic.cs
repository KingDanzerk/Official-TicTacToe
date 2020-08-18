using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text;

namespace TicTacToe
{
    
public enum GameStatus
{
    Contiue = 0,
    Win = 1,
    Draw = 2,
}

    public class BoardLogic //Board Data is retrieved from here
    {
        private User _player1;
        private User _player2;
        public User[,] boardData;
        private int _spaceFilled = 0;
        private int _draws = 0;
        private User _currentPlayer;
        private List<(int x, int y)> _playerHistory = new List<(int x, int y)>();
        public List<(int x, int y)> PlayerHistory { get { return _playerHistory; }}
        
        public (int x, int y) PlayerLastMove { get; protected set; }
        
        public User FirstPlayer { get; protected set; }
    
        public User CurrentPlayer { get { return _currentPlayer; } }

        public int Pieces { get { return _spaceFilled; } }
    
        public User OppositePlayerOfCurrent
        {
            get
            {

                if (CurrentPlayer == _player1)
                {
                    return _player2;
                }

                else
                {
                    return _player1;
                }
            }
        }
    
        public BoardLogic(User player1, User player2)
        {
            boardData = new User[3, 3];
            _player1 = player1;
            _player2 = player2;
            _currentPlayer = _player1;
            FirstPlayer = _player1;
        }

        public void newBoard()
        {
            boardData = new User[3, 3];
        }
        
        public void AddSpace(User currentplayer, int row, int column)
        {
            boardData[row, column] = currentplayer;
            PlayerLastMove = (row, column);
                _playerHistory.Add((row, column));
            _spaceFilled += 1;
        }
   
        public void SwitchPlayer()
        {
            _currentPlayer = _currentPlayer == _player1 ? _player2 : _player1;
        }

        public bool SpaceFilled(int row, int column) => boardData[row, column] != null;

        public GameStatus CheckGameStatus()
        {
            // Columns
            
            if (boardData[0,0] == boardData[1,0] && boardData[1,0] == boardData[2,0])
            {

                if (boardData[0, 0] == _player1)
                {
                    _player1.Wins += 1;
                    return GameStatus.Win;
                }

                if (boardData[0, 0] == _player2)
                {
                    _player2.Wins += 1;
                    return GameStatus.Win;
                }
                
            }
            
            if (boardData[0,1] == boardData[1,1] && boardData[1,1] == boardData[2,1])
            {
                
                if (boardData[0, 1] == _player1)
                {
                    _player1.Wins += 1;
                    return GameStatus.Win;
                }
                
                if (boardData[0, 1] == _player2)
                {
                    _player2.Wins += 1;
                    return GameStatus.Win;
                }
                
            }
            
            if (boardData[0,2] == boardData[1,2] && boardData[1,2] == boardData[2,2])
            {
                
                if (boardData[0, 2] == _player1)
                {
                    _player1.Wins += 1;
                    return GameStatus.Win;
                }
                
                if (boardData[0, 2] == _player2)
                {
                    _player2.Wins += 1;
                    return GameStatus.Win;
                }

            }
            
            // Rows

            if (boardData[0,0] == boardData[0,1] && boardData[0,1] == boardData[0,2])
            {
                
                if (boardData[0, 0] == _player1)
                {
                    _player1.Wins += 1;
                    return GameStatus.Win;
                }

                if (boardData[0, 0] == _player2)
                {
                    _player2.Wins += 1;
                    return GameStatus.Win;
                }

            }

            if (boardData[1,0] == boardData[1,1] && boardData[1,1] == boardData[1,2])
            {
                
                if (boardData[1, 0] == _player1)
                {
                    _player1.Wins += 1;
                    return GameStatus.Win;
                }
                
                if (boardData[1, 0] == _player2)
                {
                    _player2.Wins += 1;
                    return GameStatus.Win;
                }

            }

            if (boardData[2,0] == boardData[2,1] && boardData[2,1] == boardData[2,2])
            {
                
                if (boardData[2, 0] == _player1)
                {
                    _player1.Wins += 1;
                    return GameStatus.Win;
                }
               
                if (boardData[2, 0] == _player2)
                {
                    _player2.Wins += 1;
                    return GameStatus.Win;
                }

            }

            // Crosses

            if (boardData[0,0] == boardData[1,1] && boardData[1,1] == boardData[2,2])
            {
                
                if (boardData[0, 0] == _player1)
                {
                    _player1.Wins += 1;
                    return GameStatus.Win;
                }
                
                if (boardData[0, 0] == _player2)
                {
                    _player2.Wins += 1;
                    return GameStatus.Win;
                }

            }

            if (boardData[0,2] == boardData[1,1] && boardData[1,1] == boardData[2,0])
            {
                if (boardData[0, 2] == _player1)
                {
                    _player1.Wins += 1;
                    return GameStatus.Win;
                }
                
                if (boardData[0, 2] == _player2)
                {
                    _player2.Wins += 1;
                    return GameStatus.Win;
                }

            }

            // Draw or Continue
            
            else
            {
                if (_spaceFilled == 9)
                {
                    return GameStatus.Draw;
                }
            }

            return GameStatus.Contiue;

        }

        public User[,] returnBoardData()
        {
            return boardData;
        }
}
}
