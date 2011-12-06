﻿using System;
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

        private Texture2D arrow21Texture;
        private Texture2D arrow22Texture;
        private Texture2D arrow23Texture;
        private Texture2D arrow24Texture;
        private Texture2D arrow25Texture;
        private Texture2D arrow26Texture;
        private Texture2D arrow27Texture;
        private Texture2D arrow28Texture;
        private Texture2D arrow29Texture;
        private Texture2D arrow210Texture;
        private Texture2D arrow211Texture;
        private Texture2D arrow212Texture;

        private Texture2D arrow31Texture;
        private Texture2D arrow32Texture;
        private Texture2D arrow33Texture;
        private Texture2D arrow34Texture;
        private Texture2D arrow35Texture;
        private Texture2D arrow36Texture;
        private Texture2D arrow37Texture;
        private Texture2D arrow38Texture;
        private Texture2D arrow39Texture;
        private Texture2D arrow310Texture;
        private Texture2D arrow311Texture;
        private Texture2D arrow312Texture;

        public ArrowTower(Game1 game, PlayerIndex playerIndex, Vector2 startPosition) 
            : base(game, playerIndex, startPosition)
        {
            towerName = "Arrow Tower";
            damage = new Damage(10, this.ilevel, ElementType.Normal, null);      
        }

        public override void  LoadContent()
        {
            shootSound = contentManager.Load<SoundEffect>("Sound/cannon");

            //Level 1
            arrow1Texture = contentManager.Load<Texture2D>("arrow7");
            arrow2Texture = contentManager.Load<Texture2D>("arrow8");
            arrow3Texture = contentManager.Load<Texture2D>("arrow9");
            arrow4Texture = contentManager.Load<Texture2D>("arrow10");
            arrow5Texture = contentManager.Load<Texture2D>("arrow11");
            arrow6Texture = contentManager.Load<Texture2D>("arrow12");
            arrow7Texture = contentManager.Load<Texture2D>("arrow1");
            arrow8Texture = contentManager.Load<Texture2D>("arrow2");
            arrow9Texture = contentManager.Load<Texture2D>("arrow3");
            arrow10Texture = contentManager.Load<Texture2D>("arrow4");
            arrow11Texture = contentManager.Load<Texture2D>("arrow5");
            arrow12Texture = contentManager.Load<Texture2D>("arrow6");

            //Level 3
            arrow21Texture = contentManager.Load<Texture2D>("arrowLevelTwo7");
            arrow22Texture = contentManager.Load<Texture2D>("arrowLevelTwo8");
            arrow23Texture = contentManager.Load<Texture2D>("arrowLevelTwo9");
            arrow24Texture = contentManager.Load<Texture2D>("arrowLevelTwo10");
            arrow25Texture = contentManager.Load<Texture2D>("arrowLevelTwo11");
            arrow26Texture = contentManager.Load<Texture2D>("arrowLevelTwo12");
            arrow27Texture = contentManager.Load<Texture2D>("arrowLevelTwo1");
            arrow28Texture = contentManager.Load<Texture2D>("arrowLevelTwo2");
            arrow29Texture = contentManager.Load<Texture2D>("arrowLevelTwo3");
            arrow210Texture = contentManager.Load<Texture2D>("arrowLevelTwo4");
            arrow211Texture = contentManager.Load<Texture2D>("arrowLevelTwo5");
            arrow212Texture = contentManager.Load<Texture2D>("arrowLevelTwo6");

            //Level 3
            arrow31Texture = contentManager.Load<Texture2D>("arrowLevelThree1");
            arrow32Texture = contentManager.Load<Texture2D>("arrowLevelThree2");
            arrow33Texture = contentManager.Load<Texture2D>("arrowLevelThree3");
            arrow34Texture = contentManager.Load<Texture2D>("arrowLevelThree4");
            arrow35Texture = contentManager.Load<Texture2D>("arrowLevelThree5");
            arrow36Texture = contentManager.Load<Texture2D>("arrowLevelThree6");
            arrow37Texture = contentManager.Load<Texture2D>("arrowLevelThree7");
            arrow38Texture = contentManager.Load<Texture2D>("arrowLevelThree8");
            arrow39Texture = contentManager.Load<Texture2D>("arrowLevelThree9");
            arrow310Texture = contentManager.Load<Texture2D>("arrowLevelThree10");
            arrow311Texture = contentManager.Load<Texture2D>("arrowLevelThree11");
            arrow312Texture = contentManager.Load<Texture2D>("arrowLevelThree12");

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
            //spriteBatch.DrawString(font, getTowerLvl() + ", \nHP: " + hp, new Vector2(position.X - 120, position.Y + 30), Color.Black);
            
            foreach (Projectile proj in projectileList)
            {
                proj.Draw(spriteBatch);
            }
        }

        public override void setToLvlTwo()
        {
            arrow1Texture = arrow21Texture;
            arrow2Texture = arrow22Texture;
            arrow3Texture = arrow23Texture;
            arrow4Texture = arrow24Texture;
            arrow5Texture = arrow25Texture;
            arrow6Texture = arrow26Texture;
            arrow7Texture = arrow27Texture;
            arrow8Texture = arrow28Texture;
            arrow9Texture = arrow29Texture;
            arrow10Texture = arrow210Texture;
            arrow11Texture = arrow211Texture;
            arrow12Texture = arrow212Texture;

            maxHP = hp = 150;
            damage.amount = 20;
            level = "level 2";
            ilevel = 2;
        }

        public override void setToLvlThree()
        {
            arrow1Texture = arrow31Texture;
            arrow2Texture = arrow32Texture;
            arrow3Texture = arrow33Texture;
            arrow4Texture = arrow34Texture;
            arrow5Texture = arrow35Texture;
            arrow6Texture = arrow36Texture;
            arrow7Texture = arrow37Texture;
            arrow8Texture = arrow38Texture;
            arrow9Texture = arrow39Texture;
            arrow10Texture = arrow310Texture;
            arrow11Texture = arrow311Texture;
            arrow12Texture = arrow312Texture;

            maxHP = hp = 200;
            damage.amount = 30;
            level = "level 3";
            ilevel = 3;
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