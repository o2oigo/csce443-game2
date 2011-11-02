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
        public LightningTower(Game1 game, PlayerIndex playerIndex, Vector2 startPosition) 
            : base(game, playerIndex, startPosition)
        {
            lightningTexture = game.Content.Load<Texture2D>("LightningParticle");
            towerName = "Lightning Tower";
        }
 
        public override void createProjectile()
        {
            Projectile projectile = new Projectile();
            projectile.Initialize(contentManager, graphicsDevice, shootAt.Position, (float)shootRotationAngle, getTurretLength(), 10f, map);
                projectile.LoadContent("LightningProjectile");
            projectileList.Add(projectile);
            
            //game.explosion.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength() * map.ScaleB, position.Y - 25 + (float)Math.Sin(shootRotationAngle) * getTurretLength() * map.ScaleB));

            float scale = Vector2.Distance(shootAt.Position, position) / lightningTexture.Width;
            game.lightning.setScale(scale);
            game.lightning.setRotation((float)shootRotationAngle);   
            game.lightning.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * lightningTexture.Width / 2 * scale + (float)Math.Cos(shootRotationAngle) * (getTurretLength()) * map.ScaleB, position.Y - 15 + (float)Math.Sin(shootRotationAngle) * lightningTexture.Width / 2 * scale + (float)Math.Sin(shootRotationAngle) * (getTurretLength()) * map.ScaleB));
        }
    }
}
