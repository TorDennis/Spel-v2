using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spel_Projekt_Dennis
{
    class Enemy_2: Enemies
    {
        
        public Enemy_2(Texture2D texture, float X, float Y, float scale, float originX, float originY) : base(texture, X, Y, 5f, 0.3f, scale, originX, originY)
        {
        }
        public override void Update(GameWindow window)
        {
            vector.Y += speed.Y;            
            vector.X += speed.X;
            
            if (vector.Y > window.ClientBounds.Height - texture.Height - 100)
            {
                speed.Y = 0;
                speed.X = 0;
            }
            else
            {
                _scale += 0.010f;                
                if (vector.X > window.ClientBounds.Width - texture.Width || vector.X < 1)
                {
                    speed.X = -speed.X;                    
                }
                
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, vector, new Rectangle(0, 0, Convert.ToInt32(Width), Convert.ToInt32(Height)), Color.White, 0f, _origin, _scale, SpriteEffects.None, 0f);
        }
    }
}
