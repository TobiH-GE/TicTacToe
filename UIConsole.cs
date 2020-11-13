﻿using System;
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
            UIElements.Add(new UIInput("X-Position", 5, 17)); // 14 = Input X
            UIElements.Add(new UIInput("Y-Position", 5, 18)); // 15 = Input Y
        }
        public override void WaitForInput()
        {
            Point input = new Point();
            FPS fpsCounter = new FPS();

            ConsoleKeyInfo UserInput = new ConsoleKeyInfo();

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
                            UIElements[activeElement].Input = "0";
                            break;
                        case ConsoleKey.D1:
                            UIElements[activeElement].Input = "1";
                            break;
                        case ConsoleKey.D2:
                            UIElements[activeElement].Input = "2";
                            break;
                        case ConsoleKey.Enter:
                            if (activeElement == 15)
                            {
                                Byte.TryParse(UIElements[14].Input, out input.x);
                                Byte.TryParse(UIElements[15].Input, out input.y);

                                if (game.turn(input) == TurnResult.Invalid)
                                {
                                    UIElements[11].text = "invalid!";
                                }
                                else
                                {
                                    UIElements.Add(new UIText($"{game.board[input.y, input.x]}", 12 + input.x * 4, 7 + input.y * 2, (game.board[input.y, input.x] == FieldState.X ? pColor[0] : pColor[1])));
                                    UIElements[1] = (new UIText($"turn {game.turnNumber}, {game.playerNames[Convert.ToInt32(game.currentPlayerID)]} [{(game.currentPlayerID ? FieldState.X : FieldState.O)}] it's your turn!\n", 0, 2, (game.currentPlayerID ? pColor[0] : pColor[1])));
                                }
                            }

                            activeElement++;
                            if (activeElement > 15) activeElement = 14;
                            break;
                        case ConsoleKey.D9:
                            game.DrawHint();
                            break;
                        default:
                            break;
                    }
                }
            } while (true);    
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
