using System;
using System.Collections.Generic;
using System.Linq;
using Blazor.Client.Models;

namespace Blazor.Client.Services.Sudoku
{
    public class SudokuGenerator
    {
        public enum Difficulty
        {
            Easy, 
            Medium, 
            Hard
        }

        private readonly Random _random;


        public SudokuGenerator()
        {
            _random = new Random();
        }

        public SudokuGenerator(int seed)
        {
            _random = new Random(seed);
        }


        public SudokuGrid CreateGrid(Difficulty difficulty)
        {
            var solver = new SudokuSolver();
            var grid = new SudokuGrid();
            var values = Enumerable.Range(1,9).ToList();

            Shuffle<int>(ref values);

            for(int y = 0; y < 9; y++)
            {
                grid.Grid[0,y].Value = values[y];
            }

            for(int x = 1; x < 3; x++)
            {
                var list = values.Skip(x*3).Take(3).ToList();
                Shuffle<int>(ref list);

                for(int y = 0; y < 3; y++)
                {
                    grid.Grid[x,y].Value = list[y];
                }
            }
            
            var solvedGrid = solver.Solve(grid);

            // TODO: Remove elements to match the difficulty

            return solvedGrid;
        }


        private void Shuffle<T>(ref List<T> list)  
        {  
            var count = list.Count;
            for (var i = 0; i < count - 1; ++i) {
                var r = _random.Next(i, count);
                var tmp = list[i];
                list[i] = list[r];
                list[r] = tmp;
            }
        }
    }
}