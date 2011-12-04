using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace RTS
{
    class ArrowTower : Tower
    {
        private Texture2D arrow1Texture;
        private Texture2D arrow2Texture;
        private Texture2D arrow3Texture;
        private Texture2D arrow4Texture;
        private Texture2D arrow5Texture;
        private Texture2D arrow6Texture;
        private Texture2D arrow7Texture;
        private Texture2D arrow8Texture;
        private Texture2D arrow9Texture;
        private Texture2D arrow10Texture;
        private Texture2D arrow11Texture;
        private Texture2D arrow12Texture;

        public ArrowTower(Game1 game, PlayerIndex playerIndex, Vector2 startPosition) 
            : base(game, playerIndex, startPosition)
        {
            towerName = "Arrow Tower";
            damage = new Damage(10, this.ilevel, ElementType.Normal, null);      
        }

        public override void  LoadContent()
        {
            shootSound = contentManager.Load<SoundEffect>("Sound/cannon");

            arrow1Texture = contentManager.Load<Texture2D>("arrow1");
            arrow2Texture = contentManager.Load<Texture2D>("arrow2");
            arrow3Texture = contentManager.Load<Texture2D>("arrow3");
            arrow4Texture = contentManager.Load<Texture2D>("arrow4");
            arrow5Texture = contentManager.Load<Texture2D>("arrow5");
            arrow6Texture = contentManager.Load<Texture2D>("arrow6");
            arrow7Texture = contentManager.Load<Texture2D>("arrow7");
            arrow8Texture = contentManager.Load<Texture2D>("arrow8");
            arrow9Texture = contentManager.Load<Texture2D>("arrow9");
            arrow10Texture = contentManager.Load<Texture2D>("arrow10");
            arrow11Texture = contentManager.Load<Texture2D>("arrow11");
            arrow12Texture = contentManager.Load<Texture2D>("arrow12");
            //arrowTowerUpgradeTexture = contentManager.Load<Texture2D>("arrow1");
            turretTexture = contentManager.Load<Texture2D>("TowerTurret");
            font = contentManager.Load<SpriteFont>("font");
            origin.X = arrow1Texture.Width / 2;
            origin.Y = arrow1Texture.Height / 2;
        }

        public override void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            if (shootRotationAngle <= -3.5*Math.PI / 6 && shootRotationAngle > -4.5 * Math.PI / 6)
                spriteBatch.Draw(arrow5Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= -4.5 * Math.PI / 6 && shootRotationAngle > -5.5 * Math.PI / 6)
                spriteBatch.Draw(arrow4Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= -5.5 * Math.PI / 6 || shootRotationAngle > 5.5* Math.PI / 6)
                spriteBatch.Draw(arrow3Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= 5.5*Math.PI / 6 && shootRotationAngle > 4.5 * Math.PI / 6)
                spriteBatch.Draw(arrow2Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= 4.5 * Math.PI / 6 && shootRotationAngle > 3.5 * Math.PI / 6)
                spriteBatch.Draw(arrow1Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= 3.5 * Math.PI / 6 && shootRotationAngle > 2.5 * Math.PI / 6)
                spriteBatch.Draw(arrow12Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= 2.5 * Math.PI / 6 && shootRotationAngle > 1.5 * Math.PI / 6)
                spriteBatch.Draw(arrow11Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= 1.5 * Math.PI / 6 && shootRotationAngle > .5 * Math.PI / 6)
                spriteBatch.Draw(arrow10Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= .5 * Math.PI / 6 && shootRotationAngle > -.5)
                spriteBatch.Draw(arrow9Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= -.5 && shootRotationAngle > -1.5 * Math.PI / 6)
                spriteBatch.Draw(arrow8Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= -1.5 * Math.PI / 6 && shootRotationAngle > -2.5 * Math.PI / 6)
                spriteBatch.Draw(arrow7Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= -2.5 * Math.PI / 6 && shootRotationAngle > -3.5 * Math.PI / 6)
                spriteBatch.Draw(arrow6Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(turretTexture, new Vector2(position.X, position.Y - 25), null, Color.White, (float)shootRotationAngle, new Vector2(0, turretTexture.Height / 2), map.ScaleB, SpriteEffects.None, 0f);
           // spriteBatch.DrawString(font, towerName, new Vector2(position.X - 50, position.Y - 70), Color.Black);
            //spriteBatch.DrawString(font, level, new Vector2(position.X - 40, position.Y - 50), Color.Black);
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
            hp += 50;
            damage.amount = 20;
            //damage.type = ElementType.Normal;
            level = "level 2";
            ilevel = 2;
        }
 
        public override void createProjectile(Enemy target)
        {
            Projectile projectile = new Projectile();
            projectile.Initialize(contentManager, graphicsDevice, position, (float)shootRotationAngle, getTurretLength(), 2000f, map);
            projectile.LoadContent("Projectile");
            projectileList.Add(projectile);

            playShootSound();
        }

        public override Texture2D getTexture()
        {
            return arrow1Texture;
        }
    }
}
