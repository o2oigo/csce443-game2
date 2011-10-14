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

        PlayerIndex playerIndex;

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

        private bool spawnShield = true;
        private float shieldTimeLimit = 3f;
        private float shieldTimer = 0;

        private double moveRotationAngle;
        private double shootRotationAngle;    
        private float xComponent = 0;
        private float yComponent = 0;

        private Texture2D texture;
        private Texture2D turretTexture;
        private Texture2D mouseTexture;

        private List<Projectile> projectileList = new List<Projectile>(5);
        private List<Tower> towerList = new List<Tower>(5);

        private int enemiesDestroyed = 0;
        private int towerEnemiesDestroyed = 0;
        private int maxTowerCount = 2;
  
        private float circle = MathHelper.Pi * 2;
 
        public void Initialize(Game1 game, PlayerIndex index, Vector2 startPosition)
        {
            this.game = game;
            contentManager = game.Content;
            graphicsDevice = game.GraphicsDevice;
            position = startPosition;
            playerIndex = index;
            currentState = GamePad.GetState(playerIndex);
        }

        public void LoadContent(String textureName)
        {
            texture = contentManager.Load<Texture2D>(textureName);
            if(playerIndex == PlayerIndex.One)
                turretTexture = contentManager.Load<Texture2D>("TurretPlayer");
            else
                turretTexture = contentManager.Load<Texture2D>("TurretPurple");
            mouseTexture = contentManager.Load<Texture2D>("CrossHair1");
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
        }

        public void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            //spriteBatch.Begin();
            if (!currentState.IsConnected)
            {
                spriteBatch.Draw(mouseTexture, mousePos, null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(texture, position,null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(turretTexture, position, null, Color.White, (float)shootRotationAngle, new Vector2(0,turretTexture.Height/2), 1.0f, SpriteEffects.None, 0f);
            foreach (Projectile proj in projectileList)
            {
                proj.Draw(SB);
            }
            foreach (Tower tower in towerList)
            {
                tower.Draw(SB);
            }

           // spriteBatch.End();
        }

        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
           
            //Spawn Shield Timer
            if (spawnShield == true)
            {
                shieldTimer += elapsedTime;
                if (shieldTimer >= shieldTimeLimit)
                {
                    spawnShield = false;
                    shieldTimer = 0;
                }
            }

            // Get the game pad state.
            currentState = GamePad.GetState(playerIndex);
            
            //Get Input
            if (currentState.IsConnected)//Game Pad
                updateGamePad();  
            else //Keyboard and Mouse
                updateKeyboard();     

            //Update position based on speed and angle
            updateMovement();
           
            //Update Projectiles
            updateProjectiles();

            //Update Towers
            updateTowers(gameTime, enemies);

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
                speed = 0;
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

            //Create Tower
            if (currentState.Triggers.Left == 1 && oldState.Triggers.Left != 1 && towerList.Count < maxTowerCount)
            {
                createTower();
            }
        }

        public void updateKeyboard()
        {
            keystate = Keyboard.GetState();
            mousestate = Mouse.GetState();
            mousePos.X = mousestate.X;
            mousePos.Y = mousestate.Y;

            if (!keystate.IsKeyDown(Keys.A) && !keystate.IsKeyDown(Keys.D) && !keystate.IsKeyDown(Keys.W) && !keystate.IsKeyDown(Keys.S))
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

            if (keystate.IsKeyDown(Keys.Enter))
            {
                position = new Vector2(100, 100);
            }

            //Create Tower
            if (keystate.IsKeyUp(Keys.Space) && oldKeyState.IsKeyDown(Keys.Space) && towerList.Count < maxTowerCount)
            {
                createTower();
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
            if (playerIndex == PlayerIndex.One)
                projectile.LoadContent("ProjectileBlue");
            else
                projectile.LoadContent("ProjectilePurple");
            projectileList.Add(projectile);

            game.explosion.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));
            //game.smoke.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));         
        }

        public void updateProjectiles()
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

        public void createTower()
        {
            Tower tower = new Tower(game, playerIndex, this.position);
            towerList.Add(tower);
        }

        public void updateTowers(GameTime gameTime, List<Enemy> enemies)
        {
            for (int i = 0; i < towerList.Count; i++)
            {
                towerList[i].Update(gameTime, enemies);
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

        public List<Tower> getTowers()
        {
            return towerList;
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

        public int getEnemiesDestroyed()
        {
            return enemiesDestroyed;
        }

        public void enemyDestroyed()
        {
            this.enemiesDestroyed++;
        }

        public int getTowerEnemiesDestroyed()
        {
            return towerEnemiesDestroyed;
        }

        public void towerEnemyDestroyed()
        {
            this.towerEnemiesDestroyed++;
        }

        public bool isShielded()
        {
            return spawnShield;
        }

        public float getShieldTimer()
        {
            return shieldTimer;
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

            spawnShield = true;
            
        }

    }
}
