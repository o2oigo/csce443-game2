using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace RTS
{
    class LightningTower : Tower
    {
        Texture2D lightningTexture;
        Texture2D lightningTowerTexture;
        Texture2D lightningTowerUpgrade2Texture;
        Texture2D lightningTowerUpgrade3Texture;
        SoundEffect shootSound2;
        public LightningTower(Game1 game, PlayerIndex playerIndex, Vector2 startPosition, int level, bool isFire) 
            : base(game, playerIndex, startPosition)
        {
            lightningTexture = game.Content.Load<Texture2D>("LightningParticle");
            towerName = "Lightning Tower";
            this.ilevel = level;
            damage = new Damage(5, this.ilevel, ElementType.Lightning, null);
            //damage.type = ElementType.Lightning;
            damage.effect = new EnemyEffectStun(game, 5);
            //if (isFire)
            //{
            //    damage.type = ElementType.Lightning;
            //    damage.effect = new EnemyEffectStun(game, 7);
            //}
            if (level == 2)
                setToLvlTwo();
            if (level == 3)
                setToLvlThree();

            this.shootTimer = 2f;
        }

        public override void  LoadContent()
        {
            shootSound = contentManager.Load<SoundEffect>("Sound/electric-sparks1");
            shootSound2 = contentManager.Load<SoundEffect>("Sound/electric-sparks2");

            lightningTowerTexture = contentManager.Load<Texture2D>("lightningTower");
            lightningTowerUpgrade2Texture = contentManager.Load<Texture2D>("lightningTowerUpgrade");
            lightningTowerUpgrade3Texture = contentManager.Load<Texture2D>("lightningTowerUpgrade");
            turretTexture = contentManager.Load<Texture2D>("TowerTurret");
            font = contentManager.Load<SpriteFont>("font");
            origin.X = lightningTowerTexture.Width / 2;
            origin.Y = lightningTowerTexture.Height / 2;
        }

        public override void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            spriteBatch.Draw(lightningTowerTexture, position, null, Color.White, 0f, origin, 1.0f, SpriteEffects.None, 0f); 
            //spriteBatch.DrawString(font, getTowerLvl() + ", \nHP: " + hp, new Vector2(position.X - 120, position.Y + 30), Color.Black);

            foreach (Projectile proj in projectileList)
            {
                proj.Draw(spriteBatch);
            }
        }

        public override void setToLvlTwo()
        {
            lightningTowerTexture = lightningTowerUpgrade2Texture;
            maxHP = hp = 150;
            damage.amount = 10;
            //damage.type = ElementType.Normal;
            level = "level 2";
            ilevel = 2;
        }

        public override void setToLvlThree()
        {
            lightningTowerTexture = lightningTowerUpgrade3Texture;
            maxHP = hp = 200;
            damage.amount = 15;
            //damage.type = ElementType.Normal;
            level = "level 3";
            ilevel = 3;
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
            
            game.lightning.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * lightningTexture.Width / 2 * scale + (float)Math.Cos(shootRotationAngle) * (1) * map.ScaleB, position.Y - 15 + (float)Math.Sin(shootRotationAngle) * lightningTexture.Width / 2 * scale + (float)Math.Sin(shootRotationAngle) * (1) * map.ScaleB));
           // float xVal = position.X;
            //float yVal = position.Y;

           // game.lightning.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * lightningTexture.Width / 2 + (float)Math.Cos(shootRotationAngle) * (getTurretLength()) * map.ScaleB, position.Y - 15 + (float)Math.Sin(shootRotationAngle) * lightningTexture.Width / 2 + (float)Math.Sin(shootRotationAngle) * (getTurretLength()) * map.ScaleB));

            playShootSound();

        }

        public override void playShootSound()
        {
            Random rand = new Random();
            int next = rand.Next(1, 3);
            if (next == 1)
                shootSound.Play();
            else
                shootSound2.Play();
        }

        public override Texture2D getTexture()
        {
            return lightningTowerTexture;
        }
    }
}
