using ZExtended;

namespace Chess
{
    public enum Chesses : byte
    {
        Pawn,
        Horse,
        Elephant,
        Rook,
        King,
        Queen,
        Empty
    }
    public enum Sides : byte
    {
        None,
        White,
        Black
    }
    internal static class RendChess
    {
        public static Dictionary<Chesses, string[]> RENDER_CHESS = new Dictionary<Chesses, string[]>
        {
            {
                Chesses.Pawn , new string[]
                {
                    "        ",
                    "        ",
                    "  ▓▓▓▓  ",
                    "   ▓▓   ",
                    "  ▓▓▓▓  ",
                    " ▓▓▓▓▓▓ ",
                    "  ▓▓▓▓  ",
                    "        ",
                }
            },
            {
                Chesses.Horse , new string[]
                {
                    "   ▓▓   ",
                    " ▓▓▓▓▓  ",
                    "▓▓▓  ▓▓ ",
                    "     ▓▓ ",
                    "    ▓▓▓ ",
                    "   ▓▓▓  ",
                    "  ▓▓▓▓▓ ",
                    " ▓▓▓▓▓▓▓ ",
                }
            },
            {
                Chesses.Elephant , new string[]
                {
                    "   ▓▓   ",
                    "  ▓▓▓▓  ",
                    " ▓▓▓▓▓▓ ",
                    "  ▓▓▓▓  ",
                    "   ▓▓   ",
                    "   ▓▓   ",
                    "  ▓▓▓▓  ",
                    " ▓▓▓▓▓▓ ",
                }
            },
            {
                Chesses.Rook , new string[]
                {
                    "▓  ▓▓  ▓",
                    " ▓▓▓▓▓▓ ",
                    " ▓▓▓▓▓▓ ",
                    "  ▓▓▓▓  ",
                    "  ▓▓▓▓  ",
                    " ▓▓▓▓▓▓ ",
                    " ▓▓▓▓▓▓ ",
                    "▓▓▓▓▓▓▓▓",
                }
            },
            {
                Chesses.King , new string[]
                {
                    "   ▓▓   ",
                    " ▓▓▓▓▓▓ ",
                    "  ▓▓▓▓  ",
                    "   ▓▓   ",
                    "   ▓▓   ",
                    "   ▓▓   ",
                    "  ▓▓▓▓  ",
                    " ▓▓▓▓▓▓ ",
                }
            },
            {
                Chesses.Queen , new string[]
                {
                    " ▓ ▓▓ ▓ ",
                    "  ▓▓▓▓  ",
                    "   ▓▓   ",
                    "   ▓▓   ",
                    "   ▓▓   ",
                    "  ▓▓▓▓  ",
                    "  ▓▓▓▓  ",
                    " ▓▓▓▓▓▓ ",
                }
            },
            {
                Chesses.Empty , new string[]
                {
                    "        ",
                    "        ",
                    "        ",
                    "        ",
                    "        ",
                    "        ",
                    "        ",
                    "        ",
                }
            },
    };
    }

    public class Chess
    {
        public Chess(Chesses chessType) {
            type = chessType;
            position = new Vector(0, 0);
        }
        public Chesses type { get; }
        public Sides Side { get; set; }
        public char GetChar(int x, int y) => RendChess.RENDER_CHESS[type][y][x];
        public Vector position { get; set; }
    }
    internal class Program
    {
        static void Main()
        {
            
        }
    }

}