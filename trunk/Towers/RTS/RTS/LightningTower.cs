using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS
{
    class LightningTower : Tower
    {
        Texture2D lightningTexture;
        Texture2D lightningTowerTexture;
        Texture2D lightningTowerUpgradeTexture;
        public LightningTower(Game1 game, PlayerIndex playerIndex, Vector2 startPosition, int level, bool isFire) 
            : base(game, playerIndex, startPosition)
        {
            lightningTexture = game.Content.Load<Texture2D>("LightningParticle");
            towerName = "Lightning Tower";
            this.ilevel = level;
            if (isFire)
            {
                damage.type = ElementType.Lightning;
                damage.effect = new EnemyEffectStun(game, 7);
            }
            if (level == 2)
                setToLvlTwo();
            
        }

        public override void  LoadContent()
        {
            lightningTowerTexture = contentManager.Load<Texture2D>("lightningTower");
            lightningTowerUpgradeTexture = contentManager.Load<Texture2D>("lightningTowerUpgrade");
            turretTexture = contentManager.Load<Texture2D>("TowerTurret");
            font = contentManager.Load<SpriteFont>("font");
            origin.X = lightningTowerTexture.Width / 2;
            origin.Y = lightningTowerTexture.Height / 2;
        }

        public override void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            spriteBatch.Draw(lightningTowerTexture, position, null, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0f); 
            spriteBatch.DrawString(font, getTowerLvl() + ", \nHP: " + hp, new Vector2(position.X - 120, position.Y + 30), Color.Black);

            foreach (Projectile proj in projectileList)
            {
                proj.Draw(spriteBatch);
            }
        }

        public override void setToLvlTwo()
        {
            lightningTowerTexture = lightningTowerUpgradeTexture;
            base.setToLvlTwo();
        }
 
        public override void createProjectile(Enemy target)
        {
            Projectile projectile = new Projectile();
            projectile.Initialize(contentManager, graphicsDevice, shootAt.Position, (float)shootRotationAngle, getTurretLength(), 10f, map);
                projectile.LoadContent("LightningProjectile");
            projectileList.Add(projectile);
            
            //game.explosion.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength() * map.ScaleB, position.Y - 25 + (float)Math.Sin(shootRotationAngle) * getTurretLength() * map.ScaleB));

            float scale = Vector2.Distance(shootAt.Position, position) / lightningTexture.Width;
            game.lightning.setScale(scale, scale);
            game.lightning.setRotation((float)shootRotationAngle);   
            game.lightning.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * lightningTexture.Width / 2 * scale + (float)Math.Cos(shootRotationAngle) * (getTurretLength()) * map.ScaleB, position.Y - 15 + (float)Math.Sin(shootRotationAngle) * lightningTexture.Width / 2 * scale + (float)Math.Sin(shootRotationAngle) * (getTurretLength()) * map.ScaleB));
        }

        public override Texture2D getTexture()
        {
            return lightningTowerTexture;
        }
    }
}
