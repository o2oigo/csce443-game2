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

        static Texture2D hpBarFrame;
        static Texture2D hpBarInside;

        protected Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        protected Vector2 lowerLeft;
        public Vector2 LowerLeft
        {
            get { return lowerLeft; }
            //set { lowerLeft = value; }
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

        public static void LoadContent(ContentManager Content)
        {
            hpBarFrame = Content.Load<Texture2D>("hpBorder");
            hpBarInside = Content.Load<Texture2D>("hpInsid");
        }


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
                //if (sprite is Enemy && ((Enemy)sprite).Moving==true)
                //{
                //    Vector2 pos = sprite.Position;
                //    pos.X -= 40;
                //    pos.Y -= 30;
                //    Rectangle newRect = new Rectangle((int)pos.X, (int)pos.Y, (int)((((Enemy)sprite).HP / ((Enemy)sprite).MaxHP) * hpBarInside.Width), (int)hpBarInside.Height);
                //
                //    spriteBatch.Draw(hpBarInside, newRect, Color.White);
                //    spriteBatch.Draw(hpBarFrame, pos, Color.White);
                //}
                //else if (sprite is Tower)
                //{
                //    Vector2 pos = sprite.Position;
                //    pos.X -= 40;
                //    pos.Y += 40;
                //    Rectangle newRect = new Rectangle((int)pos.X, (int)pos.Y, (int)((((Tower)sprite).HP / ((Tower)sprite).MaxHP) * hpBarInside.Width), (int)hpBarInside.Height);
                //
                //    spriteBatch.Draw(hpBarInside, newRect, Color.White);
                //    spriteBatch.Draw(hpBarFrame, pos, Color.White);
                //}
            }
        }


        public void DrawHPBarEnemy(SpriteBatch spriteBatch)
        {
            if (((Enemy)this).Moving==true)
            {
                Vector2 pos = this.Position;
                pos.X -= 40;
                pos.Y -= 30;
                Rectangle newRect = new Rectangle((int)pos.X, (int)pos.Y, (int)((((Enemy)this).HP / ((Enemy)this).MaxHP) * hpBarInside.Width), (int)hpBarInside.Height);
            
                spriteBatch.Draw(hpBarFrame, pos, Color.White);
                spriteBatch.Draw(hpBarInside, newRect, Color.White);
            }
        }

        public void DrawHPBarTower(SpriteBatch spriteBatch)
        {
                Vector2 pos = this.Position;
                pos.X -= 40;
                pos.Y += 40;
                Rectangle newRect = new Rectangle((int)pos.X, (int)pos.Y, (int)((((Tower)this).HP / ((Tower)this).MaxHP) * hpBarInside.Width), (int)hpBarInside.Height);

                spriteBatch.Draw(hpBarInside, newRect, Color.White);
                spriteBatch.Draw(hpBarFrame, pos, Color.White);
        }


        public virtual void Draw(SpriteBatch spriteBatch)
        {
        }

        public static void SortSprite()
        {
            var length = (from i in allObjects
                          orderby i.Position.Y
                          select i) as List<Sprite>;
            var lambda = allObjects.OrderBy(i => (i.Position.Y)).ToList();
            allObjects = lambda;
        }

        public static List<Sprite> allObjects = new List<Sprite>();


    }
}
