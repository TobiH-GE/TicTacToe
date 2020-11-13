using System;

namespace TicTacToe
{
    class UIConsole : UI
    {
        public Game game;
        ConsoleColor[] pColor = new ConsoleColor[2] { ConsoleColor.Red, ConsoleColor.Blue };

        public override void PrintStatus(string str)
        {           
            
        }
        public override void PrintError(string str)
        {
            Text($"{str}             ", 5, 16, ConsoleColor.White, ConsoleColor.Red);
        }
        public override void PrintInfo(string str)
        {
            Text($"{str}", 5, 15);
        }
        public override void PrintHint(byte x, byte y)
        {
            if (x == 9) // wenn x/y = 9 dann ist kein Hint vorhanden
            {
                Text("Hint: no hint! choose an empty field!         ", 5, 16, ConsoleColor.DarkGray, ConsoleColor.Black);
            }
            else
            {
            Text($"Hint: {x}, {y}                           ", 5, 16, ConsoleColor.DarkGray, ConsoleColor.Black);
            Text("H", 12 + x * 4, 7 + y * 2, ConsoleColor.DarkGray, ConsoleColor.Black);
            }
        }
        public override void Start(Game game)
        {
            this.game = game;

            Console.Clear();
            Text("TicTacToe by TobiH\n",10,0);
        }
        public override void WaitForInput()
        {
            Point input;

            do
            {
                Draw(game.board);
                do
                {
                    PrintInfo("enter [0,1,2] or [9] for hint ... x-position: ");
                    input.x = (byte)EnterPosition();
                    if (input.x == 9) game.DrawHint();
                } while (input.x == 3 || input.x == 9);
                do
                {
                    PrintInfo("enter [0,1,2] or [9] for hint ... y-position: ");
                    input.y = (byte)EnterPosition();
                    if (input.y == 9) game.DrawHint();
                } while (input.y == 3 || input.y == 9);

                if (game.turn(input) == TurnResult.Invalid)
                {
                    PrintError("invalid!");
                    Console.ReadLine();
                }

            } while (game.turnNumber < 10);

            if (game.turnNumber == 10)
            {
                PrintInfo("tie!                                                 ");
            }
            else
            {
                Draw(game.board);
                PrintInfo("win!                                                 ");
            }

            PrintInfo("again? [y/n]!                                            ");
        }
        public static void Text(string text, int x, int y, ConsoleColor fColor = ConsoleColor.White, ConsoleColor bColor = ConsoleColor.Black)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = fColor;
            Console.BackgroundColor = bColor;
            Console.Write(text);
        }

        public override void Draw(FieldState[,] board)
        {
            Console.CursorVisible = false;
            Text($"turn {game.turnNumber}, {game.playerNames[Convert.ToInt32(game.currentPlayerID)]} [{(game.currentPlayerID ? FieldState.X : FieldState.O)}] it's your turn!\n", 0, 2, (game.currentPlayerID ? pColor[0] : pColor[1]));
            Text("0   1   2", 12, 5);
            for (int y = 0; y < 3; y++)
            {
                Text("   -------------", 7, 6);
                Text("0  |   |   |   |", 7, 7);
                Text("   -------------", 7, 8);
                Text("1  |   |   |   |", 7, 9);
                Text("   -------------", 7, 10);
                Text("2  |   |   |   |", 7, 11);
                Text("   -------------", 7, 12);
            }
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (board[y, x] == FieldState.O || board[y, x] == FieldState.X)
                    {
                        Text($"{board[y, x]}", 12 + x * 4, 7 + y * 2, (board[y, x] == FieldState.X ? pColor[0] : pColor[1]));
                    }
                }
            }
            Console.CursorVisible = true;
        }
        public override int EnterPosition()
        {
            int Eingabe = 0;
            try
            {
                Eingabe = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                PrintError("Error, try again.");
                return 3;
            }
            if ((Eingabe >= 0 && Eingabe <= 2) || Eingabe == 9)
                return Eingabe;
            else
            {
                PrintError("Error, try again.");
                return 3;
            }
        }
    }
}
