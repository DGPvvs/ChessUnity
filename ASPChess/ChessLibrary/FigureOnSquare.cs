namespace ChessLibrary
{
    internal class FigureOnSquare
    {
        public FigureOnSquare(Figure figure, Square square)
        {
            this.Figure = figure;
            this.Square = square;
        }

        public Figure Figure { get; init; }

        public Square Square { get; init; }
    }
}
