using System;

namespace TicTacToe
{
    class UIButton : UIObject
    {
        public UIButton(string text, int x, int y, ConsoleColor fColor = ConsoleColor.White, ConsoleColor bColor = ConsoleColor.Black, bool selected = false)
        {
            this.text = text;
            this.x = x;
            this.y = y;
            this.fColor = fColor;
            this.bColor = bColor;
            this.selected = selected;
        }
        public override void Action()
        {

        }
    }
}
