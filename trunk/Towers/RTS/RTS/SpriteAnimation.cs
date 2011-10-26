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
    class SpriteAnimation : SpriteManager
    {
        private float timeElapsed;
        public bool IsLooping = false;

        private float timeToUpdate = 0.05f;
        public int FramesPerSecond
        {
            set { timeToUpdate = (1f / value); }
        }

        public SpriteAnimation(Texture2D Texture, int frames, int rows, bool isLoop)
            : base(Texture, frames, rows)
        {
            IsLooping = isLoop;
            FramesPerSecond = 20;
        }

        public virtual void Update(GameTime gameTime)
        {
            timeElapsed += (float)
                gameTime.ElapsedGameTime.TotalSeconds;

            if (timeElapsed > timeToUpdate)
            {
                timeElapsed -= timeToUpdate;

                if (FrameIndex < FrameNum - 1)
                    FrameIndex++;
                else if (IsLooping)
                    FrameIndex = 0;
            }
        }
    }
}
