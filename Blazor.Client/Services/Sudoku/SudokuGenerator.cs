using System;
using System.Collections.Generic;
using System.Linq;
using Blazor.Client.Extensions;
using Blazor.Client.Models;
using Blazor.Client.Models.Enums;
using Blazor.Client.Services.Sudoku.Solvers;

namespace Blazor.Client.Services.Sudoku
{
    // With inspiration from: https://github.com/RutledgePaulV/sudoku-generator/blob/master/Sudoku/Generator.py
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
            
            PreAddToGrid(ref grid);
            
            var (success, solvedGrid) = solver.Solve(grid);

            // Minimize risk of getting a similar looking puzzle
            RandomizeLayout(ref grid);
            // Remove elements to match the difficulty
            RemoveElements(ref grid, difficulty);

            return solvedGrid;
        }


        private void PreAddToGrid(ref SudokuGrid grid)
        {
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
        }


        private void RandomizeLayout(ref SudokuGrid grid)
        {
            // Replace numbers 1-9 with another set of 1-9

            // Flip horizontally or vertically
            
            // Rotate 90, 180, 270 degrees

            // Or any combination of the above
        }


        private void RemoveElements(ref SudokuGrid grid, Difficulty difficulty)
        {
            var (toRemoveLogically, toRemoveRandomly) = GetDifficultySettingsParams(difficulty);
            var unRemovedLogically = RemoveElementsLogically(ref grid, toRemoveLogically);
            var unRemovedRandomly = RemoveElementsRandomly(ref grid, toRemoveRandomly);
            Console.WriteLine($"Removed {toRemoveLogically-unRemovedLogically} logically and {toRemoveRandomly-unRemovedRandomly} randomly.");
        }


        private (int toRemoveLogically, int toRemoveRandomly) GetDifficultySettingsParams(Difficulty difficulty)
        {
            switch (difficulty)
            {
                case Difficulty.Trivial:
                    return (20, 0);
                case Difficulty.Easy:
                default:
                    return (35, 0);
                case Difficulty.Medium:
                    return (81, 5);
                case Difficulty.Hard:
                    return (81, 10); 
                case Difficulty.VeryHard:
                    return (81, 20); 
            }
        }


        private int RemoveElementsLogically(ref SudokuGrid grid, int toRemove)
        {
            if (toRemove == 0)
            {
                return toRemove;
            }

            var values = Enumerable.Range(0,80).ToList();
            values.Shuffle(_seed);

            // Reset cell value if it does not make the board ambigous,
            // i.e. if it is not possible to place another value in its place
            foreach (var cell in values)
            {
                var x = cell % 9;
                var y = cell / 9;
                var validNumbers = grid.ValidValuesCount(x,y);
                if (validNumbers == 1)
                {
                    grid.Grid[x,y].Value = 0;
                    toRemove--;

                    if (toRemove == 0)
                    {
                        return 0;
                    }
                }
            }

            return toRemove;
        }


        private int RemoveElementsRandomly(ref SudokuGrid grid, int toRemove)
        {
            if (toRemove == 0)
            {
                return toRemove;
            }

            // Remove cells at random while attempting to keep a single unique solution:
            // Remove a cell at random, and attempt to solve the puzzle using remaining numbers,
            // If not possible the removal is a success. If possible, reset the value and move on.
            
            var values = Enumerable.Range(0,80).ToList();
            values.Shuffle(_seed);

            var solver = new SimpleSolver();

            foreach (var cell in values)
            {
                var x = cell % 9;
                var y = cell / 9;
                
                if (grid.Grid[x,y].Value == 0)
                {
                    continue;
                }

                var originalValue = grid.Grid[x,y].Value;
                
                var complementNumbers = Enumerable.Range(1,9).Except(new List<int>(grid.Grid[x,y].Value));
                var ambiguous = false;

                foreach (var number in complementNumbers)
                {
                    var validValuesForCell = grid.ValidValues(x,y);

                    if (validValuesForCell.Contains(number))
                    {
                        // No need to check if it is not possible to place the number
                        continue;
                    }

                    grid.Grid[x,y].Value = number;

                    var tempGrid = grid.DeepClone();
                    var (success, _) = solver.Solve(tempGrid);
                    if (success)
                    {
                        //Console.WriteLine($"Failed check for ({x},{y}) for value {number}. Original: {originalValue}");
                        grid.Grid[x,y].Value = originalValue;
                        ambiguous = true;
                        break;
                    }
                }

                if (ambiguous == false)
                {
                    Console.WriteLine($"Success check for ({x},{y}). Original: {originalValue}");
                    grid.Grid[x,y].Value = 0;
                    toRemove--;
                    
                    if (toRemove == 0)
                    {
                        return toRemove;
                    }
                }
            }

            return toRemove;
        }
    }
}