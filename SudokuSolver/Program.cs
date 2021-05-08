using System;
using System.Linq;
using SudokuSolver.Strategies;
using SudokuSolver.Workers;

namespace SudokuSolver
{
    class Program
    {
        static void Main (string[] args)
        {
            try
            {
                SudokuMapper sudokuMapper = new SudokuMapper();
                BoardStateManager boardStateManager = new BoardStateManager();
                SudokuSolverEngine sudokuSolverEngine = new SudokuSolverEngine(boardStateManager, sudokuMapper);
                SudokuFileReader sudokuFileReader = new SudokuFileReader();
                BoardDisplayer boardDisplayer = new BoardDisplayer();

                Console.WriteLine("Please enter the FileName that contains the sudoku puzzle");
                var fileName = Console.ReadLine();

                var sudokuBoard = sudokuFileReader.ReadFile(fileName);
                boardDisplayer.Display("Initial State", sudokuBoard);

                bool isSudokuSolved = sudokuSolverEngine.Solve(sudokuBoard);
                boardDisplayer.Display("Final State", sudokuBoard);

                Console.WriteLine(isSudokuSolved ? "Puzzle was solved" : "Falied to solve the puzzle");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong, try again" + ex.Message);
            }
        }
    }
}
