using System;
using System.Collections.Generic;

namespace TicTacToe
{
    class UIConsole : UI
    {
        public List<UIObject> UIElements = new List<UIObject>();
        public Game game;
        public int activeElement = 14;

        ConsoleColor[] pColor = new ConsoleColor[2] { ConsoleColor.Red, ConsoleColor.Blue };

        public override void PrintStatus(string str)
        {           
            
        }
        public override void PrintError(string str)
        {
            
        }
        public override void PrintInfo(string str)
        {
            
        }
        public override void PrintHint(byte x, byte y)
        {
            if (x == 9) // wenn x/y = 9 dann ist kein Hint vorhanden
            {
                UIElements[12]=(new UIText("Hint: *, *", 5, 16, ConsoleColor.DarkGray, ConsoleColor.Black));
            }
            else
            {
                UIElements[12] = (new UIText($"Hint: {x}, {y}", 5, 16, ConsoleColor.DarkGray, ConsoleColor.Black));
                UIElements[13] = (new UIText("H", 12 + x * 8, 7 + y * 2, ConsoleColor.DarkGray, ConsoleColor.Black));
            }
        }
        public override void Start(Game game)
        {
            this.game = game;
            Console.Clear();
            Console.CursorVisible = false;

            UIElements.Add(new UIText("TicTacToe by TobiH", 10, 0));
            UIElements.Add(new UIText($"turn {game.turnNumber}, {game.playerNames[Convert.ToInt32(game.currentPlayerID)]} [{(game.currentPlayerID ? FieldState.X : FieldState.O)}] it's your turn!\n", 0, 2, (game.currentPlayerID ? pColor[0] : pColor[1])));
            UIElements.Add(new UIText("     0       1       2", 7, 5));
            UIElements.Add(new UIText(" -------------------------", 7, 6));
            UIElements.Add(new UIText("0|\t|\t|\t|", 7, 7));
            UIElements.Add(new UIText(" -------------------------", 7, 8));
            UIElements.Add(new UIText("1|\t|\t|\t|", 7, 9));
            UIElements.Add(new UIText(" -------------------------", 7, 10));
            UIElements.Add(new UIText("2|\t|\t|\t|", 7, 11));
            UIElements.Add(new UIText(" -------------------------", 7, 12));
            UIElements.Add(new UIText("enter [0,1,2] or [H] for hint and [ESC] to exit", 5, 15)); // 10 = Infotext
            UIElements.Add(new UIText("", 20, 16)); // 11 = Error
            UIElements.Add(new UIText("", 5, 16)); // 12 = HintText
            UIElements.Add(new UIText("", 25, 16)); // 13 = HintSymbol
            UIElements.Add(new UIInput("X-Position", 5, 17)); // 14 = Input X
            UIElements.Add(new UIInput("Y-Position", 5, 18)); // 15 = Input Y
        }
        public override void WaitForInput()
        {
            Point input = new Point();
            FPS fpsCounter = new FPS();
            bool gameStatus = true;

            ConsoleKeyInfo UserInput = new ConsoleKeyInfo();

            UIElements[activeElement].selected = true;

            do
            {
                fpsCounter.Draw();
                
                for (int i = 0; i < UIElements.Count; i++)
                {
                    UIElements[i].Draw();
                }

                if (Console.KeyAvailable)
                {
                    UserInput = Console.ReadKey(true);

                    switch (UserInput.Key)
                    {
                        case ConsoleKey.UpArrow:
                            
                            break;
                        case ConsoleKey.DownArrow:
                            
                            break;
                        case ConsoleKey.D0:
                            UIElements[activeElement].input = "0";
                            break;
                        case ConsoleKey.D1:
                            UIElements[activeElement].input = "1";
                            break;
                        case ConsoleKey.D2:
                            UIElements[activeElement].input = "2";
                            break;
                        case ConsoleKey.Enter:
                            if (activeElement == 15)
                            {
                                Byte.TryParse(UIElements[14].input, out input.x);
                                Byte.TryParse(UIElements[15].input, out input.y);

                                if (game.turn(input) == TurnResult.Invalid)
                                {
                                    UIElements[11].text = "invalid!";
                                }
                                else
                                {
                                    UIElements[12].text = "          "; UIElements[13].text = " "; UIElements[14].input = " "; UIElements[15].input = " ";
                                    UIElements.Add(new UIText($"{game.board[input.y, input.x]}", 12 + input.x * 8, 7 + input.y * 2, (game.board[input.y, input.x] == FieldState.X ? pColor[0] : pColor[1])));
                                    UIElements[1] = (new UIText($"turn {game.turnNumber}, {game.playerNames[Convert.ToInt32(game.currentPlayerID)]} [{(game.currentPlayerID ? FieldState.X : FieldState.O)}] it's your turn!\n", 0, 2, (game.currentPlayerID ? pColor[0] : pColor[1])));

                                    if (game.turnNumber == 10)
                                    {
                                        UIElements[10].text = "tie! try again? [y/ESC]";
                                    }
                                    else if (game.turnNumber >= 11)
                                    {
                                        UIElements[12].text = "win! try again? [y/ESC]";
                                    }
                                }
                            }

                            activeElement++;
                            if (activeElement > 15) activeElement = 14;

                            break;
                        case ConsoleKey.H:
                            game.DrawHint();
                            break;
                        case ConsoleKey.Escape:
                            gameStatus = false;
                            UIElements.Clear();
                            break;
                        default:
                            break;
                    }
                }
            } while (gameStatus == true);    // TODO: exit, Fehlerbehandlung
        }
        public override void Draw(FieldState[,] board)
        {
            
        }
        public override int EnterPosition()
        {
            return 1;
        }
    }
}
