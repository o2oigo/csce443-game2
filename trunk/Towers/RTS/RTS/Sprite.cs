using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RTS
{
    class Sprite
    {
        protected Game1 game;
        protected ContentManager contentManager;
        protected GraphicsDevice graphicsDevice;
        protected SpriteBatch spriteBatch;
        protected Map map;

        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Sprite() { }

        public static List<Sprite> allObjects = new List<Sprite>();

        protected static void addList(Sprite obj)
        {
            allObjects.Add(obj);
        }

        public static void removeList(Sprite obj)
        {
            allObjects.Remove(obj);
        }

        public static int objCount()
        {
            return allObjects.Count();
        }

    }
}
