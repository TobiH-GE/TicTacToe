using System;

namespace TicTacToe
{
    abstract class UI
    {
        public Game game;
        public abstract void PrintStatus(ref Game game);
        public abstract void PrintError(string str);
        public abstract void PrintInfo(string str);
        public abstract void PrintHint(sbyte x, sbyte y);
        public abstract void Start();
        public abstract void WaitForInput();
        public abstract void Draw();
    }
}
