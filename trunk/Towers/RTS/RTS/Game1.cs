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
        Player player1;
        Player player2;
        List<Enemy> enemies;
        List<Player> players;
        float enemyTimer = 0;
        float enemySpawnTime = 1f;
        Random rand = new Random();
        public ExplosionParticleSystem explosion;
       // public ExplosionParticleSystem explosion2;
        public ExplosionSmokeParticleSystem smoke;

       // Dictionary<string, SoundEffect> music;
       // SoundEffect tankSong;
       // SoundEffectInstance songInstance;

        //
        //PATHFINDING//
        //Map map;
        //AI enemy;
        //PathFinder pathFinder;
        //private Rectangle gameplayArea;
        //int Counter = 0;
        //PATHFINDING//

        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferHeight = 1080;
            this.graphics.PreferredBackBufferWidth = 1920;
            this.graphics.IsFullScreen = true;

            explosion = new ExplosionParticleSystem(this, 1);
            Components.Add(explosion);

            smoke = new ExplosionSmokeParticleSystem(this, 2);
            Components.Add(smoke);

            //PATHFINDING//
            //map = new Map();
            //enemy = new AI();
            //pathFinder = new PathFinder();
            //PATHFINDING//
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
            //gameplayArea = GraphicsDevice.Viewport.TitleSafeArea;
            //map.UpdateMapViewport(gameplayArea);
            //enemy.Initialize(map);
            //pathFinder.Initialize(map);
            //PATHFINDING//
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            player1 = new Player();
            player1.Initialize(this, PlayerIndex.One, new Vector2(100,100));
            player1.LoadContent("TankPlayer");

            player2 = new Player();
            player2.Initialize(this, PlayerIndex.Two, new Vector2(200, 200));
            player2.LoadContent("TankPurple");

            enemies = new List<Enemy>(25);
            players = new List<Player>(4);

            players.Add(player1);
            players.Add(player2);

            //PATHFINDING//
            //map.LoadContent(Content);
            //enemy.LoadContent(Content);
            //PATHFINDING//


            backgroundTexture = Content.Load<Texture2D>("background");
            font = Content.Load<SpriteFont>("font");

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

            if (enemies.Count != 0)
                enemySpawnTime = .15f * enemies.Count;
            else
                enemySpawnTime = .1f;

            //Creates Enemies
            spawnEnemies(gameTime);
            
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
                currentEnemy.Update(gameTime, players);
            }

            //Detect Collisions
            detectCollisions();

            //PATHFINDING//
            //Counter++;
            //if (Counter == 10)
            //{
            //    pathFinder.IsSearching = !pathFinder.IsSearching;
            //}
            //
            //if (map.MapReload)
            //{
            //    map.ReloadMap();
            //    map.UpdateMapViewport(gameplayArea);
            //    enemy.Reset();
            //    pathFinder.Reset();
            //}
            //
            //if (pathFinder.SearchStatus == SearchStatus.PathFound && !enemy.Moving)
            //{
            //    foreach (Point point in pathFinder.FinalPath())
            //    {
            //        enemy.Waypoints.Enqueue(map.MapToWorld(point, true));
            //    }
            //    enemy.Moving = true;
            //}
            //pathFinder.Update(gameTime);
            //enemy.Update(gameTime);
            ////PATHFINDING//
            //
            //base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(backgroundTexture, new Vector2(0, 0), Color.White);
            for(int i = 0; i < enemies.Count; i++)
                enemies[i].Draw(spriteBatch);
            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch); 
            drawText();

            spriteBatch.End();

            //PATHFINDING
            //map.Draw(spriteBatch);
            //enemy.Draw(spriteBatch);
            //PATHFINDING//

            base.Draw(gameTime);
        }

        public void spawnEnemies(GameTime gameTime)
        {
            enemyTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (enemyTimer > enemySpawnTime && enemies.Count < 15)          /// Set enemy number here
            {
                int randWidth = rand.Next(this.GraphicsDevice.Viewport.Width);
                int randHeight = rand.Next(this.GraphicsDevice.Viewport.Height);
                Enemy spawn = new Enemy();      
                spawn.Initialize(this, new Vector2(randWidth, randHeight));
                spawn.LoadContent("TankEnemy");
                enemies.Add(spawn);
                enemyTimer = 0;
            }
        }

        public void detectCollisions()
        {
            //COLLISION DETECTION
            foreach (Player player in players)
            {
                Rectangle playerRect = new Rectangle((int)player.getPosition().X, (int)player.getPosition().Y, player.getTexture().Width, player.getTexture().Height);

                //Loop through all enemies
                for (int i = 0; i < enemies.Count; i++)
                {
                    //Get current enemy and create collision box
                    Enemy currentEnemy = enemies[i];
                    Rectangle currentEnemyRect = new Rectangle((int)currentEnemy.getPosition().X, (int)currentEnemy.getPosition().Y, currentEnemy.getTexture().Width, currentEnemy.getTexture().Height);

                    //Loop through all of current enemies projectiles
                    for (int j = 0; j < currentEnemy.getProjectiles().Count; j++)
                    {
                        Projectile proj = currentEnemy.getProjectiles()[j];
                        Rectangle enemyProjectileRect = new Rectangle((int)proj.getPosition().X, (int)proj.getPosition().Y, proj.getTexture().Width, proj.getTexture().Height);
                        
                        //Check if player is hit by any of current enemy's current projectile
                        if (playerRect.Intersects(enemyProjectileRect))
                        {
                            currentEnemy.getProjectiles().Remove(proj);
                            if(player.isShielded() == false)
                                player.Hit();
                        }

                        //Check if player's towers are hit by any of current enemy's current projectile
                        for (int k = 0; k < player.getTowers().Count; k++)
                        {
                            Tower tower = player.getTowers()[k];
                            Rectangle towerRect = new Rectangle((int)tower.getPosition().X, (int)tower.getPosition().Y, tower.getTexture().Width, tower.getTexture().Height);
                            if (towerRect.Intersects(enemyProjectileRect))
                            {
                                currentEnemy.getProjectiles().Remove(proj);
                                tower.Hit();
                                if(player.getTowers().Count != 0 && tower.isDead())
                                {
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
                    Rectangle currentEnemyRect = new Rectangle((int)currentEnemy.getPosition().X, (int)currentEnemy.getPosition().Y, currentEnemy.getTexture().Width, currentEnemy.getTexture().Height);

                    //Check if current enemy is hit by any of player's projectiles
                    for (int j = 0; j < player.getProjectiles().Count; j++)
                    {
                        Projectile proj = player.getProjectiles()[j];
                        Rectangle playerProjectileRect = new Rectangle((int)proj.getPosition().X, (int)proj.getPosition().Y, proj.getTexture().Width, proj.getTexture().Height);

                        if (currentEnemyRect.Intersects(playerProjectileRect))
                        {
                            player.getProjectiles().Remove(proj);
                            currentEnemy.Hit();
                            if (enemies.Count != 0 && enemies[i].isDead())
                            {
                                enemies.RemoveAt(i);
                                player.enemyDestroyed();
                            }
                        }
                    }
                }

                //Loop through all enemies
                for (int i = 0; i < enemies.Count; i++)
                {
                    //Get current enemy and create collision box
                    Enemy currentEnemy = enemies[i];
                    Rectangle currentEnemyRect = new Rectangle((int)currentEnemy.getPosition().X, (int)currentEnemy.getPosition().Y, currentEnemy.getTexture().Width, currentEnemy.getTexture().Height);

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
                                currentEnemy.Hit();
                                if (enemies.Count != 0 && enemies[i].isDead())
                                {
                                    enemies.RemoveAt(i);
                                    player.towerEnemyDestroyed();
                                }
                            }
                        }
                    }
                }
            }
        }
    

        //Draw Text method for debugging / displaying
        public void drawText()
        {
            //GamePadState gpstate = GamePad.GetState(PlayerIndex.One);
            //spriteBatch.Begin();

            spriteBatch.DrawString(font, "Player 1", new Vector2(player1.getPosition().X - 8f * 5f, player1.getPosition().Y - player1.getTurretLength() - 30f), Color.MediumBlue);
            spriteBatch.DrawString(font, "Player 2", new Vector2(player2.getPosition().X - 8f * 5f, player2.getPosition().Y - player2.getTurretLength() - 30f), Color.Purple);
            if(player1.isShielded())
                spriteBatch.DrawString(font, "Shield: " + (3 - (int)player1.getShieldTimer()), new Vector2(player1.getPosition().X - 9f * 5f, player1.getPosition().Y + player2.getTurretLength() + 10f), Color.MediumBlue);
            if (player2.isShielded())
                spriteBatch.DrawString(font, "Shield: " + (3 - (int)player2.getShieldTimer()), new Vector2(player2.getPosition().X - 9f * 5f, player2.getPosition().Y + player2.getTurretLength() + 10f), Color.Purple);

            spriteBatch.DrawString(font, "Player 1 Kills      : " + player1.getEnemiesDestroyed(), new Vector2(10, 15), Color.White);
            spriteBatch.DrawString(font, "Player 2 Kills      : " + player2.getEnemiesDestroyed(), new Vector2(510, 15), Color.White);
            
            spriteBatch.DrawString(font, "Player 1 Tower Kills: " + player1.getTowerEnemiesDestroyed(), new Vector2(10, 35), Color.White);
            spriteBatch.DrawString(font, "Player 2 Tower Kills: " + player2.getTowerEnemiesDestroyed(), new Vector2(510, 35), Color.White);
       
            spriteBatch.DrawString(font, "Player 1 Deaths     : " + player1.getTimesHit(), new Vector2(10, 55), Color.White);
            spriteBatch.DrawString(font, "Player 2 Deaths     : " + player2.getTimesHit(), new Vector2(510, 55), Color.White);

            foreach (Tower tower in player1.getTowers())
            {
                spriteBatch.DrawString(font, "" + (tower.getShotsToDestroy() - tower.getShotsTaken()), new Vector2(tower.getPosition().X - 5, tower.getPosition().Y - 60), Color.MediumBlue);
                spriteBatch.DrawString(font, "P1", new Vector2(tower.getPosition().X - 10, tower.getPosition().Y + 25), Color.MediumBlue);
            }
            foreach (Tower tower in player2.getTowers())
            {
                spriteBatch.DrawString(font, "" + (tower.getShotsToDestroy() - tower.getShotsTaken()), new Vector2(tower.getPosition().X - 5, tower.getPosition().Y - 60), Color.Purple);
                spriteBatch.DrawString(font, "P2", new Vector2(tower.getPosition().X - 10, tower.getPosition().Y + 25), Color.Purple);
            }
            
           // spriteBatch.End();
        }
    }
}
