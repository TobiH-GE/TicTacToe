using System;

namespace TicTacToe
{
    class Program                               // *** TicTacToe von TobiH ***
    {
        enum FieldState
        {
            E, X, O, H
        }
        enum TurnResult
        {
            Valid, Invalid, Tie, Win
        }
        class Point
        {
            byte x, y;
        }

        class Game
        {
            public FieldState[,] board = new FieldState[3, 3];
            public bool currentPlayerID = false;
            public string[] playerNames = new string[2];

            public FieldState[,] getBoard()
            {
                return board;
            }
            public bool getPlayerID()
            {
                return currentPlayerID;
            }
            public TurnResult turn(Point point)
            {
                TurnResult result = TurnResult.Tie;

                return result;
            }
            public void DrawHint (bool player)
            {
            }
            public byte GetHint(bool player)
            {
                return 1;
            }
            public void ResetBoard()
            {

            }
        }
        static void Main(string[] args)
        {
            Game game = new Game();
            Draw(game.board);
            Point point = new Point();
            int turnNumber = 1;

            Console.Clear();            // Bildschirm löschen
            Console.WriteLine("TicTacTow von TobiH");
            Console.WriteLine("Runde {0}, Spieler {1} ist an der Reihe. Mach dein Spielzug.\n", turnNumber, game.currentPlayerID);

            ConsoleKeyInfo UserInput = new ConsoleKeyInfo();
            do
            {
                turnNumber++;
            } while (game.turn(point) != TurnResult.Win && game.turn(point) != TurnResult.Tie && UserInput.Key != ConsoleKey.X);

            void Draw(FieldState[,] board)
            {
                Console.WriteLine("\n\n\t    0   1   2  ");
                Console.WriteLine("\t  -------------");
                Console.WriteLine("\t0 | {0} | {1} | {2} |", board[0, 0], board[0, 1], board[0, 2]);
                Console.WriteLine("\t  -------------");
                Console.WriteLine("\t1 | {0} | {1} | {2} |", board[1, 0], board[1, 1], board[1, 2]);
                Console.WriteLine("\t  -------------");
                Console.WriteLine("\t2 | {0} | {1} | {2} |", board[2, 0], board[2, 1], board[2, 2]);
                Console.WriteLine("\t  -------------\n\n");
            }
        }
    }
}
