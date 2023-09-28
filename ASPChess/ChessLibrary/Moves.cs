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
                    return CanStraightMove();

                case Figure.WhiteRook:
                case Figure.BlackRook:
                    if (fm.SingX == 0 || fm.SingY == 0)
                    {
                        return CanStraightMove(); 
                    }
                    break;
                case Figure.WhiteBishop:
                case Figure.BlackBishop:
                    if (fm.SingX != 0 && fm.SingY != 0)
                    {
                        return CanStraightMove();
                    }
                    break;

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
            Square at = fm.From;

            do
            {
                at = new Square(at.X + fm.SingX, at.Y + fm.SingY);
                Figure f = board.GetFigureAt(at);
                if (at == fm.To)
                {
                    return true;
                }
            } while (at.OnBoard() && board.GetFigureAt(at) == Figure.None);

            return false;
        }

        private bool CanKingMove()
        {
            if (fm.AbsDeltaX <= 1 && fm.AbsDeltaY <= 1)
            {
                return true;
            }

            return false;
        }

        private bool CanKnigthMove()
        {
            if (fm.AbsDeltaX == 1 && fm.AbsDeltaY == 2)
            {
                return true;
            }

            if (fm.AbsDeltaX == 2 && fm.AbsDeltaY == 1)
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
