using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS
{
    class IceTower : Tower
    {
        Texture2D iceTexture;
        Texture2D iceTowerTexture;
        Texture2D iceTowerUpgradeTexture;
        //List<AOEProjectile> AOEProjectileList(20); //NEED TO MAKE AOEPROJECTILES FOR ICE TOWER 
        public IceTower(Game1 game, PlayerIndex playerIndex, Vector2 startPosition, int level, bool isFire) 
            : base(game, playerIndex, startPosition)
        {
            iceTexture = game.Content.Load<Texture2D>("iceParticle");
            towerName = "Ice Tower";
            this.ilevel = level;
            damage = new Damage(1, this.ilevel, ElementType.Fire, null);

            if (level == 2)
                setToLvlTwo();

            this.shootTimer = .05f;
            this.towerRange = 275f;
            
        }

        public override void  LoadContent()
        {
            iceTowerTexture = contentManager.Load<Texture2D>("iceTowerNew");
            iceTowerUpgradeTexture = contentManager.Load<Texture2D>("iceTowerNew");
            turretTexture = contentManager.Load<Texture2D>("TowerTurret");
            font = contentManager.Load<SpriteFont>("font");
            origin.X = iceTowerTexture.Width / 2;
            origin.Y = iceTowerTexture.Height / 2;
        }

        public override void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            spriteBatch.Draw(iceTowerTexture, position, null, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0f); 
            spriteBatch.DrawString(font, getTowerLvl() + ", \nHP: " + hp, new Vector2(position.X - 120, position.Y + 30), Color.Black);

            foreach (Projectile proj in projectileList)
            {
                proj.Draw(spriteBatch);
            }
        }

        public override void setToLvlTwo()
        {
            iceTowerTexture = iceTowerUpgradeTexture;

            shotsToDestroy = 150;
            damage.amount = 3;
            //damage.type = ElementType.Normal;
            level = "level 2";
            ilevel = 2;
        }
 
        public override void createProjectile(Enemy target)
        {
            Projectile projectile = new Projectile();
            projectile.Initialize(contentManager, graphicsDevice, position, (float)shootRotationAngle, getTurretLength(), 2000f, map);
            projectile.LoadContent("ProjectileFireEmpty");
            projectileList.Add(projectile);
            
            game.ice.setDirection((float)shootRotationAngle);
            game.flameTowerSmoke.setDirection((float)shootRotationAngle);
            game.ice.setSpeed(500, 600);
            if (this.ilevel == 1)
            {
                game.ice.setScale(.2f, .25f);
                game.flameTowerSmoke.setScale(.15f, .2f);
            }
            else
            {
                game.ice.setScale(.3f, .4f);
                game.flameTowerSmoke.setScale(.2f, .27f);
            }
            game.ice.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));
            game.flameTowerSmoke.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));
        }

        public override Texture2D getTexture()
        {
            return iceTowerTexture;
        }
    }
}
