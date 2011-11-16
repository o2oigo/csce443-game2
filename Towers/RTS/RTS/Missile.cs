using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS
{
    public class Missile : Projectile
    {
        float time = 0;
        float missileAngle = 0;
        Enemy target;

        public Missile(Enemy target)
        {
            this.target = target;
        }

        public override void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (target != null)
            {
                Vector2 point = GetPoint(time, originalPosition, new Vector2(originalPosition.X, originalPosition.Y - 600), target.Position, target.Position);
                missileAngle = (float)Math.Atan2(point.Y - projectilePosition.Y, point.X - projectilePosition.X);
                missileAngle = missileAngle % (MathHelper.Pi * 2);
                projectilePosition = point;
            }
            time += elapsedTime * 1.2f;
        }

        private Vector2 GetPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            float cx = 3 * (p1.X - p0.X);
            float cy = 3 * (p1.Y - p0.Y);

            float bx = 3 * (p2.X - p1.X) - cx;
            float by = 3 * (p2.Y - p1.Y) - cy;

            float ax = p3.X - p0.X - cx - bx;
            float ay = p3.Y - p0.Y - cy - by;

            float Cube = t * t * t;
            float Square = t * t;

            float resX = (ax * Cube) + (bx * Square) + (cx * t) + p0.X;
            float resY = (ay * Cube) + (by * Square) + (cy * t) + p0.Y;

            return new Vector2(resX, resY);
        }

        public override void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            spriteBatch.Draw(texture, projectilePosition, null, Color.White, missileAngle, origin, 1f, SpriteEffects.None, 0f);
        }

        public Enemy getTarget()
        {
            return target;
        }

        public float getMissileAngle()
        {
            return missileAngle;
        }
    }
}
