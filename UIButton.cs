﻿using System;

namespace TicTacToe
{
    class UIButton : UIObject
    {
        Func<bool> methodName;
        public object[] pArray;
        public UIButton(string text, int x, int y, Func<bool> methodName, ConsoleColor fColor = ConsoleColor.White, ConsoleColor bColor = ConsoleColor.Black, bool selected = false, params object[] pArray) : base (text, x, y, fColor, bColor, selected)
        {
            this.methodName = methodName;
            selectable = true;
            this.pArray = pArray;
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
        public override void Action()
        {
            methodName();
        }
    }
}
