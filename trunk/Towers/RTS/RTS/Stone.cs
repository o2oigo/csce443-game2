using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RTS
{
    public enum ElementType
    {
        Fire,
        Water,
        Heal,
        Normal,
    }

    public class Stone
    {
        private Map map;
        Game1 game;
        ContentManager contentManager;
        private Texture2D stoneTexture;
        public Texture2D Texture
        {
            get { return stoneTexture; }
        }

        private ElementType type;
        public ElementType Type
        {
            get { return type; }
        }

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }

        private int timeOut = 1000;
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
            this.type = (ElementType)Enum.ToObject(typeof(ElementType), type);
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
                case ElementType.Fire:
                    stoneTexture = contentManager.Load<Texture2D>("firestone");
                    break;
                case ElementType.Water:
                    stoneTexture = contentManager.Load<Texture2D>("firestone");
                    break;
                case ElementType.Heal:
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

        public int getTypeNumber()
        {
            if (type == ElementType.Fire)
                return 1;
            else if (type == ElementType.Water)
                return 2;
            else if (type == ElementType.Heal)
                return 3;
            else
                return 2;
        }
    }
}
