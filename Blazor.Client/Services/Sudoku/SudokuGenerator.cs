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

        private readonly Difficulty _difficulty;
        private readonly Random _random;


        public SudokuGenerator(Difficulty difficulty)
        {
            _difficulty = difficulty;
            _random = new Random();
        }

        public SudokuGenerator(Difficulty difficulty, int seed)
        {
            _difficulty = difficulty;
            _random = new Random(seed);
        }


        public SudokuGrid CreateGrid()
        {
            var grid = new SudokuGrid();
            var values = Enumerable.Range(1,9).ToList();

            for(int x = 0; x < 9; x++)
            {
                Shuffle<int>(ref values);
                for(int y = 0; y < 9; y++)
                {
                    grid.Grid[x,y].Value = values[y];
                }
            }

            return grid;
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