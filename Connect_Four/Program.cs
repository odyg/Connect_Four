﻿using ConnectFour;
using System;
using System.Reflection;

namespace ConnectFour
{
    public class ConnectFourGame
    {
        protected string[,] gameBoard = new string[6, 7]; //We are creating a 2D Array here with 6 rows and 7 column 
        public static int rows { get; private set; } = 6; //we are setting the # of rows
        public static int cols { get; private set; } = 7; //we are setting the # of columns
        public string[] possibleLetters = { "A", "B", "C", "D", "E", "F", "G" }; //??????

        //----------- GAME BOARD CLASS --------------------------------------------
        public void InitializeGameBoard() //
        {
            for (int x = 0; x < rows; x++)
            {
                for (int y = 0; y < cols; y++)
                {
                    gameBoard[x, y] = ""; //  x is the row 1 - 6 AND y is the column "A", "B", "C", "D", "E", "F", "G"
                }
            }
        }

        public void PrintGameBoard()
        {
            Console.WriteLine("\n     A    B    C    D    E    F    G  ");
            for (int x = 0; x < rows; x++)
            {
                Console.WriteLine("\n   +---+----+----+----+----+----+----+");
                Console.Write(x + " |");
                for (int y = 0; y < cols; y++)
                {
                    if (gameBoard[x, y] == "O")
                    {
                        Console.Write(" " + gameBoard[x, y] + "  |"); //
                    }
                    else if (gameBoard[x, y] == "X")
                    {
                        Console.Write(" " + gameBoard[x, y] + "  |");
                    }
                    else
                    {
                        Console.Write("    |");
                    }
                }
            }
            Console.WriteLine("\n   +---+----+----+----+----+----+----+");
        }


    }//---------------------------------------------------------------------------------------------

    public class GamePlay : ConnectFourGame
    {
        public void ModifyArray(int[] spacePicked, string turn)
        {
            gameBoard[spacePicked[0], spacePicked[1]] = turn;

        }
        public bool CheckForWinner(string chip)
        {
            // Check horizontal spaces
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols - 3; x++)
                {
                    if (gameBoard[x, y] == chip && gameBoard[x + 1, y] == chip && gameBoard[x + 2, y] == chip && gameBoard[x + 3, y] == chip)
                    {
                        Console.WriteLine($"\nGame over {chip} wins! Thank you for playing :)");
                        return true;
                    }
                }
            }

            // Check vertical spaces
            for (int x = 0;
                x < rows; x++)
            {
                for (int y = 0; y < cols - 3; y++)
                {
                    if (gameBoard[x, y] == chip && gameBoard[x, y + 1] == chip && gameBoard[x, y + 2] == chip && gameBoard[x, y + 3] == chip)
                    {
                        Console.WriteLine($"\nGame over {chip} wins! Thank you for playing :)");
                        return true;
                    }
                }
            }

            // Check upper right to bottom left diagonal spaces
            for (int x = 0; x < rows - 3; x++)
            {
                for (int y = 3; y < cols; y++)
                {
                    if (gameBoard[x, y] == chip && gameBoard[x + 1, y - 1] == chip && gameBoard[x + 2, y - 2] == chip && gameBoard[x + 3, y - 3] == chip)
                    {
                        Console.WriteLine($"\nGame over {chip} wins! Thank you for playing :)");
                        return true;
                    }
                }
            }

            // Check upper left to bottom right diagonal spaces
            for (int x = 0; x < rows - 3; x++)
            {
                for (int y = 0; y < cols - 3; y++)
                {
                    if (gameBoard[x, y] == chip && gameBoard[x + 1, y + 1] == chip && gameBoard[x + 2, y + 2] == chip && gameBoard[x + 3, y + 3] == chip)
                    {
                        Console.WriteLine($"\nGame over {chip} wins! Thank you for playing :)");
                        return true;
                    }
                }
            }

            return false;

        }
        public int[] CoordinateParser(string inputString) //this takes the input (i.e. C5). create a 2 element array int[2] called 'coordinate'.
                                                          //Takes the first element (inputString[0]) turn it into the 2nd element (column) and assign an int
                                                          //based on the letter (C=2)
        {
            int[] coordinate = new int[2];

            switch (inputString[0])
            {
                case 'A':
                    coordinate[1] = 0;
                    break;
                case 'B':
                    coordinate[1] = 1;
                    break;
                case 'C':
                    coordinate[1] = 2;
                    break;
                case 'D':
                    coordinate[1] = 3;
                    break;
                case 'E':
                    coordinate[1] = 4;
                    break;
                case 'F':
                    coordinate[1] = 5;
                    break;
                case 'G':
                    coordinate[1] = 6;
                    break;
                default:
                    Console.WriteLine("Invalid");
                    break;
            }

            coordinate[0] = int.Parse(inputString[1].ToString()); //we still have to fill the coordinate[0] with an integer because of this...
                                                                  //"int[] coordinate = new int[2];" (an array of interger). int.Parse can not take a char ONLY STRING.
                                                                  //Therefore we have to take the char in inputString[1] and turn it into to a string (.ToString())

            return coordinate; // C5 will become 52 or row 5, column 2 
        }
        public bool IsSpaceAvailable(int[] intendedCoordinate)
        {
            if (gameBoard[intendedCoordinate[0], intendedCoordinate[1]] == "X" || gameBoard[intendedCoordinate[0], intendedCoordinate[1]] == "O")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool GravityChecker(int[] intendedCoordinate)
        {
            int[] spaceBelow = new int[2];
            spaceBelow[0] = intendedCoordinate[0] + 1;
            spaceBelow[1] = intendedCoordinate[1];

            if (spaceBelow[0] == 6)
            {
                return true;
            }

            if (!IsSpaceAvailable(spaceBelow))
            {
                return true;
            }

            return false;
        }

    }

    public class Player : GamePlay
    {
        public bool leaveLoop = false;
        public int turnCounter = 0;
        public bool winner;
        public Player()
        {

        }

    }

    public class Human : Player
    {
        public string name;
    }

    public class Computer : Player
    {

    }




    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Connect Four");
            Console.WriteLine("-----------------------");
            ConnectFourGame connectFourGame = new ConnectFourGame();
            var Gameplay = new GamePlay();

            bool leaveLoop = false;
            int turnCounter = 0;
            bool winner;

            while (!leaveLoop)
            {
                if (turnCounter % 2 == 0)
                {
                    /*Gameplay.PrintGameBoard();

                    while (true)
                    {
                        Console.Write("\nChoose a space: ");
                        string spacePicked = Console.ReadLine();
                        spacePicked = spacePicked.ToUpper();
                        int[] coordinate = Gameplay.CoordinateParser(spacePicked); //coordinate array now has a value

                        try
                        {
                            if (Gameplay.IsSpaceAvailable(coordinate) && Gameplay.GravityChecker(coordinate))
                            {
                                Gameplay.ModifyArray(coordinate, "O");
                                
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Not a valid coordinate");
                            }
                        }
                        catch
                        {
                            Console.WriteLine("Error occurred. Please try again.");
                        }
                    }

                    winner = Gameplay.CheckForWinner("O");
                    turnCounter++;*/
                }
                else
                {
                    while (true)
                    {

                        string cpuChoice = connectFourGame.possibleLetters[new Random().Next(connectFourGame.possibleLetters.Length)] + (new Random().Next(6)); //RANDON GENERATOR IS HERE!!!!!
                        int[] cpuCoordinate = Gameplay.CoordinateParser(cpuChoice);

                        if (Gameplay.IsSpaceAvailable(cpuCoordinate) && Gameplay.GravityChecker(cpuCoordinate))
                        {
                            Gameplay.ModifyArray(cpuCoordinate, "X");

                            break;
                        }
                    }

                    turnCounter++;
                    winner = Gameplay.CheckForWinner("X");
                }

                if (winner)
                {
                    Gameplay.PrintGameBoard();
                    break;
                }
            }


        }
    }
}
/* 
 
player as super class
human player as derived
computer player as derived
gameplay class

 
 */

//--------------------------------------------------------------------------------------

