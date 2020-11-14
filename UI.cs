using System;

namespace TicTacToe
{
    abstract class UI
    {
        public abstract void PrintStatus(ref Game game);
        public abstract void PrintError(string str);
        public abstract void PrintInfo(string str);
        public abstract void PrintHint(sbyte x, sbyte y);
        public abstract void Start(Game game);
        public abstract void WaitForInput();
        public abstract void Draw();
        public abstract int EnterPosition();
    }
}
