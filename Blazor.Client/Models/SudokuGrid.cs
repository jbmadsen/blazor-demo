using System;

namespace Blazor.Client.Models
{

    public class SudokuGrid
    {
        private SudokuCell[,] _grid = new SudokuCell[9,9];
        public SudokuCell[,] Grid => _grid;

        public SudokuGrid()
        {
            InitEmptyGrid();
        }

        private void InitEmptyGrid()
        {
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    _grid[x,y] = new SudokuCell(x,y);
                }
            }
        }

    }

}