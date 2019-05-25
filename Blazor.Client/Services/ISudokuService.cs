using System;
using Blazor.Client.Models;
using Blazor.Client.Services.Sudoku;

namespace Blazor.Client.Services
{
    public interface ISudokuService
    {
        SudokuGrid CreatePuzzle();
    }


    public class SudokuService : ISudokuService
    {
        private readonly SudokuGenerator _generator;

        public SudokuService()
        {
            _generator = new SudokuGenerator();
        }

        public SudokuGrid CreatePuzzle()
        {
            Console.WriteLine("Generating puzzle");
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var puzzle = _generator.CreateGrid(SudokuGenerator.Difficulty.Easy);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Generated puzzle in {elapsedMs} ms.");

            return puzzle;
        }
    }
}