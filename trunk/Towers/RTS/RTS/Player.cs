using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


namespace RTS
{
    class Player
    {
        Game1 game;

        ContentManager contentManager;
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;

        KeyboardState keystate;
        KeyboardState oldKeyState;
        MouseState mousestate;
        MouseState oldMousestate;
        GamePadState currentState;
        GamePadState oldState;

        private float elapsedTime;

        private Vector2 position;
        private Vector2 mousePos;
        private Vector2 origin;      
        
        private double speed = 0;
        private int timesHit = 0;

        private double moveRotationAngle;
        private double shootRotationAngle;    
        private float xComponent = 0;
        private float yComponent = 0;

        private Texture2D texture;
        private Texture2D turretTexture;
        private Texture2D mouseTexture;

        private List<Projectile> projectileList = new List<Projectile>(5);
  
        private float circle = MathHelper.Pi * 2;
 
        public void Initialize(Game1 game, Vector2 startPosition)
        {
            this.game = game;
            contentManager = game.Content;
            graphicsDevice = game.GraphicsDevice;
            position = startPosition;
            currentState = GamePad.GetState(PlayerIndex.One);
        }

        public void LoadContent(String textureName)
        {
            texture = contentManager.Load<Texture2D>(textureName);
            turretTexture = contentManager.Load<Texture2D>("TurretPlayer");
            mouseTexture = contentManager.Load<Texture2D>("Icon");
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
        }

        public void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            spriteBatch.Begin();
            if (!currentState.IsConnected)
            {
                spriteBatch.Draw(mouseTexture, mousePos, null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(texture, position,null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(turretTexture, position, null, Color.White, (float)shootRotationAngle, new Vector2(0,turretTexture.Height/2), 1.0f, SpriteEffects.None, 0f);
            foreach (Projectile proj in projectileList)
            {
                proj.Draw();
            }

            spriteBatch.End();
        }

        public virtual void Update(GameTime gameTime)
        {
            elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
           
            // Get the game pad state.
            currentState = GamePad.GetState(PlayerIndex.One);
            
            //Get Input
            if (currentState.IsConnected)//Game Pad
                updateGamePad();  
            else //Keyboard and Mouse
                updateKeyboard();     

            //Update position based on speed and angle
            updateMovement();
           
            //Remove Projectiles
            removeProjectiles();

            //Store old states
            oldMousestate = mousestate;
            oldKeyState = keystate;
            oldState = currentState;
        }

        public void updateGamePad()
        {
            //Update move angle 
            if (Math.Abs(currentState.ThumbSticks.Left.X) > 0 || Math.Abs(currentState.ThumbSticks.Left.Y) > 0)
            {
                moveRotationAngle = Math.Atan2(-currentState.ThumbSticks.Left.Y, currentState.ThumbSticks.Left.X);
                moveRotationAngle = moveRotationAngle % circle;
            }

            //Update Speed
            if (currentState.ThumbSticks.Left.X != 0 || currentState.ThumbSticks.Left.Y != 0)
                speed = 5;
            else
            {
                speed *= .85f;
                speed *= .85f;
            }

            //Update shoot angle between player position and mouse position
            if (Math.Abs(currentState.ThumbSticks.Right.X) > 0 || Math.Abs(currentState.ThumbSticks.Right.Y) > 0)
            {
                shootRotationAngle = Math.Atan2(-currentState.ThumbSticks.Right.Y, currentState.ThumbSticks.Right.X);
                shootRotationAngle = shootRotationAngle % circle;
            }

            //Shoot
            if (currentState.Triggers.Right >= .5f && oldState.Triggers.Right < .5)
            {
                createProjectile();
            }
        }

        public void updateKeyboard()
        {
            keystate = Keyboard.GetState();
            mousestate = Mouse.GetState();
            mousePos.X = mousestate.X;
            mousePos.Y = mousestate.Y;

            speed = 0;
            if (keystate.IsKeyDown(Keys.A))
            {  
                xComponent = -1;
                speed = 5;

            }
            if (keystate.IsKeyDown(Keys.D))
            {              
                xComponent = 1;
                speed = 5;
            }
            if (keystate.IsKeyDown(Keys.W))
            {             
                yComponent = -1;
                speed = 5;
            }
            else if (keystate.IsKeyDown(Keys.S))
            {
                yComponent = 1;
                speed = 5;
            }
            else
            {
                //Deprecate speed
                speed *= .85f;
                speed *= .85f;
            }
            if (keystate.IsKeyDown(Keys.Enter))
            {
                position = new Vector2(100, 100);
            }

            //Update moveRotationAngle
            moveRotationAngle = Math.Atan2(yComponent, xComponent);
            moveRotationAngle = moveRotationAngle % circle;
           
            //Reset Components
            xComponent = 0;
            yComponent = 0;

            //Shoot angle between player position and mouse position
            shootRotationAngle = Math.Atan2(mousePos.Y - position.Y, mousePos.X - position.X);

            //If mouse is clicked and released - Shoot
            if (oldMousestate.LeftButton == ButtonState.Pressed && mousestate.LeftButton == ButtonState.Released)
            {
                createProjectile();
            }
        }

        public void updateMovement()
        {
            position.X += (float)(Math.Cos(moveRotationAngle) * speed);
            position.Y += (float)(Math.Sin(moveRotationAngle) * speed);
            
            //Movement boundaries for player (keeps player on screen)
            if (position.X > graphicsDevice.Viewport.Width)
                position.X = graphicsDevice.Viewport.Width;
            else if (position.X < 0)
                position.X = 0;
            if (position.Y > graphicsDevice.Viewport.Height)
                position.Y = graphicsDevice.Viewport.Height;
            else if (position.Y < 0)
                position.Y = 0;
        }

        public void createProjectile()
        {
            Projectile projectile = new Projectile();
            projectile.Initialize(contentManager, graphicsDevice, position, (float)shootRotationAngle, getTurretLength(), 30f);
            projectile.LoadContent("Projectile");
            projectileList.Add(projectile);

            game.explosion.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));
            game.smoke.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));         
        }

        public void removeProjectiles()
        {
            for (int i = 0; i < projectileList.Count; i++)
            {
                //Remove Projectile if it goes off-screen
                Projectile proj = projectileList[i];
                if (proj.getPosition().X > graphicsDevice.Viewport.Width || proj.getPosition().X < 0
                    || proj.getPosition().Y > graphicsDevice.Viewport.Height || proj.getPosition().Y < 0)
                {
                    projectileList.Remove(proj);
                }
                proj.Update();
            }
        }

        public float getTurretLength()
        {
            return turretTexture.Width;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public void setPosition(Vector2 pos)
        {
            position = pos;
        }

        public Vector2 getPosition()
        {
            return position;
        }

        public List<Projectile> getProjectiles()
        {
            return projectileList;
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

        public int getTimesHit()
        {
            return timesHit;
        }

        //If the player was hit by an enemy or projectile
        public void Hit()
        {
            timesHit++;
            if (timesHit % 4 == 0)
                setPosition(new Vector2(100, 100));
            if (timesHit % 4 == 1)
                setPosition(new Vector2(graphicsDevice.Viewport.Width - 100, 100));
            if (timesHit % 4 == 2)
                setPosition(new Vector2(100, graphicsDevice.Viewport.Height - 100));
            if (timesHit % 4 == 3)
                setPosition(new Vector2(graphicsDevice.Viewport.Width - 100, graphicsDevice.Viewport.Height - 100));
        }

    }
}
