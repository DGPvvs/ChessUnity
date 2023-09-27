namespace ChessLibrary
{
    enum Color
    {
        None = 0,
        While = 1,
        Black = 2
    }

    static class ColorMethod
    {
        public static Color FlipColor(this Color color)
        {
            if (color.Equals(Color.Black))
            {
                return Color.While;
            }

            if (color.Equals(Color.While))
            {
                return Color.Black;
            }

            return Color.None;
        }
    }
}
