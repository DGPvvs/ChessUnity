namespace ChessLibrary
{
    internal class Moves
    {
        public Moves(Board board)
        {
            this.Board = board;
        }

        public FigureMoving FigureMoving { get; set; }

        public Board Board { get; set; }

        public bool CanMove(FigureMoving figureMoving)
        {
            this.FigureMoving = figureMoving;
        }
    }
}
