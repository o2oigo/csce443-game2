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
    /// PHYSICALONLY [PUDDING - BROWN]
    /// Moves slower, but has higher HP.  
    /// Attacks but lower range than Attacking enemy
    /// WEAKNESS: NORMAL
    /// STRENGTH: NONE
    /// </summary>
    public class PhysicalOnlyEnemy : Enemy
    {
        public override void Initialize(Game1 game, float health)
        {
            base.Initialize(game, health);

            range = 200;
            weakAgainst = ElementType.Normal;
            strongAgainst = ElementType.None;
            //moveSpeed = 50.0f;
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

            base.LoadContent();
        }

        public override void Update(GameTime gameTime, List<Tower> towers)
        {
            base.Update(gameTime, towers);
        }

        public override void Hit(Damage dmg)
        {
            if (dmg.type == ElementType.Normal || dmg.type == ElementType.None)
            {
                base.Hit(dmg);
            }
        }
    }
}
