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
                if (sudokuBoard[givenRow, col] != sudokuBoard[givenRow, givenCol] &&
                    sudokuBoard[givenRow, col].ToString().Length > 1)
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
                sudokuBoard[eliminateFromRow, eliminateFromCol] = Convert.ToInt32(sudokuBoard[eliminateFromRow, eliminateFromCol].ToString().Replace(valueToEliminate.ToString(), string.Empty));
            }
        }

        private void EliminatePairFromCol(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            // if no pair then dont do anything
            if (!HasPairInCol(sudokuBoard, givenRow, givenCol))
            {
                return;
            }

            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                if (sudokuBoard[row, givenCol] != sudokuBoard[givenRow, givenCol] &&
                    sudokuBoard[row, givenCol].ToString().Length > 1)
                {
                    EliminatePair(sudokuBoard, sudokuBoard[givenRow, givenCol], row, givenCol);
                }
            }
        }

        private void EliminatePairFromBlock(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            if (!HasPairInBlock(sudokuBoard, givenRow, givenCol))
            {
                return;
            }

            var sudokuMap = _sudokuMapper.Find(givenRow, givenCol);

            for (int row = sudokuMap.StartRow; row <= sudokuMap.StartRow + 2; row++)
            {
                for (int col = sudokuMap.StartCol; col <= sudokuMap.StartCol + 2; col++)
                {
                   if (sudokuBoard[row, col].ToString().Length > 1 && sudokuBoard[row, col] != sudokuBoard[givenRow, givenCol])
                    {
                        EliminatePair(sudokuBoard, sudokuBoard[givenRow, givenCol], row, col);
                    }
                }
            }
        }

        private bool HasPairInBlock(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                for (int col = 0; col < sudokuBoard.GetLength(1); col++)
                {
                    var elementSame = givenRow == row && givenCol == col;
                    var elementInSameBlock = _sudokuMapper.Find(givenRow, givenCol).StartRow == _sudokuMapper.Find(row, col).StartRow &&
                        _sudokuMapper.Find(givenRow, givenCol).StartCol == _sudokuMapper.Find(row, col).StartCol;

                    if (!elementSame && elementInSameBlock && IsNakedPair(sudokuBoard[givenRow, givenCol], sudokuBoard[row, col]))
                    {
                        return true;
                    }
                }
            }
            return false;
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

        private bool HasPairInCol(int[,] sudokuBoard, int givenRow, int givenCol)
        {
            for (int row = 0; row < sudokuBoard.GetLength(0); row++)
            {
                if (givenRow != row && IsNakedPair(sudokuBoard[row, givenCol], sudokuBoard[givenRow, givenCol]))
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
