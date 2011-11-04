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
   // //public struct SpriteSheet
   // //{
   // //    public Texture2D texture;
   // //    public int frame;
   // //    public int width;
   // //    public int height;
   // //    public Rectangle size;
   // //    public Rectangle[] rectangles;
   // //
   // //    public SpriteSheet(Texture2D txt, int col)
   // //    {
   // //        texture = txt;
   // //        frame = col;
   // //        width = texture.Width/col;
   // //        height = texture.Height;
   // //        size = new Rectangle(0, 0, width, height);
   // //
   // //        rectangles = new Rectangle[frame];
   // //        for (int i = 0; i < frame; i++)
   // //        {
   // //            rectangles[i] = new Rectangle(i * width, 0, width, height);
   // //        }
   // //    }
   // //}
   //
   // class SpriteManager
   // {
   //     //public Texture2D Texture;
   //     public Dictionary<String, SpriteSheet> textureMap;
   //
   //     //public Vector2 Position = Vector2.Zero;
   //     //public Vector2 Speed = Vector2.Zero;
   //     //public Vector2 Direction = Vector2.Zero;
   //     //public Vector2 Origin;
   //
   //     public Rectangle Size;
   //     public Rectangle[,] Rectangles;
   //     public int currentTexture = 0; 
   //     //public int FrameIndex = 0;
   //     //public int RowIndex = 0;
   //
   //     public void Initialize(Dictionary<String, Texture2D> textureMap, Dictionary<String, int> frameNum)
   //     {
   //     }
   //
   //     public SpriteManager(Texture2D Texture, int frames, int rows)
   //     {
   //         FrameNum = frames;
   //         this.Texture = Texture;
   //         int width = Texture.Width / frames;
   //         int height = Texture.Height / rows;
   //         Size = new Rectangle(0, 0, width, height);
   //         Rectangles = new Rectangle[rows, frames];
   //         for (int i = 0; i < rows; i++)
   //         {
   //             for (int j = 0; j < frames; j++)
   //                 Rectangles[i, j] = new Rectangle(
   //                     j * width, i * height, width, height);
   //         }
   //     }
   //
   //     public void Draw(SpriteBatch spriteBatch)
   //     {
   //         spriteBatch.Draw(Texture, Position, Rectangles[RowIndex, FrameIndex],
   //             Color);
   //     }
   // }
}
