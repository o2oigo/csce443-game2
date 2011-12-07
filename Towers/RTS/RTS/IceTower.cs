﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace RTS
{
    class IceTower : Tower
    {
        Texture2D iceTexture;
        Texture2D iceTowerTexture;
        Texture2D iceTowerUpgrade2Texture;
        Texture2D iceTowerUpgrade3Texture;
        SoundEffectInstance instance;
        SoundEffect iceSound;

        float particleTimer = .35f;
        float particleElapsedTime = 5f;

        public IceTower(Game1 game, PlayerIndex playerIndex, Vector2 startPosition, int level, bool isFire) 
            : base(game, playerIndex, startPosition)
        {
            

            iceTexture = game.Content.Load<Texture2D>("iceParticle");
            towerName = "Ice Tower";
            this.ilevel = level;
            damage = new Damage(0, this.ilevel, ElementType.Ice, null);

            if (level == 2)
                setToLvlTwo();
            if (level == 3)
                setToLvlThree();

            this.shootTimer = .05f;
            this.soundTimer = 1f;
            this.towerRange = 175f;

            iceSound.Play();
            
        }

        public override void  LoadContent()
        {
            iceSound = contentManager.Load<SoundEffect>("Sound/ice cracking");
            shootSound = contentManager.Load<SoundEffect>("Sound/flametower-loop");
           // startSound = contentManager.Load <SoundEffect>("Sound/flametower-start");        
            instance = iceSound.CreateInstance();
            //startInstance = startSound.CreateInstance();
            instance.IsLooped = true;
           // startInstance.IsLooped = false;

            iceTowerTexture = contentManager.Load<Texture2D>("iceLevel1");
            iceTowerUpgrade2Texture = contentManager.Load<Texture2D>("iceLevel2");
            iceTowerUpgrade3Texture = contentManager.Load<Texture2D>("iceLevel3");
            turretTexture = contentManager.Load<Texture2D>("TowerTurret");
            font = contentManager.Load<SpriteFont>("font");
            origin.X = iceTowerTexture.Width / 2;
            origin.Y = iceTowerTexture.Height / 2;
        }

        public override void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            spriteBatch.Draw(iceTowerTexture, position, null, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0f); 
            //spriteBatch.DrawString(font, getTowerLvl() + ", \nHP: " + hp, new Vector2(position.X - 120, position.Y + 30), Color.Black);

            foreach (Projectile proj in projectileList)
            {
                proj.Draw(spriteBatch);
            }
        }

        public override void setToLvlTwo()
        {
            iceTowerTexture = iceTowerUpgrade2Texture;
            maxHP = hp = 150;
            level = "level 2";
            ilevel = 2;
        }

        public override void setToLvlThree()
        {
            iceTowerTexture = iceTowerUpgrade3Texture;
            maxHP = hp = 200;
            level = "level 3";
            ilevel = 3;
        }
 
        public override void createProjectile(Enemy target)
        {
           /* Projectile projectile = new Projectile();
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
            }*/
            game.ice.PickRandomDirection();
            game.ice.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));
            //game.flameTowerSmoke.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));

            
           // playShootSound();

        }

        public override void Update(GameTime gameTime, List<Enemy> enemies)
        {
            //Elapsed Time Calculations
            elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //shootElapsedTime += elapsedTime;
            //hp = shotsToDestroy - shotsTaken;
            updateTurret(enemies);
            //updateProjectiles(gameTime);
            
           // playShootSound(elapsedTime);

            updateParticleSystem(elapsedTime);
            playShootSound(elapsedTime);
        }

        public void playShootSound(float elapsedTime)
        {
            if (isShooting == true)
            {
                soundElapsedTime += elapsedTime;
                instance.Play();
            }
            else if (isShooting == false)
            {
                soundElapsedTime = 0;
                instance.Stop();
            }
            else
                soundElapsedTime += elapsedTime;
        }

        public void updateParticleSystem(float elapsedTime)
        {
            particleElapsedTime += elapsedTime;
            if (particleElapsedTime >= particleTimer)
            {
                game.ice.PickRandomDirection();
                game.ice.AddParticles(position);
                particleElapsedTime = 0f;
            }
        }

        public SoundEffectInstance getSound()
        {
            return this.instance;
        }

        public override Texture2D getTexture()
        {
            return iceTowerTexture;
        }
    }
}
