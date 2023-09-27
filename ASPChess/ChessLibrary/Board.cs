namespace ChessLibrary
{
    using System.Collections.Generic;
    using System.Text;
    using static ChessLibrary.Commons.GlobalConstants.ErrorMessages;

    class Board
    {
        private const string EigthOne = "11111111";

        Figure[,] figures;

        public Board(string fen)
        {
            this.Fen = fen;
            figures = new Figure[8, 8];
            Init();
        }
        public string Fen { get; private set; }

        public Color MoveColor { get; private set; }

        public int MoveNumber { get; private set; }

        public Figure GetFigureAt(Square square)
        {
            if (square.OnBoard())
            {
                char f = (char)figures[square.X, square.Y];
                if (f == '.')
                {
                    return Figure.None;
                }
                else
                {
                    Figure result = (Figure)f;
                    return result;
                }
            }

            return Figure.None;
        }

        public void SetFigureAt(Square square, Figure figure)
        {
            if (square.OnBoard())
            {
                this.figures[square.X, square.Y] = figure;
            }
        }

        public Board Move(FigureMoving fm)
        {
            Board next = new Board(this.Fen);

            next.SetFigureAt(fm.From, Figure.None);

            Figure figure = Figure.None;

            if (fm.Promotion == Figure.None)
            {
                figure = fm.Figure;
            }
            else
            {
                figure = fm.Promotion;
            }

            next.SetFigureAt(fm.To, figure);

            if (MoveColor == Color.Black)
            {
                next.MoveNumber++;
            }

            next.MoveColor = MoveColor.FlipColor();

            next.GenerateNewFen();

            return next;
        }

        void GenerateNewFen()
        {
            string color = "w";
            if (this.MoveColor.Equals(Color.Black))
            {
                color = "b";
            }

            Fen = $"{FenFigures()} {color} - - 0 {MoveNumber}";
        }

        string FenFigures()
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    Figure f = figures[x, y];
                    if ((char)f == '.' || f == Figure.None)
                    {
                        sb.Append("1");
                    }
                    else
                    {
                        sb.Append(figures[x, y].ToChar());
                    }
                }

                if (y > 0)
                {
                    sb.Append("/");
                }
            }

            for (int j = 8; j >= 2; j--)
            {
                sb.Replace(EigthOne.Substring(0, j), j.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        void Init()
        {
            string[] parts = Fen.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 6)
            {
                throw new ArgumentOutOfRangeException(WrongFEN);
            }

            this.InitFigures(parts[0]);

            this.MoveColor = Color.While;

            if (parts[1] == "b")
            {
                this.MoveColor = Color.Black;
            }

            this.MoveNumber = int.Parse(parts[5]);
        }

        void InitFigures(string data)
        {
            for (int j = 8; j >= 2; j--)
            {
                data = data.Replace(j.ToString(), $"{j - 1}1");
            }

            data = data.Replace('1', '.');

            string[] lines = data.Split('/', StringSplitOptions.RemoveEmptyEntries);

            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    Figure figure = Figure.None;

                    if (!lines[7 - y][x].Equals("."))
                    {
                        figure = (Figure)lines[7 - y][x];
                    }

                    this.figures[x, y] = figure;
                }
            }
        }

        public IEnumerable<FigureOnSquare> YieldFigures()
        {
            foreach (Square square in Square.YieldSquares())
            {
                if (GetFigureAt(square).GetColor() == MoveColor)
                {
                    yield return new FigureOnSquare(GetFigureAt(square), square);
                }
            }

        }
    }
}
