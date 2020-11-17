using System;

namespace TicTacToe
{
    public class UIObject //TODO: string name
    {
        public string text;
        public string input;
        public int x;
        public int y;
        protected ConsoleColor fColor;
        protected ConsoleColor bColor;
        public bool selected;
        public bool active;
        public bool selectable;
        public UIObject(string text, int x, int y, ConsoleColor fColor = ConsoleColor.White, ConsoleColor bColor = ConsoleColor.Black, bool selected = false)
        {
            this.text = text;
            this.x = x;
            this.y = y;
            this.fColor = fColor;
            this.bColor = bColor;
            this.selected = selected;
            this.active = true;
        }

        public virtual void Draw()
        {
            
        }
        public virtual void Action()
        {

        }

        public virtual void selectedToggle()
        {
            selected = !selected;
        }
    }
}
