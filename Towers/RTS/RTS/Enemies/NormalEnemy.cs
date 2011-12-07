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
    /// STOCKING - BLUE?
    /// Just a normal enemy. Nothing interesting. 
    /// Attacks, more damage then HPEnemy but less than Attacking Enemy
    /// WEAKNESS: NONE
    /// STRENGTH: NONE
    /// </summary>
    public class NormalEnemy : Enemy
    {
        public override void Initialize(Game1 game, float health)
        {
            base.Initialize(game, health);
            attackDamage = 0;
            range = 200;
            weakAgainst = ElementType.None;
            strongAgainst = ElementType.None;
            //moveSpeed = 50.0f;
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

            base.LoadContent();
        }

        public override void Update(GameTime gameTime, List<Tower> towers)
        {
            base.Update(gameTime, towers);
            Attack(towers, gameTime);
        }
    }
}
