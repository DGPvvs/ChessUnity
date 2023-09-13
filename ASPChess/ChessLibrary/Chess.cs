namespace ChessLibrary
{
    public class Chess
    {
        private string fen;
        private Board board;

        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            this.Fen = fen;
            this.board = new Board(this.Fen);
        }

        private Chess(Board board)
        {
            this.board = board;
        }

        public string Fen
        {
            get => this.fen;

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Невалиден аргумент!");
                }

                this.fen = value;
            }            
        }

        public Chess Move(string move)
        {
            FigureMoving figureMoving = new FigureMoving(move);

            Board newBoard = this.board.Move(figureMoving);

            Chess newChess = new Chess(newBoard);
            newChess.Fen = newBoard.Fen;

            return newChess;
        }

        public char GetFigureAt(int x, int y)
        {
            Square square = new Square(x, y);
            Figure figure = this.board.GetFigureAt(square);

            char result = '.';

            if (!figure.Equals(Figure.None))
            {
                result = figure.ToChar();
            }

            return result;
        }
    }
}