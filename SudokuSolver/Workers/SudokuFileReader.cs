using System;
using System.IO;
using System.Linq;

namespace SudokuSolver.Workers
{
    public class SudokuFileReader
    {
       public int[,] ReadFile(string filename)
        {
            // creates a multidimensional array wih 9 rows and 9 columns [row, column]
            int[,] sudokuBoard = new int[9, 9];

            try
            {
                var sudokuBoardLines = File.ReadAllLines(filename);
                // each row of numbers in the file is now a line of items

                int row = 0;
                foreach (var line in sudokuBoardLines)
                {
                    // rows looks like |0| | |2|3|7|6|8| |
                    string[] sudokuLineElements = line.Split('|').Skip(1).Take(9).ToArray();
                    // the split forces an empty string to each side of the |

                    int col = 0;
                    foreach (var lineElement in sudokuLineElements)
                    {
                        // if this is true ? do this : otherwise do this
                        sudokuBoard[row, col] = lineElement.Equals(" ") ? 0 : Convert.ToInt16(lineElement);

                        //go to next column
                        col++;
                    }

                    // go to next row
                    row++;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong reading the file" + ex.Message);
            }

            return sudokuBoard;
        }
    }
}
