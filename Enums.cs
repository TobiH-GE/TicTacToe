using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    enum FieldState
    {
        Empty, X, O, Hint
    }
    enum TurnResult
    {
        Valid, Invalid, Tie, Win
    }
    enum Status
    {
        Started, Stopped, Win, Tie
    }
    enum Direction
    {
        Up, Down, Left, Right
    }
}
