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
    /// PUDDING - NORMAL
    /// Only gets hit by Magic (Fire/Lightning/Ice)
    /// WEAKNESS: None
    /// STRENGTH: Normal
    /// </summary>
    public class MagicOnlyEnemy : Enemy
    {
        public override void Initialize(Game1 game, float health)
        {
            base.Initialize(game, health);

            range = 200;
            weakAgainst = ElementType.None;
            strongAgainst = ElementType.Normal;
            //moveSpeed = 50.0f;
        }


        public override void LoadContent()
        {
            Texture2D tFront = contentManager.Load<Texture2D>("enemymNewFrontB");
            Texture2D tBack = contentManager.Load<Texture2D>("enemymNewBackB");
            Texture2D tRight = contentManager.Load<Texture2D>("enemymNewRightB");
            Texture2D tRightDown = contentManager.Load<Texture2D>("enemymNew45B");
            Texture2D tRightUp = contentManager.Load<Texture2D>("enemymNew135B");
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
            if (dmg.type != ElementType.Normal && dmg.type != ElementType.None)
            {
                base.Hit(dmg);
            }
        }
    }
}
