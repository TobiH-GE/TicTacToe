using System;

namespace TicTacToe
{
    class Point
    {
        public byte x, y;
        public bool xHasValue = false;
        public bool yHasValue = false;
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

            } while (true);
        }
    }
}
