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
    class Player : Sprite
    {
        //Game1 game;
        //ContentManager contentManager;
        //GraphicsDevice graphicsDevice;
        //SpriteBatch spriteBatch;
        //Map map;

        PlayerIndex playerIndex;

        KeyboardState keystate;
        KeyboardState oldKeyState;
        MouseState mousestate;
        MouseState oldMousestate;
        GamePadState currentState;
        GamePadState oldState;

        private float elapsedTime;

        //private Vector2 position;
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

        // fredy code for User Interface (tower menu)
        private Texture2D mouseTexture;
        private Texture2D menu1Texture;
        private Texture2D menu2Texture;
        private Texture2D menu3Texture;
        private Texture2D menu4Texture;
        private bool buildMode = false;
        private bool mainBuildMode = false;
        private bool maxCapacityTower = false;

        private List<Projectile> projectileList = new List<Projectile>(5);
        private List<Tower> towerList = new List<Tower>(5);

        private int enemiesDestroyed = 0;
        private int towerEnemiesDestroyed = 0;
        private int maxTowerCount = 6;

        private float circle = MathHelper.Pi * 2;

        public void Initialize(Game1 game, PlayerIndex index, Vector2 startPosition)
        {
            this.game = game;
            contentManager = game.Content;
            graphicsDevice = game.GraphicsDevice;
            position = startPosition;
            playerIndex = index;
            currentState = GamePad.GetState(playerIndex);
            map = game.Map;
        }

        public void LoadContent(String textureName)
        {
            texture = contentManager.Load<Texture2D>(textureName);
            if (playerIndex == PlayerIndex.One)
                turretTexture = contentManager.Load<Texture2D>("TurretPlayer");
            else
                turretTexture = contentManager.Load<Texture2D>("TurretPurple");
            mouseTexture = contentManager.Load<Texture2D>("CrossHair1");
            menu1Texture = contentManager.Load<Texture2D>("buildTowerMenu");
            menu2Texture = contentManager.Load<Texture2D>("cancelMenu");
            menu3Texture = contentManager.Load<Texture2D>("buildTowerMenuSelect");
            menu4Texture = contentManager.Load<Texture2D>("cancelMenuSelect");

            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch = SB;
            //spriteBatch.Begin();
            if (!currentState.IsConnected)
            {
                spriteBatch.Draw(mouseTexture, mousePos, null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            }
            spriteBatch.Draw(texture, position, null, Color.White, (float)moveRotationAngle, origin, map.ScaleB, SpriteEffects.None, 0f);
            spriteBatch.Draw(turretTexture, position, null, Color.White, (float)shootRotationAngle, new Vector2(0, turretTexture.Height / 2), map.ScaleB, SpriteEffects.None, 0f);
            foreach (Projectile proj in projectileList)
            {
                proj.Draw(spriteBatch);
            }
            foreach (Tower tower in towerList)
            {
                tower.Draw(spriteBatch);
            }



            if (buildMode == true && mainBuildMode == true)
            {
                if (shootRotationAngle > -2.39 && shootRotationAngle < -0.93)
                {
                    spriteBatch.Draw(menu3Texture, new Vector2(position.X - 152, position.Y - 130), Color.White);
                    spriteBatch.Draw(menu2Texture, new Vector2(position.X - 152, position.Y - 130), Color.White);
                }
                else if (shootRotationAngle > 0.55 && shootRotationAngle < 2.59)
                {
                    spriteBatch.Draw(menu1Texture, new Vector2(position.X - 152, position.Y - 130), Color.White);
                    spriteBatch.Draw(menu4Texture, new Vector2(position.X - 152, position.Y - 130), Color.White);
                }
                else
                {
                    spriteBatch.Draw(menu1Texture, new Vector2(position.X - 152, position.Y - 130), Color.White);
                    spriteBatch.Draw(menu2Texture, new Vector2(position.X - 152, position.Y - 130), Color.White);
                }
            }



            // spriteBatch.End();
        }

        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (towerList.Count < 6)
            {
                maxCapacityTower = false;
            }

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
            if (currentState.IsButtonDown(Buttons.RightShoulder) && oldState.IsButtonUp(Buttons.RightShoulder))
            {
                createProjectile();
            }

            //Build Mode
            if (oldState.IsButtonUp(Buttons.LeftShoulder) && currentState.IsButtonDown(Buttons.LeftShoulder) && mainBuildMode == false)
            {
                buildMode = true;
                mainBuildMode = true;
            }
            else if (mainBuildMode == true && oldState.IsButtonUp(Buttons.LeftShoulder) && currentState.IsButtonDown(Buttons.LeftShoulder))
            {
                if (shootRotationAngle > -2.39 && shootRotationAngle < -0.76 && towerList.Count < maxTowerCount && maxCapacityTower == false)
                {
                    buildMode = false;
                    mainBuildMode = false;
                    if (map.TileTypeAt(position) == MapTileType.MapBarrier)
                    {
                        createTower();
                    }
                    if (towerList.Count == maxTowerCount)
                    {
                        maxCapacityTower = true;
                    }
                }
                else if (shootRotationAngle > 0.55 && shootRotationAngle < 2.59)
                {
                    buildMode = false;
                    mainBuildMode = false;
                }
            }
        }

        public void updateKeyboard()
        {
            keystate = Keyboard.GetState();
            mousestate = Mouse.GetState();
            mousePos.X = mousestate.X;
            mousePos.Y = mousestate.Y;

            //if (!keystate.IsKeyDown(Keys.A) && !keystate.IsKeyDown(Keys.D) && !keystate.IsKeyDown(Keys.W) && !keystate.IsKeyDown(Keys.S))
            speed = 0;

            if (keystate.IsKeyDown(Keys.A) && buildMode == false)
            {
                xComponent = -1;
                speed = 5;

            }
            if (keystate.IsKeyDown(Keys.D) && buildMode == false)
            {
                xComponent = 1;
                speed = 5;
            }
            if (keystate.IsKeyDown(Keys.W) && buildMode == false)
            {
                yComponent = -1;
                speed = 5;
            }
            else if (keystate.IsKeyDown(Keys.S) && buildMode == false)
            {
                yComponent = 1;
                speed = 5;
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
            if (buildMode == false)
            {
                if (oldMousestate.LeftButton == ButtonState.Pressed && mousestate.LeftButton == ButtonState.Released)
                {
                    createProjectile();
                }
            }

            //Open Main Build Menu
            if (keystate.IsKeyUp(Keys.Space) && oldKeyState.IsKeyDown(Keys.Space))
            {
                buildMode = true;
                mainBuildMode = true;
            }
            if (mainBuildMode == true && oldMousestate.LeftButton == ButtonState.Pressed && mousestate.LeftButton == ButtonState.Released)
            {
                if (shootRotationAngle > -2.39 && shootRotationAngle < -0.76 && towerList.Count < maxTowerCount && maxCapacityTower == false)
                {
                    buildMode = false;
                    mainBuildMode = false;
                    if (map.TileTypeAt(position) == MapTileType.MapBarrier)
                    {
                        createTower();
                    }
                    if (towerList.Count == maxTowerCount)
                    {
                        maxCapacityTower = true;
                    }
                }
                else if (shootRotationAngle > 0.55 && shootRotationAngle < 2.59)
                {
                    buildMode = false;
                    mainBuildMode = false;
                }
            }

            if (keystate.IsKeyUp(Keys.Escape) && oldKeyState.IsKeyDown(Keys.Escape))
            {
                game.Exit();
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

        //public void setPosition(Vector2 pos)
        //{
        //    position = pos;
        //}

        //public Vector2 getPosition()
        //{
        //    return position;
        //}

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

        public bool getMaxTower()
        {
            return maxCapacityTower;
        }

        //If the player was hit by an enemy or projectile
        public void Hit()
        {
            timesHit++;
            if (timesHit % 4 == 0)
                Position = new Vector2(100, 100);
            if (timesHit % 4 == 1)
                Position = new Vector2(graphicsDevice.Viewport.Width - 100, 100);
            if (timesHit % 4 == 2)
                Position = new Vector2(100, graphicsDevice.Viewport.Height - 100);
            if (timesHit % 4 == 3)
                Position = new Vector2(graphicsDevice.Viewport.Width - 100, graphicsDevice.Viewport.Height - 100);
            buildMode = false;
            mainBuildMode = false;
            spawnShield = true;

        }

    }
}
