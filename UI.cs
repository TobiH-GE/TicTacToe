using System;

namespace TicTacToe
{
    abstract class UI
    {
        public abstract void PrintStatus(string str);
        public abstract void PrintError(string str);
        public abstract void PrintInfo(string str);
        public abstract void PrintHint(byte x, byte y);
        public abstract void Start(Game game);
        public abstract void WaitForInput();
        public abstract void Draw(FieldState[,] board);
        public abstract int EnterPosition();
    }
}
