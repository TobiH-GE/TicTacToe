using System;

namespace TicTacToe
{
    class UIInput : UIObject
    {
        public UIInput(string text, int x, int y, ConsoleColor fColor = ConsoleColor.White, ConsoleColor bColor = ConsoleColor.Black, bool selected = false, string input = "")
        {
            this.text = text;
            this.x = x;
            this.y = y;
            this.fColor = fColor;
            this.bColor = bColor;
            this.active = false;
            this.selected = false;
            this.Input = Input;
        }

        public override void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = fColor;
            if (selected)
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.BackgroundColor = bColor;
            }
            Console.Write(text + ": " + Input);
            Console.ResetColor();
        }
    }
}
