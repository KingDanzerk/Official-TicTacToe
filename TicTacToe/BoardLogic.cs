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
        private User[,] boardData;
        private int _spaceFilled = 0;
        private int _draws = 0;
        private User _currentPlayer;
        private User _firstPlayer;
        private List<(int x, int y)> _history = new List<(int x, int y)>();
        private List<(int x, int y)> _human1History = new List<(int x, int y)>();
        private List<(int x, int y)> _human2History = new List<(int x, int y)>();
        private List<(int x, int y)> _AIHistory = new List<(int x, int y)>();

        public AI AIPlayer { get { return GetAI(); } }

        /// <summary>
        /// Returns history of all players
        /// </summary>
        /// 

        public List<(int x, int y)> History { get { return _history; } }
        
        /// <summary>
        /// Returns last move.
        /// </summary>
        /// 

        public (int x, int y) PlayerLastMove { get; protected set; }
        
        /// <summary>
        /// Returns the first player of the game
        /// </summary>
        /// 
        
        public User FirstPlayer { get { return _firstPlayer; } protected set { } }

        /// <summary>
        /// Returns the current player
        /// </summary>
    
        public User CurrentPlayer { get { return _currentPlayer; } }

        /// <summary>
        /// Returns how many pieces on the board.
        /// </summary>
        /// 

        public int Pieces { get { return _spaceFilled; } }
    
        /// <summary>
        /// Returns opposite player of current player
        /// </summary>
        /// 

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
    
        /// <summary>
        /// Creates game, first argument == first player, second argument == player 2
        /// </summary>
        /// <param name="player1"></param>
        /// <param name="player2"></param>
        /// 

        public BoardLogic(User player1, User player2)
        {
            boardData = new User[3, 3];
            _player1 = player1;
            _player2 = player2;
            _currentPlayer = _player1;
            _firstPlayer = _player1;
        }

        /// <summary>
        /// creates a new board
        /// </summary>
        /// 

        public void ResetGame()
        {
            newBoard();
            SwitchFirstPlayer();
            ResetHistory();
            _spaceFilled = 0;
            _currentPlayer = FirstPlayer;
        }

        private void SwitchFirstPlayer()
        {
            _firstPlayer = _firstPlayer == _player1 ? _player2 : _player1;
        }
        private  void ResetHistory()
        {
            _AIHistory = new List<(int x, int y)>();
            _human1History = new List<(int x, int y)>();
            _human2History = new List<(int x, int y)>();
            _history = new List<(int x, int y)>();
        }


        private void newBoard()
        {
            boardData = new User[3, 3];
        }

        public AI GetAI()
        {
            if (_player1 is AI)
            {
                return (AI)_player1;
            }

            if (_player2 is AI)
            {
                return (AI)_player2;
            }

            else
            {
                return null;
            }
        } 

        /// <summary>
        /// Adds that player onto the board data
        /// </summary>
        /// <param name="currentplayer"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        
        public void AddSpace(User currentplayer, int row, int column)
        {
            boardData[row, column] = currentplayer;
            PlayerLastMove = (row, column);
            _history.Add((row, column));
           
            if (currentplayer == _player1)
            {
                if (_player1 is Player)
                {
                    _human1History.Add((row, column));
                }

                if (_player1 is AI)
                {
                    _AIHistory.Add((row, column));
                }
            }

            if (currentplayer == _player2)
            {
                if (_player2 is Player)
                {
                    _human2History.Add((row, column));
                }

                if (_player2 is AI)
                {
                    _AIHistory.Add((row, column));
                }
            }
           
            _spaceFilled += 1;
        }
   
        /// <summary>
        /// Switches the current player
        /// </summary>
        /// 

        public void SwitchPlayer()
        {
            _currentPlayer = _currentPlayer == _player1 ? _player2 : _player1;
        }

        /// <summary>
        /// Checks if a space is filled or not
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        /// 

        public bool SpaceFilled(int row, int column) => boardData[row, column] != null;

        /// <summary>
        /// Checks for a win, draw or continue
        /// </summary>
        /// <returns></returns>
        /// 
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

        /// <summary>
        /// Returns board information
        /// </summary>
        /// <returns></returns>
        /// 

        public User[,] returnBoardData()
        {
            return boardData;
        }

        /// <summary>
        /// Returns a players history
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>

        public List<(int x, int y)> ReturnPlayerHistory(User player)
        {
            if (player == FirstPlayer)
            {
                if (player is Player)
                {
                    return _human1History;
                }

                if (player is AI)
                {
                    return _AIHistory;
                }
            }

            if (player != FirstPlayer)
            {
                if (player is Player)
                {
                    return _human2History;
                }
            }

            return _AIHistory;
        }
}
}
