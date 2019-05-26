using System;

namespace Blazor.Client.Models
{

    public class SudokuCell
    {
        public Coordinate Position { get; set; }

        public int Value { get; set; } = 0;

        public bool Enabled { get; set; } // Starting values should not be editable

        public bool Valid { get; set; } = true;


        public SudokuCell(int x, int y)
        {
            Position = new Coordinate(x, y);
        }


        public SudokuCell DeepClone()
        {
            return new SudokuCell(this.Position.X, this.Position.Y)
            {
                Value = this.Value,
                Valid = this.Valid,
                Enabled = this.Enabled,
            };
        }
    }

}