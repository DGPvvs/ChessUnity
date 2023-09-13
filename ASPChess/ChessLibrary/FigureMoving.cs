namespace ChessLibrary
{
    internal class FigureMoving
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

            if (move.Length.Equals(6))
            {
                this.Promotion = (Figure)move[5];
            }
            else
            {
                this.Promotion = Figure.None;
            }
        }

        public Figure Figure { get; init; }

        public Square From { get; init; }

        public Square To { get; init; }

        public Figure Promotion { get; init; }
    }
}
