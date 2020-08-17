using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;

namespace TicTacToe
{
    class AIBrain
    {
        
        private User _firstPlayer;
        private User _AI;
        private User _enemy;

        public AIBrain(User firstplayer, AI robot, Player enemy)
        {
            _firstPlayer = firstplayer;
            _AI = robot;
            _enemy = enemy;
        }
        
        public (int x, int y) BestMoveAI(User [,] data, int amountOfPieces)
        {
            int row = 0;
            int column = 0;
            int diagonal = 0;
            
            if (_AI == _firstPlayer && amountOfPieces == 0)
            {
                return (0, 0);
            }

            if (_AI != _firstPlayer && amountOfPieces == 1)
            {
                if (data[1,1] == null)
                {
                    return (1, 1);
                }

                else
                {
                    return (0, 0);
                }
            }

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
                            for (int g = 2, j = 0; g < 3; g--, j++)
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
                            for (int g = 2, j = 0; g < 3; g--, j++)
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

            else if (data[0, 0] == null)
            {
                return (0, 0);
            }

            else if (data[0, 2] == null)
            {
                return (0, 2);
            }

            else if (data[2, 0] == null)
            {
                return (2, 0);
            }
            
            return (2, 2);

        } //Returns the next best move for AI
    }
}
