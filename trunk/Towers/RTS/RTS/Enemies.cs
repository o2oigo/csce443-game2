using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RTS
{
    public class HPEnemy : Enemy
    {
        public override void Initialize(Game1 game)
        {
            base.Initialize(game);

            range = 200;
            hp = 300;
            weakAgainst = ElementType.Fire;
            strongAgainst = ElementType.Lightning;
            moveSpeed = 50.0f;
        }


        public override void LoadContent()
        {
            // animation
            Texture2D tFront = contentManager.Load<Texture2D>("snFront");
            Texture2D tBack = contentManager.Load<Texture2D>("snBack");
            Texture2D tRight = contentManager.Load<Texture2D>("snRight");
            Texture2D tRightDown = contentManager.Load<Texture2D>("snRightDown");
            Texture2D tRightUp = contentManager.Load<Texture2D>("snRightUp");
            //Texture2D tLeft = contentManager.Load<Texture2D>("stLeft");
            Dictionary<String, SpriteSheet> txtMap = new Dictionary<string, SpriteSheet>();
            txtMap["front"] = new SpriteSheet(tFront, 1);
            txtMap["back"] = new SpriteSheet(tBack, 1);
            txtMap["right"] = new SpriteSheet(tRight, 1);
            txtMap["rightdown"] = new SpriteSheet(tRightDown, 1);
            txtMap["rightup"] = new SpriteSheet(tRightUp, 1);
           //txtMap["left"] = new SpriteSheet(tLeft, 1);

            animation = new SpriteAnimation(txtMap, true);
            animation.CurrentSprite = "front";
            //

            base.LoadContent();
        }
    }

    public class FastEnemy : Enemy
    {
        public override void Initialize(Game1 game)
        {
            base.Initialize(game);

            range = 200;
            hp = 100;
            weakAgainst = ElementType.Lightning;
            strongAgainst = ElementType.Fire;
            moveSpeed = 100.0f;
        }


        public override void LoadContent()
        {
            // animation
            Texture2D tFront = contentManager.Load<Texture2D>("stFront");
            Texture2D tBack = contentManager.Load<Texture2D>("stBack");
            Texture2D tRight = contentManager.Load<Texture2D>("stRight");
            //Texture2D tLeft = contentManager.Load<Texture2D>("stLeft");
            Texture2D tRightDown = contentManager.Load<Texture2D>("stRightDown");
            Texture2D tRightUp = contentManager.Load<Texture2D>("stRightUp");
            Dictionary<String, SpriteSheet> txtMap = new Dictionary<string, SpriteSheet>();
            txtMap["front"] = new SpriteSheet(tFront, 1);
            txtMap["back"] = new SpriteSheet(tBack, 1);
            txtMap["right"] = new SpriteSheet(tRight, 1);
            //txtMap["left"] = new SpriteSheet(tLeft, 1);
            txtMap["rightdown"] = new SpriteSheet(tRightDown, 1);
            txtMap["rightup"] = new SpriteSheet(tRightUp, 1);

            animation = new SpriteAnimation(txtMap, true);
            animation.CurrentSprite = "front";
            //

            base.LoadContent();
        }
    }

    public class NormalEnemy : Enemy
    {
        public override void Initialize(Game1 game)
        {
            base.Initialize(game);

            range = 200;
            hp = 100;
            weakAgainst = ElementType.Lightning;
            strongAgainst = ElementType.Fire;
            moveSpeed = 50.0f;
        }

        public override void LoadContent()
        {
            // animation
            Texture2D tFront = contentManager.Load<Texture2D>("fFront");
            Texture2D tBack = contentManager.Load<Texture2D>("fBack");
            Texture2D tRight = contentManager.Load<Texture2D>("fRight");
            Texture2D tRightDown = contentManager.Load<Texture2D>("fRightDown");
            Texture2D tRightUp = contentManager.Load<Texture2D>("fRightUp");
            Dictionary<String, SpriteSheet> txtMap = new Dictionary<string, SpriteSheet>();
            txtMap["front"] = new SpriteSheet(tFront, 1);
            txtMap["back"] = new SpriteSheet(tBack, 1);
            txtMap["right"] = new SpriteSheet(tRight, 1);
            txtMap["rightdown"] = new SpriteSheet(tRightDown, 1);
            txtMap["rightup"] = new SpriteSheet(tRightUp, 1);

            animation = new SpriteAnimation(txtMap, true);
            animation.CurrentSprite = "front";
            //

            base.LoadContent();
        }
    }

    public class AttackingEnemy : Enemy
    {
        public override void Initialize(Game1 game)
        {
            base.Initialize(game);

            range = 400;
            hp = 100;
            weakAgainst = ElementType.Lightning;
            strongAgainst = ElementType.Fire;
            moveSpeed = 50.0f;
        }


        public override void LoadContent()
        {
            // animation
            Texture2D tFront = contentManager.Load<Texture2D>("elfFront");
            Texture2D tBack = contentManager.Load<Texture2D>("elfBack");
            Texture2D tRight = contentManager.Load<Texture2D>("elfRight");
            Texture2D tLeft = contentManager.Load<Texture2D>("elfLeft");
            Dictionary<String, SpriteSheet> txtMap = new Dictionary<string, SpriteSheet>();
            txtMap["front"] = new SpriteSheet(tFront, 16);
            txtMap["back"] = new SpriteSheet(tBack, 16);
            txtMap["right"] = new SpriteSheet(tRight, 16);
            txtMap["left"] = new SpriteSheet(tLeft, 16);

            animation = new SpriteAnimation(txtMap, true);
            animation.CurrentSprite = "front";
            //
            base.LoadContent();
        }

        public override void Update(GameTime gameTime, List<Tower> towers)
        {
            base.Update(gameTime, towers);
            Attack(towers, gameTime);
        }
    }



}
