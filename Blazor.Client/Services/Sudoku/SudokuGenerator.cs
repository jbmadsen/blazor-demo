using System;
using System.Collections.Generic;
using System.Linq;
using Blazor.Client.Extensions;
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

        private readonly int? _seed;


        public SudokuGenerator()
        {
        }

        public SudokuGenerator(int seed)
        {
            _seed = seed;
        }


        public SudokuGrid CreateGrid(Difficulty difficulty)
        {
            var solver = new SudokuSolver(asGenerator: true);
            var grid = new SudokuGrid();
            var values = Enumerable.Range(1,9).ToList();

            values.Shuffle();

            for(int y = 0; y < 9; y++)
            {
                grid.Grid[0,y].Value = values[y];
            }

            for(int x = 1; x < 3; x++)
            {
                var list = values.Skip(x*3).Take(3).ToList();
                list.Shuffle();

                for(int y = 0; y < 3; y++)
                {
                    grid.Grid[x,y].Value = list[y];
                }
            }
            
            var solvedGrid = solver.Solve(grid);

            // TODO: Remove elements to match the difficulty

            return solvedGrid;
        }
    }
}