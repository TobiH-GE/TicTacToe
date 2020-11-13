using System;
using System.Collections.Generic;

namespace TicTacToe
{
    class UIConsole : UI
    {
        public List<UIObject> UIElements = new List<UIObject>();
        public Game game;
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
                UIElements[12]=(new UIText("Hint: no hint! choose an empty field!", 5, 16, ConsoleColor.DarkGray, ConsoleColor.Black));
            }
            else
            {
                UIElements[12] = (new UIText($"Hint: {x}, {y}", 5, 16, ConsoleColor.DarkGray, ConsoleColor.Black));
                UIElements[13] = (new UIText("H", 12 + x * 4, 7 + y * 2, ConsoleColor.DarkGray, ConsoleColor.Black));
            }
        }
        public override void Start(Game game)
        {
            this.game = game;
            Console.CursorVisible = false;
            UIElements.Add(new UIText("TicTacToe by TobiH", 10, 0));
            UIElements.Add(new UIText($"turn {game.turnNumber}, {game.playerNames[Convert.ToInt32(game.currentPlayerID)]} [{(game.currentPlayerID ? FieldState.X : FieldState.O)}] it's your turn!\n", 0, 2, (game.currentPlayerID ? pColor[0] : pColor[1])));
            UIElements.Add(new UIText("0   1   2", 12, 5));
            UIElements.Add(new UIText("   -------------", 7, 6));
            UIElements.Add(new UIText("0  |   |   |   |", 7, 7));
            UIElements.Add(new UIText("   -------------", 7, 8));
            UIElements.Add(new UIText("1  |   |   |   |", 7, 9));
            UIElements.Add(new UIText("   -------------", 7, 10));
            UIElements.Add(new UIText("2  |   |   |   |", 7, 11));
            UIElements.Add(new UIText("   -------------", 7, 12));
            UIElements.Add(new UIText("", 5, 15)); // 10 = Infotext
            UIElements.Add(new UIText("", 20, 16)); // 11 = Error
            UIElements.Add(new UIText("", 5, 16)); // 12 = HintText
            UIElements.Add(new UIText("", 25, 16)); // 13 = HintSymbol
        }
        public override void WaitForInput()
        {
            Point input = new Point();
            FPS fpsCounter = new FPS();

            ConsoleKeyInfo cs = new ConsoleKeyInfo();
            int currentInput = 0;

            do
            {
                // update
                UIElements[1]=(new UIText($"turn {game.turnNumber}, {game.playerNames[Convert.ToInt32(game.currentPlayerID)]} [{(game.currentPlayerID ? FieldState.X : FieldState.O)}] it's your turn!\n", 0, 2, (game.currentPlayerID ? pColor[0] : pColor[1])));

                fpsCounter.Draw();
                for (int i = 0; i < UIElements.Count; i++)
                {
                    UIElements[i].Draw();
                }

                if (currentInput == 0) UIElements[10].text = "enter [0,1,2] or [9] for hint ... x-position:";
                if (currentInput == 1) UIElements[10].text = "enter [0,1,2] or [9] for hint ... y-position:";

                if (Console.KeyAvailable)
                {
                    cs = Console.ReadKey(true);

                    if (currentInput == 0)
                    {
                        if (Char.IsNumber(cs.KeyChar))
                        {
                            Byte.TryParse(cs.KeyChar.ToString(), out input.x);
                            input.xHasValue = true;
                            //Console.WriteLine("input.x hat Wert");
                        }
                        else if (cs.Key == ConsoleKey.Enter && input.xHasValue)
                        {
                            if (input.x == 9) game.DrawHint();
                            else currentInput++;
                        }
                    }
                    if (currentInput == 1)
                    {
                        if (Char.IsNumber(cs.KeyChar))
                        {
                            Byte.TryParse(cs.KeyChar.ToString(), out input.y);
                            input.yHasValue = true;
                            //Console.WriteLine("input.y hat Wert");
                        }
                        else if (cs.Key == ConsoleKey.Enter && input.yHasValue)
                        {
                            if (input.y == 9) game.DrawHint();
                            else currentInput++;
                        }
                    }
                    if (currentInput == 2)
                    {
                        input.xHasValue = false;
                        input.yHasValue = false;

                        //Console.WriteLine("starte Spielzug");
                        if (game.turn(input) == TurnResult.Invalid)
                        {
                            UIElements[11].text = "invalid!";
                        }
                        else
                        {
                            UIElements.Add(new UIText($"{game.board[input.y, input.x]}", 12 + input.x * 4, 7 + input.y * 2, (game.board[input.y, input.x] == FieldState.X ? pColor[0] : pColor[1])));
                        }
                        currentInput = 0;
                    }
                }
            } while (game.turnNumber < 10); //TODO: Nachfolgenden Code in Realtime einbauen

            if (game.turnNumber == 10)
            {
                UIElements[10].text = "tie! again? [y/n]";
            }
            else
            {
                Draw(game.board);
                UIElements[10].text = "win! again? [y/n]";
            }
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
