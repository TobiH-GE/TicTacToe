using System;

namespace TicTacToe
{
    partial class Program                               // *** TicTacToe von TobiH ***
    {
        enum FieldState
        {
            E, X, O, H
        }
        enum TurnResult
        {
            Valid, Invalid, Tie, Win
        }
        struct Point
        {
            public byte x, y;
        }
        static void Main(string[] args)
        {
            Game game = new Game();
            
            TurnResult result = TurnResult.Invalid;
            Point point;

            do
            {
                Draw(game.board);
                do
                {
                    Console.WriteLine("Bitte X-Position eingeben: ");
                    point.x = (byte)EingabePosition();
                } while (point.x == 3);
                do
                {
                    Console.WriteLine("Bitte Y-Position eingeben: ");
                    point.y = (byte)EingabePosition();
                } while (point.y == 3);

                result = game.turn(point);

            } while (result != TurnResult.Win && result != TurnResult.Tie);

            if (result == TurnResult.Win) Console.WriteLine("gewonnen!");
            else Console.WriteLine("unentschieden!");


            void Draw(FieldState[,] board)
            {
                Console.Clear();
                Console.WriteLine("TicTacToe von TobiH");
                Console.WriteLine("Runde {0}, Spieler {1} ist an der Reihe. Mach dein Spielzug.\n", game.turnNumber, game.currentPlayerID);

                Console.WriteLine("\n\n\t    0   1   2  ");
                Console.WriteLine("\t  -------------");
                Console.WriteLine("\t0 | {0} | {1} | {2} |", board[0, 0], board[0, 1], board[0, 2]);
                Console.WriteLine("\t  -------------");
                Console.WriteLine("\t1 | {0} | {1} | {2} |", board[1, 0], board[1, 1], board[1, 2]);
                Console.WriteLine("\t  -------------");
                Console.WriteLine("\t2 | {0} | {1} | {2} |", board[2, 0], board[2, 1], board[2, 2]);
                Console.WriteLine("\t  -------------\n\n");
            }

            int EingabePosition()
            {
                int Eingabe = 0;
                try
                {
                    Eingabe = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Falsche Eingabe, bitte erneut versuchen.");
                    return 3;
                }
                if (Eingabe >= 0 && Eingabe <= 2)
                    return Eingabe;
                else
                {
                    Console.WriteLine("Nur Zahlen von 0 - 2 sind erlaubt, bitte erneut versuchen.");
                    return 3;
                }
            }
        }
    }
}
