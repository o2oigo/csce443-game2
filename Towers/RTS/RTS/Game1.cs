using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace RTS
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D backgroundTexture;

        UserInterface userInterface;

        Texture2D treeTexture;

        Player player1;
        Wave wave;
        // Player player2;

        private List<Enemy> enemies;
        public List<Enemy> Enemies
        {
            get { return enemies; }
        }

        List<Player> players;
        List<Stone> stones;
        private List<Tree> trees;
        public List<Tree> Trees
        {
            get { return trees; }
        }
        private House house;
        public House House
        {
            get { return house; }
        }


        float enemyTimer = 0;
        float enemySpawnTime = 1f;
        Random rand = new Random();
        SpriteFont font;

        public ExplosionParticleSystem explosion;
        // public ExplosionParticleSystem explosion2;
        public ExplosionSmokeParticleSystem smoke;

        int live = 10;

        public FireParticleSystem fire;
        public LightningParticleSystem lightning;


        // Dictionary<string, SoundEffect> music;
        // SoundEffect tankSong;
        // SoundEffectInstance songInstance;

        private Map map;
        
        public Map Map
        {
            get { return map; }
        }
        private Rectangle gameplayArea;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferHeight = 1024;
            this.graphics.PreferredBackBufferWidth = 1280;
            //this.graphics.IsFullScreen = true;
            
            map = new Map();

            explosion = new ExplosionParticleSystem(this, 2000);
            Components.Add(explosion);

            smoke = new ExplosionSmokeParticleSystem(this, 2);
            Components.Add(smoke);

            fire = new FireParticleSystem(this, 20000);
            Components.Add(fire);

            lightning = new LightningParticleSystem(this, 10);
            Components.Add(lightning);

            
        }

        public SpriteBatch getSpriteBatch()
        {
            return spriteBatch;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
           

            //PATHFINDING//
            //Rectangle test = new Rectangle(0, 0, this.graphics.PreferredBackBufferHeight, this.graphics.PreferredBackBufferWidth);
            //Rectangle test = new Rectangle(0,0,this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height);
            gameplayArea = GraphicsDevice.Viewport.TitleSafeArea;
            //map.UpdateMapViewport(gameplayArea);
            map.ReloadMap();
            map.UpdateMapViewport(gameplayArea);
            //PATHFINDING//
            CreateTrees();
            wave = new Wave(this);
            house = new House(this, map.getBaseCoordinate());
            house.LoadContent();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            enemies = new List<Enemy>(25);
            players = new List<Player>(4);
            stones = new List<Stone>();
            trees = new List<Tree>();

            userInterface = new UserInterface();
            userInterface.Initialize(this, PlayerIndex.One, new Vector2(100, 100));
            userInterface.LoadContent();
            player1 = new Player();
            player1.Initialize(this, PlayerIndex.One, new Vector2(100, 100));
            player1.LoadContent("wizard");

            // player2 = new Player();
            // player2.Initialize(this, PlayerIndex.Two, new Vector2(200, 200));
            // player2.LoadContent("TankPurple");


            players.Add(player1);
            //  players.Add(player2);

            //PATHFINDING//
            map.LoadContent(Content);
            //PATHFINDING//

            backgroundTexture = Content.Load<Texture2D>("levelOne");
            font = Content.Load<SpriteFont>("font");
            treeTexture = Content.Load<Texture2D>("tree1");

            // tankSong = Content.Load<SoundEffect>("2DTankPOM");
            //music = new Dictionary<string, SoundEffect>();
            //music.Add("tankSong", tankSong);
            //songInstance = new SoundEffectInstance();
            //if (songInstance != null)
            //    songInstance.Dispose();
            //songInstance = music["tankSong"].CreateInstance();
            //songInstance.IsLooped = true;
            //songInstance.Play();

            //tankSong.Play();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            explosion.Update(gameTime);
            fire.Update(gameTime);
            lightning.Update(gameTime);
            
            if (userInterface.getRestartGameStatus() == true)
            {
                restartGame();
                userInterface.setRestartGameStatus(false);
                
            }
            userInterface.Update();

            if (userInterface.getShowGameScreen() == true)
            {
                
                if (enemies.Count != 0)
                    enemySpawnTime = .15f * enemies.Count;
                else
                    enemySpawnTime = 0.1f;

                //Creates Enemies
                //spawnEnemies(gameTime);
                wave.Update(gameTime);

                //Update Player
                foreach (Player player in players)
                {
                    player.Update(gameTime, enemies);
                }

                //Update Enemies
                for (int i = 0; i < enemies.Count; i++)
                {
                    //Get current enemy and update
                    Enemy currentEnemy = enemies[i];
                    currentEnemy.Update(gameTime, players[0].getTowers());
                }

                //Update Stone Timer
                if (stones.Count() > 0)
                {
                    for (int i = 0; i < stones.Count; i++)
                    {
                        stones[i].Update(gameTime);
                        if (!stones[i].isAppear) stones.Remove(stones[i]);
                    }
                }

                //Detect Collisions
                detectCollisions();

                checkDeadEnemies();

            }

            if (live < 1)
            {
                userInterface.setShowGameoverScreen(true);
                live = 10;
            }


            //base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
           
            if (userInterface.getShowGameScreen() == true || userInterface.getShowPauseScreen() == true)
            {
                spriteBatch.Draw(backgroundTexture, gameplayArea, Color.White);
                //PATHFINDING
                //map.Draw(spriteBatch);
                //DrawTrees(spriteBatch);
                //PATHFINDING//
                for (int i = 0; i < enemies.Count; i++)
                    enemies[i].Draw(spriteBatch);
                player1.Draw(spriteBatch);

                // Sprite.DrawT(spriteBatch);
                foreach (Tree t in trees)
                {
                    t.Draw(spriteBatch);
                }
                house.Draw(spriteBatch);

                //player2.Draw(spriteBatch);
                drawText();
  
                spriteBatch.DrawString(font, "Lives: " + live, new Vector2(500, this.GraphicsDevice.Viewport.Height - 50), Color.White);

                if (stones.Count() > 0)
                {
                    foreach (Stone i in stones) i.Draw(spriteBatch);
                }

            }

            if (userInterface.booltest == true)
            {
                spriteBatch.DrawString(font, "booltest", new Vector2(700, 800), Color.Tomato);
            }


            userInterface.Draw(spriteBatch);

         
            spriteBatch.End();


            base.Draw(gameTime);
        }

        //public void spawnEnemies(GameTime gameTime)
        //{
        //    enemyTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        //    if (enemyTimer > enemySpawnTime /*&& enemies.Count < 15*/)          /// Set enemy number here
        //    {
        //        int randWidth = rand.Next(this.GraphicsDevice.Viewport.Width);
        //        int randHeight = rand.Next(this.GraphicsDevice.Viewport.Height);
        //        FastEnemy spawn = new FastEnemy();
        //        spawn.Initialize(this, new Vector2(randWidth, randHeight), map);
        //        spawn.LoadContent();
        //        enemies.Add(spawn);
        //        enemyTimer = 0;
        //    }
        //}

        public void detectCollisions()
        {
            //COLLISION DETECTION
            foreach (Player player in players)
            {
                Rectangle playerRect = new Rectangle((int)player.Position.X, (int)player.Position.Y, player.getTexture().Width, player.getTexture().Height);

                //Loop through all player's tower
                for (int k = 0; k < player.getTowers().Count; k++)
                {
                    Tower tower = player.getTowers()[k];
                    Rectangle towerRect = new Rectangle((int)tower.Position.X, (int)tower.Position.Y, tower.getTexture().Width, tower.getTexture().Height);

                    //Check if player stand near a tower
                    if (towerRect.Intersects(playerRect))
                    {
                        tower.setPlayerIsNear(true);
                    }
                    else
                    {
                        tower.setPlayerIsNear(false);
                    }
                }

                //Loop through all enemies
                Rectangle houseRect = new Rectangle((int)house.Origin.X - (house.Texture.Width / 2), (int)house.Origin.Y, house.Texture.Width , house.Texture.Height/2);
                for (int i = 0; i < enemies.Count; i++)
                {
                    //Get current enemy and create collision box
                    Enemy currentEnemy = enemies[i];
                    Rectangle currentEnemyRect = new Rectangle((int)currentEnemy.Position.X, (int)currentEnemy.Position.Y, currentEnemy.getTexture().Width, currentEnemy.getTexture().Height);

                    //Ckeck if current enemy and exit point                   
                    //if (map.TileTypeAt(currentEnemy.Position) == MapTileType.MapExit)
                    if (currentEnemyRect.Intersects(houseRect))
                    {
                        live--;
                        Sprite.removeList(enemies[i]);
                        enemies.RemoveAt(i);
                    }

                    //Loop through all of current enemies projectiles
                    for (int j = 0; j < currentEnemy.getProjectiles().Count; j++)
                    {
                        Projectile proj = currentEnemy.getProjectiles()[j];
                        Rectangle enemyProjectileRect = new Rectangle((int)proj.getPosition().X, (int)proj.getPosition().Y, proj.getTexture().Width, proj.getTexture().Height);

                        //Check if player is hit by any of current enemy's current projectile
                       /* if (playerRect.Intersects(enemyProjectileRect))
                        {
                            currentEnemy.getProjectiles().Remove(proj);
                            if (player.isShielded() == false)
                                player.Hit();
                        }*/

                        //Check if player's towers are hit by any of current enemy's current projectile
                        for (int k = 0; k < player.getTowers().Count; k++)
                        {
                            Tower tower = player.getTowers()[k];
                            Rectangle towerRect = new Rectangle((int)tower.Position.X, (int)tower.Position.Y, tower.getTexture().Width, tower.getTexture().Height);
                            if (towerRect.Intersects(enemyProjectileRect))
                            {
                                currentEnemy.getProjectiles().Remove(proj);
                                tower.Hit();
                                if (player.getTowers().Count != 0 && tower.isDead())
                                {
                                    Sprite.removeList(tower);
                                    player.getTowers().RemoveAt(k);

                                }
                            }
                        }
                    }
                }

                //Loop through all enemies
                for (int i = 0; i < enemies.Count; i++)
                {
                    //Get current enemy and create collision box
                    Enemy currentEnemy = enemies[i];
                    Rectangle currentEnemyRect = new Rectangle((int)currentEnemy.Position.X, (int)currentEnemy.Position.Y, currentEnemy.getTexture().Width, currentEnemy.getTexture().Height);

                    //Check if current enemy is hit by any of player's projectiles
                    for (int j = 0; j < player.getProjectiles().Count; j++)
                    {
                        Projectile proj = player.getProjectiles()[j];
                        Rectangle playerProjectileRect = new Rectangle((int)proj.getPosition().X, (int)proj.getPosition().Y, proj.getTexture().Width, proj.getTexture().Height);

                        if (currentEnemyRect.Intersects(playerProjectileRect))
                        {
                            player.getProjectiles().Remove(proj);
                            currentEnemy.Hit(10.0f);
                            //if (enemies.Count != 0 && enemies[i].isDead())
                            //{
                            //    int randNum = rand.Next(1, 10);
                            //    if (randNum < 4)
                            //    {
                            //        Stone newStone = new Stone();
                            //        newStone.Initialize(this, enemies[i].Position, randNum);
                            //        stones.Add(newStone);
                            //    }
                            //    Sprite.removeList(enemies[i]);
                            //    enemies.RemoveAt(i);
                            //    player.enemyDestroyed();
                            //}
                        }
                    }
                }

                //Loop through all enemies
                for (int i = 0; i < enemies.Count; i++)
                {
                    //Get current enemy and create collision box
                    Enemy currentEnemy = enemies[i];
                    Rectangle currentEnemyRect = new Rectangle((int)currentEnemy.Position.X, (int)currentEnemy.Position.Y, currentEnemy.getTexture().Width, currentEnemy.getTexture().Height);

                    //Check if current enemy is hit by any of player's tower's projectiles
                    for (int k = 0; k < player.getTowers().Count; k++)
                    {
                        Tower tower = player.getTowers()[k];
                        for (int j = 0; j < tower.getProjectiles().Count; j++)
                        {
                            Projectile proj = tower.getProjectiles()[j];
                            Rectangle towerProjectileRect = new Rectangle((int)proj.getPosition().X, (int)proj.getPosition().Y, proj.getTexture().Width, proj.getTexture().Height);

                            if (currentEnemyRect.Intersects(towerProjectileRect))
                            {
                                tower.getProjectiles().Remove(proj);
                                currentEnemy.Hit(tower.Damage);
                                //if (enemies.Count != 0 && enemies[i].isDead())
                                //{
                                //    int randNum = rand.Next(1, 10);
                                //    if (randNum < 4)
                                //    {
                                //        Stone newStone = new Stone();
                                //        newStone.Initialize(this, enemies[i].Position, randNum);
                                //        stones.Add(newStone);
                                //    }
                                //    Sprite.removeList(enemies[i]);
                                //    enemies.RemoveAt(i);
                                //    player.towerEnemyDestroyed();
                                //}
                            }
                        }
                    }
                }
                //Loop through all stones
                for (int j = 0; j < stones.Count; j++)
                {
                    Rectangle currentStoneRect = new Rectangle((int)stones[j].Position.X, (int)stones[j].Position.Y, stones[j].Texture.Width, stones[j].Texture.Height);
                    if (playerRect.Intersects(currentStoneRect))
                    {
                        player.addStoneToInventory(stones[j]);
                        stones.Remove(stones[j]);
                    }
                }
            }
        }

        public void checkDeadEnemies()
        {
            for (int i=0; i<enemies.Count(); i++)
            {
                if (enemies[i].isDead())
                {
                    int randNum = rand.Next(0, 10);
                    if (randNum < 3)
                    {
                        Stone newStone = new Stone();
                        newStone.Initialize(this, enemies[i].Position, randNum);
                        stones.Add(newStone);
                    }
                    Sprite.removeList(enemies[i]);
                    enemies.RemoveAt(i);
                    player1.addMoney(3);
                   // player.enemyDestroyed();
                }
            }
        }
        //Draw Text method for debugging / displaying
        public void drawText()
        {
            int width = this.GraphicsDevice.Viewport.Width;
            //GamePadState gpstate = GamePad.GetState(PlayerIndex.One);
            //spriteBatch.Begin();

            //  spriteBatch.DrawString(font, "Player 1", new Vector2(player1.getPosition().X - 8f * 5f, player1.getPosition().Y - player1.getTurretLength() - 30f), Color.MediumBlue);
            //   spriteBatch.DrawString(font, "Player 2", new Vector2(player2.getPosition().X - 8f * 5f, player2.getPosition().Y - player2.getTurretLength() - 30f), Color.Purple);
            if (player1.isShielded())
                spriteBatch.DrawString(font, "Shield: " + ((int)player1.getShieldTimeLimit() - (int)player1.getShieldTimer()), new Vector2(player1.Position.X - 9f * 5f, player1.Position.Y + player1.getTurretLength() + 10f), Color.MediumBlue);
            //  if (player2.isShielded())
            //      spriteBatch.DrawString(font, "Shield: " + (3 - (int)player2.getShieldTimer()), new Vector2(player2.getPosition().X - 9f * 5f, player2.getPosition().Y + player2.getTurretLength() + 10f), Color.Purple);

            spriteBatch.DrawString(font, "Player 1 Kills      : " + player1.getEnemiesDestroyed(), new Vector2(width - 400, 15), Color.White);
            //    spriteBatch.DrawString(font, "Player 2 Kills      : " + player2.getEnemiesDestroyed(), new Vector2(510, 15), Color.White);

            spriteBatch.DrawString(font, "Player 1 Tower Kills: " + player1.getTowerEnemiesDestroyed(), new Vector2(width - 400, 35), Color.White);
            //   spriteBatch.DrawString(font, "Player 2 Tower Kills: " + player2.getTowerEnemiesDestroyed(), new Vector2(510, 35), Color.White);

            spriteBatch.DrawString(font, "Player 1 Deaths     : " + player1.getTimesHit(), new Vector2(width - 400, 55), Color.White);
            //   spriteBatch.DrawString(font, "Player 2 Deaths     : " + player2.getTimesHit(), new Vector2(510, 55), Color.White);
            if (player1.getMaxTower() == true)
            {
                spriteBatch.DrawString(font, "Tower Limit Reach", new Vector2(500, 35), Color.White);
            }

            foreach (Tower tower in player1.getTowers())
            {
                if (tower.getPlayerIsNear() == true)
                    spriteBatch.DrawString(font, "Player near tower", new Vector2(500, 55), Color.White);
                // spriteBatch.DrawString(font, "" + (tower.getShotsToDestroy() - tower.getShotsTaken()), new Vector2(tower.getPosition().X - 5, tower.getPosition().Y - 60), Color.MediumBlue);
                //d spriteBatch.DrawString(font, "P1", new Vector2(tower.getPosition().X - 10, tower.getPosition().Y + 25), Color.MediumBlue);
            }
            spriteBatch.DrawString(font, "TILE: " + map.TileString(new Vector2(player1.Position.X, player1.Position.Y)), new Vector2(500, 100), Color.MediumBlue);
            spriteBatch.DrawString(font, "SPRITE COUNT: " + Sprite.objCount(), new Vector2(500, 150), Color.MediumBlue);
              spriteBatch.DrawString(font, "Player 1 Shoot Angle     : " + player1.getShootRotationAngle(), new Vector2(510, 75), Color.Black);
            //   foreach (Tower tower in player2.getTowers())
            //   {
            //       spriteBatch.DrawString(font, "" + (tower.getShotsToDestroy() - tower.getShotsTaken()), new Vector2(tower.getPosition().X - 5, tower.getPosition().Y - 60), Color.Purple);
            //       spriteBatch.DrawString(font, "P2", new Vector2(tower.getPosition().X - 10, tower.getPosition().Y + 25), Color.Purple);
            //   }

            // spriteBatch.End();
        }

      
        public void restartGame()
        {

            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                Enemy currentEnemy = enemies[i];
                Sprite.removeList(enemies[i]);
                enemies.RemoveAt(i);
            }
            
            for (int k = 0; k < player1.getTowers().Count; k++)
            {
                Tower tower = player1.getTowers()[k];
                Sprite.removeList(tower);
                player1.getTowers().RemoveAt(k);

            }

            player1.restartGameLevel1();
            //enemies.Clear();
            stones.Clear();
            wave = new Wave(this);

            this.ResetElapsedTime();

            enemyTimer = 0;
            enemySpawnTime = 1f;
            live = 10;
        }


        //public void DrawTrees(SpriteBatch spriteBatch)
        //{
        //    foreach (Point pt in map.getTrees())
        //    {
        //
        //        Vector2 offset = new Vector2(0, 0);
        //        for (int i = 0; i < map.TreeDict[pt].Count; i++)
        //        {
        //            Vector2 tilePosition = map.MapToWorld(pt.X, pt.Y, true);
        //            tilePosition.Y -= treeTexture.Height*map.ScaleB;
        //            tilePosition.X -= treeTexture.Width*map.ScaleB / 2;
        //            offset = (map.TreeDict[pt])[i];
        //            tilePosition.X += offset.X;
        //            tilePosition.Y += offset.Y;
        //            spriteBatch.Draw(treeTexture, tilePosition, null, Color.White, 0f, Vector2.Zero, map.ScaleB, SpriteEffects.None, 0f);
        //        }
        //    }
        //}

        public void CreateTrees()
        {
            foreach (Point pt in map.getTrees())
            {
                Vector2 tilePosition = map.MapToWorld(pt.X, pt.Y, true);
                trees.Add(new Tree(this, tilePosition));
            }
            foreach (Tree t in trees)
            {
                int i = rand.Next(8);
                t.LoadContent(i);
            }
        }

    }
}
