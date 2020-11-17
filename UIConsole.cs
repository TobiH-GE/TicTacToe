using System;
using System.Collections.Generic;

namespace TicTacToe
{
    class UIConsole : UI
    {
        public List<UIObject> UIElements = new List<UIObject>();
        
        FPS fpsCounter = new FPS();
        Point input = new Point();
        ConsoleKeyInfo UserInput = new ConsoleKeyInfo();
        ConsoleColor[] pColor = new ConsoleColor[2] { ConsoleColor.Red, ConsoleColor.Blue };
        private int activeElement;
        public int ActiveElement
        {
            get
            {
                return activeElement;
            }
            set
            {
                if (value >= UIElements.Count) value = 0;
                if (value < 0) value = UIElements.Count - 1;

                UIElements[activeElement].selected = false;
                activeElement = value;
                UIElements[activeElement].selected = true;
            }
        }

        public override void PrintStatus(ref Game game)
        {
            UIElements[GetUIElementByName("Status")] = (new UIText("Status", $"turn {game.turnNumber}, {game.playerNames[Convert.ToInt32(game.currentPlayerID)]} [{(game.currentPlayerID ? FieldState.X : FieldState.O)}] it's your turn!\n", 10, 2, true, (game.currentPlayerID ? pColor[0] : pColor[1])));
        }
        public override void PrintError(string str)
        {
            UIElements[GetUIElementByName("Error")].text = str;
        }
        public override void PrintInfo(string str)
        {
            UIElements[GetUIElementByName("Hint")].text = str;
        }
        public override void PrintHint(sbyte x, sbyte y)
        {
            if (x == -1) // wenn x/y = -1 dann ist kein Hint vorhanden
            {
                UIElements[GetUIElementByName("Hint")] = (new UIText("Hint", "Hint: *, *", 5, 16, true, ConsoleColor.DarkGray, ConsoleColor.Black));
            }
            else
            {
                UIElements[GetUIElementByName("Hint")] = (new UIText("Hint", $"Hint: {x}, {y}", 5, 16, true, ConsoleColor.DarkGray, ConsoleColor.Black));
                UIElements[GetUIElementByName("HintSymbol")] = (new UIText("HintSymbol", "H", 20 + x * 8, 7 + y * 2, true, ConsoleColor.DarkGray, ConsoleColor.Black));
            }
        }
        public override void Start()
        {
            Console.Clear();
            Console.CursorVisible = false;
            game.status = Status.Started;

            UIElements.Add(new UIText("Titel", "TicTacToe by TobiH ", 20, 0));
            UIElements.Add(new UIText("Status", $"turn {game.turnNumber}, {game.playerNames[Convert.ToInt32(game.currentPlayerID)]} [{(game.currentPlayerID ? FieldState.X : FieldState.O)}] it's your turn!\n", 10, 2, true, (game.currentPlayerID ? pColor[0] : pColor[1])));
            UIElements.Add(new UIText("", "     0       1       2", 15, 5));
            UIElements.Add(new UIText("", " ╔═══════╦═══════╦═══════╗", 15, 6));
            UIElements.Add(new UIText("", "0║\t║\t║\t║", 15, 7));
            UIElements.Add(new UIText("", " ╠═══════╬═══════╬═══════╣", 15, 8));
            UIElements.Add(new UIText("", "1║\t║\t║\t║", 15, 9));
            UIElements.Add(new UIText("", " ╠═══════╬═══════╬═══════╣", 15, 10));
            UIElements.Add(new UIText("", "2║\t║\t║\t║", 15, 11));
            UIElements.Add(new UIText("", " ╚═══════╩═══════╩═══════╝", 15, 12));
            UIElements.Add(new UIText("Info", "enter [0,1,2] or [H] for hint and [ESC] to exit", 5, 15));
            UIElements.Add(new UIText("Error", "", 20, 16));
            UIElements.Add(new UIText("Hint", "", 5, 16));
            UIElements.Add(new UIText("HintSymbol", "", 25, 16));
            UIElements.Add(new UIInput("X-Position", "X-Position", 5, 17, true, Next));
            UIElements.Add(new UIInput("Y-Position", "Y-Position", 5, 18, true, () => { ActiveElement = GetUIElementByName("Ok"); return true; }));

            // Buttons für das Spielbrett
            for (byte y = 0; y <= 2; y++)
            {
                for (byte x = 0; x <= 2; x++)
                {
                    byte x1 = new byte(); byte y1 = new byte(); x1 = x; y1 = y;
                    UIElements.Add(new UIButton($"Button {x},{y}", " ", 20 + x * 8, 7 + y * 2, true, () => { Place(y1 ,x1) ; return true; }));
                }
            }

            // Debug - ein paar Buttons zum Test
            UIElements.Add(new UIInput("","Test 1", 5, 20, true, Next));
            UIElements.Add(new UIInput("","Test 2", 5, 21, true, Next));
            UIElements.Add(new UIInput("","Test 3", 5, 22, true, Next));
            UIElements.Add(new UIInput("","Test 4", 5, 23, true, Next));
            //

            UIElements.Add(new UIButton("Ok", "OK", 20, 26, true, Ok));
            UIElements.Add(new UIButton("Exit", "Exit", 30, 26, true, Exit));
            ActiveElement = 14;
        }
        public override void WaitForInput()
        {
            Draw();

            // Debug - Ausgabe von Infos
            Console.SetCursorPosition(50, 1);
            Console.WriteLine(" " + findNextUIElement(Direction.Up).ToString() + " ");
            Console.SetCursorPosition(50, 3);
            Console.WriteLine(" " + findNextUIElement(Direction.Down).ToString() + " ");
            Console.SetCursorPosition(47, 2);
            Console.WriteLine(" " + findNextUIElement(Direction.Left).ToString() + " ");
            Console.SetCursorPosition(53, 2);
            Console.WriteLine(" " + findNextUIElement(Direction.Right).ToString() + " ");
            //

            if (Console.KeyAvailable)
            {
                UserInput = Console.ReadKey(true);

                switch (UserInput.Key)
                {
                    case ConsoleKey.UpArrow:
                        ActiveElement = findNextUIElement(Direction.Up);
                        break;
                    case ConsoleKey.DownArrow:
                        ActiveElement = findNextUIElement(Direction.Down);
                        break;
                    case ConsoleKey.LeftArrow:
                        ActiveElement = findNextUIElement(Direction.Left);
                        break;
                    case ConsoleKey.RightArrow:
                        ActiveElement = findNextUIElement(Direction.Right);
                        break;
                    case ConsoleKey.D0:
                        UIElements[ActiveElement].input = "0";
                        break;
                    case ConsoleKey.D1:
                        UIElements[ActiveElement].input = "1";
                        break;
                    case ConsoleKey.D2:
                        UIElements[ActiveElement].input = "2";
                        break;
                    case ConsoleKey.Enter:
                        UIElements[ActiveElement].Action();
                        break;
                    case ConsoleKey.H:
                        game.DrawHint();
                        break;
                    case ConsoleKey.Y:
                        if (game.status == Status.Tie || game.status == Status.Win)
                        {
                            UIElements.Clear();
                            game.ResetBoard();
                            Start();
                        }
                        break;
                    case ConsoleKey.Escape:
                        game.status = Status.Stopped;
                        break;
                    default:
                        break;
                }
            }
        }
        public int findNextUIElement(Direction direction)
        {
            UIObject active = UIElements[activeElement];
            int found = -1;
            double foundDistance = 9999;

            for (int i = 0; i < UIElements.Count; i++)
            {
                if (UIElements[i].visible && UIElements[i].selectable)
                {
                    if (direction == Direction.Up)
                    {
                        if (UIElements[i].y < active.y && DistanceTo(UIElements[i]) < foundDistance)
                        {
                            found = i;
                            foundDistance = DistanceTo(UIElements[i]);
                        }
                    }
                    else if (direction == Direction.Down)
                    {
                        if (UIElements[i].y > active.y && DistanceTo(UIElements[i]) < foundDistance)
                        {
                            found = i;
                            foundDistance = DistanceTo(UIElements[i]);
                        }
                    }
                    else if (direction == Direction.Left)
                    {
                        if (UIElements[i].x < active.x && DistanceTo(UIElements[i]) < foundDistance)
                        {
                            found = i;
                            foundDistance = DistanceTo(UIElements[i]);
                        }
                    }
                    else if (direction == Direction.Right)
                    {
                        if (UIElements[i].x > active.x && DistanceTo(UIElements[i]) < foundDistance)
                        {
                            found = i;
                            foundDistance = DistanceTo(UIElements[i]);
                        }
                    }
                }
            }
            return found;
        }
        public int GetUIElementByName(string name)
        {
            for (int i = 0; i < UIElements.Count; i++)
            {
                if (UIElements[i].name == name) return i;
            }
            return -1;
        }
        public double DistanceTo(UIObject uobject)
        {
            return Math.Sqrt(Math.Pow(uobject.x - UIElements[activeElement].x, 2) + Math.Pow((uobject.y - UIElements[activeElement].y) * 2, 2)); // * 2 Hack, vertikale Distanz optisch grösser als rechnerische Distanz
        }
        public bool Place(byte y, byte x)
        {
            UIElements[GetUIElementByName("X-Position")].input = x.ToString();
            UIElements[GetUIElementByName("Y-Position")].input = y.ToString();
            ActiveElement = GetUIElementByName("Ok"); ; 
            return true;
        }
        public bool Next()
        {
            ActiveElement = findNextUIElement(Direction.Down);
            return true;
        }
        public bool Ok()
        {
            startTurn(game, input);
            ActiveElement = GetUIElementByName("X-Position");
            checkEndGame();
            return true;
        }
        public bool Exit()
        {
            game.status = Status.Stopped;
            return true;
        }
        public void checkEndGame()
        {
            if (game.turnNumber == 10)
            {
                game.status = Status.Tie;
                UIElements[GetUIElementByName("Status")].text = "               >>>     tie! try again? [y/ESC]     <<<               ";
            }
            else if (game.turnNumber >= 11)
            {
                game.status = Status.Win;
                UIElements[GetUIElementByName("Status")] = (new UIText("Status", $">>> {game.playerNames[Convert.ToInt32(game.currentPlayerID)]} [{(game.currentPlayerID ? FieldState.X : FieldState.O)}] wins! try again? [y/ESC] <<<", 10, 2, true, (game.currentPlayerID ? pColor[0] : pColor[1])));
            }
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
                UIElements[GetUIElementByName($"Button {input.x},{input.y}")].visible = false;
                UIElements[GetUIElementByName("X-Position")].input = " ";
                UIElements[GetUIElementByName("Y-Position")].input = " ";
                UIElements[GetUIElementByName("HintSymbol")].visible = false;
                UIElements[GetUIElementByName("Hint")].text = "          ";
                UIElements.Add(new UIText($"Board {input.x},{input.y}", $"{game.board[input.y, input.x]}", 20 + input.x * 8, 7 + input.y * 2, true, (game.board[input.y, input.x] == FieldState.X ? pColor[0] : pColor[1])));
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
    }
}
