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
    class AIBrain
    {

        private User _firstPlayer;
        private User _AI;
        private User _enemy;
        private (int x, int y) _lastMove;
        private List<(int x, int y)> _AIHistory = new List<(int x, int y)>();
        private List<(int x, int y)> _enemyMoves;
        
        public (int x, int y) AcrossFromLastMove
        {
            get
            {
                return GetAcross(_AIHistory.Last());
            }
        } //Gets move across from move inputted

        public AIBrain(User firstplayer, AI robot, Player enemy) 
        {
            _firstPlayer = firstplayer;
            _AI = robot;
            _enemy = enemy;
        }

        public (int x, int y) BestMoveAI(User[,] data, int amountOfPieces, List<(int x, int y)> enemyMoves)
        {
            if (_AI == _firstPlayer)
            {
                return OffenseCorner(data, amountOfPieces, enemyMoves);
            }

            else
            {
                return P2Hard(data, amountOfPieces, enemyMoves);
            }
        } //Returns the next best move for AI

        public void SaveLastMoves(int x, int y)
        {
            _AIHistory.Add((x, y));
        } //SavesLastMove

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

            return (-1, -1);
        } //Checks for a AI win

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

            return (-1, -1);
        } //Checks for a enemy win

        private (int x, int y) OffenseCorner(User[,] data, int amountOfPieces, List <(int x, int y)> enemyMoves)
        {
            (int x, int y) EnemyAttack = CheckEnemyEmptySpaces(data, amountOfPieces);
            (int x, int y) AIAttack = CheckAIEmptySpaces(data, amountOfPieces);

            if (_AI == _firstPlayer && amountOfPieces == 0)
            {
                return RandomEmptyCorner(_AIHistory, enemyMoves); ;
            }

            if (_AI == _firstPlayer && amountOfPieces == 2)
            {

                if (data[AcrossFromLastMove.x, AcrossFromLastMove.y] == null)
                {
                    return (AcrossFromLastMove.x, AcrossFromLastMove.y);
                }

                else
                {
                    return RandomEmptyCorner(_AIHistory, enemyMoves);
                }

            }

            if (_AI == _firstPlayer && amountOfPieces >= 4)
            {
                if (AIAttack != (-1, -1))
                {
                    return AIAttack;

                }

                if (EnemyAttack != (-1, -1))
                {
                    return EnemyAttack;
                }
            }

            return RandomEmptyCorner(_AIHistory, enemyMoves);

        } //1st player strategy

        private (int x, int y) P2Hard(User[,] data, int amountOfPieces, List<(int x, int y)> enemyMoves)
        {
            (int x, int y) EnemyAttack = CheckEnemyEmptySpaces(data, amountOfPieces);
            (int x, int y) AIAttack = CheckAIEmptySpaces(data, amountOfPieces);
            List<(int x, int y)> TakenCorners;
            List<(int x, int y)> TakenMiddle;
            int checkCorners = TryCorners(data, out TakenCorners);
            int checkMiddle = TryMiddle(data, out TakenMiddle);


            if (_AI != _firstPlayer && amountOfPieces == 1)
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

            if (_AI != _firstPlayer && amountOfPieces <= 4)
            {

                if (EnemyAttack != (-1, -1))
                {
                    return EnemyAttack;
                }
                
                if (checkCorners == 3)
                {
                    return BlockCornerToMiddle(enemyMoves);
                }

                if (checkMiddle == 2)
                {
                    return DoubleCorner(enemyMoves);
                }

            }

            if (_AI != _firstPlayer && amountOfPieces >= 5)
            {
                if (EnemyAttack != (-1,-1))
                {
                    return EnemyAttack;
                }

                if (AIAttack != (-1,-1))
                {
                    return AIAttack;
                }
                
                if (checkMiddle == 0)
                {
                    return RandomEmptyCorner(_AIHistory, enemyMoves);
                }
            }

            return RandomEmptyMiddle(_AIHistory, enemyMoves);

        } //2nd player strategy Hard Mode

        private (int x, int y) P2Medium(User[,] data, int amountOfPieces, List<(int x, int y)> enemyMoves)
        {
            (int x, int y) EnemyAttack = CheckEnemyEmptySpaces(data, amountOfPieces);
            (int x, int y) AIAttack = CheckAIEmptySpaces(data, amountOfPieces);
            List<(int x, int y)> TakenCorners;
            List<(int x, int y)> TakenMiddle;
            int checkCorners = TryCorners(data, out TakenCorners);
            int checkMiddle = TryMiddle(data, out TakenMiddle);
            Random randomizer = new Random();
            int randomFactor = randomizer.Next(2);

            if (_AI != _firstPlayer && amountOfPieces == 1)
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

            if (_AI != _firstPlayer && amountOfPieces <= 4)
            {

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

            if (_AI != _firstPlayer && amountOfPieces >= 5)
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

        } //2nd player strategy Medium

        private (int x, int y) P2Easy(User[,] data, int amountOfPieces, List<(int x, int y)> enemyMoves)
        {
            (int x, int y) EnemyAttack = CheckEnemyEmptySpaces(data, amountOfPieces);
            (int x, int y) AIAttack = CheckAIEmptySpaces(data, amountOfPieces);
            List<(int x, int y)> TakenCorners;
            List<(int x, int y)> TakenMiddle;
            int checkCorners = TryCorners(data, out TakenCorners);
            int checkMiddle = TryMiddle(data, out TakenMiddle);
            Random randomizer = new Random();
            int randomFactor = randomizer.Next(2);

            if (_AI != _firstPlayer && amountOfPieces == 1)
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

            if (_AI != _firstPlayer && amountOfPieces <= 4)
            {

                if (EnemyAttack != (-1, -1))
                {
                    return EnemyAttack;
                }

            }

            if (_AI != _firstPlayer && amountOfPieces >= 5)
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

        } 

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
            
        } //Checks if  any corner is taken

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

        } //Checks if any middle piece is taken

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
            
        } //CornerMiddlePLay

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
        } //Gets Coordinate across from a coordinate

        private (int x, int y) RandomEmptyCorner(List <(int x, int y)> lastMoves, List<(int x, int y)> enemyLastMoves) //Gets a random empty corner
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

            return randomCorner[randomNumber.Next(randomCorner.Count)];
        }
        
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

            if (randomMiddle.Count == 0)
            {
                return (-1, -1);
            }

            return randomMiddle[randomNumber.Next(randomMiddle.Count)];
        } //Gets a random empty middle


                


    }
}