using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongClone
{
    enum GameState
    {
        Menu,
        Play
    }

    class ScreenManager : DrawableGameComponent
    {
        public static GameState gameState = GameState.Menu;
        public static Keyboard keyboard;
        public static SpriteFont spriteFont;
        public static bool isExiting = false;

        protected MenuScreen menuScreen;
        protected GameScreen gameScreen;
        protected SpriteBatch spriteBatch;

        public ScreenManager(Game game)
            : base(game)
        {
            menuScreen = new MenuScreen();
            gameScreen = new GameScreen();
            keyboard = new Keyboard();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = PongGame.content.Load<SpriteFont>("basic");
            menuScreen.LoadContent();
            menuScreen.UpdateTextPositioning();
            gameScreen.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            keyboard.Update();
            if (gameState == GameState.Menu)
            {
                menuScreen.Update(gameTime);
            }
            else if (gameState == GameState.Play)
            {
                gameScreen.Update(gameTime);
            }
            base.Update(gameTime);

            if (isExiting)
            {
                Game.Exit();
            }
        } 
        
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (gameState == GameState.Menu)
            {
                menuScreen.Draw(spriteBatch);
            }
            else if (gameState == GameState.Play)
            {
                gameScreen.Draw(spriteBatch);
            }
            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
