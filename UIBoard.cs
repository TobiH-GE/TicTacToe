using System;

namespace TicTacToe
{
    class UIBoard : UIObject
    {
        FieldState[,] board;
        public UIBoard(FieldState[,] board)
        {
            this.board = board;
        }

        public override void Draw()
        {
            Console.CursorVisible = false;

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (board[y, x] == FieldState.O || board[y, x] == FieldState.X)
                    {
                        UIText text8 = new UIText($"{board[y, x]}", 12 + x * 4, 7 + y * 2);
                    }
                }
            }

            Console.CursorVisible = true;
        }
    }
}
