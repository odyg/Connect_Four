using ConnectFour;
using System;
using System.Reflection;

namespace ConnectFour
{
    using System;

    public abstract class Game
    {
        protected string[,] gameBoard;

        public abstract void InitializeGameBoard();
        public abstract void PrintGameBoard();
        public abstract bool IsColumnFull(int col);
        public abstract bool IsGameBoardFull();
        public abstract int GetColumnIndex(string position);  // Modified method to accept column and row position
        public abstract bool CheckForWinner(string chip);
        public abstract void Play();
    }

    public class ConnectFourGame : Game
    {
        private const int rows = 6;
        private const int cols = 7;
        private readonly char[] possibleLetters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G' };

        public override void InitializeGameBoard()
        {
            gameBoard = new string[rows, cols];
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    gameBoard[row, col] = "";
                }
            }
        }

       
        public override void PrintGameBoard()
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
                        Console.Write(" " + gameBoard[x, y] + "  |");
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

        public override bool IsColumnFull(int col) //This will loop to check the rows. The col will stay the same.
        {
            for (int row = 0; row < rows; row++)
            {
                if (gameBoard[row, col] == "")
                {
                    return false;
                }
            }
            return true;
        }

        public override bool IsGameBoardFull() //This will loop to check if all columns are full using IsColumnFull method. 
        {
            for (int col = 0; col < cols; col++)
            {
                if (!IsColumnFull(col))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetColumnIndex(string position)  // Modified method to accept column and row position
        {
            if (position.Length != 2)
            {
                return -1;
            }

            char letter = char.ToUpper(position[0]);
            int row;
            if (!int.TryParse(position[1].ToString(), out row))
            {
                return -1;
            }

            int colIndex = Array.IndexOf(possibleLetters, letter); 
            //this is where we convert the letter to an int. we are comparing 
            //the letter to the array of "possibleLetter" if found we get index value
            if (colIndex < 0 || colIndex >= cols || row < 1 || row > rows)
            {
                return -1;
            }

            return colIndex;
        }

        public override bool CheckForWinner(string chip)
        {
            // Check for horizontal wins
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols - 3; col++)
                {
                    if (gameBoard[row, col] == chip && gameBoard[row, col + 1] == chip &&
                        gameBoard[row, col + 2] == chip && gameBoard[row, col + 3] == chip)
                    {
                        return true;
                    }
                }
            }

            // Check for vertical wins
            for (int row = 0; row < rows - 3; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    if (gameBoard[row, col] == chip && gameBoard[row + 1, col] == chip &&
                        gameBoard[row + 2, col] == chip && gameBoard[row + 3, col] == chip)
                    {
                        return true;
                    }
                }
            }

            // Check for diagonal wins (positive slope)
            for (int row = 0; row < rows - 3; row++)
            {
                for (int col = 0; col < cols - 3; col++)
                {
                    if (gameBoard[row, col] == chip && gameBoard[row + 1, col + 1] == chip &&
                        gameBoard[row + 2, col + 2] == chip && gameBoard[row + 3, col + 3] == chip)
                    {
                        return true;
                    }
                }
            }

            // Check for diagonal wins (negative slope)
            for (int row = 3; row < rows; row++)
            {
                for (int col = 0; col < cols - 3; col++)
                {
                    if (gameBoard[row, col] == chip && gameBoard[row - 1, col + 1] == chip &&
                        gameBoard[row - 2, col + 2] == chip && gameBoard[row - 3, col + 3] == chip)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        public override void Play()
        {
            InitializeGameBoard();
            PrintGameBoard();

            Console.WriteLine("Enter first player name: ");
            string Player1 = Console.ReadLine();
            Console.WriteLine("Enter second player name: ");
            string Player2 = Console.ReadLine();

            string Name = Player1;
            string player = "X";
            while (true)
            {
                Console.WriteLine($"\nPlayer {player}'s turn. ({Name})");
                Console.Write("Enter the column and row position (e.g., C5): ");
                string input = Console.ReadLine().Trim().ToUpper(); //reformat the input
                Console.WriteLine();

                int colIndex = GetColumnIndex(input); //This will check the input using this method
                if (colIndex >= 0 && !IsColumnFull(colIndex)) //if colIndex is -1 from this method GetColumnIndex() it will ask the player to try again. 
                {
                    for (int row = rows - 1; row >= 0; row--) /*NOTE: The code is only checking if the row input is out of range. GetColumnIndex() method
                                                               * If it is, then it will ask the player to try again. If it's within range the code will find the next empty space
                                                               within that column with this loop. This will improve gameplay and reduce error */
                    {
                        if (gameBoard[row, colIndex] == "")
                        {
                            gameBoard[row, colIndex] = player;
                            break;
                        }
                    }

                    PrintGameBoard();

                    if (CheckForWinner(player)) //we are checking for winners using this method CheckForWinner()
                    {
                        Console.WriteLine($"Player {player} ({Name}) wins!");
                        break;
                    }

                    if (IsGameBoardFull())
                    {
                        Console.WriteLine("It's a tie!");
                        break;
                    }

                    if (player == "X")
                    {
                        player = "O";
                        Name = Player2;
                    }
                    else
                    {
                        player = "X";
                        Name = Player1;
                    }
                    
                }
               
                else
                {
                    Console.WriteLine("Invalid position. Please try again.");
                }
            }
        }
    }

    public class HumanVsHumanGame : ConnectFourGame
    {
        public override void Play()
        {
            Console.WriteLine("Connect Four - Human vs. Human\n");
            base.Play();
        }
    }

    public class HumanVsCpuGame : ConnectFourGame
    {
        public override void Play()
        {
            Console.WriteLine("Connect Four - Human vs. CPU\n");
            // ... implementation for playing against the CPU
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Connect Four - Main Menu");
                Console.WriteLine("1. Play Human vs. Human");
                Console.WriteLine("2. Play Human vs. CPU");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice (1-3): ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Game humanVsHumanGame = new HumanVsHumanGame();                        
                        humanVsHumanGame.Play();
                        break;
                    case "2":
                        Game humanVsCpuGame = new HumanVsCpuGame();
                        humanVsCpuGame.Play();
                        break;
                    case "3":
                        Console.WriteLine("\nThank you for playing Connect Four. Goodbye!");
                        return;
                    default:
                        Console.WriteLine("\nInvalid choice. Please try again.");
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

