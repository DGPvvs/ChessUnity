namespace ChessDemo
{
    using ChessLibrary;
    using System.Text;

    public class Program
    {
        static void Main(string[] args)
        {
            Chess chess = new Chess();

            bool isLoopExit = false;

            while (!isLoopExit)
            {
                Console.Clear();
                Console.WriteLine(chess.fen);
                Print(ChessToAscii(chess));

                Console.WriteLine(string.Join(Environment.NewLine, chess.GetAllMoves()));                

                string move = Console.ReadLine();

                if (string.IsNullOrEmpty(move))
                {
                    isLoopExit = true;
                }
                else
                {
                    chess = chess.Move(move);
                }
            }
        }

        static string ChessToAscii(Chess chess)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($" +-----------------+");

            for (int y = 7; y >= 0; y--)
            {
                sb.Append($"{y + 1} | ");

                for (int x = 0; x < 8; x++)
                {
                    sb.Append($"{chess.GetFigureAt(x, y)} ");
                }

                sb.AppendLine("|");
            }

            sb.AppendLine($" +-----------------+");
            sb.AppendLine($"    a b c d e f g h");

            return sb.ToString();
        }

        static void Print(string text)
        {
            ConsoleColor oldForeColor = Console.ForegroundColor;
            foreach (var x in text)
            {
                if (x >= 'a' && x <= 'z')
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                else if (x >= 'A' && x <= 'Z')
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }

                Console.Write(x);
            }

            Console.ForegroundColor = oldForeColor;
        }
    }
}