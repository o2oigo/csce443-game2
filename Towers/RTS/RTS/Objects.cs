using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RTS
{
    public class Tree
    {
        Game1 game;
        private Texture2D treeTexture;
        public Texture2D Texture
        {
            get { return treeTexture; }
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

        Random rand = new Random();
        Map map;
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
            //int i = rand.Next(8);
            treeTexture = game.Content.Load<Texture2D>(treeTextureList[i]);
            position.X -= treeTexture.Width / 2;
            position.Y -= treeTexture.Height / 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(treeTexture, position, null, Color.White, 0f, new Vector2(0, treeTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);
        }
    }

    public class House
    {
        Game1 game;
        private Texture2D houseTexture;
        public Texture2D Texture
        {
            get { return houseTexture; }
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

        public House(Game1 game, Vector2 pos)
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
