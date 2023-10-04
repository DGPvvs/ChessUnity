namespace ChessLibrary
{
    public class Chess
    {
        Board board;
        Moves moves;

        public string fen { get; private set; }

        IList<FigureMoving> allMoves;

        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            this.fen = fen;
            board = new Board(fen);
            moves = new Moves(board);

            this.allMoves = new List<FigureMoving>();
        }

        Chess(Board board)
        {
            this.board = board;
            this.fen = board.Fen;
            moves = new Moves(board);

            this.allMoves = new List<FigureMoving>();
        }

        public Chess Move(string move)
        {
            FigureMoving fm = new FigureMoving(move);

            if (!moves.CanMove(fm))
            {
                return this;
            }

            if (board.IsCheckAfterMove(fm))
            {
                return this;
            }

            Board nextBoard = board.Move(fm);

            Chess nextChess = new Chess(nextBoard);

            return nextChess;
        }

        public char GetFigureAt(int x, int y)
        {
            Square square = new Square(x, y);
            Figure f = this.board.GetFigureAt(square);

            char result = '.';

            if (!f.Equals(Figure.None))
            {
                result = f.ToChar();
            }

            return result;
        }

        void FindAllMoves()
        {
            allMoves = new List<FigureMoving>();

            foreach (FigureOnSquare fs in board.YieldFigures())
            {
                foreach (Square to in Square.YieldSquares())
                {
                    FigureMoving fm = new FigureMoving(fs, to);

                    if (this.moves.CanMove(fm))
                    {
                        if (!board.IsCheckAfterMove(fm))
                        {
                            this.allMoves.Add(fm);
                        }
                    }
                }
            }
        }

        public ICollection<string> GetAllMoves()
        {
            FindAllMoves();

            List<string> list = new List<string>();

            foreach (FigureMoving fm in allMoves)
            {
                list.Add(fm.ToString());
            }

            return list;
        }

        public bool IsCheck()
        {
            return board.IsCheck();
        }
    }
}