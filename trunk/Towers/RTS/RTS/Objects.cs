using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RTS
{
    public abstract class Object
    {
    }

    public class Snow
    {
        static List<Snow> snowList = new List<Snow>();

        private Texture2D snowTexture;
        Game1 game;
        double snowTimer;
        Random rand = new Random();

        private Vector2 origin;
        public Vector2 Origin
        {
            get { return origin; }
        }
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }

        public Snow(Game1 game)
        {
            this.game = game;
            //snowList.Add(this);
            position = origin = new Vector2(rand.Next(0, 500),0);
        }

        public void createSnow(Game1 game)
        {
            snowList.Add(new Snow(game));
        }
         public void LoadContent()
         {
             snowTexture = game.Content.Load<Texture2D>("snow");
         }

        public void Update(GameTime gameTime)
        {
            snowTimer = gameTime.ElapsedGameTime.Milliseconds;
            if (snowTimer > 100)
            {
                position.Y++;
                snowTimer = 0;
            }
            //if (snowTimer > 100)
            {
                Snow tmp = new Snow(this.game);
                tmp.LoadContent();
                snowList.Add(tmp);
                snowTimer = 0;
            }
            for (int i=0; i<snowList.Count; i++)
            {
                snowList[i].Update(gameTime);
                if (snowList[i].Position.Y > game.getCurrentRectangle().Height)
                {
                    snowList.RemoveAt(i);
                }
            }
        }

         public void Draw(SpriteBatch spriteBatch)
         {
             spriteBatch.Draw(snowTexture, position,Color.White);
         }
        //public void updateSnow(GameTime gameTime)
        //{
        //    snowTimer += gameTime.ElapsedGameTime.Milliseconds;
        //    if (snowTimer > 1000)
        //    {
        //        Snow tmp = new Snow(this);
        //        tmp.LoadContent();
        //        snowList.Add(tmp);
        //        snowTimer = 0;
        //    }
        //    //foreach (Snow snow in snowList)
        //    for (int i=0; i<snowList.Count; i++)
        //    {
        //        snowList[i].Update(gameTime);
        //        if (snowList[i].Position.Y > rectDict[wave.CurrentLevel].Height)
        //        {
        //            snowList.RemoveAt(i);
        //         
        //        }
        //    }
        //}
    }


    public class Tree : Sprite
    {
        //Game1 game;
        private Texture2D treeTexture;
        public Texture2D Texture
        {
            get { return treeTexture; }
        }
        private Vector2 origin;
        public Vector2 Origin
        {
            get { return origin; }
        }

        Random rand = new Random();
        //Map map;
        static List<String> treeTextureList = new List<String>();

        public Tree(Game1 game, Vector2 pos)
        {
            this.game = game;
            map = game.Map;
            position = pos;
            origin = pos;

            treeTextureList.Add("treeA");
            treeTextureList.Add("treeB");
            treeTextureList.Add("treeC");
            treeTextureList.Add("treeD");
            treeTextureList.Add("treeE");
            treeTextureList.Add("treeF");
            treeTextureList.Add("treeG");
            treeTextureList.Add("treeH");
        }

        public void LoadContent(int i)
        {
            treeTexture = game.Content.Load<Texture2D>(treeTextureList[i]);
            position.X -= treeTexture.Width / 2;
            position.Y -= treeTexture.Height / 2; 
            //bark = new Rectangle((int)origin.X, (int)origin.Y, 50, 50);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(treeTexture, position, null, Color.White, 0f, new Vector2(0, treeTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);
        }
    }

    public class Lamp : Sprite
    {
        //Game1 game;
        private Texture2D lampTexture;
        public Texture2D Texture
        {
            get { return lampTexture; }
        }
        private Vector2 origin;
        public Vector2 Origin
        {
            get { return origin; }
        }

        Random rand = new Random();
        //Map map;

        public Lamp(Game1 game, Vector2 pos)
        {
            this.game = game;
            map = game.Map;
            position = pos;
            origin = pos;
        }

        public void LoadContent()
        {
            lampTexture = game.Content.Load<Texture2D>("lamppost");
            position.X -= lampTexture.Width / 2;
            position.Y -= lampTexture.Height / 2;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(lampTexture, position, null, Color.White, 0f, new Vector2(0, lampTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);
        }
    }

    public class EndPoint
    {
        Game1 game;
        private Texture2D houseTexture;
        public Texture2D Texture
        {
            get { return houseTexture; }
        }

        public Rectangle FeetSize
        {
            get { return new Rectangle((int)position.X, (int)position.Y, houseTexture.Width, 20); }
        }

        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
        }
        private Vector2 origin;
        public Vector2 Origin
        {
            get { return origin; }
        }

        public EndPoint(Game1 game, Vector2 pos)
        {
            this.game = game;
            position = pos;
            origin = pos;
            origin.X += 20;
            origin.Y += 40;
        }

        public void LoadContent()
        {
            houseTexture = game.Content.Load<Texture2D>("house");
            position.X -= (houseTexture.Width / 2) ;
            position.Y -= (houseTexture.Height / 2) ;
            position.X += 20;
            position.Y += 40;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(houseTexture, position, null, Color.White, 0f, new Vector2(0, houseTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);
        }
    }
}
