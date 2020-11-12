namespace TicTacToe
{
    partial class Program                               // *** TicTacToe von TobiH ***
    {
        class Game
        {
            public FieldState[,] board = new FieldState[3, 3];
            public bool currentPlayerID = false;
            public string[] playerNames = new string[] {"Player 1", "Player 2"};
            public int turnNumber = 1;

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
                TurnResult result = TurnResult.Valid;
                FieldState playerFieldState = FieldState.X;

                if (currentPlayerID == true) playerFieldState = FieldState.O;

                if (board[point.y, point.x] != FieldState.Empty)
                {
                    return TurnResult.Invalid;
                }

                board[point.y, point.x] = playerFieldState;

                if (checkWin(playerFieldState))
                {
                    result = TurnResult.Win;
                }

                turnNumber++;

                if (turnNumber == 10) return TurnResult.Tie;

                currentPlayerID = !currentPlayerID;
                return result;
            }

            private bool checkWin(FieldState playerFieldState)
            {
                int counterX = 0;                     // für den horizontalen Test  
                int counterY = 0;                     // für den vertikalen Test
                int counterDiag1 = 0;             // für den Test von oben links nach unten rechts
                int counterDiag2 = 0;             // für den Test von oben rechts nach unten links

                for (int y = 0; y <= 2; y++)                // Haupt-Testschleife, wir testen alles in einem Rutsch
                {
                    counterX = 0;
                    counterY = 0;
                    counterDiag1 = 0;
                    counterDiag2 = 0;

                    for (int x = 0; x <= 2; x++)            // wir erhöhen x in jedem Durchgang
                    {
                        if (board[y, x] == playerFieldState)      // horizontaler Test, fängt an bei [0,0] [0,1] [0,2] ...
                        {
                            counterX++;               // Anzahl der gleichen Steine in dieser Reihe um 1 erhöhen
                        }
                        if (board[x, y] == playerFieldState)      // vertikaler Test (wir tauschen einfach x und y), Test fängt an bei [0,0] [1,0] [2,0] ...
                        {
                            counterY++;               // Anzahl der gleichen Steine in dieser Spalte um 1 erhöhen
                        }
                        if (board[x, x] == playerFieldState)      // Test diagonal von oben links, x wird immer um 1 erhöht daher setzen wir einfach [x,x] ein, das ergibt den Test für [0,0] [1,1] [2,2]
                        {
                            counterDiag1++;       // Anzahl der gleichen Steine in dieser Diagonalen um 1 erhöhen
                        }
                        if (board[2 - x, x] == playerFieldState)  // Test diagonal von oben rechts, x wird immer um 1 erhöht, mit [2 - x, x] erhalten wir also [2,0] [1,1] [0,2]
                        {
                            counterDiag2++;       // Anzahl der gleichen Steine in dieser Diagonalen um 1 erhöhen
                        }
                        if (counterX == 3 || counterY == 3 || counterDiag1 == 3 || counterDiag2 == 3) // Sobald irgendwo 3 gleiche Steine gezählt wurden, dann ...
                        {
                            return true;         // raus aus der Funktion, Gewinner steht fest, kein weiteres Prüfen notwendig
                        }
                    }
                }
                return false;
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
    }
}
