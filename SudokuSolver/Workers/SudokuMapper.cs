using System;
using SudokuSolver.Data;

namespace SudokuSolver.Workers
{
    public class SudokuMapper
    {
        public SudokuMap Find(int givenRow, int givenCol)
        {
            SudokuMap sudokuMap = new SudokuMap();

            // the first box on first row
            if ((givenRow >= 0 && givenRow <= 2) && givenCol >=0 && givenCol <= 2)
            {
                sudokuMap.StartRow = 0;
                sudokuMap.StartCol = 0;
            } // next is the second box on first row
           else if ((givenRow >= 0 && givenRow <= 2) && givenCol >= 3 && givenCol <= 5)
            {
                sudokuMap.StartRow = 0;
                sudokuMap.StartCol = 3;
            } // next is the third box on first row
            else if ((givenRow >= 0 && givenRow <= 2) && givenCol >= 6 && givenCol <= 8)
            {
                sudokuMap.StartRow = 0;
                sudokuMap.StartCol = 6;
            } // next is the first box on second row
            else if ((givenRow >= 3 && givenRow <= 5) && givenCol >= 0 && givenCol <= 2)
            {
                sudokuMap.StartRow = 3;
                sudokuMap.StartCol = 0;
            } // next is the second box on the second row
            else if ((givenRow >= 3 && givenRow <= 5) && givenCol >= 3 && givenCol <= 5)
            {
                sudokuMap.StartRow = 3;
                sudokuMap.StartCol = 3;
            } // next is the third box on the second row
            else if ((givenRow >= 3 && givenRow <= 5) && givenCol >= 6 && givenCol <= 8)
            {
                sudokuMap.StartRow = 3;
                sudokuMap.StartCol = 6;
            } // next is the first box on the third row
            else if ((givenRow >= 6 && givenRow <= 8) && givenCol >= 0 && givenCol <= 2)
            {
                sudokuMap.StartRow = 6;
                sudokuMap.StartCol = 0;
            } // next is the second box on the third row
            else if ((givenRow >= 6 && givenRow <= 8) && givenCol >= 3 && givenCol <= 5)
            {
                sudokuMap.StartRow = 6;
                sudokuMap.StartCol = 3;
            } // next is the third box on the third row
            else if ((givenRow >= 6 && givenRow <= 8) && givenCol >= 6 && givenCol <= 8)
            {
                sudokuMap.StartRow = 6;
                sudokuMap.StartCol = 6;
            }



            return sudokuMap;
        }
    }
}
