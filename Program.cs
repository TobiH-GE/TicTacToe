using System;

namespace TicTacToe
{
    struct Point
    {
        public byte x, y;
    }
    class Program                               // *** TicTacToe von TobiH ***
    {
        static void Main(string[] args)
        {
            UI UIGame = new UIConsole();

            Game game;           
            Point input;
            do
            {
                game = new Game(UIGame);
                do
                {
                    UIGame.Draw(game.board);
                    do
                    {
                        UIGame.PrintInfo("enter [0,1,2] or [9] for hint ... x-position: ");
                        input.x = (byte)enterPosition();
                        if (input.x == 9) game.DrawHint();
                    } while (input.x == 3 || input.x == 9);
                    do
                    {
                        UIGame.PrintInfo("enter [0,1,2] or [9] for hint ... y-position: ");
                        input.y = (byte)enterPosition();
                        if (input.y == 9) game.DrawHint();
                    } while (input.y == 3 || input.y == 9);

                    if (game.turn(input) == TurnResult.Invalid)
                    {
                        UIGame.PrintError("invalid!");
                        Console.ReadLine();
                    }

                } while (game.turnNumber < 10);

                if (game.turnNumber == 10)
                {
                    UIGame.PrintInfo("tie!                                                 ");
                }
                else
                {
                    UIGame.Draw(game.board);
                    UIGame.PrintInfo("win!                                                 ");
                }

            UIGame.PrintInfo("again? [y/n]!                                            ");
            } while (Console.ReadKey().Key != ConsoleKey.N);

            int enterPosition()
            {
                int Eingabe = 0;
                try
                {
                    Eingabe = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    UIGame.PrintError("Error, try again.");
                    return 3;
                }
                if ((Eingabe >= 0 && Eingabe <= 2) || Eingabe == 9)
                    return Eingabe;
                else
                {
                    UIGame.PrintError("Error, try again.");
                    return 3;
                }
            }
        }
    }
}
