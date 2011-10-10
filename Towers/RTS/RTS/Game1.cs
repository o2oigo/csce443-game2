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
        Player player1;
        Player player2;
        //Enemy enemy;
        List<Enemy> enemies;
        List<Player> players;
        float enemyTimer = 0;
        Random rand = new Random();
        public ExplosionParticleSystem explosion;
        public ExplosionParticleSystem explosion2;
        public ExplosionSmokeParticleSystem smoke;

        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 768*3;
            this.graphics.PreferredBackBufferWidth = 1024*3;
            this.graphics.IsFullScreen = true;

            explosion = new ExplosionParticleSystem(this, 1);
            Components.Add(explosion);

            smoke = new ExplosionSmokeParticleSystem(this, 2);
            Components.Add(smoke);       
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
            player2.LoadContent("TankPlayer");

            enemies = new List<Enemy>(5);
            players = new List<Player>(4);

            players.Add(player1);
            players.Add(player2);

           
            font = Content.Load<SpriteFont>("font");
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

            //Creates Enemies
            spawnEnemies(gameTime);
            
            //Update Player
            foreach (Player player in players)
            {
                player.Update(gameTime);
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
         
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            player1.Draw(spriteBatch);
            player2.Draw(spriteBatch);
            for(int i = 0; i < enemies.Count; i++)
                enemies[i].Draw(spriteBatch);
            drawText();
            base.Draw(gameTime);
        }

        public void spawnEnemies(GameTime gameTime)
        {
            enemyTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (enemyTimer > .9f && enemies.Count < 25)          /// Set enemy number here
            {
                int random = rand.Next(100);
                Enemy spawn = new Enemy();
                if (random % 4 == 0)
                    spawn.Initialize(this, new Vector2(300, 300));
                if (random % 4 == 1)
                    spawn.Initialize(this, new Vector2(this.GraphicsDevice.Viewport.Width - 300, 300));
                if (random % 4 == 2)
                    spawn.Initialize(this, new Vector2(100, this.GraphicsDevice.Viewport.Height - 300));
                if (random % 4 == 3)
                    spawn.Initialize(this, new Vector2(this.GraphicsDevice.Viewport.Width - 300, this.GraphicsDevice.Viewport.Height - 300));

                spawn.LoadContent("Tank");
                enemies.Add(spawn);
                enemyTimer = 0;
            }
        }

        public void detectCollisions()
        {
            //COLLISION DETECTION
            Rectangle playerRect = new Rectangle((int)player1.getPosition().X, (int)player1.getPosition().Y, player1.getTexture().Width, player1.getTexture().Height);
            Rectangle player2Rect = new Rectangle((int)player2.getPosition().X, (int)player2.getPosition().Y, player2.getTexture().Width, player2.getTexture().Height);

            //Loop through all enemies
            for (int i = 0; i < enemies.Count; i++)
            {
                //Get current enemy and create collision box
                Enemy currentEnemy = enemies[i];  
                Rectangle currentEnemyRect = new Rectangle((int)currentEnemy.getPosition().X, (int)currentEnemy.getPosition().Y, currentEnemy.getTexture().Width, currentEnemy.getTexture().Height);

                //Check if player is hit by any of current enemy's projectiles
                for (int j = 0; j < currentEnemy.getProjectiles().Count; j++)
                {
                    Projectile proj = currentEnemy.getProjectiles()[j];
                    Rectangle enemyProjectileRect = new Rectangle((int)proj.getPosition().X, (int)proj.getPosition().Y, proj.getTexture().Width, proj.getTexture().Height);
                    if (playerRect.Intersects(enemyProjectileRect))
                    {
                        currentEnemy.getProjectiles().Remove(proj);
                        player1.Hit();
                    }
                    if (player2Rect.Intersects(enemyProjectileRect))
                    {
                        currentEnemy.getProjectiles().Remove(proj);
                        player2.Hit();
                    }
                }

                //Check if current enemy is hit by any of player's projectiles
                for (int j = 0; j < player1.getProjectiles().Count; j++)
                {
                    Projectile proj = player1.getProjectiles()[j];
                    Rectangle playerProjectileRect = new Rectangle((int)proj.getPosition().X, (int)proj.getPosition().Y, proj.getTexture().Width, proj.getTexture().Height);
                    
                    if (currentEnemyRect.Intersects(playerProjectileRect))
                    {
                        player1.getProjectiles().Remove(proj);
                        currentEnemy.Hit();
                        if (enemies[i].isDead())
                        {
                            enemies.RemoveAt(i);
                            player1.enemyDestroyed();
                        }
                    }
                }
                for (int j = 0; j < player2.getProjectiles().Count; j++)
                {
                    Projectile proj = player2.getProjectiles()[j];
                    Rectangle player2ProjectileRect = new Rectangle((int)proj.getPosition().X, (int)proj.getPosition().Y, proj.getTexture().Width, proj.getTexture().Height);

                    if (currentEnemyRect.Intersects(player2ProjectileRect))
                    {
                        player2.getProjectiles().Remove(proj);
                        currentEnemy.Hit();
                        if (enemies[i].isDead())
                        {
                            enemies.RemoveAt(i);
                            player2.enemyDestroyed();
                        }
                    }
                }
            }
        }

        //Draw Text method for debugging / displaying
        public void drawText()
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(font, "Player 1 Enemies Destroyed: " + player1.getEnemiesDestroyed(), new Vector2(30, 45), Color.White);
            spriteBatch.DrawString(font, "Player 2 Enemies Destroyed: " + player2.getEnemiesDestroyed(), new Vector2(530, 45), Color.White);
       
            spriteBatch.DrawString(font, "Player 1 Deaths: " + player1.getTimesHit(), new Vector2(30, 75), Color.White);
            spriteBatch.DrawString(font, "Player 2 Deaths: " + player2.getTimesHit(), new Vector2(530, 75), Color.White);        
            spriteBatch.End();
        }
    }
}
