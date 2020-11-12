using System;

namespace TicTacToe
{
    partial class Program                               // *** TicTacToe von TobiH ***
    {
        struct Point
        {
            public byte x, y;
        }
        static void Main(string[] args)
        {
            Game game = new Game();
            
            Point point;

            do
            {
                Draw(game.board);
                do
                {
                    Console.WriteLine("x-position: ");
                    point.x = (byte)enterPosition();
                } while (point.x == 3);
                do
                {
                    Console.WriteLine("y-Position: ");
                    point.y = (byte)enterPosition();
                } while (point.y == 3);

                if (game.turn(point) == TurnResult.Invalid)
                {
                    Console.WriteLine("invalid!");
                    Console.ReadLine();
                }

            } while (game.turnNumber < 10);

            if (game.turnNumber == 10) Console.WriteLine("tie!");
            else Console.WriteLine("win!");

            void Draw(FieldState[,] board)
            {
                Console.Clear();
                Console.WriteLine("TicTacToe by TobiH\n");
                Console.WriteLine("round {0}, {1} it's your turn!\n", game.turnNumber, game.playerNames[Convert.ToInt32(game.currentPlayerID)]);

                Console.Write("\n\n\t      0   1   2  ");
                for (int y = 0; y < 3; y++)
                {
                    Console.Write("\n\t    -------------\n\t"+y+"   ");
                    for (int x = 0; x < 3; x++)
                    {
                        if(board[y, x] == FieldState.Empty)
                        {
                            Console.Write("|   ");
                        }
                        else
                        {
                            Console.Write("| " + board[y, x] + " ");
                        }
                    }
                    Console.Write("|");
                }
                Console.WriteLine("\n\t    -------------\n\n");
            }

            int enterPosition()
            {
                int Eingabe = 0;
                try
                {
                    Eingabe = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Error, try again.");
                    return 3;
                }
                if (Eingabe >= 0 && Eingabe <= 2)
                    return Eingabe;
                else
                {
                    Console.WriteLine("only 0 - 2 is allowed, try again.");
                    return 3;
                }
            }
        }
    }
}
