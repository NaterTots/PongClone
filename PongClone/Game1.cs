
#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
#endregion

namespace TinyTennisXNA
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        ContentManager content;

        private Bat _player1;
        private Bat _player2;
        private Ball _ball;
        private GameState gameState;
        public static Texture2D SpriteTexture;
        public static SpriteBatch SpriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            content = new ContentManager(Services);

            //graphics.SynchronizeWithVerticalRetrace = false;

            gameState = new GameState();

            //Create the bat Sprites - they need the keyboard controls and the gameplay area limits
            _player1 = new Bat(30, Keys.Q, Keys.A, 0, Window.ClientBounds.Height);

            //use this line for a second human player
            //_player2 = new Bat( ClientSize.Width - 30 - Bat.Width, Keys.P, Keys.L, 0, ClientSize.Height);

            //use this line for a computer player
            _player2 = new Bat(Window.ClientBounds.Width - 30 - Bat.Width, 0, Window.ClientBounds.Height);

            //Create the ball sprite. It needs the gameplay area limits and references to the bats to be able to check for collisions
            _ball = new Ball(0, Window.ClientBounds.Width, 0, Window.ClientBounds.Height, _player1, _player2, gameState);

            //Connect the AI player with the ball
            _player2.Ball = _ball;

        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            SpriteBatch = new SpriteBatch(this.graphics.GraphicsDevice); 
            base.Initialize();
        }


        /// <summary>
        /// Load your graphics content.  If loadAllContent is true, you should
        /// load content from both ResourceManagementMode pools.  Otherwise, just
        /// load ResourceManagementMode.Manual content.
        /// </summary>
        /// <param name="loadAllContent">Which type of content to load.</param>
        protected override void LoadGraphicsContent(bool loadAllContent)
        {
            if (loadAllContent)
            {
                SpriteTexture = content.Load<Texture2D>("whitePixel");
            }

            // TODO: Load any ResourceManagementMode.Manual content
        }


        /// <summary>
        /// Unload your graphics content.  If unloadAllContent is true, you should
        /// unload content from both ResourceManagementMode pools.  Otherwise, just
        /// unload ResourceManagementMode.Manual content.  Manual content will get
        /// Disposed by the GraphicsDevice during a Reset.
        /// </summary>
        /// <param name="unloadAllContent">Which type of content to unload.</param>
        protected override void UnloadGraphicsContent(bool unloadAllContent)
        {
            if (unloadAllContent == true)
            {
                content.Unload();
            }
        }


        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the default game to exit on Xbox 360 and Windows
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.CornflowerBlue);

            //Perform any animation
            _player1.Update(gameTime.TotalGameTime.TotalSeconds, gameTime.ElapsedGameTime.TotalSeconds);
            _player2.Update(gameTime.TotalGameTime.TotalSeconds, gameTime.ElapsedGameTime.TotalSeconds);
            _ball.Update(gameTime.TotalGameTime.TotalSeconds, gameTime.ElapsedGameTime.TotalSeconds);

            //Draw the scores
            //player1Score.Text = gameState.Player1Score.ToString();
            //player1Score.Refresh();
            //player2Score.Text = gameState.Player2Score.ToString();
            //player2Score.Refresh();

            //and the game objects
            _player1.Draw();
            _player2.Draw();
            _ball.Draw();


            base.Draw(gameTime);
        }
    }
}