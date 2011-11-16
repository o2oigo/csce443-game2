﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS
{
    class MissileTower : Tower
    {
        Texture2D missileTexture;
        Texture2D missileTowerTexture;
        //Texture2D missileTowerUpgradeTexture;
        public MissileTower(Game1 game, PlayerIndex playerIndex, Vector2 startPosition, int level, bool isFire) 
            : base(game, playerIndex, startPosition)
        {
            missileTexture = game.Content.Load<Texture2D>("fireParticle");
            towerName = "cannon12";
            this.ilevel = level;
            damage = new Damage(30, this.ilevel, ElementType.Normal, null);
            if (isFire)
            {
                damage.type = ElementType.Fire;
               // damage.effect = new EnemyEffectBurn(game, 5, 0.2f);
            }
            if (level == 2)
                setToLvlTwo();

            this.shootTimer = 5f;
            this.towerRange = 2000f;
            
        }

        public override void  LoadContent()
        {
            missileTowerTexture = contentManager.Load<Texture2D>("missileTowerNew");
            //missileTowerUpgradeTexture = contentManager.Load<Texture2D>("lightningTowerUpgrade");
            turretTexture = contentManager.Load<Texture2D>("TowerTurret");
            font = contentManager.Load<SpriteFont>("font");
            origin.X = missileTowerTexture.Width / 2;
            origin.Y = missileTowerTexture.Height / 2;
        }

        public override void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            spriteBatch.Draw(missileTowerTexture, position, null, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0f); 
            spriteBatch.DrawString(font, getTowerLvl() + ", \nHP: " + hp, new Vector2(position.X - 120, position.Y + 30), Color.Black);

            foreach (Projectile proj in projectileList)
            {
                proj.Draw(spriteBatch);
            }
        }

        public override void updateProjectiles(GameTime gameTime)
        {
            for (int i = 0; i < projectileList.Count; i++)
            {
                //Remove Projectile if it goes off-screen
                Missile proj = (Missile)projectileList[i];
                if (proj.getPosition().X > graphicsDevice.Viewport.Width || proj.getPosition().X < 0
                    || proj.getPosition().Y > graphicsDevice.Viewport.Height || proj.getPosition().Y < 0)
                {
                    projectileList.Remove(proj);
                }
                
                //Remove Projectile if target is missing or dead
                if (proj.getTarget() == null || proj.getTarget().isDead())
                {
                    game.explosion.AddParticles(proj.getPosition());
                    game.smoke.AddParticles(proj.getPosition());
                    projectileList.Remove(proj);
                }
                else
                {
                    proj.Update(gameTime);
                    game.fireTower.setScale(.2f, .25f);
                    game.fireTower.setDirection(proj.getMissileAngle() + (float)Math.PI);
                    game.fireTower.AddParticles(proj.getPosition());
                }
            }
        }

        public override void setToLvlTwo()
        {
           // missileTowerTexture = missileTowerUpgradeTexture;

            shotsToDestroy = 150;
            damage.amount = 50f;
            //damage.type = ElementType.Normal;
            level = "level 2";
            ilevel = 2;
        }
 
        public override void createProjectile(Enemy target)
        {
            Missile projectile = new Missile(target);
            projectile.Initialize(contentManager, graphicsDevice, position, (float)(-Math.PI/2), getTurretLength(), 2000f, map);
            projectile.LoadContent("missile");
            projectileList.Add(projectile);
            
           // game.fireTower.setDirection((float)shootRotationAngle);
            //game.fireTower.setSpeed(500, 600);
           // if(this.ilevel == 1)
           //     game.fireTower.setScale(.15f, .2f);
           // else
          //      game.fireTower.setScale(.3f, .4f);
          //  game.fireTower.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));
            
        }
        
        public override Texture2D getTexture()
        {
            return missileTowerTexture;
        }
    }
}