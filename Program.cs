using System;

namespace TicTacToe
{
    struct Point
    {
        public sbyte x, y;
    }
    class Program                               // *** TicTacToe von TobiH ***
    {
        static void Main(string[] args)
        {
            UI UIGame = new UIConsole();
            Game game;

            do
            {
                game = new Game(UIGame);
                UIGame.WaitForInput();

            } while (game.status != Status.stopped);
        }
    }
}
