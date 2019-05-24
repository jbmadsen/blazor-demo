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
            return _generator.CreateGrid(SudokuGenerator.Difficulty.Easy);
        }
    }
}