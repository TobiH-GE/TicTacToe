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
            
            do
            {
                game = new Game(UIGame);
                UIGame.WaitForInput();

            } while (Console.ReadKey().Key != ConsoleKey.N);
        }
    }
}
