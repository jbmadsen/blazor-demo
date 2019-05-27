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


        public bool IsSolved()
        {
            return false;
        }


        public List<int> ValidValues(int x, int y)
        {
            try
            {
                var inRow = NumbersInRow(Grid[x,y]);
                var inColumn = NumbersInColumn(Grid[x,y]);
                var inSquare = NumbersInSquare(Grid[x,y]);

                var values = Enumerable.Range(1,9).ToList();
                var remainder = values.Except(inRow).Except(inColumn).Except(inSquare);
                return remainder.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception for ({x},{y}): {ex}");
                return new List<int>();
            }
        }


        public int ValidValuesCount(int x, int y)
        {
            return ValidValues(x,y).Count;
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


        private IEnumerable<int> NumbersInRow(SudokuCell cell)
        {
            var result = new List<int>();
            for(int y = 0; y < 9; y++)
            {
                if (cell.Position.Y == y)
                {
                    continue;
                }

                if (Grid[cell.Position.X,y].Value != 0)
                {
                    result.Add(Grid[cell.Position.X,y].Value);
                }
            }

            return result.Distinct();
        }


        private IEnumerable<int> NumbersInColumn(SudokuCell cell)
        {
            var result = new List<int>();
            for(int x = 0; x < 9; x++)
            {
                if (cell.Position.X == x)
                {
                    continue;
                }

                if (Grid[x, cell.Position.Y].Value != 0)
                {
                    result.Add(Grid[x, cell.Position.Y].Value);
                }
            }

            return result.Distinct();;
        }


        private IEnumerable<int> NumbersInSquare(SudokuCell cell)
        {
            var result = new List<int>();

            var x = ( cell.Position.X / 3 ) * 3;
            var y = ( cell.Position.Y / 3 ) * 3;

            for (int i = x; i < x + 3; i++)
            {
                for (int j = y; j < y + 3; j++)
                {
                    if (Grid[i,j] == cell)
                    {
                        continue;
                    }

                    if (Grid[i, j].Value != 0)
                    {
                        result.Add(Grid[i, j].Value);
                    }
                }
            }

            return result.Distinct();
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


        public SudokuGrid DeepClone()
        {
            var grid =  new SudokuGrid();
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    grid.Grid[x,y] = _grid[x,y].DeepClone();
                }
            }
            return grid;
        }
    }

}