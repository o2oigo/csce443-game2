using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RTS
{

    public class Projectile
    {
        ContentManager contentManager;
        GraphicsDevice graphicsDevice;

        protected SpriteBatch spriteBatch;

        protected Vector2 origin;
        protected Vector2 projectilePosition;
        protected Vector2 originalPosition;

        protected Map map;

        float speed;
        protected double shootRotationAngle = 0;

        protected Texture2D texture;

        public void Initialize(ContentManager CM, GraphicsDevice GD, Vector2 pos, float angle, float turretLength, float flightSpeed, Map map)
        {
            this.map = map;
            this.shootRotationAngle = angle;
            this.speed = flightSpeed;
            projectilePosition = new Vector2(pos.X + (float)Math.Cos(shootRotationAngle) * turretLength * map.ScaleB, pos.Y + (float)Math.Sin(shootRotationAngle) * turretLength * map.ScaleB);
            this.originalPosition = projectilePosition;
            this.contentManager = CM;
            this.graphicsDevice = GD;
            //spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void LoadContent(String textureName)
        {
            texture = contentManager.Load<Texture2D>(textureName);
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
        }
        
        public virtual void Update()
        {   
            projectilePosition.X += (float)(Math.Cos(shootRotationAngle) * speed);
            projectilePosition.Y += (float)(Math.Sin(shootRotationAngle) * speed);
        }

        public virtual void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            //spriteBatch.Begin();
            spriteBatch.Draw(texture, projectilePosition, null, Color.White, (float)shootRotationAngle, origin, map.ScaleB, SpriteEffects.None, 0f);
           // spriteBatch.End();
        }

        public Vector2 getPosition()
        {
            return projectilePosition;
        }

        public Texture2D getTexture()
        {
            return texture;
        }
    }
}
