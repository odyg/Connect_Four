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
    }//------------------
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



    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Connect Four - Main Menu");
        }

    }

}