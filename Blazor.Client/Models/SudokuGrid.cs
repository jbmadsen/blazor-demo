using System;
using System.Linq;
using System.Collections.Generic;

namespace Blazor.Client.Models
{

    public class SudokuGrid
    {
        private SudokuCell[,] _grid = new SudokuCell[9,9];
        public SudokuCell[,] Grid => _grid;

        public IEnumerable<SudokuCell> GridAsIEnumerable => 
            from SudokuCell item in _grid select item; // Convert 2D array to an Ienumerable;

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

        public void PrintAll()
        {
            var str = "";
            foreach(var cell in GridAsIEnumerable)
            {
                str += $"{cell.Value}";
                if (cell.Position.Y == 8)
                {
                    str += "\n";
                }
            }
            Console.WriteLine(str);
        }
    }

}