namespace ChessLibrary
{
    class FigureMoving : IComparable<FigureMoving>
    {
        public FigureMoving(FigureOnSquare figureOnSquare, Square to, Figure promotion = Figure.None)
        {
            this.Figure = figureOnSquare.Figure;
            this.From = figureOnSquare.Square;
            this.To = to;
            this.Promotion = promotion;
        }

        public FigureMoving(string move)
        {
            this.Figure = (Figure)move[0];
            this.From = new Square(move.Substring(1, 2));
            this.To = new Square(move.Substring(3, 2));

            if (move.Length == 6)
            {
                this.Promotion = (Figure)move[5];
            }
            else
            {
                this.Promotion = Figure.None;
            }
        }

        public Figure Figure { get; private set; }

        public Square From { get; private set; }

        public Square To { get; private set; }

        public Figure Promotion { get; private set; }

        public int DeltaX => To.X - From.X;

        public int DeltaY => To.Y - From.Y;

        public int AbsDeltaX => Math.Abs(DeltaX);

        public int AbsDeltaY => Math.Abs(DeltaY);

        public int SingX => Math.Sign(DeltaX);

        public int SingY => Math.Sign(DeltaY);

        public int CompareTo(FigureMoving other)
        {
            int comp = this.From.X.CompareTo(other.To.X);

            if (comp == 0)
            {
                comp = this.From.Y.CompareTo(other.To.Y);
            }

            return comp;
        }

        public override bool Equals(object? obj)
        {
            FigureMoving? fm = obj as FigureMoving;

            if (fm is null)
            {
                return false;
            }

            return fm.CompareTo(this).Equals(0);
        }

        public override int GetHashCode()
        {
            return ((this.From.X.GetHashCode() ^ 2) * this.From.Y.GetHashCode())
                 + ((this.To.X.GetHashCode() ^ 2) * this.To.Y.GetHashCode());
        }

        public override string ToString()
        {
            string promotion = string.Empty;

            if (Promotion != Figure.None)
            {
                promotion = Promotion.ToString();
            }

            return $"{Figure.ToString()} {From.Name} {To.Name} {promotion}";
        }
    }
}
