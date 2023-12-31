﻿namespace ChessLibrary
{
    class FigureOnSquare
    {
        public FigureOnSquare(Figure figure, Square square)
        {
            this.Figure = figure;
            this.Square = square;
        }

        public Figure Figure { get; private set; }

        public Square Square { get; private set; }
    }
}
