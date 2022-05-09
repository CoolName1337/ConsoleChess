using ZExtended;
using System.Runtime.InteropServices;

namespace Chess
{
    static class Field
    {
        readonly static int size = 8;
        readonly static int sqrSize = 8;

        public static int[,] fieldB = new int[size, size];
        public static Chess[,] fieldC = new Chess[size, size];
        public static List<string> RendField = new List<string>();

        static public Sides NextSide() => nowSide != Sides.Black ? nowSide = Sides.Black : nowSide = Sides.White;
        static public Sides nowSide;

        static public void Render()
        {
            RendField.Clear();
            char CharNow;
            for (int i = 0; i < sqrSize; i++) 
            {
                for (int y = 0; y < size; y++) 
                {
                    string res = "";
                    for (int j = 0; j < sqrSize; j++) 
                    {
                        for (int x = 0; x < size; x++)
                        {
                            CharNow = fieldC[i, j].GetChar(x, y);

                            if (Cursor.position == new Vector(j, i)) {
                                if(((y > 0 || y < 7) && (x == 0 || x == 7)) || y == 0 || y == 7) { 
                                    res += "##";
                                    continue;
                                }
                            }
                            if (Cursor.chess != null && Cursor.chess.position == new Vector(j, i)) {
                                if (((y < 3 || y > 4) && (x == 0 || x == 7)) || (y == 0 || y == 7) && (x < 3 || x > 4)) {
                                    res += "##";
                                    continue;
                                }
                            }
                            if (CharNow == ' ') {
                                res += fieldB[i, j] == 1 ? "██" : "  ";
                            }
                            else {
                                for (int k = 0; k < 2; k++)
                                    res += fieldC[i, j].Side == Sides.Black ? '░' : '▓';
                            }
                        }
                    }
                    RendField.Add(res);
                }
            }
            Console.Clear();
            Console.Write(string.Join("\n", RendField));
        }
        
        public static void Start()
        {
            Chesses[] RowChessesTypes = new Chesses[] { Chesses.Rook, Chesses.Horse, Chesses.Elephant, Chesses.King, Chesses.Queen, Chesses.Elephant, Chesses.Horse, Chesses.Rook };
            
            void SetEmpty() {
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        fieldC[i, j] = new Chess(Chesses.Empty);
            }

            void SetField() {
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        fieldB[i, j] = j % 2 == 1 ? i % 2 : (i + 1) % 2;
            }

            void SetPawn() {
                for (int i = 0; i < 8; i++) { 
                    fieldC[1, i] = new Chess(Chesses.Pawn) { Side = Sides.Black };
                    fieldC[6, i] = new Chess(Chesses.Pawn) { Side = Sides.White };
                }
            }

            void SetOtherFig() {
                for (int i = 0; i < 8; i++) {
                    fieldC[0, i] = new Chess(RowChessesTypes[i]) { Side = Sides.Black };
                    fieldC[7, i] = new Chess(RowChessesTypes[i]) { Side = Sides.White };
                }
            }

            nowSide = Sides.White;
            SetEmpty();
            SetField();
            SetPawn();
            SetOtherFig();
        }
    }

    static class Cursor
    {
        public static Chess chess { get; set; }

        private static Vector pos = new Vector();
        public static Vector position { 
            get { return pos; }
            set { pos = new Vector(Extensions.Clamp(value.X, 0, 7), Extensions.Clamp(value.Y, 0, 7)); } 
        }
        public static void MoveChess() {
            Field.fieldC[chess.position.Y, chess.position.X] = new Chess(Chesses.Empty);
            Field.fieldC[position.Y, position.X] = chess;
            chess = null;
        }
        public static void CatchCess(Chess _chess) {
            chess = _chess;
            chess.position = position;
        }
    }

    internal static class Program 
    { 
        private static List<Chess> kings = new List<Chess>();
        private static void Optimization()
        { // Списал(((

            const int STD_OUTPUT_HANDLE = -11;

            [DllImport("kernel32.dll")]
            static extern IntPtr GetStdHandle(int handle);

            [DllImport("kernel32.dll", SetLastError = true)]
            static extern bool SetConsoleDisplayMode(IntPtr ConsoleHandle, uint Flags, IntPtr NewScreenBufferDimensions);

            var hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
            SetConsoleDisplayMode(hConsole, 1, IntPtr.Zero);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
        }
        private static void PrintStart() => Console.WriteLine("Any button to start play in my very cool chess. ESC to quit");
        private static void SetStandart() => Console.Title = "Chess";
        private static bool CheckStart() => Console.ReadKey().Key != ConsoleKey.Escape;
        private static void Begin()
        {
            Console.Clear();
            Field.Start();
            Field.Render();
        }
        private static bool CheckWinner()
        {
            kings.Clear();
            foreach (var chess in Field.fieldC)
                if (chess.type == Chesses.King)
                    kings.Add(chess);
            return kings.Count != 1;
        }
        private static void Update()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Spacebar:
                    Chess catchChess = Field.fieldC[Cursor.position.Y, Cursor.position.X];
                    if (Cursor.chess != null && catchChess.Side != Cursor.chess.Side) {
                        Cursor.MoveChess();
                        Field.NextSide();
                    }
                    else if (catchChess.Side == Field.nowSide && catchChess.type != Chesses.Empty) {
                        Cursor.CatchCess(catchChess);
                    }
                    break;
                case ConsoleKey.W:
                    Cursor.position += Vector.Down;
                    break;
                case ConsoleKey.S:
                    Cursor.position += Vector.Up;
                    break;
                case ConsoleKey.A:
                    Cursor.position += Vector.Left;
                    break;
                case ConsoleKey.D:
                    Cursor.position += Vector.Right;
                    break;
                case ConsoleKey.Escape:
                    Cursor.chess = null;
                    break;
            }
            Field.Render();
            Thread.Sleep(100);
        }
        private static void PrintWinner() {
            Console.Clear();
            Console.WriteLine($"{kings[0].Side} WIN !!!!");
            PrintStart();
        }
        
        static public void Main()
        {
            Optimization();
            PrintStart();
            SetStandart();
            while (CheckStart()) {
                Begin();
                while (CheckWinner()) {
                    Update();
                }
                PrintWinner();
            }

        }
    }
}