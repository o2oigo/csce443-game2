using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RTS
{
    class SpriteManager
    {

        public Texture2D Texture;
        public Vector2 Position = Vector2.Zero;
        public Vector2 Speed = Vector2.Zero;
        public Vector2 Direction = Vector2.Zero;
        public Vector2 Origin;

        public int FrameNum = 0;
        public Rectangle Size;
        public Rectangle[,] Rectangles;
        public int FrameIndex = 0;
        public int RowIndex = 0;

        public Color Color = Color.White;
        public float Rotation = 0f;
        public float Scale = 1f;
        public SpriteEffects SpriteEffect;

        public SpriteManager(Texture2D Texture, int frames, int rows)
        {
            FrameNum = frames;
            this.Texture = Texture;
            int width = Texture.Width / frames;
            int height = Texture.Height / rows;
            Size = new Rectangle(0, 0, width, height);
            Rectangles = new Rectangle[rows, frames];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < frames; j++)
                    Rectangles[i, j] = new Rectangle(
                        j * width, i * height, width, height);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Rectangles[RowIndex, FrameIndex],
                Color);
        }
    }
}
