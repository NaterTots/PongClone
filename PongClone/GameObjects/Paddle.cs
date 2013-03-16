using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongClone
{
    class Paddle
    {
        enum Controller
        {
            Player,
            Watson
        }

        Controller controller;
        Vector2 position;
        Vector2 velocity;
        float speed;
        public Rectangle Boundary { get; set; }

        public Paddle(Vector2 position, float speed, bool playerControlled)
        {
            this.position = position;
            this.speed = speed;
            controller = (playerControlled ? Controller.Player : Controller.Watson);
            velocity = Vector2.Zero;
        }

        public void Update(Ball ball)
        {
            velocity = Vector2.Zero;

            if (controller == Controller.Player)
            {
                if (ScreenManager.keyboard.Up)
                {
                    velocity.Y = speed * -1;
                }
                else if (ScreenManager.keyboard.Down)
                {
                    velocity.Y = speed;
                }
                position += velocity;
            }
            else //if (controller == Controller.Watson)
            {
                if (ball.Velocity.X > 0) 
                { 
                    float distance = position.Y + 25 - ball.Position.Y;
                    if (distance > 15)
                    {
                        velocity.Y = speed * -1;
                    }
                    else if (distance < -15)
                    {
                        velocity.Y = speed;
                    }
                    position += velocity; 
                }
            }

            #region Check Position 
            
            if (position.Y < 10)
            {
                position.Y = 10;
            }
            else if (position.Y > 540)
            {
                position.Y = 540;
            }
            
            #endregion

            Boundary = new Rectangle((int)position.X, (int)position.Y, 10, 50);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameScreen.pixel, Boundary, Color.White);
        }
    }
}
