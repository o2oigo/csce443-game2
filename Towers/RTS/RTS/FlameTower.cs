﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace RTS
{
    class FlameTower : Tower
    {
        Texture2D flameTexture;
        Texture2D flameTowerTexture;
        Texture2D flameTowerUpgradeTexture;
        SoundEffect startSound;
        SoundEffectInstance instance;
        SoundEffectInstance startInstance;

        public FlameTower(Game1 game, PlayerIndex playerIndex, Vector2 startPosition, int level, bool isFire) 
            : base(game, playerIndex, startPosition)
        {
            flameTexture = game.Content.Load<Texture2D>("fireParticle");
            towerName = "Flame Tower";
            this.ilevel = level;
            damage = new Damage(1, this.ilevel, ElementType.Fire, null);
            damage.type = ElementType.Fire;

            if (level == 2)
                setToLvlTwo();

            this.shootTimer = .05f;
            this.towerRange = 300f;
            
        }

        public override void  LoadContent()
        {
            shootSound = contentManager.Load<SoundEffect>("Sound/flametower-loop");
            startSound = contentManager.Load<SoundEffect>("Sound/flametower-start");
            instance = shootSound.CreateInstance();
            startInstance = startSound.CreateInstance();
            instance.IsLooped = true;
            startInstance.IsLooped = false;

            flameTowerTexture = contentManager.Load<Texture2D>("flameTowerNew");
            flameTowerUpgradeTexture = contentManager.Load<Texture2D>("flameTowerNew");
            turretTexture = contentManager.Load<Texture2D>("TowerTurret");
            font = contentManager.Load<SpriteFont>("font");
            origin.X = flameTowerTexture.Width / 2;
            origin.Y = flameTowerTexture.Height / 2;
        }

        public override void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            spriteBatch.Draw(flameTowerTexture, position, null, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0f); 
            spriteBatch.DrawString(font, getTowerLvl() + ", \nHP: " + hp, new Vector2(position.X - 120, position.Y + 30), Color.Black);

            foreach (Projectile proj in projectileList)
            {
                proj.Draw(spriteBatch);
            }
        }

        public override void setToLvlTwo()
        {
            flameTowerTexture = flameTowerUpgradeTexture;

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
            
            game.fireTower.setDirection((float)shootRotationAngle);
            game.flameTowerSmoke.setDirection((float)shootRotationAngle);
            game.fireTower.setSpeed(500, 600);
            if (this.ilevel == 1)
            {
                game.fireTower.setScale(.2f, .25f);
                game.flameTowerSmoke.setScale(.15f, .2f);
            }
            else
            {
                game.fireTower.setScale(.3f, .4f);
                game.flameTowerSmoke.setScale(.2f, .27f);
            }
            game.fireTower.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));
            game.flameTowerSmoke.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));

            
        }

        public override void Update(GameTime gameTime, List<Enemy> enemies)
        {
            //Elapsed Time Calculations
            elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            shootElapsedTime += elapsedTime;
            hp = shotsToDestroy - shotsTaken;
            updateTurret(enemies);
            updateProjectiles(gameTime);

            playShootSound(elapsedTime);
        }

        public void playShootSound(float elapsedTime)
        {

            if (isShooting == true && soundElapsedTime == 0)
            {
                soundElapsedTime += elapsedTime;
                startInstance.Play();
            }
            else if (isShooting == true && soundElapsedTime >= .2f)
            {
                soundElapsedTime += elapsedTime;
                instance.Resume();
                startInstance.Stop();
            }
            else if (isShooting == false)
            {
                soundElapsedTime = 0;
                instance.Stop();
            }
            else
                soundElapsedTime += elapsedTime;
        }

        public override Texture2D getTexture()
        {
            return flameTowerTexture;
        }
    }
}
