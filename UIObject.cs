﻿using System;

namespace TicTacToe
{
    public class UIObject
    {
        public string name;
        public string text;
        public string input;
        public int x;
        public int y;
        public bool visible;
        protected ConsoleColor fColor;
        protected ConsoleColor bColor;
        public bool selected;
        public bool active = true;
        public bool selectable;
        public UIObject(string name, string text, int x, int y, bool visible = true, ConsoleColor fColor = ConsoleColor.White, ConsoleColor bColor = ConsoleColor.Black, bool selected = false)
        {
            this.name = name;
            this.text = text;
            this.x = x;
            this.y = y;
            this.visible = visible;
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
