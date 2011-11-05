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
    public struct SpriteSheet
    {
        public Texture2D texture;
        public int frames;
        public int width;
        public int height;
        public Rectangle size;
        public Rectangle[] rectangles;

        public SpriteSheet(Texture2D txt, int col)
        {
            texture = txt;
            frames = col;
            width = texture.Width / col;
            height = texture.Height;
            size = new Rectangle(0, 0, width, height);

            rectangles = new Rectangle[frames];
            for (int i = 0; i < frames; i++)
            {
                rectangles[i] = new Rectangle(i * width, 0, width, height);
            }
        }
    }

    public class SpriteAnimation
    {
        private String currentSprite;
        public String CurrentSprite
        {
            get { return currentSprite; }
            set { currentSprite = value; }
        }

        private int frameIndex = 0;
        public int FrameIndex
        {
            get { return frameIndex; }
        }

        private float timeElapsed;
        public bool IsLooping = false;

        private float timeToUpdate = 0.05f;
        public int FramesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }

        public SpriteSheet currentSpriteSheet()
        {
            return textureMap[currentSprite];
        }

        private Dictionary<String, SpriteSheet> textureMap;

        public SpriteAnimation(Dictionary<String, SpriteSheet> txtMap, bool isLoop)
        {
            textureMap = txtMap;
            IsLooping = isLoop;
            FramesPerSecond = 20;
        }

        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)
                gameTime.ElapsedGameTime.TotalSeconds;

            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                if (frameIndex < currentSpriteSheet().frames - 1)
                    frameIndex++;
                else if (IsLooping)
                    frameIndex = 0;
            }
        }
    }
}
