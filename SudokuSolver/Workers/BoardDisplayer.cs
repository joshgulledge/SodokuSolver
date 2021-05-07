using System;
namespace SudokuSolver.Workers
{
    public class BoardDisplayer
    {
        public void Display(string title, int[,] sudokoBoard)
        {
            // as long the title isnt empy
            if (!title.Equals(string.Empty))
            {
                // print the title then and empty line
                Console.WriteLine("{0} {1}", title, Environment.NewLine);
            }

            //start looping through the multidimensional array
            for (int row = 0; row < sudokoBoard.GetLength(0); row++)
            {
                //start with this
                Console.Write("|");

                //then print the each element in array followed by a |
                for (int col = 0; col < sudokoBoard.GetLength(1); col++)
                {
                    Console.WriteLine("{0}{1}", sudokoBoard[row, col],"|");
                }
                //give another line
                Console.WriteLine();
            }
            //and another line
            Console.WriteLine();

        }
    }
}
