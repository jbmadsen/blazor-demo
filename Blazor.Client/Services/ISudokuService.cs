using System;
using System.Collections.Generic;
using System.Linq;
using Blazor.Client.Models;
using Blazor.Client.Models.Enums;
using Blazor.Client.Services.Sudoku;

namespace Blazor.Client.Services
{
    public interface ISudokuService
    {
        List<string> GetDifficulties();
        SudokuGrid CreatePuzzle(Difficulty difficulty);
    }


    public class SudokuService : ISudokuService
    {
        private readonly SudokuGenerator _generator;


        public SudokuService()
        {
            _generator = new SudokuGenerator();
        }


        public List<string> GetDifficulties()
        {
            return (Enum.GetNames(typeof(Difficulty))).ToList(); 
        }


        public SudokuGrid CreatePuzzle(Difficulty difficulty)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var puzzle = _generator.CreateGrid(difficulty);

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"Generated {difficulty} puzzle in {elapsedMs} ms.");

            return puzzle;
        }
    }
}