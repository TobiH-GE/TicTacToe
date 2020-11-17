using System;

namespace TicTacToe
{
    class UIInput : UIObject
    {
        Func<bool> methodName;
        public UIInput(string name, string text, int x, int y, Func<bool> methodName, ConsoleColor fColor = ConsoleColor.White, ConsoleColor bColor = ConsoleColor.Black, bool selected = false) : base (name, text, x, y, fColor, bColor, selected)
        {
            this.methodName = methodName;
            selectable = true;
        }
        public override void Draw()
        {
            if (visible)
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

                Console.Write(text + ":");
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(" " + input);
                Console.ResetColor();
            }
        }
        public override void Action()
        {
            methodName();
        }
    }
}
