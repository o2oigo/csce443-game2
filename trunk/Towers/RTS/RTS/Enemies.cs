using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RTS
{
    //SNOWMAN (attacks)
    public class HPEnemy : Enemy
    {
        public override void Initialize(Game1 game, float health)
        {
            base.Initialize(game,health);

            range = 200;
            weakAgainst = ElementType.Fire;
            strongAgainst = ElementType.Lightning;
            moveSpeed = 50.0f;
        }


        public override void LoadContent()
        {
            Texture2D tFront = contentManager.Load<Texture2D>("enemysnowFront");
            Texture2D tBack = contentManager.Load<Texture2D>("enemysnowBack");
            Texture2D tRight = contentManager.Load<Texture2D>("enemysnowRight");
            Texture2D tRightDown = contentManager.Load<Texture2D>("enemysnow45");
            Texture2D tRightUp = contentManager.Load<Texture2D>("enemysnow135");
            Dictionary<String, SpriteSheet> txtMap = new Dictionary<string, SpriteSheet>();
            txtMap["front"] = new SpriteSheet(tFront, 1);
            txtMap["back"] = new SpriteSheet(tBack, 1);
            txtMap["right"] = new SpriteSheet(tRight, 1);
            txtMap["rightDown"] = new SpriteSheet(tRightDown, 1);
            txtMap["rightUp"] = new SpriteSheet(tRightUp, 1);
            animation = new SpriteAnimation(txtMap, true);
            animation.CurrentSprite = "front";

            base.LoadContent();
        }

        public override void Update(GameTime gameTime, List<Tower> towers)
        {
            base.Update(gameTime, towers);
            Attack(towers, gameTime);
        }
    }

    //RED 2
    public class FastEnemy : Enemy
    {
        public override void Initialize(Game1 game, float health)
        {
            base.Initialize(game,health);

            range = 200;
            //hp = 100;
            weakAgainst = ElementType.Lightning;
            strongAgainst = ElementType.Fire;
            moveSpeed = 100.0f;
        }


        public override void LoadContent()
        {
            Texture2D tFront = contentManager.Load<Texture2D>("enemystockFront");
            Texture2D tBack = contentManager.Load<Texture2D>("enemystockBack");
            Texture2D tRight = contentManager.Load<Texture2D>("enemystockRight");
            Texture2D tRightDown = contentManager.Load<Texture2D>("enemystock45");
            Texture2D tRightUp = contentManager.Load<Texture2D>("enemystock135");
            Dictionary<String, SpriteSheet> txtMap = new Dictionary<string, SpriteSheet>();
            txtMap["front"] = new SpriteSheet(tFront, 1);
            txtMap["back"] = new SpriteSheet(tBack, 1);
            txtMap["right"] = new SpriteSheet(tRight, 1);
            txtMap["rightDown"] = new SpriteSheet(tRightDown, 1);
            txtMap["rightUp"] = new SpriteSheet(tRightUp, 1);

            animation = new SpriteAnimation(txtMap, true);
            animation.CurrentSprite = "front";
            //

            base.LoadContent();
        }
    }

    //RED
    public class NormalEnemy : Enemy
    {
        public override void Initialize(Game1 game, float health)
        {
            base.Initialize(game,health);

            range = 200;
            //hp = 100;
            weakAgainst = ElementType.None;
            strongAgainst = ElementType.None;
            moveSpeed = 50.0f;
        }

        public override void LoadContent()
        {
            Texture2D tFront = contentManager.Load<Texture2D>("enemypuddingFront");
            Texture2D tBack = contentManager.Load<Texture2D>("enemypuddingBack");
            Texture2D tRight = contentManager.Load<Texture2D>("enemypuddingRight");
            Texture2D tRightDown = contentManager.Load<Texture2D>("enemypudding45");
            Texture2D tRightUp = contentManager.Load<Texture2D>("enemypudding135");
            Dictionary<String, SpriteSheet> txtMap = new Dictionary<string, SpriteSheet>();
            txtMap["front"] = new SpriteSheet(tFront, 15);
            txtMap["back"] = new SpriteSheet(tBack, 15);
            txtMap["right"] = new SpriteSheet(tRight, 15);
            txtMap["rightDown"] = new SpriteSheet(tRightDown, 15);
            txtMap["rightUp"] = new SpriteSheet(tRightUp, 15);

            animation = new SpriteAnimation(txtMap, true);
            animation.CurrentSprite = "front";
            //

            base.LoadContent();
        }
    }

    //
    public class AttackingEnemy : Enemy
    {
        public override void Initialize(Game1 game, float health)
        {
            base.Initialize(game,health);

            range = 400;
            //hp = 100;
            weakAgainst = ElementType.Fire;
            strongAgainst = ElementType.Lightning;
            moveSpeed = 50.0f;
        }


        public override void LoadContent()
        {
            Texture2D tFront = contentManager.Load<Texture2D>("enemyMuffinFront");
            Texture2D tBack = contentManager.Load<Texture2D>("enemyMuffinBack");
            Texture2D tRight = contentManager.Load<Texture2D>("enemyMuffinRight");
            Texture2D tRightDown = contentManager.Load<Texture2D>("enemyMuffin45");
            Texture2D tRightUp = contentManager.Load<Texture2D>("enemyMuffin135");
            Dictionary<String, SpriteSheet> txtMap = new Dictionary<string, SpriteSheet>();
            txtMap["front"] = new SpriteSheet(tFront, 21);
            txtMap["back"] = new SpriteSheet(tBack, 21);
            txtMap["right"] = new SpriteSheet(tRight, 21);
            txtMap["rightUp"] = new SpriteSheet(tRightUp, 21);
            txtMap["rightDown"] = new SpriteSheet(tRightDown, 21);

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

    //CANDYCANE
    public class RegenEnemy : Enemy
    {
        int regenTimes = 2;

        public override void Initialize(Game1 game, float health)
        {
            base.Initialize(game, health);

            range = 400;
            //hp = 100;
            weakAgainst = ElementType.Lightning;
            strongAgainst = ElementType.Fire;
            moveSpeed = 50.0f;
        }


        public override void LoadContent()
        {
            Texture2D tFront = contentManager.Load<Texture2D>("enemyflyFront");
            Texture2D tBack = contentManager.Load<Texture2D>("enemyflyBack");
            Texture2D tRight = contentManager.Load<Texture2D>("enemyflyRight");
            Texture2D tRightDown = contentManager.Load<Texture2D>("enemyfly45");
            Texture2D tRightUp = contentManager.Load<Texture2D>("enemyfly135");
            Dictionary<String, SpriteSheet> txtMap = new Dictionary<string, SpriteSheet>();
            txtMap["front"] = new SpriteSheet(tFront, 22);
            txtMap["back"] = new SpriteSheet(tBack, 22);
            txtMap["right"] = new SpriteSheet(tRight, 22);
            txtMap["rightUp"] = new SpriteSheet(tRightUp, 22);
            txtMap["rightDown"] = new SpriteSheet(tRightDown, 22);

            animation = new SpriteAnimation(txtMap, true);
            animation.CurrentSprite = "front";
            //
            base.LoadContent();
        }

        public override void Update(GameTime gameTime, List<Tower> towers)
        {
            base.Update(gameTime, towers);

            if (hp < 0.2 * maxHP && regenTimes>0) regen();

        }

        public void regen()
        {
            float tmp = (hp + 0.3f*(maxHP));
            if (tmp<maxHP) hp = tmp;
            else hp = maxHP;
            regenTimes--;
        }
    }

    //PUDDING
    public class MagicOnlyEnemy : Enemy
    {
    }




}
