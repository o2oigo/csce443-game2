using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RTS
{
    public enum StoneType
    {
        Fire,
        Water,
        Heal,
    }

    class Stone
    {
        private Map map;
        Game1 game;
        ContentManager contentManager;
        private Texture2D stoneTexture;
        public Texture2D Texture
        {
            get { return stoneTexture; }
        }

        private StoneType type;
        public StoneType Type
        {
            get { return type; }
        }

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }

        private int timeOut = 100;
        public int TimeOut
        {
            get { return timeOut; }
        }

        private bool appear = false;
        public bool isAppear
        {
            get { return appear; }
        }

        public void Initialize(Game1 game, Vector2 startPosition, int type)
        {
            this.game = game;
            contentManager = game.Content;
            position = startPosition;
            map = game.Map;
            this.type = (StoneType)Enum.ToObject(typeof(StoneType), type);
            appear = true;

            LoadContent();
        }

        public void Update(GameTime gameTime)
        {
            if (timeOut <= 0) appear = false;
            else timeOut--;
        }

        public void LoadContent()
        {
            switch (type)
            {
                case StoneType.Fire:
                    stoneTexture = contentManager.Load<Texture2D>("firestone");
                    break;
                case StoneType.Water:
                    stoneTexture = contentManager.Load<Texture2D>("firestone");
                    break;
                case StoneType.Heal:
                    stoneTexture = contentManager.Load<Texture2D>("firestone");
                    break;
                default:
                    stoneTexture = contentManager.Load<Texture2D>("firestone");
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(stoneTexture, position, null, Color.White, 0, new Vector2(0, stoneTexture.Height / 2), map.ScaleB, SpriteEffects.None, 0f);
        }
    }
}
