using System;

namespace TicTacToe
{
    class UIInput : UIObject
    {
        Func<bool> methodName;
        public UIInput(string text, int x, int y, Func<bool> methodName, ConsoleColor fColor = ConsoleColor.White, ConsoleColor bColor = ConsoleColor.Black, bool selected = false)
        {
            this.text = text;
            this.x = x;
            this.y = y;
            this.methodName = methodName;
            this.fColor = fColor;
            this.bColor = bColor;
            this.active = false;
            this.selected = false;
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
            Console.Write(text + ": " + input);
            Console.ResetColor();
        }
        public override void Action()
        {
            methodName();
        }
    }
}
