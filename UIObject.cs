using System;

namespace TicTacToe
{
    class UIObject
    {
        public string text;
        protected int x;
        protected int y;
        protected ConsoleColor fColor;
        protected ConsoleColor bColor;
        public bool selected;

        /// <summary>
        /// zeichnet ein UI-Element an Position x,y
        /// </summary>
        public virtual void Draw()
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
