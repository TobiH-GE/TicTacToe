namespace TicTacToe
{
    partial class Program                               // *** TicTacToe von TobiH ***
    {
        class Game
        {
            public FieldState[,] board = new FieldState[3, 3];
            public bool currentPlayerID = false;
            public string[] playerNames = new string[2];
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

                if (board[point.y, point.x] != FieldState.E)
                {
                    return result;
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
                int SteineZaehlerX = 0;                     // für den horizontalen Test  
                int SteineZaehlerY = 0;                     // für den vertikalen Test
                int SteineZaehlerDiagonal1 = 0;             // für den Test von oben links nach unten rechts
                int SteineZaehlerDiagonal2 = 0;             // für den Test von oben rechts nach unten links

                for (int y = 0; y <= 2; y++)                // Haupt-Testschleife, wir testen alles in einem Rutsch
                {
                    SteineZaehlerX = 0;
                    SteineZaehlerY = 0;
                    SteineZaehlerDiagonal1 = 0;
                    SteineZaehlerDiagonal2 = 0;

                    for (int x = 0; x <= 2; x++)            // wir erhöhen x in jedem Durchgang
                    {
                        if (board[y, x] == playerFieldState)      // horizontaler Test, fängt an bei [0,0] [0,1] [0,2] ...
                        {
                            SteineZaehlerX++;               // Anzahl der gleichen Steine in dieser Reihe um 1 erhöhen
                        }
                        if (board[x, y] == playerFieldState)      // vertikaler Test (wir tauschen einfach x und y), Test fängt an bei [0,0] [1,0] [2,0] ...
                        {
                            SteineZaehlerY++;               // Anzahl der gleichen Steine in dieser Spalte um 1 erhöhen
                        }
                        if (board[x, x] == playerFieldState)      // Test diagonal von oben links, x wird immer um 1 erhöht daher setzen wir einfach [x,x] ein, das ergibt den Test für [0,0] [1,1] [2,2]
                        {
                            SteineZaehlerDiagonal1++;       // Anzahl der gleichen Steine in dieser Diagonalen um 1 erhöhen
                        }
                        if (board[2 - x, x] == playerFieldState)  // Test diagonal von oben rechts, x wird immer um 1 erhöht, mit [2 - x, x] erhalten wir also [2,0] [1,1] [0,2]
                        {
                            SteineZaehlerDiagonal2++;       // Anzahl der gleichen Steine in dieser Diagonalen um 1 erhöhen
                        }
                        if (SteineZaehlerX == 3 || SteineZaehlerY == 3 || SteineZaehlerDiagonal1 == 3 || SteineZaehlerDiagonal2 == 3) // Sobald irgendwo 3 gleiche Steine gezählt wurden, dann ...
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
