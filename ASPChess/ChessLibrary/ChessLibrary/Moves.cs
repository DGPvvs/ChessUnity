namespace ChessLibrary
{
    using System;

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
                    result = CanKingMove();
                    break;

                case Figure.WhiteQueen:
                case Figure.BlackQueen:
                    result = CanStraightMove();
                    break;

                case Figure.WhiteRook:
                case Figure.BlackRook:
                    if (fm.SingX == 0 || fm.SingY == 0)
                    {
                        result = CanStraightMove();
                    }
                    break;
                case Figure.WhiteBishop:
                case Figure.BlackBishop:
                    if (fm.SingX != 0 && fm.SingY != 0)
                    {
                        result = CanStraightMove();
                    }
                    break;

                case Figure.WhiteKnight:
                case Figure.BlackKnight:
                    result = CanKnigthMove();
                    break;

                case Figure.WhitePawn:
                case Figure.BlackPawn:
                    result = CanPownMove();
                    break;

                default:
                    return result;
            }

            return result;
        }

        private bool CanPownMove()
        {
            if (fm.From.Y < 1 || fm.From.Y > 6)
            {
                return false;
            }

            int stepY = -1;

            if (fm.Figure.GetColor() == Color.While)
            {
                stepY = 1;
            }

            return CanPawnGo(stepY)
                || CanPawnJump(stepY)
                || CanPawnEat(stepY);
        }

        private bool CanPawnEat(int stepY)
        {
            if (board.GetFigureAt(fm.To) != Figure.None && (char)board.GetFigureAt(fm.To) != '.')
            {
                if (fm.AbsDeltaX == 1)
                {
                    if (fm.DeltaY == stepY)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CanPawnJump(int stepY)
        {
            if ((board.GetFigureAt(fm.To) == Figure.None) || (char)board.GetFigureAt(fm.To) == '.')
            {
                if (fm.DeltaX == 0)
                {
                    if (fm.DeltaY == 2 * stepY)
                    {
                        if (fm.From.Y == 1 || fm.From.Y == 6)
                        {
                            if ((board.GetFigureAt(new Square(fm.From.X, fm.From.Y + stepY)) == Figure.None) || (char)board.GetFigureAt(fm.To) == '.')
                            {
                                return true;
                            }                            
                        }
                    }
                }
            }

            return false;
        }

        private bool CanPawnGo(int stepY)
        {
            if ((board.GetFigureAt(fm.To) == Figure.None) || (char)board.GetFigureAt(fm.To) == '.')
            {
                if (fm.DeltaX == 0)
                {
                    if (fm.DeltaY == stepY)
                    {
                        return true;
                    }
                }
            }

            return false;
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
