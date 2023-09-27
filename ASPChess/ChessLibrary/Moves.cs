namespace ChessLibrary
{
    class Moves
    {
        FigureMoving fm;
        Board board;

        public Moves(Board board)
        {
            this.board = board;
        }

        public bool CanMove(FigureMoving fm)
        {
            this.fm = fm;
            bool result = this.CanMoveFrom() && this.CanMoveTo() && CanFigureMove();

            return result;
        }

        bool CanFigureMove()
        {
            bool result = false;

            switch (fm.Figure)
            {
                case Figure.WhiteKing:
                case Figure.BlackKing:
                    return CanKingMove();

                case Figure.WhiteQueen:
                case Figure.BlackQueen:
                    return false;

                case Figure.WhiteRook:
                case Figure.BlackRook:
                    return CanStraightMove(); 

                case Figure.WhiteBishop:
                case Figure.BlackBishop:
                    return false;

                case Figure.WhiteKnight:
                case Figure.BlackKnight:
                    return this.CanKnigthMove();

                case Figure.WhitePawn:
                case Figure.BlackPawn:
                    return false;

                default:
                    return result;
            }

            return result;
        }

        private bool CanStraightMove()
        {
            Square at = new Square(this.fm.From.X, this.fm.From.Y);

            do
            {
                at = new Square(at.X + this.fm.SingX, at.Y + this.fm.SingY);
                if (at.Equals(this.fm.To))
                {
                    return true;
                }
            } while (at.OnBoard() && this.board.GetFigureAt(at).Equals(Figure.None));

            return false;
        }

        private bool CanKingMove()
        {
            if (fm.AbsDelayX <= 1 && fm.AbsDelayY <= 1)
            {
                return true;
            }

            return false;
        }

        private bool CanKnigthMove()
        {
            if (fm.AbsDelayX == 1 && fm.AbsDelayY == 2)
            {
                return true;
            }

            if (fm.AbsDelayX == 2 && fm.AbsDelayY == 1)
            {
                return true;
            }

            return false;
        }

        bool CanMoveTo()
        {
            bool result = fm.To.OnBoard()
                && fm.From != fm.To
                && board.GetFigureAt(fm.To).GetColor() != board.MoveColor;

            return result;
        }

        bool CanMoveFrom()
        {
            bool result = fm.From.OnBoard()
                && fm.Figure.GetColor() == board.MoveColor;

            return result;
        }
    }
}
