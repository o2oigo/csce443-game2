using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RTS
{
    //CANDYCANE 
    //Regenerates an amount of HP overtime
    public class RegenEnemy : Enemy
    {
        int regenTimes = 2;

        public override void Initialize(Game1 game, float health)
        {
            base.Initialize(game, health);

            range = 200;
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
            txtMap["front"] = new SpriteSheet(tFront, 1);
            txtMap["back"] = new SpriteSheet(tBack, 1);
            txtMap["right"] = new SpriteSheet(tRight, 1);
            txtMap["rightUp"] = new SpriteSheet(tRightUp, 1);
            txtMap["rightDown"] = new SpriteSheet(tRightDown, 1);

            animation = new SpriteAnimation(txtMap, true);
            animation.CurrentSprite = "front";
            //
            base.LoadContent();
        }

        public override void Update(GameTime gameTime, List<Tower> towers)
        {
            base.Update(gameTime, towers);

            if (hp < 0.2 * maxHP && regenTimes > 0) regen();

        }

        public void regen()
        {
            float tmp = (hp + 0.3f * (maxHP));
            if (tmp < maxHP) hp = tmp;
            else hp = maxHP;
            regenTimes--;
        }
    }
}
