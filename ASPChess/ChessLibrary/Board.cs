namespace ChessLibrary
{
    using System.Text;
    using static ChessLibrary.Commons.GlobalConstants.ErrorMessages;

    internal class Board
    {
        private const string EigthOne = "11111111";

        private Figure[,] figures;
        private string? fen;        

        public Board(string fen)
        {
            this.Fen = fen;
            this.figures = new Figure[8, 8];
            this.Init();            
        }
        public string Fen
        {
            get => this.fen;
            private set => this.fen = value;
        }

        public Color MoveColor { get; private set; }

        public int MoveNumber { get; private set; }

        public Figure GetFigureAt(Square square)
        {
            if (square.OnBoard())
            {
                return this.figures[square.X, square.Y];
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

        public Board Move(FigureMoving figureMoving)
        {
            Board result = new Board(this.Fen);
            Figure figure = Figure.None;

            result.SetFigureAt(figureMoving.From, figure);

            if (figureMoving.Promotion.Equals(figure))
            {
                figure = figureMoving.Figure;
            }
            else
            {
                figure = figureMoving.Promotion;
            }

            result.SetFigureAt(figureMoving.To, figure);

            if (this.MoveColor.Equals(Color.Black))
            {
                this.MoveNumber++;
            }

            result.MoveColor = this.MoveColor.FlipColor();

            result.Fen = result.GenerateNewFen();

            return result;
        }

        private string GenerateNewFen()
        {
            string color = "w";
            if (this.MoveColor.Equals(Color.Black))
            {
                color = "b";
            }

            return $"{this.FenFigures()} {color} - - 0 {this.MoveNumber}";
        }

        private string FenFigures()
        {
            StringBuilder sb = new StringBuilder();

            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    if (this.figures[x, y].Equals(Figure.None))
                    {
                        sb.Append("1");
                    }
                    else
                    {
                        sb.Append(this.figures[x, y].ToChar());
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

        private void Init()
        {
            string[] fenParts = this.Fen.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (!fenParts.Length.Equals(6))
            {
                throw new ArgumentOutOfRangeException(WrongFEN);
            }

            this.InitFigures(fenParts[0]);

            if (fenParts[1].Equals("b"))
            {
                this.MoveColor = Color.Black;
            }
            else
            {
                this.MoveColor = Color.While;
            }

            this.MoveNumber = int.Parse(fenParts[5]);
        }

        private void InitFigures(string data)
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
    }
}
