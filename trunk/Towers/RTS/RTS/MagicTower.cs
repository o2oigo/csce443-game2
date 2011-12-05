using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS
{
    class MagicTower : Tower
    {
        private Texture2D magic1Texture;

        public MagicTower(Game1 game, PlayerIndex playerIndex, Vector2 startPosition) 
            : base(game, playerIndex, startPosition)
        {
            towerName = "Magic Tower";
            damage = new Damage(0, this.ilevel, ElementType.Normal, null);
            this.towerRange = 0f;
        }

        public override void  LoadContent()
        {
            magic1Texture = contentManager.Load<Texture2D>("MagicTower");
            //arrowTowerUpgradeTexture = contentManager.Load<Texture2D>("arrow1");
            turretTexture = contentManager.Load<Texture2D>("TowerTurret");
            font = contentManager.Load<SpriteFont>("font");
            origin.X = magic1Texture.Width / 2;
            origin.Y = magic1Texture.Height / 2;
        }

        public override void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            spriteBatch.Draw(magic1Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);

            spriteBatch.DrawString(font, getTowerLvl() + ", \nHP: " + hp, new Vector2(position.X - 120, position.Y + 30), Color.Black);
            
            foreach (Projectile proj in projectileList)
            {
                proj.Draw(spriteBatch);
            }
        }

        public override void setToLvlTwo()
        {
            //arrowTowerTexture = arrowTowerUpgradeTexture;
            //shotsToDestroy = 150;
            //hp += 50;
            hp = 150;
            damage.amount = 20;
            //damage.type = ElementType.Normal;
            level = "level 2";
            ilevel = 2;
        }
 
        public override void createProjectile(Enemy target)
        {
            // This tower should not attack
            // It may have some kind of ability that player need to activated it manually and will use some resources.


            /*
            Projectile projectile = new Projectile();
            projectile.Initialize(contentManager, graphicsDevice, position, (float)shootRotationAngle, getTurretLength(), 2000f, map);
            projectile.LoadContent("Projectile");
            projectileList.Add(projectile);     
             */
        }

        public override Texture2D getTexture()
        {
            return magic1Texture;
        }
    }
}
