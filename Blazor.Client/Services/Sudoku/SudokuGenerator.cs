using System;
using System.Collections.Generic;
using System.Linq;
using Blazor.Client.Extensions;
using Blazor.Client.Models;
using Blazor.Client.Models.Enums;
using Blazor.Client.Services.Sudoku.Solvers;

namespace Blazor.Client.Services.Sudoku
{
    public class SudokuGenerator
    {
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
            ISudokuSolver solver = new SimpleSolver(seed: _seed, asGenerator: true);
            var grid = new SudokuGrid();
            var values = Enumerable.Range(1,9).ToList();

            values.Shuffle(_seed);

            for(int y = 0; y < 9; y++)
            {
                grid.Grid[0,y].Value = values[y];
            }

            for(int x = 1; x < 3; x++)
            {
                var list = values.Skip(x*3).Take(3).ToList();
                list.Shuffle(_seed);

                for(int y = 0; y < 3; y++)
                {
                    grid.Grid[x,y].Value = list[y];
                }
            }
            
            var solvedGrid = solver.Solve(grid);

            // Remove elements to match the difficulty
            RemoveElements(ref grid, difficulty);

            return solvedGrid;
        }


        private void RemoveElements(ref SudokuGrid grid, Difficulty difficulty)
        {
            var values = Enumerable.Range(1,82).ToList();
            values.Shuffle(_seed);
            var elementsToRemove = 0;

            switch (difficulty)
            {
                case Difficulty.Easy:
                    elementsToRemove = 35;
                    break;
                case Difficulty.Medium:
                    elementsToRemove = 45;
                    break;
                case Difficulty.Hard:
                    elementsToRemove = 55;
                    break;
            }

            values = values.Take(elementsToRemove).ToList();

            for(int x = 0; x < 9; x++)
            {
                for(int y = 0; y < 9; y++)
                {
                    if (values.Contains(x * 9 + y))
                    {
                        grid.Grid[x,y].Value = 0;
                    }
                    else
                    {
                        grid.Grid[x,y].Enabled = false;
                    }
                }
            }
        }
    }
}