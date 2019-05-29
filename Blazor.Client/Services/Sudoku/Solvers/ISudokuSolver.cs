using Blazor.Client.Models;

namespace Blazor.Client.Services.Sudoku.Solvers
{
    // I might want to look at this solver, and port it to C# :
    // https://github.com/attractivechaos/plb/blob/master/sudoku/incoming/sudoku_solver.c

    public interface ISudokuSolver
    {
        (bool success, SudokuGrid grid) Solve(SudokuGrid grid);
    }
}