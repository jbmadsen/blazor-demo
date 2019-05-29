using System;

namespace Blazor.Client.Models
{
    public class SudokuCell
    {
        public Coordinate Position { get; set; }

        public int Value { get; set; } = 0;

        private bool _enabled;
        public bool Enabled 
        {
            get => _enabled;
            set
            {
                _enabled = value;
                SetValue(this.Value, this.Valid);
            } 
        } // Starting values should not be editable

        public bool Valid { get; set; } = true;

        public string ValueColor { get; set; }
        public Action<string> ValueColorChanged { get; set; }

        public string BackgroundColor { get; set; }
        public Action<string> BackgroundColorChanged { get; set; }

        public SudokuCell(int x, int y)
        {
            Position = new Coordinate(x, y);
        }

        public void ResetColors()
        {
            BackgroundColor = "ffffff";
            BackgroundColorChanged?.Invoke(BackgroundColor);
            ValueColor = "#686a6e";
            ValueColorChanged?.Invoke(ValueColor);
        }

        public void SetSelected(bool state)
        {
            if (Enabled == false)
            {
                return;
            }

            BackgroundColor = state ? "#add8e6" : "ffffff";
            BackgroundColorChanged?.Invoke(BackgroundColor);
        }

        public void SetValue(int value, bool valid)
        {
            this.Value = value;
            this.Valid = valid;

            if (Enabled == false || value == 0)
            {
                ValueColor = "#686a6e";
                ValueColorChanged?.Invoke(ValueColor);
                return;
            }

            ValueColor = Valid ? "#0000FF" : "#FF0000";
            ValueColorChanged?.Invoke(ValueColor);
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