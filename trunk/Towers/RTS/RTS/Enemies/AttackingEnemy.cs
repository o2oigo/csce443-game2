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
    /// Petulent Pudding (Sr)
    /// Has ability to attack, higher attack range, higher attack damage
    /// WEAKNESS: LIGHTNING
    /// STRENGTH: FIRE
    /// </summary>

    public class AttackingEnemy : Enemy
    {
        public override void Initialize(Game1 game, float health)
        {
            base.Initialize(game, health);

            attackDamage = 10;
            range = 400;
            weakAgainst = ElementType.Lightning;
            strongAgainst = ElementType.Fire;
            //moveSpeed = 0.5f;
        }


        public override void LoadContent()
        {
            Texture2D tFront = contentManager.Load<Texture2D>("enemypuddingFrontB");
            Texture2D tBack = contentManager.Load<Texture2D>("enemypuddingBackB");
            Texture2D tRight = contentManager.Load<Texture2D>("enemypuddingRightB");
            Texture2D tRightDown = contentManager.Load<Texture2D>("enemypudding45B");
            Texture2D tRightUp = contentManager.Load<Texture2D>("enemypudding135B");
            Dictionary<String, SpriteSheet> txtMap = new Dictionary<string, SpriteSheet>();
            txtMap["front"] = new SpriteSheet(tFront, 15);
            txtMap["back"] = new SpriteSheet(tBack, 15);
            txtMap["right"] = new SpriteSheet(tRight, 15);
            txtMap["rightUp"] = new SpriteSheet(tRightUp, 15);
            txtMap["rightDown"] = new SpriteSheet(tRightDown, 15);

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
