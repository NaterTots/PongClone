using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PongClone
{
    class Text
    {
        string text;
        public Vector2 Position { get; set; }

        Color colorActive, colorBasic, colorCurrent;
        public bool Active { get; set; }
        
        public Text(string text, Vector2 position)
        {
            this.text = text;
            Position = position;
            Active = false;
            colorActive = Color.Green;
            colorBasic = Color.White;
            colorCurrent = colorBasic;
        }

        public Vector2 Size
        {
            get
            {
                return ScreenManager.spriteFont.MeasureString(text);
            }
        }

        public void Update()
        {
            colorCurrent = (Active ? colorActive : colorBasic);
        }

        public void Draw(SpriteBatch batch)
        {
            batch.DrawString(ScreenManager.spriteFont, text, Position, colorCurrent);
        }
    }
}
