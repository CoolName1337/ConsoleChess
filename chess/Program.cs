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

        public static int[,] fieldCursor = new int[size, size];

        public static List<string> RendField = new List<string>();

        static public Sides GetNextSide(Sides side) => side != Sides.Black ? Sides.Black : Sides.White;

        static public Sides nowSide;

        static public int count = 0;

        static public void Render()
        {
            RendField.Clear();
            char CharNow;
            for (int i = 0; i < sqrSize; i++) {
                for (int y = 0; y < size; y++) 
                {
                    string res = "";
                    for (int j = 0; j < sqrSize; j++) {
                        for (int x = 0; x < size; x++)
                        {
                            CharNow = fieldC[i, j].GetChar(x, y);

                            if (Cursor.position == new Vector(j, i)) {
                                if(((y > 0 || y < 7) && (x == 0 || x == 7)) || (y == 0 || y == 7)) { 
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
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    fieldC[i, j] = new Empty();

            Chess[] FIGURES_UP = new Chess[] { new Rook(), new Horse(), new Elephant(), new King(), new Queen(), new Elephant(), new Horse(), new Rook() };
            Chess[] FIGURES_DOWN = new Chess[] { new Rook(), new Horse(), new Elephant(), new King(), new Queen(), new Elephant(), new Horse(), new Rook() };

            void SetPawn() {
                for (int i = 0; i < 8; i++)
                    fieldC[1, i] = new Pawn() { Side = Sides.Black };
                for (int i = 0; i < 8; i++) 
                    fieldC[6, i] = new Pawn() { Side = Sides.White }; ;
            }


            void SetOtherFig() {
                for (int i = 0; i < 8; i++) {
                    FIGURES_UP[i].Side = Sides.Black;
                    fieldC[0, i] = FIGURES_UP[i];
                    FIGURES_DOWN[i].Side = Sides.White;
                    fieldC[7, i] = FIGURES_DOWN[i];
                }
            }

            void SetField() {
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        fieldB[i, j] = j % 2 == 1 ? i % 2 : (i + 1) % 2;
            }
            nowSide = Sides.White;
            count = 0;
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
            get  {
                return pos;
            }
            set {
                pos = new Vector(Extensions.Clamp(value.X, 0, 7), Extensions.Clamp(value.Y, 0, 7));
            } 
        }



    }



    internal static class Program
    {
        static List<Chess> kings = new List<Chess>();
        static void SetStandart()
        {
            Console.Title = "Chess";
            //Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            //Console.SetWindowPosition(0, 0);
        }
        static bool CheckWinner()
        {
            kings.Clear();
            foreach (var chess in Field.fieldC)
            {
                if (chess.type == Chesses.King)
                {
                    kings.Add(chess);
                }
            }
            return kings.Count != 1;
        }
        static Sides CheckWinnerSide() => kings[0].Side;

        static void Optimization() { // Списал(((

            const int STD_OUTPUT_HANDLE = -11;

            [DllImport("kernel32.dll")]
            static extern IntPtr GetStdHandle(int handle);

            [DllImport("kernel32.dll", SetLastError = true)]
            static extern bool SetConsoleDisplayMode(IntPtr ConsoleHandle, uint Flags, IntPtr NewScreenBufferDimensions);

            var hConsole = GetStdHandle(STD_OUTPUT_HANDLE);
            SetConsoleDisplayMode(hConsole, 1, IntPtr.Zero);
            Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
        }

        static void Begin()
        {
            Console.Clear();
            Field.Start();
            Field.Render();
        }


        static void Update()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Escape:
                    Cursor.chess = null;
                    Field.Render();
                    break;
                case ConsoleKey.Spacebar:
                    var catchChess = Field.fieldC[Cursor.position.Y, Cursor.position.X];
                    if (Cursor.chess != null && 
                        Cursor.chess.position != Cursor.position &&
                        catchChess.Side != Cursor.chess.Side) {

                        Field.fieldC[Cursor.chess.position.Y, Cursor.chess.position.X] = new Empty();
                        Field.fieldC[Cursor.position.Y, Cursor.position.X] = Cursor.chess;
                        Cursor.chess = null;
                        Field.nowSide = Field.GetNextSide(Field.nowSide);
                        Field.count++;
                        
                    }
                    else if (catchChess.Side == Field.nowSide && catchChess.type != Chesses.Empty) {
                        Cursor.chess = catchChess;
                        Cursor.chess.position = Cursor.position;
                    }
                    Field.Render();
                    break;
                case ConsoleKey.W:
                    Cursor.position += new Vector(0, -1);
                    Field.Render();
                    break;
                case ConsoleKey.S:
                    Cursor.position += new Vector(0, 1);
                    Field.Render();
                    break;
                case ConsoleKey.A:
                    Cursor.position += new Vector(-1, 0);
                    Field.Render();
                    break;
                case ConsoleKey.D:
                    Cursor.position += new Vector(1, 0);
                    Field.Render();
                    break;
                default:
                    break;

            }
            Thread.Sleep(100);
        }

        static bool CheckStart() => Console.ReadKey().Key != ConsoleKey.Escape;

        static void PrintWinner(Sides winner) {
            Console.Clear();
            Console.WriteLine($"{winner} WIN !!!!");
            PrintStart();
        }

        static void PrintStart() {
            Console.WriteLine("Any button to start play in my very cool chess. ESC to quit");
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
                PrintWinner(CheckWinnerSide());
            }

        }
    }
}