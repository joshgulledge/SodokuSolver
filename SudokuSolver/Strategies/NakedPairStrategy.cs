using System;
using SudokuSolver.Workers;

namespace SudokuSolver.Strategies
{
    public class NakedPairStrategy : ISudokuStrategy
    {
        private readonly SudokuMapper _sudokuMapper;

        public NakedPairStrategy(SudokuMapper sudokuMapper)
        {
            _sudokuMapper = sudokuMapper;
        }

        public int[,] Solve(int[,] sudokuBoard)
        {
            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                for (int col = 0; col < sudokuBoard.GetLength(1); col++)
                {
                    EliminatePairFromRow(sudokuBoard, row, col);
                    EliminatePairFromCol(sudokuBoard, row, col);
                    EliminatePairFromBlock(sudokuBoard, row, col);

                }
            }

            return sudokuBoard;
        }

        
        

        private void EliminatePairFromRow(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            // if no pair then dont do anything
            if (!HasPairInRow(sudokuBoard, givenRow, givenCol))
            {
                return;
            }

            for (int col = 0; col < sudokuBoard.GetLength(1); col++)
            {
                if (sudokuBoard[givenRow, givenCol] != sudokuBoard[givenRow, givenCol] &&
                    sudokuBoard[givenRow, givenCol].ToString().Length > 1)
                {
                    EliminatePair(sudokuBoard, sudokuBoard[givenRow, givenCol], givenRow, col);
                }
            }

        }

        private void EliminatePair(int[,] sudokuBoard, int valuesToEliminate, int eliminateFromRow, int eliminateFromCol)
        {
            var valuesToEliminateArray = valuesToEliminate.ToString().ToCharArray();

            foreach (var valueToEliminate in valuesToEliminateArray)
            {
                sudokuBoard[eliminateFromRow, eliminateFromCol] = Convert.ToInt32(sudokuBoard[eliminateFromRow, eliminateFromCol].ToString().Replace(valueToEliminate.ToSting(), string.Empty));
            }
        }

        private void EliminatePairFromCol(int[,] sudokuBoard, int givenRow, int giveCol)
        {
            
        }

        private void EliminatePairFromBlock(int[,] sudokuBoard, int givenRow, int giveCol)
        {
            
        }

        private bool HasPairInRow(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            for (int col = 0; col < sudokuBoard.GetLength(1); col++)
            {
                if (givenCol != col && IsNakedPair(sudokuBoard[givenRow, col], sudokuBoard[givenRow, givenCol]))
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsNakedPair(int firstPair, int secondPair)
        {
            return firstPair.ToString().Length == 2 && firstPair == secondPair;
        }
    }
}
