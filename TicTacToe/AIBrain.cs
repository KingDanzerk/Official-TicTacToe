using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace TicTacToe
{
/// <summary>
    /// Handles all operations relating with AI
    /// </summary>
    
    public class AIBrain
    {
        
        private User _AI;
        private User _enemy;
        private List<(int x, int y)> _AIHistory = new List<(int x, int y)>();


        /// <summary>
        /// Returns the coordinate across from AIs last move
        /// </summary>

        //public User FirstPlayer { get { return _firstPlayer; } set { } }
        public (int x, int y) AcrossFromLastMove
        {
            get
            {
                return GetAcross(_AIHistory.Last());
            }
        } 
       

        /// <summary>
        /// Retrives who is the firstplayer, who is the AI, and who is the AI going against
        /// </summary>
        /// <param name="firstplayer"></param>
        /// <param name="robot"></param>
        /// <param name="enemy"></param>
       
        public AIBrain(AI robot, Player enemy) 
        {
            _AI = robot;
            _enemy = enemy;
        }
        
        /// <summary>
        /// Returns the best move for the AI to make 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="amountOfPieces"></param>
        /// <param name="enemyMoves"></param>
        /// <returns></returns>
        
        public (int x, int y) BestMoveAI(User[,] data, User firstPlayer, int amountOfPieces, List<(int x, int y)> enemyMoves)
        {

            DrawHistoryReset(amountOfPieces, firstPlayer);
            if (_AI == firstPlayer)
            {
                return OffenseCorner(data, firstPlayer, amountOfPieces, enemyMoves);
            }

            else
            {
                return P2Hard(data, firstPlayer, amountOfPieces, enemyMoves);
            }
        } 
        
        /// <summary>
        /// Adds the AIs last move to the _AIHistory Field
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// 

        public void SaveLastMoves(int x, int y)
        {
            _AIHistory.Add((x, y));
        } 

        private void DrawHistoryReset(int pieces, User firstPlayer)
        {

            if (_AI == firstPlayer)
            {
                if (pieces == 7)
                {
                    _AIHistory = new List<(int x, int y)>();
                }
            }
            
            if (_AI != firstPlayer)
            {
                if (pieces == 8)
                {
                    _AIHistory = new List<(int x, int y)>();
                }
            }
        }

        /// <summary>
        /// Returns a coordinate if AI is about to win, else it will return (-1,1)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="amountOfPieces"></param>
        /// <returns></returns>
        /// 

        private (int x, int y) CheckAIEmptySpaces(User[,] data, int amountOfPieces)
        {
            int row = 0;
            int column = 0;
            int diagonal = 0;

            if (amountOfPieces > 1) //ChecksAI
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int e = 0; e < 3; e++)
                    {
                        if (data[i, e] == _AI)
                        {
                            row += 1;

                            if (row == 2)
                            {
                                for (int g = 0; g < 3; g++)
                                {
                                    if (data[i, g] == null)
                                    {
                                        return (i, g);
                                    }
                                }
                            }
                        }
                    }

                    row = 0;
                }

                //Column

                for (int i = 0; i < 3; i++)
                {
                    for (int e = 0; e < 3; e++)
                    {
                        if (data[e, i] == _AI)
                        {
                            column += 1;

                            if (column == 2)
                            {
                                for (int g = 0; g < 3; g++)
                                {
                                    if (data[g, i] == null)
                                    {
                                        return (g, i);
                                    }
                                }
                            }
                        }
                    }
                    column = 0;
                }

                //diagonals

                for (int i = 0, e = 0; i < 3; i++, e++)
                {
                    if (data[i, e] == _AI)
                    {
                        diagonal += 1;

                        if (diagonal == 2)
                        {
                            for (int g = 0, j = 0; g < 3; g++, j++)
                            {
                                if (data[g, j] == null)
                                {
                                    return (g, j);
                                }
                            }
                        }
                    }
                }

                diagonal = 0;

                for (int i = 2, e = 0; i > -1; i--, e++)
                {
                    if (data[i, e] == _AI)
                    {
                        diagonal += 1;

                        if (diagonal == 2)
                        {
                            for (int g = 2, j = 0; g > -1; g--, j++)
                            {
                                if (data[g, j] == null)
                                {
                                    return (g, j);
                                }
                            }
                        }
                    }
                }

                row = 0;
                column = 0;
                diagonal = 0;
            }//ChecksAI

            return (-1, -1); // If returns (-1,1) it means there was no space to take for AI to win
        } 

        /// <summary>
        /// Returns a coordinate if enemy is about to win, else if there is no place where the enemy will win, then returns (-1,1)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="amountOfPieces"></param>
        /// <returns></returns>

        private (int x, int y) CheckEnemyEmptySpaces(User[,] data, int amountOfPieces)
        {
            int row = 0;
            int column = 0;
            int diagonal = 0;

            if (amountOfPieces > 1)
            {

                //Rows

                for (int i = 0; i < 3; i++)
                {
                    for (int e = 0; e < 3; e++)
                    {
                        if (data[i, e] == _enemy)
                        {
                            row += 1;

                            if (row == 2)
                            {
                                for (int g = 0; g < 3; g++)
                                {
                                    if (data[i, g] == null)
                                    {
                                        return (i, g);
                                    }
                                }
                            }
                        }
                    }

                    row = 0;
                }

                //Column

                for (int i = 0; i < 3; i++)
                {
                    for (int e = 0; e < 3; e++)
                    {
                        if (data[e, i] == _enemy)
                        {
                            column += 1;

                            if (column == 2)
                            {
                                for (int g = 0; g < 3; g++)
                                {
                                    if (data[g, i] == null)
                                    {
                                        return (g, i);
                                    }
                                }
                            }
                        }
                    }

                    column = 0;
                }

                //diagonals

                for (int i = 0, e = 0; i < 3; i++, e++)
                {
                    if (data[i, e] == _enemy)
                    {
                        diagonal += 1;

                        if (diagonal == 2)
                        {
                            for (int g = 0, j = 0; g < 3; g++, j++)
                            {
                                if (data[g, j] == null)
                                {
                                    return (g, j);
                                }
                            }
                        }
                    }
                }

                diagonal = 0;

                for (int i = 2, e = 0; i > -1; i--, e++)
                {
                    if (data[i, e] == _enemy)
                    {
                        diagonal += 1;

                        if (diagonal == 2)
                        {
                            for (int g = 2, j = 0; g > -1; g--, j++)
                            {
                                if (data[g, j] == null)
                                {
                                    return (g, j);
                                }
                            }
                        }
                    }
                }

                row = 0;
                column = 0;
                diagonal = 0;

            } //ChecksEnemy

            return (-1, -1); //If returns (-1,-1), it means enemy has no way to win
        } 

        /// <summary>
        /// Strategy for AI when he is the first player
        /// </summary>
        /// <param name="data"></param>
        /// <param name="amountOfPieces"></param>
        /// <param name="enemyMoves"></param>
        /// <returns></returns>
        /// 

        private (int x, int y) OffenseCorner(User[,] data, User firstPlayer, int amountOfPieces, List <(int x, int y)> enemyMoves)
        {
            (int x, int y) EnemyAttack = CheckEnemyEmptySpaces(data, amountOfPieces);
            (int x, int y) AIAttack = CheckAIEmptySpaces(data, amountOfPieces);
            

            if (_AI == firstPlayer && amountOfPieces == 0)
            {
                return RandomEmptyCorner(_AIHistory, enemyMoves); ;
            }

            if (_AI == firstPlayer && amountOfPieces == 2)
            {
                
                if (data[AcrossFromLastMove.x, AcrossFromLastMove.y] == null)
                {
                    return (AcrossFromLastMove.x, AcrossFromLastMove.y);
                }

                if (data[1, 1] == null)
                {
                    return (1, 1);
                }

                else
                {
                    return RandomEmptyCorner(_AIHistory, enemyMoves);
                }

            }

            if (_AI == firstPlayer && amountOfPieces >= 4)
            {
                if (AIAttack != (-1, -1))
                {
                    _AIHistory = new List<(int x, int y)>();
                    return AIAttack;

                }

                if (EnemyAttack != (-1, -1))
                {
                    return EnemyAttack;
                }
            }

            return RandomEmptyCorner(_AIHistory, enemyMoves);

        } 
        
        /// <summary>
        /// Strategy for AI when he is player2, Hard Mode (Unbeatable)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="amountOfPieces"></param>
        /// <param name="enemyMoves"></param>
        /// <returns></returns>
        /// 

        private (int x, int y) P2Hard(User[,] data, User firstPlayer, int amountOfPieces, List<(int x, int y)> enemyMoves)
        {
            (int x, int y) EnemyAttack = CheckEnemyEmptySpaces(data, amountOfPieces);
            (int x, int y) AIAttack = CheckAIEmptySpaces(data, amountOfPieces);
            List<(int x, int y)> TakenCorners;
            List<(int x, int y)> TakenMiddle;
            int checkCorners = TryCorners(data, out TakenCorners);
            int checkMiddle = TryMiddle(data, out TakenMiddle);
            

            if (_AI != firstPlayer && amountOfPieces == 1)
            {

                if (data[1,1] == null)
                {
                    return (1, 1);
                }

                else
                {
                    return RandomEmptyCorner(_AIHistory, enemyMoves);
                }
            }

            if (_AI != firstPlayer && amountOfPieces <= 4)
            {

                if (AIAttack != (-1, -1))
                {
                    _AIHistory = new List<(int x, int y)>(); 
                    return AIAttack;
                }
                
                if (EnemyAttack != (-1, -1))
                {
                    return EnemyAttack;
                }
                
                if (checkCorners == 3)
                {
                    for (int i = 0; i < TakenCorners.Count; i++)
                    {
                        if (enemyMoves[0] == TakenCorners[i])
                        {
                            return BlockCornerToMiddle(enemyMoves);
                        }
                    }

                }

                if (checkMiddle == 2)
                {
                    return DoubleCorner(enemyMoves);
                }

            }

            if (_AI != firstPlayer && amountOfPieces >= 5)
            {
                
                if (AIAttack != (-1, -1))
                {
                    _AIHistory = new List<(int x, int y)>();
                    return AIAttack;

                }

                if (EnemyAttack != (-1,-1))
                {
                    return EnemyAttack;
                }

                if (checkMiddle == 0)
                {
                    return RandomEmptyCorner(_AIHistory, enemyMoves);
                }

                if (checkCorners == 1 && checkMiddle == 0)
                {
                    return RandomEmptyCorner(_AIHistory, enemyMoves);
                }
            }

            return RandomEmptyMiddle(_AIHistory, enemyMoves);

        } 

        /// <summary>
        /// Strategy for AI when he is player 2, Medium Mode (Sometimes beatable)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="amountOfPieces"></param>
        /// <param name="enemyMoves"></param>
        /// <returns></returns>
        /// 

        private (int x, int y) P2Medium(User[,] data, User firstPlayer, int amountOfPieces, List<(int x, int y)> enemyMoves) //for future updates
        {
            (int x, int y) EnemyAttack = CheckEnemyEmptySpaces(data, amountOfPieces);
            (int x, int y) AIAttack = CheckAIEmptySpaces(data, amountOfPieces);
            List<(int x, int y)> TakenCorners;
            List<(int x, int y)> TakenMiddle;
            int checkCorners = TryCorners(data, out TakenCorners);
            int checkMiddle = TryMiddle(data, out TakenMiddle);
            Random randomizer = new Random();
            int randomFactor = randomizer.Next(2);

            if (_AI != firstPlayer && amountOfPieces == 1)
            {

                if (data[1, 1] == null)
                {
                    return (1, 1);
                }

                else
                {
                    return RandomEmptyCorner(_AIHistory, enemyMoves);
                }
            }

            if (_AI != firstPlayer && amountOfPieces <= 4)
            {
                
                if (AIAttack != (-1, -1))
                {
                    return AIAttack;
                }

                if (EnemyAttack != (-1, -1))
                {
                    return EnemyAttack;
                }

                if (checkCorners == 3 && randomFactor == 2)
                {
                    return BlockCornerToMiddle(enemyMoves);
                }

                if (checkMiddle == 2 && randomFactor == 1)
                {
                    return DoubleCorner(enemyMoves);
                }

            }

            if (_AI != firstPlayer && amountOfPieces >= 5)
            {

                if (EnemyAttack != (-1, -1))
                {
                    return EnemyAttack;
                }

                if (checkMiddle == 0)
                {
                    return RandomEmptyCorner(_AIHistory, enemyMoves);
                }
            }

            return RandomEmptyMiddle(_AIHistory, enemyMoves);

        }  

        /// <summary>
        /// Strategy for AI when he is player 2, (Beatable)
        /// </summary>
        /// <param name="data"></param>
        /// <param name="amountOfPieces"></param>
        /// <param name="enemyMoves"></param>
        /// <returns></returns>

        private (int x, int y) P2Easy(User[,] data, User firstPlayer, int amountOfPieces, List<(int x, int y)> enemyMoves)
        {
            (int x, int y) EnemyAttack = CheckEnemyEmptySpaces(data, amountOfPieces);
            (int x, int y) AIAttack = CheckAIEmptySpaces(data, amountOfPieces);
            List<(int x, int y)> TakenCorners;
            List<(int x, int y)> TakenMiddle;
            int checkCorners = TryCorners(data, out TakenCorners);
            int checkMiddle = TryMiddle(data, out TakenMiddle);
            Random randomizer = new Random();
            int randomFactor = randomizer.Next(2);

            if (_AI != firstPlayer && amountOfPieces == 1)
            {

                if (data[1, 1] == null)
                {
                    return (1, 1);
                }

                else
                {
                    return RandomEmptyCorner(_AIHistory, enemyMoves);
                }
            }

            if (_AI != firstPlayer && amountOfPieces <= 4)
            {

                if (EnemyAttack != (-1, -1))
                {
                    return EnemyAttack;
                }

            }

            if (_AI != firstPlayer && amountOfPieces >= 5)
            {
                if (EnemyAttack != (-1, -1))
                {
                    return EnemyAttack;
                }

                if (AIAttack != (-1, -1))
                {
                    return AIAttack;
                }

                if (checkMiddle == 0)
                {
                    return RandomEmptyCorner(_AIHistory, enemyMoves);
                }
            }

            return RandomEmptyMiddle(_AIHistory, enemyMoves);

        } //for future update

        /// <summary>
        /// Checks if any corner is taken. Returns how many corners are remaining.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="taken"></param>
        /// <returns></returns>

        private int TryCorners(User[,] data, out List<(int x, int y)> taken)
        {
            List<(int x, int y)> randomCorner = new List<(int x, int y)> { (0, 0), (2, 0), (0, 2), (2, 2) };
            taken = new List<(int x, int y)>();
            int count = 0;

            for (int i = 0; i < randomCorner.Count; i++)
            {
                if (data[randomCorner[i].x, randomCorner[i].y] == null)
                {
                    count += 1;
                }

                else
                {
                    taken.Add(randomCorner[i]);
                }
            }

            return count;
            
        } 

        /// <summary>
        /// Checks if any middle area is taken, Returns how many middle spots are remaining.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="taken"></param>
        /// <returns></returns>
        /// 

        private int TryMiddle(User[,] data, out List<(int x, int y)> taken)
        {
            List<(int x, int y)> randomMiddle = new List<(int x, int y)> { (0, 1), (1, 2), (2, 1), (1, 0) };
            taken = new List<(int x, int y)>();
            int count = 0;

            for (int i = 0; i < randomMiddle.Count; i++)
            {
                if (data[randomMiddle[i].x, randomMiddle[i].y] == null)
                {
                    count += 1;
                }

                else
                {
                    taken.Add(randomMiddle[i]);
                }
            }

            return count;

        } 

        /// <summary>
        /// A combination strategy for AI when he is player 2. (The L play)
        /// </summary>
        /// <param name="enemyLastmoves"></param>
        /// <returns></returns>
        /// 
        private (int x, int y) BlockCornerToMiddle(List<(int x, int y)> enemyLastmoves) 
        {

            if (enemyLastmoves[0] == (0,0))
            {
                if (enemyLastmoves[1] == (2,1))
                {
                    return (2, 0);
                }

                if (enemyLastmoves[1] == (1,2))
                {
                    return (0, 2);
                }

            }

            if (enemyLastmoves[0] == (0,2))
            {
                if (enemyLastmoves[1] == (2,1))
                {
                    return (2, 2);
                }

                if (enemyLastmoves[1] == (1,0))
                {
                    return (0, 0);
                }
            }

            if (enemyLastmoves[0] == (2,2))
            {
                if (enemyLastmoves[1] == (0,1))
                {
                    return (0, 2);
                }

                if (enemyLastmoves[1] == (1,0))
                {
                    return (2, 0);
                }
            }

            if (enemyLastmoves[0] == (2,0))
            {
                if (enemyLastmoves[1] == (0,1))
                {
                    return (0, 0);
                }

                if (enemyLastmoves[1] == (1,2))
                {
                    return (2, 2);
                }
            }
            
            return RandomEmptyMiddle(_AIHistory, enemyLastmoves);
            
        } 

        /// <summary>
        /// A combination strategy for AI when he is player 2. (The 2 middle across play)
        /// </summary>
        /// <param name="enemyLastMoves"></param>
        /// <returns></returns>

        private(int x, int y) DoubleCorner(List<(int x, int y)> enemyLastMoves)
        {

            if (enemyLastMoves[0] == (1,0))
            {
                if (enemyLastMoves[1] == (2,1))
                {
                    return (2, 0);
                }

                if (enemyLastMoves[1] == (0,1))
                {
                    return (0, 0);
                }
            }

            if (enemyLastMoves[0] == (2,1))
            {
                if (enemyLastMoves[1] == (1,0))
                {
                    return (2, 0);
                }

                if (enemyLastMoves[1] == (1,2))
                {
                    return (2, 2);
                }
            }

            if (enemyLastMoves[0] == (1,2))
            {
                if (enemyLastMoves[1] == (2,1))
                {
                    return (2,2);
                }

                if (enemyLastMoves[1] == (0,1))
                {
                    return (0, 2);
                }
            }

            if (enemyLastMoves[0] == (0,1))
            {
                if (enemyLastMoves[1] == (1,0))
                {
                    return (0, 0);
                }

                if (enemyLastMoves[1] == (1,2))
                {
                    return (0, 2);
                }
            }

            return RandomEmptyCorner(_AIHistory, enemyLastMoves);

        }

        /// <summary>
        /// Function that returns a corner from coordinate inputted.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// 

        private (int x, int y) GetAcross((int x, int y) input)
        {
            if (input == (0,0))
            {
                return (2, 2);
            }

            if (input == (2,0))
            {
                return (0, 2);
            }

            if (input == (0,2))
            {
                return (2, 0);
            }

            else
            {
                return (0, 0);
            }
        } 

        /// <summary>
        /// Returns a random empty corner based on enemys move history and AIs move history
        /// </summary>
        /// <param name="lastMoves"></param>
        /// <param name="enemyLastMoves"></param>
        /// <returns></returns>
        /// 

        private (int x, int y) RandomEmptyCorner(List <(int x, int y)> lastMoves, List<(int x, int y)> enemyLastMoves)
        {
            Random randomNumber = new Random();
            
            List<(int x, int y)> randomCorner = new List<(int x, int y)> { (0, 0), (2, 0), (0, 2), (2, 2) };
            

            for (int i = 0; i < _AIHistory.Count; i++)
            {
                for (int e = 0; e < randomCorner.Count; e++)
                {
                    if (_AIHistory[i] == randomCorner[e])
                    {
                        randomCorner.Remove(randomCorner[e]);
                        e = -1;
                    }
                }
            }

            for (int i = 0; i < enemyLastMoves.Count; i++)
            {
                for (int e = 0; e < randomCorner.Count; e++)
                {
                    if (enemyLastMoves[i] == randomCorner[e])
                    {
                        randomCorner.Remove(randomCorner[e]);
                        e = -1;
                    }
                }
            }

            if (randomCorner.Count == 1)
            {
                return randomCorner[0];
            }

            else
            {
                return randomCorner[randomNumber.Next(randomCorner.Count)];
            }
            
        }
        
        /// <summary>
        /// Returns a random middle point based on AIs last moves and Enemys last moves
        /// </summary>
        /// <param name="lastMoves"></param>
        /// <param name="enemyLastMoves"></param>
        /// <returns></returns>
        /// 
        private (int x, int y) RandomEmptyMiddle(List<(int x, int y)> lastMoves, List<(int x, int y)> enemyLastMoves)
        {
            Random randomNumber = new Random();
            List<(int x, int y)> randomMiddle = new List<(int x, int y)> { (0, 1), (1, 2), (2, 1), (1, 0) };

            for (int i = 0; i < enemyLastMoves.Count; i++)
            {
                for (int e = 0; e < randomMiddle.Count; e++)
                {
                    if (enemyLastMoves[i] == randomMiddle[e])
                    {
                        randomMiddle.Remove(randomMiddle[e]);
                        e -= 1;
                    }
                }
            }

            for (int i = 0; i < lastMoves.Count; i++)
            {
                for (int e = 0; e < randomMiddle.Count; e++)
                {
                    if (lastMoves[i] == randomMiddle[e])
                    {
                        randomMiddle.Remove(randomMiddle[e]);
                        e -= 1;
                    }
                }
            }

            if (randomMiddle.Count == 1)
            {
                return randomMiddle[0];
            }

            else
            {
                return randomMiddle[randomNumber.Next(randomMiddle.Count)];
            }
            
        } 

    }
}