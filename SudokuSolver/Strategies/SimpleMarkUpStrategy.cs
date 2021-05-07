using System;
using System.Linq;
using SudokuSolver.Workers;

namespace SudokuSolver.Strategies
{
    public class SimpleMarkUpStrategy : ISudokuStrategy
    {

        private readonly SudokuMapper _sudokuMapper;

        public SimpleMarkUpStrategy(SudokuMapper sudokuMapper)
        {
            _sudokuMapper = sudokuMapper;
        }

        // solves the board
        public int[,] Solve(int[,] sudokuBoard)
        {
            // loop through all the spots on the board
            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                for (int col = 0; col < sudokuBoard.GetLength(1); col++)
                {
                    // if the number isnt solved for the correct number, try to colve it
                    if (sudokuBoard[row, col] == 0 || sudokuBoard[row, col].ToString().Length > 1)
                    {
                        // find all the possibilities 
                        var possibilitiesInRowAndCol = GetPossibilitiesInRowAndCol(sudokuBoard, row, col);
                        var possibilitiesInBlock = GetPossibilitiesInBlock(sudokuBoard, row, col);
                        sudokuBoard[row, col] = GetPossibilityIntersection(possibilitiesInRowAndCol, possibilitiesInBlock);

                    }
                }
                
            }

            return sudokuBoard;
        }

        // get all the possibilities for the row and col
        private int GetPossibilitiesInRowAndCol(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            int[] possibilities = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // loop through all the whole column
           for (int col = 0; col < 9; col++)
            {
                // if the number is valid, then its the right number
                if(IsValidSingle(sudokuBoard[givenRow, col]))
                {
                    possibilities[sudokuBoard[givenRow, col] - 1] = 0;
                }
            }

            // loop through the whole row
            for (int row = 0; row < 9; row++)
            {
                // if the number is valid then its the right number
                if (IsValidSingle(sudokuBoard[row, givenCol]))
                {
                    possibilities[sudokuBoard[row, givenCol] - 1] = 0;
                }
            }

            // return a number from a string from all numbers that are left
            return Convert.ToInt32(String.Join(string.Empty, possibilities.Select(p => p).Where(p => p != 0)));
        }

       
        // gets all possibilites in the block
        private int GetPossibilitiesInBlock(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            int[] possibilities = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            // starts a map of the blocks with the given rows and columns
            var sudokuMap = _sudokuMapper.Find(givenRow, givenCol);

            // loops through all the blocks
            for (int row = sudokuMap.StartRow; row < sudokuMap.StartRow + 2; row++)
            {
                for (int col = sudokuMap.StartCol; col < sudokuMap.StartCol + 2; col++)
                {
                    // checks to see if the number is correct
                    if (IsValidSingle(sudokuBoard[row, col]))
                    {
                        possibilities[sudokuBoard[row, col] - 1] = 0;
                    }
                }

            }

            // return a number from a string from all numbers that are left
            return Convert.ToInt32(String.Join(string.Empty, possibilities.Select(p => p).Where(p => p != 0)));
        }

        private int GetPossibilityIntersection(int possibilitiesInRowAndCol, int possibilitiesInBlock)
        {
            var possibilitiesInRowAndColCharArray = possibilitiesInRowAndCol.ToString().ToCharArray();
            var possibilitiesInBlockCharArray = possibilitiesInBlock.ToString().ToCharArray();
            var possibilitiesSubset = possibilitiesInRowAndColCharArray.Intersect(possibilitiesInBlockCharArray);

            return Convert.ToInt32(string.Join(string.Empty, possibilitiesSubset));
        }

        private bool IsValidSingle(int cellDigit)
        {
            return cellDigit != 0 && cellDigit.ToString().Length == 1;
        }
    }
}
