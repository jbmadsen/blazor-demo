using Blazor.Client.Models;

namespace Blazor.Client.Services.Sudoku.Solvers
{
    public interface ISudokuSolver
    {
        (bool success, SudokuGrid grid) Solve(SudokuGrid grid);
    }
}