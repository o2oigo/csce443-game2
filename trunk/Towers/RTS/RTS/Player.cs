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
        private float shieldTimeLimit = 100f;
        private float shieldTimer = 0;

        private double moveRotationAngle;
        private double shootRotationAngle;
        private float xComponent = 0;
        private float yComponent = 0;

        private Texture2D texture;
        private Texture2D turretTexture;

        // fredy code for User Interface (tower menu)
        private bool boolForTest = false;
        private Texture2D mouseTexture;
        private Texture2D menu1Texture;
        private Texture2D menu2Texture;
        private Texture2D menu3Texture;
        private Texture2D menu4Texture;
        private Texture2D buildTexture;
        private Texture2D cancelTexture;
        private Texture2D upgradeTexture;
        private Texture2D sellTexture;
        private Texture2D enhanceTexture;
        private Texture2D canonTowerBuildTexture;
        private Texture2D ligthningTowerBuildTexture;
        private Texture2D backgroundTexture;
        private Texture2D startMenuTexture;
        private Texture2D quitMenuTexture;

        private bool buildMode = false;
        private bool mainBuildMode = false;
        private bool upgradeBuildMode = false;
        private bool maxCapacityTower = false;
        SpriteFont font;
        private int fireStoneInInventory = 0;
        private int waterStoneInInventory = 0;
        private int healStoneInInventory = 0;
        private int money = 35;
        private float timeForResources = 0;
        private Vector2 uiPosition1;
        private Vector2 uiPosition2;
        private Vector2 uiPosition3;
        private Vector2 uiPosition4;

        private List<Projectile> projectileList = new List<Projectile>(5);
        private List<Tower> towerList = new List<Tower>(20);
        private List<Stone> stoneList = new List<Stone>(20);

        private int enemiesDestroyed = 0;
        private int towerEnemiesDestroyed = 0;
        private int maxTowerCount = 10;

        private float circle = MathHelper.Pi * 2;

        public void Initialize(Game1 game, PlayerIndex index, Vector2 startPosition)
        {
            this.game = game;
            this.contentManager = game.Content;
            this.graphicsDevice = game.GraphicsDevice;
            this.position = startPosition;
            this.playerIndex = index;
            this.currentState = GamePad.GetState(playerIndex);
            this.map = game.Map;
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
            buildTexture = contentManager.Load<Texture2D>("buildSmall");
            cancelTexture = contentManager.Load<Texture2D>("cancelSmall");
            upgradeTexture = contentManager.Load<Texture2D>("upgradeSmall");
            enhanceTexture = contentManager.Load<Texture2D>("enhanceSmall");
            sellTexture = contentManager.Load<Texture2D>("sellSmall");
            font = contentManager.Load<SpriteFont>("font");

            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            uiPosition1 = new Vector2 (80, game.GraphicsDevice.Viewport.Height - 50);
            uiPosition2 = new Vector2(250, game.GraphicsDevice.Viewport.Height - 70);
            uiPosition3 = new Vector2(500, game.GraphicsDevice.Viewport.Height - 70);
            uiPosition4 = new Vector2(700, game.GraphicsDevice.Viewport.Height - 70);

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


            if (boolForTest == false)
            {
                spriteBatch.DrawString(font, "test", new Vector2(position.X - 50, position.Y + 80), Color.White);
            }
            spriteBatch.DrawString(font, fireStoneInInventory + " Fire Stone", new Vector2(uiPosition2.X, uiPosition2.Y + 40), Color.White);
            spriteBatch.DrawString(font, waterStoneInInventory + " Water Stone", new Vector2(uiPosition2.X, uiPosition2.Y + 20), Color.White);
            spriteBatch.DrawString(font, healStoneInInventory + " Heal Stone", uiPosition2, Color.White);
            spriteBatch.DrawString(font, "Resources: " + money, uiPosition1, Color.White);

            
            foreach (Tower tower in towerList)
            {
                tower.Draw(spriteBatch);
            }


            if (buildMode == true && mainBuildMode == true && upgradeBuildMode == false)
            {

                /*
                if (shootRotationAngle > -2.39 && shootRotationAngle < -0.93)
                {
                    spriteBatch.Draw(buildTexture, new Vector2(position.X - 40, position.Y - 110), Color.White);
                    spriteBatch.Draw(cancelTexture, new Vector2(position.X - 40, position.Y + 50), Color.White);
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
                 */

                spriteBatch.Draw(buildTexture, new Vector2(position.X - 40, position.Y - 110), Color.White);
                spriteBatch.Draw(cancelTexture, new Vector2(position.X - 40, position.Y + 50), Color.White);

            }

            if (upgradeBuildMode == true && buildMode == true)
            {
                spriteBatch.Draw(upgradeTexture, new Vector2(position.X - 40, position.Y - 110), Color.White);
                spriteBatch.Draw(cancelTexture, new Vector2(position.X - 40, position.Y + 50), Color.White);
                spriteBatch.Draw(sellTexture, new Vector2(position.X - 100, position.Y - 30), Color.White);
                spriteBatch.Draw(enhanceTexture, new Vector2(position.X + 20, position.Y - 30), Color.White);
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

            foreach (Tower tower in towerList)
            {
                //Set upgrade mode if near tower
                if (tower.getPlayerIsNear() == true)
                {
                    upgradeBuildMode = true;
                    mainBuildMode = false;
                }
                else
                {
                    mainBuildMode = true;
                    upgradeBuildMode = false;
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

            //Update Inventory
            updateInventory();

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
                    if (map.TileTypeAt(position) == MapTileType.MapGrass)
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

            if (keystate.IsKeyDown(Keys.NumPad1))
            {
                Stone fStone = new Stone();
                fStone.Initialize(game,position,0);
                addStoneToInventory(fStone);
            }
            
            if (keystate.IsKeyDown(Keys.NumPad2))
            {
                Stone wStone = new Stone();
                wStone.Initialize(game, position, 1);
                addStoneToInventory(wStone);
            }
            
            if (keystate.IsKeyDown(Keys.NumPad3))
            {
                Stone hStone = new Stone();
                hStone.Initialize(game, position, 2);
                addStoneToInventory(hStone);
            }

            if (keystate.IsKeyDown(Keys.NumPad7))
            {
                removeStoneFromInventory(0);
            }

            if (keystate.IsKeyDown(Keys.NumPad8))
            {
                removeStoneFromInventory(1);
            }

            if (keystate.IsKeyDown(Keys.NumPad9))
            {
                removeStoneFromInventory(2);
            }

            if (keystate.IsKeyDown(Keys.NumPad4))
            {
                addMoney(10);
            }

            if (keystate.IsKeyDown(Keys.NumPad5))
            {
                removeMoney(50);
            }

            if (keystate.IsKeyDown(Keys.NumPad6))
            {
                removeMoney(10);
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
            if (buildMode == true && oldMousestate.LeftButton == ButtonState.Pressed && mousestate.LeftButton == ButtonState.Released)
            {
                if (shootRotationAngle > -2.39 && shootRotationAngle < -0.76 && buildMode == true && towerList.Count < maxTowerCount && maxCapacityTower == false)
                {
                    
                    if (mainBuildMode == true)
                    {
                        if (map.TileTypeAt(position) == MapTileType.MapGrass)
                        {
                            if (money >= 15)
                            {
                                removeMoney(15);
                                createTower();
                                buildMode = false;
                                mainBuildMode = false;
                            }
                        }
                        if (towerList.Count == maxTowerCount)
                        {
                            maxCapacityTower = true;
                        }
                    }
                    else if (upgradeBuildMode == true)
                    {
                        for (int i = 0; i < towerList.Count(); i++)
                        {
                            if (towerList[i].getPlayerIsNear() == true)
                            {
                                if (money >= 10)
                                {
                                    removeMoney(10);

                                    towerList[i].setToLvlTwo();
                                    buildMode = false;
                                    upgradeBuildMode = false;
                                }
                            }

                        }
                    }

                    
                }
                else if (shootRotationAngle > 0.55 && shootRotationAngle < 2.59)
                {
                    buildMode = false;
                    mainBuildMode = false;
                }

                else if (shootRotationAngle >= -0.93 && shootRotationAngle <= 0.55 && upgradeBuildMode == true)
                {
                    for (int i = 0; i < towerList.Count(); i++)
                    {
                        if (towerList[i].getPlayerIsNear() == true)
                        {
                            if (fireStoneInInventory >= 1)
                            {
                                removeStoneFromInventory(0);

                                towerList[i].setToFireTower();
                            }
                        }

                    }
                    buildMode = false;
                    mainBuildMode = false;
                }

                else if (shootRotationAngle >= -3.14 && shootRotationAngle <= -2.38 || shootRotationAngle >= 2.59 && shootRotationAngle <= 3.14)
                {
                    for (int i = 0; i < towerList.Count(); i++)
                    {
                        if (towerList[i].getPlayerIsNear() == true)
                        {
                            if (towerList[i].getTowerLvl() == "level 1")
                            {
                                addMoney(5);
                                Sprite.removeList(towerList[i]);
                                towerList.RemoveAt(i);
                            }
                            else if (towerList[i].getTowerLvl() == "level 2")
                            {
                                addMoney(10);
                                Sprite.removeList(towerList[i]);
                                towerList.RemoveAt(i);
                            }
                            
                        }
                    }
                    buildMode = false;
                    mainBuildMode = false;
                }
            }

            if (keystate.IsKeyUp(Keys.Delete) && oldKeyState.IsKeyDown(Keys.Delete))
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
            projectile.Initialize(contentManager, graphicsDevice, position, (float)shootRotationAngle, getTurretLength(), 30f, map);
            if (playerIndex == PlayerIndex.One)
                projectile.LoadContent("ProjectileBlue");
            else
                projectile.LoadContent("ProjectilePurple");
            projectileList.Add(projectile);

             game.explosion.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength() * map.ScaleB, position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength() * map.ScaleB));
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

        // checking how many stone in the player inventory
        public void updateInventory()
        {
            fireStoneInInventory = 0;
            waterStoneInInventory = 0;
            healStoneInInventory = 0;

            for (int i = 0; i < stoneList.Count; i++)
            {
                Stone _stone = stoneList[i];
                if (_stone.Type == ElementType.Fire)
                {
                    fireStoneInInventory++;
                }
                else if (_stone.Type == ElementType.Water)
                {
                    waterStoneInInventory++;
                }
                else if (_stone.Type == ElementType.Heal)
                {
                    healStoneInInventory++;
                }
                
            }
        }

        public void addStoneToInventory(Stone st)
        {
            //Stone stone = new Stone();
            //stone.Initialize(game, this.position, _stoneType);
            stoneList.Add(st);
        }

        public void removeStoneFromInventory(int _stoneType)
        {
            Stone removedStone = new Stone();
            removedStone.Initialize(game, this.position, _stoneType);
            if (stoneList.Count > 0)
            {
                for (int i = 0; i < stoneList.Count; i++)
                {
                    Stone _stone = stoneList[i];
                    if (_stone.Type == removedStone.Type)
                    {
                        stoneList.Remove(_stone);
                        break;
                    }

                }
            }
        }

        public void addMoney(int _money)
        {
            money += _money;
        }

        public void removeMoney(int _money)
        {
            money -= _money;
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

        public float getShieldTimeLimit()
        {
            return shieldTimeLimit;
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


        public void restartGameLevel1()
        {
            position = new Vector2(100, 100);
            speed = 0;
            timesHit = 0;

            spawnShield = true;
            shieldTimeLimit = 100f;
            shieldTimer = 0;
            xComponent = 0;
            yComponent = 0;

            boolForTest = false;
            buildMode = false;
            mainBuildMode = false;
            upgradeBuildMode = false;
            maxCapacityTower = false;
            
            fireStoneInInventory = 0;
            waterStoneInInventory = 0;
            healStoneInInventory = 0;
            money = 35;
            timeForResources = 0;
            
            projectileList.Clear();
            towerList.Clear();
            stoneList.Clear();

            enemiesDestroyed = 0;
            towerEnemiesDestroyed = 0;
        }

       

    }
}
