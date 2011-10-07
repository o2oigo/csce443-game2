using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RTS
{
    class Projectile
    {
        ContentManager contentManager;
        GraphicsDevice graphicsDevice;

        SpriteBatch spriteBatch;

        Vector2 origin;
        Vector2 projectilePosition;

        float speed;
        double shootRotationAngle = 0;

        Texture2D texture;

        public void Initialize(ContentManager CM, GraphicsDevice GD, Vector2 pos, float angle, float turretLength, float flightSpeed)
        {
            this.shootRotationAngle = angle;
            this.speed = flightSpeed;
            projectilePosition = new Vector2(pos.X + (float)Math.Cos(shootRotationAngle) * turretLength, pos.Y + (float)Math.Sin(shootRotationAngle) * turretLength);            
            this.contentManager = CM;
            this.graphicsDevice = GD;
            spriteBatch = new SpriteBatch(graphicsDevice);
            
        }

        public void LoadContent(String textureName)
        {
            texture = contentManager.Load<Texture2D>(textureName);
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
        }
        
        public void Update()
        {   
            projectilePosition.X += (float)(Math.Cos(shootRotationAngle) * speed);
            projectilePosition.Y += (float)(Math.Sin(shootRotationAngle) * speed);
        }

        public void Draw()
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, projectilePosition, null, Color.White, (float)shootRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.End();
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
