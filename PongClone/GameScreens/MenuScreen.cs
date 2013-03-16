using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongClone
{
    enum MenuState
    {
        Main,
        Help
    }

    class MenuScreen
    {
        List<Text> mainStateText, helpStateText;
        MenuState menuState = MenuState.Main;
        int selection;
        Texture2D menuScreenImage;

        public MenuScreen()
        {
            selection = 0;

            #region Main

            mainStateText = new List<Text>();
            mainStateText.Add(new Text("PLAY GAME", new Vector2(500, 400)));
            mainStateText.Add(new Text("HELP", new Vector2(500, 5.0f)));
            mainStateText.Add(new Text("QUIT", new Vector2(500, 5.0f)));

            #endregion

            #region Help

            helpStateText = new List<Text>();
            helpStateText.Add(new Text("HELP", new Vector2(500, 275)));
            helpStateText.Add(new Text("UP ARROW - MOVE UP", new Vector2(50, 375)));
            helpStateText.Add(new Text("DOWN ARROW - MOVE DOWN", new Vector2(50, 5.0f)));
            helpStateText.Add(new Text("ESCAPE (IN MAIN MENU) - QUIT THE GAME", new Vector2(50, 5.0f)));
            helpStateText.Add(new Text("BACKSPACE (IN GAME) - GO TO MAIN MENU", new Vector2(50, 50.0f)));

            #endregion
        }

        public void UpdateTextPositioning() 
        { 
            #region Menu Items 
            for (int i = 1; i < mainStateText.Count; i++)
                mainStateText[i].Position += new Vector2(0, mainStateText[i - 1].Position.Y
                    + mainStateText[i - 1].Size.Y); 
            #endregion 
            
            #region Help Items 
            for (int i = 2; i < helpStateText.Count; i++)
                helpStateText[i].Position += new Vector2(0, helpStateText[i - 1].Position.Y
                    + helpStateText[i - 1].Size.Y); 
            #endregion 
        }

        public void LoadContent()
        {
            menuScreenImage = PongGame.content.Load<Texture2D>("menuscreen");
        }

        public void Update(GameTime time)
        {
            if (menuState == MenuState.Main)
            {
                #region Main
                if (ScreenManager.keyboard.Down)
                {
                    if (selection < mainStateText.Count - 1)
                    {
                        ++selection;
                    }
                    else
                    {
                        selection = 0;
                    }
                }
                else if (ScreenManager.keyboard.Up)
                {
                    if (selection > 0)
                    {
                        --selection;
                    }
                    else
                    {
                        selection = mainStateText.Count - 1;
                    }
                }
                else if (ScreenManager.keyboard.PauseOrQuit)
                {
                    ScreenManager.isExiting = true;
                }
                else if (ScreenManager.keyboard.MenuSelection)
                {
                    switch (selection)
                    {
                        case 0:
                            {
                                ScreenManager.gameState = GameState.Play;
                                break;
                            }
                        case 1:
                            {
                                menuState = MenuState.Help;
                                break;
                            }
                        case 2:
                            {
                                ScreenManager.isExiting = true;
                                break;
                            }
                    }
                }

                for (int i = 0; i < mainStateText.Count; i++) 
                { 
                    if (i == selection) 
                    {
                        if (!mainStateText[i].Active)
                        {
                            mainStateText[i].Active = true;
                        }
                    } 
                    else 
                    {
                        if (mainStateText[i].Active)
                        {
                            mainStateText[i].Active = false;
                        }
                    }
                    mainStateText[i].Update(); 
                }
                #endregion
            }
            else
            {
                #region Help

                if (ScreenManager.keyboard.MenuSelection || ScreenManager.keyboard.PauseOrQuit)
                {
                    menuState = MenuState.Main;
                }

                #endregion
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 origin = new Vector2(800 / 2 - (600 / 2), 0);
            spriteBatch.Draw(menuScreenImage, Vector2.Zero, Color.White);

            if (menuState == MenuState.Main)
            {
                #region Main
                
                foreach (Text t in mainStateText)
                {
                    t.Draw(spriteBatch);
                }

                #endregion
            }
            else
            {
                #region Help
                foreach (Text t in helpStateText)
                {
                    t.Draw(spriteBatch);
                }
                #endregion
            }
        }
    }
}
