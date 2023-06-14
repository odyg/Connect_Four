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

        /*public override void PrintGameBoard()
        {
            Console.WriteLine();
            for (int row = rows - 1; row >= 0; row--)
            {
                Console.Write("| ");
                for (int col = 0; col < cols; col++)
                {
                    Console.Write(gameBoard[row, col] + " | ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("-----------------------------");
            Console.WriteLine("  A   B   C   D   E   F   G");
            Console.WriteLine();
        }*/

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

        public override bool IsColumnFull(int col)
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

        public override bool IsGameBoardFull()
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

            string player = "O";
            while (true)
            {
                Console.WriteLine($"\nPlayer {player}'s turn.");
                Console.Write("Enter the column and row position (e.g., C5): ");
                string input = Console.ReadLine().Trim().ToUpper();
                Console.WriteLine();

                int colIndex = GetColumnIndex(input);
                if (colIndex >= 0 && !IsColumnFull(colIndex))
                {
                    for (int row = rows - 1; row >= 0; row--)
                    {
                        if (gameBoard[row, colIndex] == "")
                        {
                            gameBoard[row, colIndex] = player;
                            break;
                        }
                    }

                    PrintGameBoard();

                    if (CheckForWinner(player))
                    {
                        Console.WriteLine($"Player {player} wins!");
                        break;
                    }

                    if (IsGameBoardFull())
                    {
                        Console.WriteLine("It's a tie!");
                        break;
                    }

                    player = (player == "O") ? "X" : "O";
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

