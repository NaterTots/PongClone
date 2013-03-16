using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PongClone
{
    class Keyboard
    {
        public KeyboardState CurrentKeyboardState { get; set; }
        public KeyboardState LastKeyboardState { get; set; }

        public bool IsKeyPress(Keys key)
        {
            return CurrentKeyboardState.IsKeyDown(key);
        }

        public bool IsNewKeyPress(Keys key)
        {
            return (LastKeyboardState.IsKeyUp(key) &&
                CurrentKeyboardState.IsKeyDown(key));
        }

        public bool PauseOrQuit
        {
            get { return IsNewKeyPress(Keys.Escape); }
        }

        public bool MenuSelection
        {
            get { return IsNewKeyPress(Keys.Enter); }
        }

        public bool Up
        {
            get
            {
                if (ScreenManager.gameState == GameState.Menu)
                {
                    return IsNewKeyPress(Keys.Up);
                }
                else
                {
                    return IsKeyPress(Keys.Up);
                }
            }
        }

        public bool Down
        {
            get
            {
                if (ScreenManager.gameState == GameState.Menu)
                {
                    return IsNewKeyPress(Keys.Down);
                }
                else
                {
                    return IsKeyPress(Keys.Down);
                }
            }
        }

        public void Update()
        {
            LastKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Microsoft.Xna.Framework.Input.Keyboard.GetState();
        }
    }
}
