using System;

namespace TicTacToe
{
    struct Point
    {
        public sbyte x;
        public sbyte y;
    }
    class Program                               // *** TicTacToe von TobiH ***
    {
        static void Main(string[] args)
        {
            UI UIGame = new UIConsole();
            Game game = new Game(UIGame);

            do
            {
                UIGame.WaitForInput();
            } while (game.status != Status.Stopped);

        Console.Clear();
        }
    }
}
