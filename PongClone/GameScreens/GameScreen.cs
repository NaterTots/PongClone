using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongClone
{
    enum GameScreenState
    {
        Playing,
        GameOver
    }

    class GameScreen
    {
        Paddle playerPaddle, watsonPaddle;
        Ball ball;
        private static GameScreenState screenState;
        public static GameScreenState ScreenState
        {
            get
            {
                return screenState;
            }
            set
            {
                if (screenState != value && value == GameScreenState.GameOver)
                {
                    SetGameOverText(PlayerScore > WatsonScore);
                }
                screenState = value;
            }
        }

        public static int PlayerScore;
        public static int WatsonScore;

        static string gameOverText;

        public static Texture2D pixel;

        public GameScreen()
        {
            playerPaddle = new Paddle(new Vector2(20, 275), 8, true);
            watsonPaddle = new Paddle(new Vector2(780, 275), 8, false);
            ball = new Ball(6);
            screenState = GameScreenState.Playing;
        }

        public void LoadContent()
        {
            pixel = PongGame.content.Load<Texture2D>("whitepixel");
        }

        public void Update(GameTime gameTime)
        {
            if (screenState == GameScreenState.Playing)
            {
                CheckCollisions();
                playerPaddle.Update(ball);
                watsonPaddle.Update(ball);
                ball.Update();

                if (ScreenManager.keyboard.PauseOrQuit)
                {
                    ScreenManager.gameState = GameState.Menu;
                }
            }
            else //if (screenState == GameScreenState.GameOver)
            {
                if (ScreenManager.keyboard.MenuSelection)
                {
                    if (WatsonScore > PlayerScore)
                    {
                        WatsonScore = -1;
                        PlayerScore = 0;
                    }
                    else
                    {
                        WatsonScore = 0;
                        PlayerScore = -1;
                    }
                    screenState = GameScreenState.Playing;
                }
                else if (ScreenManager.keyboard.PauseOrQuit)
                {
                    ScreenManager.isExiting = true;
                }
            }
        }

        private void CheckCollisions()
        {
            if (ball.Boundary.Intersects(playerPaddle.Boundary)) 
            {
                if (ball.Velocity.X < 0)
                {
                    ball.Collision();
                }
            } 
            else if (ball.Boundary.Intersects(watsonPaddle.Boundary)) 
            {
                if (ball.Velocity.X > 0)
                {
                    ball.Collision();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            playerPaddle.Draw(spriteBatch);
            watsonPaddle.Draw(spriteBatch);
            ball.Draw(spriteBatch);

            spriteBatch.DrawString(ScreenManager.spriteFont, "PLAYER: " + PlayerScore,
                new Vector2(30, 10), Color.Green);
            spriteBatch.DrawString(ScreenManager.spriteFont, "WATSON: " + WatsonScore,
                new Vector2(600, 10), Color.Green);

            if (screenState == GameScreenState.GameOver)
            {
                spriteBatch.DrawString(ScreenManager.spriteFont, gameOverText,
                    new Vector2(400 - ScreenManager.spriteFont.MeasureString(gameOverText).X / 2, 250),
                    (WatsonScore > PlayerScore ? Color.Red : Color.Green));
                spriteBatch.DrawString(ScreenManager.spriteFont, "PRESS <ENTER> TO RESTART",
                    new Vector2(200, 350), Color.White);
                spriteBatch.DrawString(ScreenManager.spriteFont, "PRESS <ESC> TO QUIT",
                    new Vector2(200, 375), Color.White);
            }
        }

        private static void SetGameOverText(bool winner)
        {
            Random random = new Random((int)DateTime.Now.Ticks);

            if (winner)
            {
                switch (random.Next(1, 5))
                {
                    case 1:
                        gameOverText = "WATSON IS (A) TOAST(ER)!";
                        break;
                    case 2:
                        gameOverText = "STICK WITH JEOPARDY, WATSON!";
                        break;
                    case 3:
                        gameOverText = "YOU ARE THE WINNER!";
                        break;
                    case 4:
                        gameOverText = "BATTLE BALL CHAMPION!";
                        break;
                }
            }
            else
            {
                switch (random.Next(1, 5))
                {
                    case 1:
                        gameOverText = "SAY HELLO TO YOUR COMPUTER OVERLORD!";
                        break;
                    case 2:
                        gameOverText = "JEOPARDY, SUPER BATTLE BALL, WHAT'S NEXT?";
                        break;
                    case 3:
                        gameOverText = "YOU ARE THE LOSER :(";
                        break;
                    case 4:
                        gameOverText = "BETTER LUCK NEXT TIME.";
                        break;
                }
            }
        }
    }
}
