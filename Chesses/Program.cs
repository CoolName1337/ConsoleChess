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
    class RendChess
    {
        public static Dictionary<Chesses, string[]> RENDER_CHESS = new Dictionary<Chesses, string[]>()
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

    public abstract class Chess
    {
        abstract public Chesses type { get; }
        public Sides Side { get; set; }
        public char GetChar(int x, int y) => RendChess.RENDER_CHESS[type][y][x];
        public Vector position { get; set; }
        public Vector[] CanMoveTo { get; set; }
    }
    public class Empty : Chess {
        public override Chesses type => Chesses.Empty;
    }
    public class Pawn : Chess {
        public override Chesses type => Chesses.Pawn;
    }
    public class Horse : Chess {
        public override Chesses type => Chesses.Horse;
    }
    public class Elephant : Chess {
        public override Chesses type => Chesses.Elephant;
    }
    public class Rook : Chess
    {
        public override Chesses type => Chesses.Rook;
    }
    public class King : Chess {
        public override Chesses type => Chesses.King;
    }
    public class Queen : Chess {
        public override Chesses type => Chesses.Queen;
    }

    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("█▓▒░");
        }
    }

}