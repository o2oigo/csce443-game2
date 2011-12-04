using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RTS
{
    public abstract class Sprite
    {
        protected Game1 game;
        protected ContentManager contentManager;
        protected GraphicsDevice graphicsDevice;
        protected SpriteBatch spriteBatch;
        protected Map map;
        protected SpriteAnimation animation;


        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Rectangle FeetSize
        {
            get { return new Rectangle((int)position.X, (int)position.Y, Size.Width, 10); }
        }

        public Rectangle SizePos
        {
            get { return new Rectangle((int)position.X, (int)position.Y, Size.Width, Size.Height); }
        }

        public Rectangle Size
        {
            get { return animation.currentSpriteSheet().size; }
        }
        ~Sprite()
        {
            allObjects.Remove(this);
        }

        //public Vector2 getPosition()
        //{
        //    return position;
        //}

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

        public Sprite() {
            addList(this);
        }

        public static void DrawT(SpriteBatch spriteBatch)
        {
            //TODO: sort allObjects
            //for (int i = 0; i < allObjects.Count(); i++)
            //{
            //    allObjects[i].Draw(spriteBatch);
            //}
            foreach (Sprite sprite in allObjects)
            {
                sprite.Draw(spriteBatch);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public static void SortSprite()
        {
            var length = (from i in allObjects
                          orderby i.Position.Y
                          select i) as List<Sprite>;
            var lambda = allObjects.OrderBy(i => i.Position.Y).ToList();
            allObjects = lambda;
        }

        public static List<Sprite> allObjects = new List<Sprite>();


    }
}
