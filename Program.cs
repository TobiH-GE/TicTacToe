using System;

namespace TicTacToe
{
    class Program                               // *** TicTacToe von TobiH ***
    {
        enum FieldState
        {
            Empty, X, O, Hint
        }
        enum TurnResult
        {
            Valid, Invalid, Tie, Win
        }
        class Point
        {
            byte x, y;
        }

        class Game
        {
            FieldState[,] board = new FieldState[3, 3];
            bool currentPlayerID = false;
            public string[] playerNames = new string[2];

            public FieldState[,] getBoard()
            {
                return board;
            }
            public bool getPlayerID()
            {
                return currentPlayerID;
            }
            public TurnResult turn(Point point)
            {
                TurnResult result = TurnResult.Tie;

                return result;
            }
            public void DrawHint (bool player)
            {
            }
            public byte GetHint(bool player)
            {
                return 1;
            }
            public void ResetBoard()
            {

            }
        }
        static void Main(string[] args)
        {
            void Draw(FieldState board)
            {

            }
        }
    }
}
