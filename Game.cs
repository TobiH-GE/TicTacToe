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

            private bool checkWin(FieldState playerFieldState, int checkValue = 3) // zum Test ob mit checkValue Steinen gewonnen
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
                        if (counterX == checkValue || counterY == checkValue || counterDiag1 == checkValue || counterDiag2 == checkValue) // Sobald irgendwo checkValue gleiche Steine gezählt wurden, dann ...
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

                hint = GetHint(3, (currentPlayerID ? FieldState.X : FieldState.O)); // prüfe ob Spieler gewinnen kann
                if (hint.x != 9)
                    return ($"Hint: " + hint.x + ", " + hint.y);

                hint = GetHint(3, (!currentPlayerID ? FieldState.X : FieldState.O)); // prüfe ob der Gegner gewinnen kann
                if (hint.x != 9)
                    return ($"Hint: " + hint.x + ", " + hint.y);

                hint = GetHint(2, (currentPlayerID ? FieldState.X : FieldState.O)); // prüfe ob Spieler einen zweiten Stein in eine Reihe legen kann und dann mit einem dritten die Reihe gewinnt
                if (hint.x != 9)
                    return ($"Hint: " + hint.x + ", " + hint.y);

                return "Hint: choose an empty field!"; // kein Hint vorhanden
            }

            public Point GetHint(int checkValue, FieldState fState)
            {
                Point returnHint = new Point();
            
                for (byte y = 0; y < 3; y++)
                {
                    for (byte x = 0; x < 3; x++)
                    {
                        if (board[y, x] == FieldState.Empty) // prüfen des Spielfelds auf einen freien Platz
                        {
                            board[y, x] = fState;   // ein Teststein setzen

                            if (checkWin(fState, checkValue))   // testen ob mit Anzahl checkValue gewonnen wird
                            {
                                if (checkValue == 3) // testen ob 3 Steine in Reihe liegen
                                {
                                    returnHint.x = x; // Werte x y merken
                                    returnHint.y = y;
                                    board[y, x] = FieldState.Empty; // Teststein wieder vom Feld löschen
                                    return returnHint; // Werte zurückgeben
                                }
                                if (checkValue == 2) // testen ob 2 Steine in Reihe liegen
                                {
                                    returnHint = GetHint(3, fState); // testen ob mit einem 3 Stein ein Gewinn möglich
                                    board[y, x] = FieldState.Empty; // 2. Teststein wieder vom Feld löschen
                                    if (returnHint.x != 9) return returnHint; // Gewinn mit 3. möglich
                                }
                            }
                            board[y, x] = FieldState.Empty; // kein Gewinn möglich, Teststein wieder vom Feld löschen
                        }
                    }
                }  
                returnHint.x = 9; // kein Gewinn möglich
                return returnHint;
            }
            public void ResetBoard()
            {

            }   
        }
    }
}
