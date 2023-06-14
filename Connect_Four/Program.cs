using ConnectFour;
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
        public bool winner;
        public string Name = null;
        public string Turn = null;

        public void ChangeBoard(int[] spacePicked, string turn)
        {
            base.gameBoard[spacePicked[0], spacePicked[1]] = turn;
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
                        Console.WriteLine($"\nGame over {Name} wins! Woot Woot");
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
                        Console.WriteLine($"\nGame over {Name} wins! Get it!");
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
                        Console.WriteLine($"\nGame over {Name} wins! Great job!");
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
                        Console.WriteLine($"\nGame over {Name} wins! {Name} swept the competition.");
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
        public bool IsSpaceAvailable(int[] intendedCoord)
        {
            if (gameBoard[intendedCoord[0], intendedCoord[1]] == "X" || gameBoard[intendedCoord[0], intendedCoord[1]] == "O")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool GravityChecker(int[] intendedCoord)
        {
            int[] spaceBelow = new int[2];
            spaceBelow[0] = intendedCoord[0] + 1;
            spaceBelow[1] = intendedCoord[1];

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
  

    public class Human : GamePlay
    {
        int[] coordinate = null;
        

        public Human(string name, string turn)
        {
            base.Name = name;
            base.Turn = turn;
        }
        public void HumanPlayer()
        {
            bool isValidCoord = false;
            do
            {
                Console.Write($"\nChoose a space {Name}: ");
                string spacePicked = Console.ReadLine();
                spacePicked = spacePicked.ToUpper();

                coordinate = base.CoordinateParser(spacePicked);
                try
                {
                    if (base.IsSpaceAvailable(coordinate) && base.GravityChecker(coordinate))
                    {
                        base.ChangeBoard(coordinate, Turn);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Not a valid coordinate. Please try again.");
                    }
                }
                catch
                {
                    Console.WriteLine("Error occurred. Please try again.");
                }

            } while (!isValidCoord);
            
        }

        public void CheckingForWinner()
        {
            winner = base.CheckForWinner(Turn);
            
        }
    }

    public class Computer : GamePlay
    {
        string cpuChoice = "";
        int[] cpuCoordinate = null;
        
        public void pcPlayer()
        {
            bool isValidCoord = false;
            do
            {
                cpuChoice = base.possibleLetters[new Random().Next(base.possibleLetters.Length)] + (new Random().Next(6)); //RANDON GENERATOR IS HERE!!!!!
                cpuCoordinate = base.CoordinateParser(cpuChoice);

                if (base.IsSpaceAvailable(cpuCoordinate) && base.GravityChecker(cpuCoordinate))
                {
                    base.ChangeBoard(cpuCoordinate, "0");
                    break;
                }

            } while (!isValidCoord);
            
            base.PrintGameBoard();
        }
        public void CheckingForWinner()
        {
            winner = base.CheckForWinner("O");
        }
    }

    internal class Program
    {
        static void Main(string[] args) //STILL NEEDS TO GIVE THE OPTION TO PLAY AGAINST ANOTHER PLAYER BETTER.
        {
            Console.WriteLine("Welcome to Connect Four");
            Console.WriteLine("-----------------------");

            ConnectFourGame game = new ConnectFourGame();
            ConnectFourGame Gameplay = new GamePlay();
            game.InitializeGameBoard();
            game.PrintGameBoard();


            //Human humanPlayer = new Human();
            Computer computerPlayer = new Computer();
            bool looping = false;

            do
            {
                Console.WriteLine("Do you want to play Person vs Person? (Yes/No)");
                string answer = Console.ReadLine();
                answer = answer.ToUpper();
                
                    if (answer == "YES")
                    {
                        Console.WriteLine("Player 1 ('X') name? ");
                        string player1 = Console.ReadLine();
                        Console.WriteLine("Player 2 ('O') name? ");
                        string player2 = Console.ReadLine();
                        Human Player1 = new Human(player1, "X");
                        Human Player2 = new Human(player2, "O");

                        while (true)
                        {
                            Player1.HumanPlayer();
                            Player1.CheckingForWinner();
                            Player1.PrintGameBoard(); //I need Player 1 and Player 2 gameboard to merge somehow.
                            if (Player1.winner)
                                break;
                            Player2.HumanPlayer();
                            Player2.CheckingForWinner();
                            Player2.PrintGameBoard();
                            if (Player2.winner)
                                break;
                        } looping = true;
                    }
                    else if (answer == "NO")
                    {
                        Console.WriteLine("You're going against a SUPER computer (Player 2 - 'O')");
                        Console.WriteLine("Player 1 ('X') name? ");
                        string player1 = Console.ReadLine();
                    }
                
            } while (!looping);



            /*while (true)
            {
                // Human's turn
                humanPlayer.HumanPlayer();
                humanPlayer.CheckingForWinner();
                //humanPlayer.PrintGameBoard();
                if (humanPlayer.winner)
                    break;

                // Computer's turn
                computerPlayer.pcPlayer();
                computerPlayer.CheckingForWinner();
                //computerPlayer.PrintGameBoard();
                if (computerPlayer.winner)
                    break;
                
            }*/

            Console.WriteLine("Game Over");
            Console.ReadLine();







            /*
            Console.WriteLine("Welcome to Connect Four");
            Console.WriteLine("-----------------------");
            ConnectFourGame connectFourGame = new ConnectFourGame();
            var Gameplay = new GamePlay();

            bool leaveLoop = false;
            int turnCounter = 0;
            //bool winner;

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
                    turnCounter++;
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

            */
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

