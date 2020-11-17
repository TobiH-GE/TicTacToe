using System;

namespace TicTacToe
{
    class UIText : UIObject
    {
        public UIText(string text, int x, int y, ConsoleColor fColor = ConsoleColor.White, ConsoleColor bColor = ConsoleColor.Black, bool selected = false) : base(text, x, y, fColor, bColor, selected)
        {
            selectable = false;
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

            Console.Write(text);
            Console.ResetColor();
        }
    }   
}
