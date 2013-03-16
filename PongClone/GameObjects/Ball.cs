using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongClone
{
    class Ball
    {
        private Vector2 velocity;
        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
        }
        public Vector2 Position { get; set; }

        protected Rectangle boundary;
        public Rectangle Boundary
        {
            get
            {
                return boundary;
            }
        }

        protected float speed;
        protected float angle;

        public Ball(float speed)
        {
            this.speed = speed;
            Reset();
        }

        private void Reset()
        {
            if (GameScreen.WatsonScore >= 5 || GameScreen.PlayerScore >= 5)
            {
                GameScreen.ScreenState = GameScreenState.GameOver;
            }
            else
            {
                speed = 6;
                Position = new Vector2((800 / 2) - 5, (600 / 2) - 5);
                RandomizeAngle();
                velocity = new Vector2((float)(speed * Math.Cos(angle)), (float)(speed * Math.Sin(angle)));
                boundary = new Rectangle((int)Position.X, (int)Position.Y, 10, 10);
            }
        }

        private void RandomizeAngle()
        {
            Random randomAngle = new Random();
            int r = 0;
            while (r % 2 == 0)
            {
                r = randomAngle.Next(1, 7);
            }

            angle = MathHelper.ToRadians(r * 45);
        }

        public void Update()
        {
            if (Position.Y <= 0 || Position.Y >= 600)
            {
                velocity.Y = velocity.Y * -1;
            }

            if (Position.X <= 0)
            {
                GameScreen.WatsonScore++;
                Reset();
            }
            else if (Position.X >= 800)
            {
                GameScreen.PlayerScore++;
                Reset();
            }

            Position += velocity;
            boundary = new Rectangle((int)Position.X, (int)Position.Y, 10, 10);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GameScreen.pixel, boundary, Color.White);
        }

        public void Collision()
        {
            if (speed < 11)
            {
                velocity.Y *= 1.1f;
                velocity.X *= -1.1f;

                speed = (float)velocity.Length();
            }
            else
            {
                velocity.X *= -1;
            }
        }
    }
}
