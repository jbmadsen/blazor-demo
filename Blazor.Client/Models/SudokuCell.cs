using System;

namespace Blazor.Client.Models
{

    public class SudokuCell
    {
        public Coordinate Position { get; set; }

        public int Value { get; set; }

        public bool Enabled { get; set; } // Starting values should not be editable


        public SudokuCell(int x, int y)
        {
            Position = new Coordinate(x, y);
        }
    }

}