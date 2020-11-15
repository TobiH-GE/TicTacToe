using System;
using System.Collections.Generic;

namespace TicTacToe
{
    class UIConsole : UI
    {
        public List<UIObject> UIElements = new List<UIObject>();
        public Game game;
        FPS fpsCounter = new FPS();
        public int activeElement = 14;

        ConsoleColor[] pColor = new ConsoleColor[2] { ConsoleColor.Red, ConsoleColor.Blue };

        public override void PrintStatus(ref Game game)
        {
            UIElements[1] = (new UIText($"turn {game.turnNumber}, {game.playerNames[Convert.ToInt32(game.currentPlayerID)]} [{(game.currentPlayerID ? FieldState.X : FieldState.O)}] it's your turn!\n", 10, 2, (game.currentPlayerID ? pColor[0] : pColor[1])));
        }
        public override void PrintError(string str)
        {
            UIElements[11].text = str;
        }
        public override void PrintInfo(string str)
        {
            UIElements[10].text = str;
        }
        public override void PrintHint(sbyte x, sbyte y)
        {
            if (x == -1) // wenn x/y = -1 dann ist kein Hint vorhanden
            {
                UIElements[12]=(new UIText("Hint: *, *", 5, 16, ConsoleColor.DarkGray, ConsoleColor.Black));
            }
            else
            {
                UIElements[12] = (new UIText($"Hint: {x}, {y}", 5, 16, ConsoleColor.DarkGray, ConsoleColor.Black));
                UIElements[13] = (new UIText("H", 20 + x * 8, 7 + y * 2, ConsoleColor.DarkGray, ConsoleColor.Black));
            }
        }
        public override void Start(Game game)
        {
            this.game = game;
            Console.Clear();
            Console.CursorVisible = false;

            UIElements.Add(new UIText("TicTacToe by TobiH ", 20, 0));
            UIElements.Add(new UIText($"turn {game.turnNumber}, {game.playerNames[Convert.ToInt32(game.currentPlayerID)]} [{(game.currentPlayerID ? FieldState.X : FieldState.O)}] it's your turn!\n", 10, 2, (game.currentPlayerID ? pColor[0] : pColor[1])));
            UIElements.Add(new UIText("     0       1       2", 15, 5));
            UIElements.Add(new UIText(" ╔═══════╦═══════╦═══════╗", 15, 6));
            UIElements.Add(new UIText("0║\t║\t║\t║", 15, 7));
            UIElements.Add(new UIText(" ╠═══════╬═══════╬═══════╣", 15, 8));
            UIElements.Add(new UIText("1║\t║\t║\t║", 15, 9));
            UIElements.Add(new UIText(" ╠═══════╬═══════╬═══════╣", 15, 10));
            UIElements.Add(new UIText("2║\t║\t║\t║", 15, 11));
            UIElements.Add(new UIText(" ╚═══════╩═══════╩═══════╝", 15, 12));
            UIElements.Add(new UIText("enter [0,1,2] or [H] for hint and [ESC] to exit", 5, 15)); // 10 = Infotext
            UIElements.Add(new UIText("", 20, 16)); // 11 = Error
            UIElements.Add(new UIText("", 5, 16)); // 12 = HintText
            UIElements.Add(new UIText("", 25, 16)); // 13 = HintSymbol
            UIElements.Add(new UIInput("X-Position", 5, 17, nextElement)); // 14 = Input X
            UIElements.Add(new UIInput("Y-Position", 5, 18, nextElement)); // 15 = Input Y
            UIElements.Add(new UIButton("", 5, 19)); // 16 = invisible Button
        }
        public override void WaitForInput()
        {
            Point input = new Point();
            game.status = Status.started;

            ConsoleKeyInfo UserInput = new ConsoleKeyInfo();

            UIElements[activeElement].selected = true;

            do
            {
                Draw();

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
                            UIElements[activeElement].Action();

                            if (activeElement == 16)
                            {
                                startTurn(game, input);
                                nextElement();
                            }
                            checkEndGame();
                            break;
                        case ConsoleKey.H:
                            game.DrawHint();
                            break;
                        case ConsoleKey.Y:
                            if (game.status == Status.tie || game.status == Status.win)
                            {
                                return;
                            }
                            break;
                        case ConsoleKey.Escape:
                            game.status = Status.stopped;
                            UIElements.Clear();
                            break;
                        default:
                            break;
                    }
                }
            } while (game.status != Status.stopped);    // TODO: exit, restart, Fehlerbehandlung
        }
        public void checkEndGame()
        {
            if (game.turnNumber == 10)
            {
                game.status = Status.tie;
                UIElements[10].text = "tie! try again? [y/ESC]";
            }
            else if (game.turnNumber >= 11)
            {
                game.status = Status.win;
                UIElements[12].text = "win! try again? [y/ESC]";
            }
        }
        public bool nextElement()
        {
            UIElements[activeElement].selectedToggle();
            activeElement++;
            if (activeElement > 16) activeElement = 14;
            UIElements[activeElement].selectedToggle();
            return true;
        }
        public void startTurn(Game game, Point input)
        {
            sbyte.TryParse(UIElements[14].input, out input.x);
            sbyte.TryParse(UIElements[15].input, out input.y);

            if (game.turn(input) == TurnResult.Invalid)
            {
                UIElements[11].text = "invalid!";
            }
            else
            {
                UIElements[12].text = "          "; UIElements[13].text = " "; UIElements[14].input = " "; UIElements[15].input = " ";
                UIElements.Add(new UIText($"{game.board[input.y, input.x]}", 20 + input.x * 8, 7 + input.y * 2, (game.board[input.y, input.x] == FieldState.X ? pColor[0] : pColor[1])));
                PrintStatus(ref game);
            }
        }
        public override void Draw()
        {
            fpsCounter.Draw();

            for (int i = 0; i < UIElements.Count; i++)
            {
                UIElements[i].Draw();
            }
        }
        public override int EnterPosition()
        {
            return 1;
        }
    }
}
