using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RTS
{    
    /// <summary>
    /// SNOWMAN
    /// Moves slower, but has higher HP.  
    /// Attacks but lower range than Attacking enemy
    /// WEAKNESS: FIRE
    /// STRENGTH: LIGHTNING
    /// </summary>

    public class HPEnemy : Enemy
    {
        public override void Initialize(Game1 game, float health)
        {
            base.Initialize(game, health);

            attackDamage = 3;
            range = 200;
            weakAgainst = ElementType.Fire;
            strongAgainst = ElementType.Lightning;
            maxSpeed = moveSpeed  = 0.3f;
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
}
