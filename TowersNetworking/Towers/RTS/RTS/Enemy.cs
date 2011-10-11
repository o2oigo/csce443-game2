using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RTS
{
    class Enemy
    {
        Game1 game;

        SpriteFont font;

        ContentManager contentManager;
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;

        private Texture2D texture;
        private Texture2D turretTexture;

        private Vector2 position;
        private Vector2 origin;

        private List<Projectile> projectileList = new List<Projectile>(5);

        Random rand = new Random();

        private int shotsTaken = 0;
        private int shotsToDestroy = 2;
        private bool dead = false;

        private double speed = 1.5;

        private double moveRotationAngle;
        private double shootRotationAngle;
        private double playerRotationAngle;
        private double projectileRotationAngle;

        private float elapsedTime;
        private float shootElapsedTime;
        private float shootTimer = 1.6f;
        private float circle = MathHelper.Pi * 2;

        public float getTurretLength()
        {
            return turretTexture.Width;
        }

        public void Initialize(Game1 game, Vector2 startPosition)
        {
            this.game = game;
            contentManager = game.Content;
            graphicsDevice = game.GraphicsDevice;
            position = startPosition;
        }

        public void LoadContent(String textureName)
        {
            texture = contentManager.Load<Texture2D>(textureName);
            turretTexture = contentManager.Load<Texture2D>("Turret");
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
            font = contentManager.Load<SpriteFont>("font");
        }

        public void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(turretTexture, position, null, Color.White, (float)shootRotationAngle, new Vector2(0, turretTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);
            foreach (Projectile proj in projectileList)
            {
                proj.Draw();
            }

            spriteBatch.End();
        }

        public virtual void Update(GameTime gameTime, Player player)
        {
            

            //Elapsed Time Calculations
            elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            shootElapsedTime += elapsedTime;

            //Update movement and angles

           /* if (Vector2.Distance(this.position, players[0].getPosition()) < Vector2.Distance(this.position, players[1].getPosition()))
                updateMovement(players[0]);
            else
                updateMovement(players[1]);*/
            updateMovement(player);


            //Create and update projectiles (shoot)
            updateProjectiles();

        }

        public void updateMovement(Player player)
        {
            //Calculate Rotation Angles and Enemy Movement
            playerRotationAngle = (Math.Atan2(player.getPosition().Y - position.Y, player.getPosition().X - position.X) + 2 * circle) % circle;
            float difference = WrapAngle((float)playerRotationAngle - (float)moveRotationAngle);
            difference = MathHelper.Clamp(difference, -elapsedTime, elapsedTime);
            moveRotationAngle += difference;
            moveRotationAngle = moveRotationAngle % circle;

            //Update position based on speed and angle
            position.X += (float)(Math.Cos(moveRotationAngle) * speed);
            position.Y += (float)(Math.Sin(moveRotationAngle) * speed);

            //Shoot angle
            shootRotationAngle = Math.Atan2(player.getPosition().Y - position.Y, player.getPosition().X - position.X);

            //Adjusted shoot angle with variation for bullet realism
            int xVariation = rand.Next(-100, 100);
            int yVariation = rand.Next(-100, 100);
            projectileRotationAngle = Math.Atan2(player.getPosition().Y + yVariation - position.Y, player.getPosition().X + xVariation - position.X);
        }

        public void updateProjectiles()
        {
            //Shoot every few seconds / Adds projectiles to screen / Adds particle effects
            if (shootElapsedTime >= shootTimer)
            {
                shootElapsedTime = 0;

                //Create new projectiles
                Projectile projectile = new Projectile();
                projectile.Initialize(contentManager, graphicsDevice, position, (float)projectileRotationAngle, getTurretLength(), 6f);
                projectile.LoadContent("Projectile");
                projectileList.Add(projectile);

                //Add explosion to particle system
                game.explosion.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));
                game.smoke.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));
            }

            //Update Projectiles
            foreach (Projectile proj in projectileList)
            {
                proj.Update();
            }

            //Remove Projectile if it goes off-screen
            for (int i = 0; i < projectileList.Count; i++)
            {
                Projectile proj = projectileList[i];
                if (proj.getPosition().X > graphicsDevice.Viewport.Width || proj.getPosition().X < 0
                    || proj.getPosition().Y > graphicsDevice.Viewport.Height || proj.getPosition().Y < 0)
                {
                    projectileList.RemoveAt(i);
                }
            }      
        }


        private static float WrapAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public List<Projectile> getProjectiles()
        {
            return projectileList;
        }

        public void setPosition(Vector2 pos)
        {
            position = pos;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public float getProjectileCount()
        {
            return projectileList.Count;
        }

        public double getShootRotationAngle()
        {
            return shootRotationAngle;
        }

        public double getMoveRotationAngle()
        {
            return moveRotationAngle;
        }

        public double getPlayerRotationAngle()
        {
            return playerRotationAngle;
        }

        public void Hit()
        {
            shotsTaken++;
            if (shotsTaken >= shotsToDestroy)
                dead = true;
        }

        public bool isDead()
        {
            return dead;
        }
    }
}
