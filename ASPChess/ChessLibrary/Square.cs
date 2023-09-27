namespace ChessLibrary
{
    using System.Collections.Generic;

    struct Square
    {
        public static Square none = new Square(-1, -1);

        public Square(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Square(string e2) : this(-1, -1)
        {

            bool isValid = e2.Length.Equals(2) && e2[0] >= 'a' && e2[0] <= 'h' && e2[1] >= '1' && e2[1] <= '8';
            if (isValid)
            {
                this.X = e2[0] - 'a';
                this.Y = e2[1] - '1';
            }
            else
            {
                this = none;
            }
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public string Name => $"{(char)('a' + this.X)}{this.Y + 1}";

        public bool OnBoard()
        {
            bool isValid = this.X >= 0 && this.X < 8;
            isValid = isValid && this.Y >= 0 && this.Y < 8;

            return isValid;
        }

        public static bool operator ==(Square square1, Square square2) => square1.X == square2.X && square1.Y == square2.Y;

        public static bool operator !=(Square square1, Square square2) => square1.X != square2.X || square1.Y != square2.Y;

        public static IEnumerable<Square> YieldSquares()
        {
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    yield return new Square(x, y);
                }
            }
        }
    }
}
