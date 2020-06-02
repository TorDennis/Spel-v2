using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel_Projekt_Dennis
{
    class Player : PhysicalObject
    {
        private int points = 0;
        double timeSinceLastBullet;
        public Player(Texture2D texture, float X, float Y) : base(texture, X, Y, 0, 0)
        {
        }
        public void Update(GameWindow window, GameTime gameTime)
        {
            KeyboardState keyboardstate = Keyboard.GetState();
            MouseState state = Mouse.GetState();
            vector.X = state.X;
            vector.Y = state.Y;

            if (vector.X < 0) vector.X = 0;
            if (vector.X > window.ClientBounds.Width - texture.Width) vector.X = window.ClientBounds.Width - texture.Width;
            if (vector.Y < 0) vector.Y = 0;
            if (vector.Y > window.ClientBounds.Height - texture.Width) vector.Y = window.ClientBounds.Height - texture.Height;

            //Skjuta funktion

            if (state.LeftButton == ButtonState.Pressed)
            {
                if (gameTime.TotalGameTime.TotalMilliseconds > timeSinceLastBullet + 300)
                {
                    timeSinceLastBullet = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, Color.White);
        }
        public void Reset()
        {
            timeSinceLastBullet = 0;
            isAlive = true;
            points = 0;
        }
        public int Points { get { return points; } set { points = value; } }
    }
}
