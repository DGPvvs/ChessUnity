namespace ChessLibrary
{
    enum Figure
    {
        None,

        WhiteKing = 'K',
        WhiteQueen = 'Q',
        WhiteRook = 'R',
        WhiteBishop = 'B',
        WhiteKnight = 'N',
        WhitePawn = 'P',

        BlackKing = 'k',
        BlackQueen = 'q',
        BlackRook = 'r',
        BlackBishop = 'b',
        BlackKnight = 'n',
        BlackPawn = 'p'
    }

    internal static class FigureMethod
    {
        public static char ToChar(this Figure figure)
        {
            return (char)figure;
        }

        public static Color GetColor(this Figure figure)
        {
            if (figure.ToString().Contains("White"))
            {
                return Color.While;
            }

            if (figure.ToString().Contains("Black"))
            {
                return Color.Black;
            }

            return Color.None;
        }
    }

}
