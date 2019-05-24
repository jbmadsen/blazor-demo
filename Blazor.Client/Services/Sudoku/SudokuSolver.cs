using System;
using System.Linq;
using System.Collections.Generic;
using Blazor.Client.Models;

namespace Blazor.Client.Services.Sudoku
{
    public class SudokuSolver
    {
        public SudokuSolver()
        {
            
        }


        public SudokuGrid Solve(SudokuGrid grid)
        {
            SolverFunction(ref grid);
            return grid;
        }


        private bool SolverFunction(ref SudokuGrid grid)
        {
            //grid.PrintAll(); // Debug only

            if (IsFull(grid))
            {
                return true;
            }
            
            var cell = GetNextEmptyCell(grid);
            var possibleValues = PossibleValuesForPosition(grid, cell);

            for(int i = 1; i < 10; i++)
            {
                if (possibleValues.Any(v => v == i))
                {
                    cell.Value = i;
                    if (SolverFunction(ref grid))
                    {
                        return true;
                    }
                }
            }

            cell.Value = 0;
            return false;
        }


        private SudokuCell GetNextEmptyCell(SudokuGrid grid)
        {
            for(int x = 0; x < 9; x++)
            {
                for(int y = 0; y < 9; y++)
                {
                    if (grid.Grid[x,y].Value == 0)
                    {
                        return grid.Grid[x,y];
                    }
                }
            }
            return null;
        }


        private bool IsFull(SudokuGrid grid)
        {
            var cells = grid.GridAsIEnumerable;
            return cells.All(c => c.Value != 0);
        }


        public List<int> PossibleValuesForPosition(SudokuGrid grid, SudokuCell cell)
        {
            var cells = grid.GridAsIEnumerable;
            var possibleValues = new List<int>();

            for(int i = 1; i < 10; i++)
            {
                if (CanAddValue(cells, cell, i) == false)
                {
                    continue;
                }
                possibleValues.Add(i);
            }

            return possibleValues;
        }


        public bool CanAddValue(IEnumerable<SudokuCell> cells, SudokuCell cell, int value)
        {
            if (IsInRow(cells, cell, value) || IsInColumn(cells, cell, value) || IsInSquare(cells, cell, value))
            {
                return false;
            }
            return true;
        }


        private bool IsInRow(IEnumerable<SudokuCell> cells, SudokuCell cell, int value)
        {
            var isInRow = cells.Any(c => c.Position.X == cell.Position.X && c.Value == value);
            return isInRow;
        }


        private bool IsInColumn(IEnumerable<SudokuCell> cells, SudokuCell cell, int value)
        {
            var isInRow = cells.Any(c => c.Position.Y == cell.Position.Y && c.Value == value);
            return isInRow;
        }


        private bool IsInSquare(IEnumerable<SudokuCell> cells, SudokuCell cell, int value)
        {
            var x = ( cell.Position.X / 3 ) * 3;
            var y = ( cell.Position.Y / 3 ) * 3;

            for (int i = x; i < x + 3; i++)
            {
                for (int j = y; j < y + 3; j++)
                {
                    if (cells.Any(c => c.Position.X == i && c.Position.Y == j && c.Value == value))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}