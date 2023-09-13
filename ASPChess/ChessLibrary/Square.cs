namespace ChessLibrary
{
    internal struct Square
    {
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
        }

        public int X { get; init; }
        public int Y { get; init; }

        public bool OnBoard()
        {
            bool isValid = this.X >= 0 && this.X < 8;
            isValid = isValid && this.Y >= 0 && this.Y < 8;

            return isValid;
        }
    }
}
