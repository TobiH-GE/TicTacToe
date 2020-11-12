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
                if (board[point.y, point.x] != FieldState.Empty)
                {
                    return TurnResult.Invalid;
                }

                board[point.y, point.x] = (currentPlayerID ? FieldState.X : FieldState.O);

                if (checkWin(board[point.y, point.x]))
                {
                    turnNumber = 11;
                    return TurnResult.Win;
                }

                turnNumber++;

                currentPlayerID = !currentPlayerID;
                return TurnResult.Valid;
            }

            private bool checkWin(FieldState playerFieldState, int checkValue = 3)
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
                        if (counterX == checkValue || counterY == checkValue || counterDiag1 == checkValue || counterDiag2 == checkValue) // Sobald irgendwo 3 gleiche Steine gezählt wurden, dann ...
                        {
                            return true;         // raus aus der Funktion, Gewinner steht fest, kein weiteres Prüfen notwendig
                        }
                    }
                }
                return false;
            }
            public string DrawHint()
            {
                Point hint;

                hint = GetHint(3, currentPlayerID); // prüfe ob Spieler gewinnen kann
                if (hint.x != 9)
                    return ($"Hint: " + hint.x + ", " + hint.y);

                hint = GetHint(3, !currentPlayerID); // prüfe ob der Gegner gewinnen kann
                if (hint.x != 9)
                    return ($"Hint: " + hint.x + ", " + hint.y);

                hint = GetHint(2, currentPlayerID); // prüfe ob Spieler einen zweiten Stein in eine Reihe legen kann
                if (hint.x != 9)
                    return ($"Hint: " + hint.x + ", " + hint.y);

                return "Hint: choose an empty field!"; // kein Hint vorhanden
            }
            public Point GetHint(int checkValue, bool playerID)
            {
                Point returnHint = new Point();

                for (byte y = 0; y < 3; y++)
                {
                    for (byte x = 0; x < 3; x++)
                    {
                        if (board[y, x] == FieldState.Empty)
                        {
                            board[y, x] = (playerID ? FieldState.X : FieldState.O);

                            if (checkWin(board[y, x] = (playerID ? FieldState.X : FieldState.O), checkValue))
                            {
                                returnHint.x = x;
                                returnHint.y = y;
                                board[y, x] = FieldState.Empty;
                                return returnHint;
                            }
                            board[y, x] = FieldState.Empty;
                        }
                    }
                }
                returnHint.x = 9;
                return returnHint;
            }
            public void ResetBoard()
            {

            }   
        }
    }
}
