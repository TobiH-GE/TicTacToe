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
        public override void Start() //TODO: alle Objekte, die nicht gezeichnet werden müssen auf active = false
        {
            Console.Clear();
            Console.CursorVisible = false;
            game.status = Status.started;

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
            UIElements.Add(new UIText("", 5, 16)); // HintText
            UIElements.Add(new UIText("", 25, 16)); // HintSymbol
            UIElements.Add(new UIInput("X-Position", 5, 17, Next)); // Input X
            UIElements.Add(new UIInput("Y-Position", 5, 18, Next)); // Input Y

            // Buttons für das Spielbrett
            for (int y = 0; y <= 2; y++)
            {
                for (int x = 0; x <= 2; x++)
                {
                    UIElements.Add(new UIButton($" ", 20 + x * 8, 7 + y * 2, Place));
                }
            }

            // Debug - ein paar Buttons zum Test
            UIElements.Add(new UIInput("Test 1", 5, 20, Next));
            UIElements.Add(new UIInput("Test 2", 5, 21, Next));
            UIElements.Add(new UIInput("Test 3", 5, 22, Next));
            UIElements.Add(new UIInput("Test 4", 5, 23, Next));
            //

            UIElements.Add(new UIButton("OK", 20, 26, Ok)); // Ok Button
            UIElements.Add(new UIButton("Exit", 30, 26, Exit)); // Exit Button

            ActiveElement = 14;
        }
        public override void WaitForInput()
        {
            Draw();

            // Debug - Ausgabe von Infos
            Console.SetCursorPosition(50, 1);
            Console.WriteLine(" " + findNextUIElement(Direction.up).ToString() + " ");
            Console.SetCursorPosition(50, 3);
            Console.WriteLine(" " + findNextUIElement(Direction.down).ToString() + " ");
            Console.SetCursorPosition(47, 2);
            Console.WriteLine(" " + findNextUIElement(Direction.left).ToString() + " ");
            Console.SetCursorPosition(53, 2);
            Console.WriteLine(" " + findNextUIElement(Direction.right).ToString() + " ");
            //

            if (Console.KeyAvailable)
            {
                UserInput = Console.ReadKey(true);

                switch (UserInput.Key)
                {
                    case ConsoleKey.UpArrow:
                        ActiveElement = findNextUIElement(Direction.up);
                        break;
                    case ConsoleKey.DownArrow:
                        ActiveElement = findNextUIElement(Direction.down);
                        break;
                    case ConsoleKey.LeftArrow:
                        ActiveElement = findNextUIElement(Direction.left);
                        break;
                    case ConsoleKey.RightArrow:
                        ActiveElement = findNextUIElement(Direction.right);
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
                    case ConsoleKey.Y:          // TODO: neue Bug! entfernen!
                        if (game.status == Status.tie || game.status == Status.win)
                        {
                            UIElements.Clear();
                            game = new Game(this);
                        }
                        break;
                    case ConsoleKey.Escape:
                        game.status = Status.stopped;
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
                if (direction == Direction.up)
                {
                    if (UIElements[i].selectable && UIElements[i].y < active.y && DistanceTo(UIElements[i]) < foundDistance)
                    {
                        found = i;
                        foundDistance = DistanceTo(UIElements[i]);
                    }
                }
                else if (direction == Direction.down)
                {
                    if (UIElements[i].selectable && UIElements[i].y > active.y && DistanceTo(UIElements[i]) < foundDistance)
                    {
                        found = i;
                        foundDistance = DistanceTo(UIElements[i]);
                    }
                }
                else if (direction == Direction.left)
                {
                    if (UIElements[i].selectable && UIElements[i].x < active.x && DistanceTo(UIElements[i]) < foundDistance)
                    {
                        found = i;
                        foundDistance = DistanceTo(UIElements[i]);
                    }
                }
                else if (direction == Direction.right)
                {
                    if (UIElements[i].selectable && UIElements[i].x > active.x && DistanceTo(UIElements[i]) < foundDistance)
                    {
                        found = i;
                        foundDistance = DistanceTo(UIElements[i]);
                    }
                }
            }
            return found;
        }
        public double DistanceTo(UIObject uobject)
        {
            return Math.Sqrt(Math.Pow(uobject.x - UIElements[activeElement].x, 2) + Math.Pow((uobject.y - UIElements[activeElement].y) * 2, 2)); // * 2 Hack, vertikale Distanz optisch grösser als rechnerische Distanz
        }
        public bool Place()
        {
            UIElements[14].input = ((UIElements[activeElement].x - 20) / 8).ToString();
            UIElements[15].input = ((UIElements[activeElement].y - 7) / 2).ToString();
            return true;
        }
        public bool Next()
        {
            ActiveElement++;
            return true;
        }
        public bool Ok()
        {
            startTurn(game, input);
            ActiveElement++;
            checkEndGame();
            return true;
        }
        public bool Exit()
        {
            game.status = Status.stopped;
            return true;
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
                if (UIElements[i].active) UIElements[i].Draw();
            }
        }
    }
}
