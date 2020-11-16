using System;

namespace TicTacToe
{
    abstract class UIObject
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
