using Blazor.Client.Models;

namespace Blazor.Client.Services.Sudoku.Solvers
{
    public interface ISudokuSolver
    {
        SudokuGrid Solve(SudokuGrid grid);
    }
}