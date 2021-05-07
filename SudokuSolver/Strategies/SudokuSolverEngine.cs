using System;
using System.Collections.Generic;
using System.Linq;
using SudokuSolver.Workers;

namespace SudokuSolver.Strategies
{
    public class SudokuSolverEngine
    {
        // new instances of the state manager and mapper classes
        private readonly BoardStateManager _boardStateManager = new BoardStateManager();
        private readonly SudokuMapper _sudokuMapper = new SudokuMapper();

        // instances of classes are passed in as dependant argumnets of the solver engine
        public SudokuSolverEngine(BoardStateManager boardStateManager, SudokuMapper sudokuMapper)
        {
            // set the instances as private variables inside this class
            _boardStateManager = boardStateManager;
            _sudokuMapper = sudokuMapper;
        }

        // returns whether this board is solved or not
        public bool Solve(int[,] sudokuBoard)
        {
            // list of all the strategies
            List<ISudokuStrategy> strategies = new List<ISudokuStrategy>()
            {

            };

            // has the state of the board 
            var currentState = _boardStateManager.GenerateState(sudokuBoard);
            var nextState = _boardStateManager.GenerateState(strategies.First().Solve(sudokuBoard));

            // continue solving with all the strategies unil its solved
            while(!_boardStateManager.IsSolved(sudokuBoard) && currentState != nextState)
            {
                // move state to next state to make sure the board is updated
                currentState = nextState;

                foreach (var strategy in strategies)
                {
                    nextState = _boardStateManager.GenerateState(strategy.Solve(sudokuBoard));

                }
            }

            return _boardStateManager.IsSolved(sudokuBoard);
        }
    }
}
