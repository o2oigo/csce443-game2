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
        Dictionary<int, Texture2D> mapTextureDict;
        private Dictionary<int, Rectangle> rectDict;

        Video video;

        VideoPlayer player;
        Texture2D videoTexture;
        bool quickFixSound;

        public Rectangle getCurrentRectangle()
        {
            return rectDict[wave.CurrentLevel];
        }

        UserInterface userInterface;
        Camera camera;

        Player player1;
        Player player2;
        private Wave wave;
        public Wave Wave
        {
            get { return wave; }
        }
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
        private List<Lamp> lamps;
        public List<Lamp> Lamps
        {
            get { return lamps; }
        }

        Random rand = new Random();
        SpriteFont font;

        public ExplosionParticleSystem explosion;
        // public ExplosionParticleSystem explosion2;
        public ExplosionSmokeParticleSystem smoke;

        int live = 100;

        public FireParticleSystem fire;
        public FireParticleSystem fireTower;
        public LightningParticleSystem lightning;
        public FlameTowerSmokeParticleSystem flameTowerSmoke;
        public IceParticleSystem ice;
        public LampParticleSystem light;

        Dictionary<string, SoundEffect> music;
     //   SoundEffect soundeffect;
      //  SoundEffectInstance songInstance;

        SoundEffect menuSound;
        SoundEffect level1Sound;
        SoundEffect level2Sound;
        SoundEffect level3Sound;
        public SoundEffectInstance menuSoundInstance;
        public SoundEffectInstance level1SoundInstance;
        public SoundEffectInstance level2SoundInstance;
        public SoundEffectInstance level3SoundInstance;

        float lampTimer = .8f;
        float lampElapsedTime = 5f;

       // Song menuSong;
      //  Song level1Song; 

        private Map map;
        public Map Map
        {
            get { return map; }
        }
        //private Rectangle gameplayArea;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferHeight = 1024;
            this.graphics.PreferredBackBufferWidth = 1280;
            //this.graphics.IsFullScreen = true;
            
            map = new Map();
  
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
            //gameplayArea = GraphicsDevice.Viewport.TitleSafeArea;
            //map.UpdateMapViewport(test);
            //map.ReloadMap();
            CreateTrees();
            CreateLamps();
            userInterface.setScreenStatus("loadingGameScreen1",true);
            //wave = new Wave(this);
            wave = new Wave(this,userInterface);
            quickFixSound = false;
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
            lamps = new List<Lamp>();
            //snowList = new List<Snow>();

            player1 = new Player();
            player1.Initialize(this, PlayerIndex.One, new Vector2(300, 400));
            player1.LoadContent("wizard");

            player2 = new Player();
            player2.Initialize(this, PlayerIndex.Two, new Vector2(200, 200));
            player2.LoadContent("wizard");


            players.Add(player1);
            players.Add(player2);


            userInterface = new UserInterface();
            userInterface.Initialize(this, PlayerIndex.One, new Vector2(100, 100), players);
            userInterface.LoadContent();

            map.LoadContent(Content);

            //background//
            mapTextureDict = new Dictionary<int, Texture2D>();
            mapTextureDict.Add(1, Content.Load<Texture2D>("levelOne"));
            mapTextureDict.Add(2, Content.Load<Texture2D>("levelTwo"));
            mapTextureDict.Add(3, Content.Load<Texture2D>("levelThree"));
            backgroundTexture = mapTextureDict[1];

            rectDict = new Dictionary<int, Rectangle>();
            rectDict.Add(1, new Rectangle(0,0,1280,1024));
            rectDict.Add(2, new Rectangle(0,0,1984, 1536));
            rectDict.Add(3, new Rectangle(0,0,1984, 1536));
            wave = new Wave(this, userInterface);

            map.UpdateMapViewport(rectDict[1]);
            map.ReloadMap();

            font = Content.Load<SpriteFont>("font");

            camera = new Camera(GraphicsDevice.Viewport);
            camera.Initialize(this, PlayerIndex.One, new Vector2(100, 100), players);

            explosion = new ExplosionParticleSystem(this, 10000, camera);
            Components.Add(explosion);

            smoke = new ExplosionSmokeParticleSystem(this, 10000, camera);
            Components.Add(smoke);

            flameTowerSmoke = new FlameTowerSmokeParticleSystem(this, 10000, camera);
            Components.Add(flameTowerSmoke);

            fire = new FireParticleSystem(this, 10000, camera);
            Components.Add(fire);

            ice = new IceParticleSystem(this, 10000, camera);
            Components.Add(ice);

            fireTower = new FireParticleSystem(this, 10000, camera);
            fireTower.setSpeed(500, 600);
            Components.Add(fireTower);

            lightning = new LightningParticleSystem(this, 20, camera);
            Components.Add(lightning);

            light = new LampParticleSystem(this, 100, camera);
            Components.Add(light);

           /* soundeffect = Content.Load<SoundEffect>("MainSong");
            music = new Dictionary<string, SoundEffect>();
            music.Add("song", soundeffect);
            //songInstance = new SoundEffectInstance();
            if (songInstance != null)
                songInstance.Dispose();
            songInstance = music["song"].CreateInstance();
            songInstance.IsLooped = true;
            songInstance.Play();

            soundeffect.Play();
            */

           // menuSong = Content.Load<Song>("Sound/menuSong");  
           // level1Song = Content.Load<Song>("Sound/level1Song");
            menuSound = Content.Load<SoundEffect>("Sound/menuSong");
            level1Sound = Content.Load<SoundEffect>("Sound/level1Song");
            level2Sound = Content.Load<SoundEffect>("Sound/level2Song");
            level3Sound = Content.Load<SoundEffect>("Sound/level3Song");
            music = new Dictionary<string, SoundEffect>();
            music.Add("menuSound", menuSound);
            music.Add("level1Sound", level1Sound);
            music.Add("level2Sound", level2Sound);
            music.Add("level3Sound", level3Sound);

            if (menuSoundInstance != null)
                menuSoundInstance.Dispose();
            menuSoundInstance = music["menuSound"].CreateInstance();
            menuSoundInstance.IsLooped = true;
            if (level1SoundInstance != null)
                level1SoundInstance.Dispose();
            level1SoundInstance = music["level1Sound"].CreateInstance();
            level1SoundInstance.IsLooped = true; 
            if (level2SoundInstance != null)
                level2SoundInstance.Dispose();
            level2SoundInstance = music["level2Sound"].CreateInstance();
            level2SoundInstance.IsLooped = true;     
            if (level3SoundInstance != null)
                level3SoundInstance.Dispose();
            level3SoundInstance = music["level3Sound"].CreateInstance();
            level3SoundInstance.IsLooped = true;

           // menuSoundInstance.Play();

            video = Content.Load<Video>("intro");
            player = new VideoPlayer();

            player.IsLooped = false;
            player.Play(video);
            Sprite.LoadContent(Content);
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
            if (player.State == MediaState.Stopped)
            {
                if (!quickFixSound)
                {
                    menuSoundInstance.Play();
                    quickFixSound = true;
                }

                float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                // Allows the game to exit
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();

                explosion.Update(gameTime);
                smoke.Update(gameTime);
                flameTowerSmoke.Update(gameTime);
                fire.Update(gameTime);
                lightning.Update(gameTime);
                fireTower.Update(gameTime);
                ice.Update(gameTime);
                light.Update(gameTime);


                userInterface.setWavesNumber(wave.CurrentWave);


                if (userInterface.getScreen("restartGame") == true)
                {
                    restartGame();
                    userInterface.setScreenStatus("restartGame", false);
                }
                userInterface.Update();

                if (userInterface.getScreen("showGameScreen") == true)
                {

                    camera.Update(gameTime);
                    wave.Update(gameTime);

                    //Update Player

                    player1.Update(gameTime, enemies);
                    // player2.addOtherPlayerTowers(player1.getTowers());
                    player2.Update(gameTime, enemies);
                    // player1.addOtherPlayerTowers(player2.getTowers());
                    Player.updateTowers(gameTime, enemies);


                    //Update Enemies
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        //Get current enemy and update
                        Enemy currentEnemy = enemies[i];
                        currentEnemy.Update(gameTime, Player.getTowers());
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

                    //Update Lamp Particles
                    lampElapsedTime += elapsedTime;
                    if (lampElapsedTime >= lampTimer)
                    {
                        lampElapsedTime = 0f;
                        foreach (Lamp lamp in lamps)
                            light.AddParticles(new Vector2(lamp.Position.X + 21, lamp.Position.Y - 27));
                    }

                    //Detect Collisions
                    detectCollisions();

                    checkDeadEnemies();
                    Sprite.SortSprite();
                    //updateTowers();

                }

                if (live < 1)
                {
                    userInterface.setScreenStatus("showGameOverScreen", true);
                    live = 10;
                }
                else if (wave.isLevelFinish == true && wave.CurrentLevel == 1 && userInterface.getScreen("showGameScreen") == true)
                {
                    userInterface.setScreenStatus("showGameScreen", false);
                    userInterface.setScreenStatus("showLevel2Screen", true);
                    resetGame();
                }
                else if (wave.isLevelFinish == true && wave.CurrentLevel == 2 && userInterface.getScreen("showGameScreen") == true)
                {
                    userInterface.setScreenStatus("showGameScreen", false);
                    userInterface.setScreenStatus("showLevel3Screen", true);
                    resetGame();
                }
                else if (wave.isGameFinish == true && userInterface.getScreen("showGameScreen") == true)
                {
                    userInterface.setScreenStatus("showWinScreen", true);
                }

                if (userInterface.getScreen("showWinScreen") && video != null)// && !quickVidFix)
                {
                    video = Content.Load<Video>("ending");
                    player.IsLooped = false;
                    player.Play(video);
                    video = null;
                    //quickVidFix = true;
                    //player.IsLooped = false;
                    //userInterface.ShowWinScreen = false;
                }
                else if (Wave.isGameFinish && video == null)
                {
                    //masterReset();
                    this.Exit();
                }
            }
            //else if (Keyboard.GetState().IsKeyDown(Keys.Delete)) ;

            //base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.ViewMatrix);
           
            if (userInterface.getScreen("showGameScreen") == true || userInterface.getScreen("showPauseScreen") == true)
            {
                spriteBatch.Draw(backgroundTexture, rectDict[wave.CurrentLevel], Color.White);

                //foreach (Tower tower in player1.getTowers())
                //    tower.Draw(spriteBatch);
                //foreach (Tower tower in player2.getTowers())
                //    tower.Draw(spriteBatch);
                //
                //for (int i = 0; i < enemies.Count; i++)
                //    enemies[i].Draw(spriteBatch);

                //player1.Draw(spriteBatch);
                //player2.Draw(spriteBatch);

                Sprite.DrawT(spriteBatch);

                spriteBatch.End();
                base.Draw(gameTime);
                spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, camera.ViewMatrix);

                //foreach (Tree t in trees)
                //{
                //    t.Draw(spriteBatch);
                //}
                map.Draw(spriteBatch);

                //player2.Draw(spriteBatch);
                drawText();

                if (stones.Count() > 0)
                {
                    foreach (Stone i in stones) i.Draw(spriteBatch);
                }

                foreach (Enemy e in enemies)
                {
                    e.DrawHPBarEnemy(spriteBatch);
                }

               // foreach (Player p in players)
              //  {
                    foreach (Tower t in Player.getTowers())
                    {
                        t.DrawHPBarTower(spriteBatch);
                    }
              //  }

                foreach (Player p in players)
                {
                    p.DrawBuildMenu(spriteBatch);
                }
            }
            spriteBatch.End();
            // note

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            userInterface.Draw(spriteBatch);   
            spriteBatch.End();

            Rectangle screen = new Rectangle(GraphicsDevice.Viewport.X,
         GraphicsDevice.Viewport.Y,
        GraphicsDevice.Viewport.Width,
        GraphicsDevice.Viewport.Height);

            // Draw the video, if we have a texture to draw.
            if (player.State != MediaState.Stopped)
            {
                videoTexture = player.GetTexture();
                spriteBatch.Begin();
                spriteBatch.Draw(videoTexture, screen, Color.White);
                spriteBatch.End();
            }

        }

        public void detectCollisions()
        {
            //COLLISION DETECTION
            foreach (Player player in players)
            {
                Rectangle playerRect = new Rectangle((int)(player.Position.X - player.getOrigin().X), (int)(player.Position.Y - player.getOrigin().Y), player.Size.Width, player.Size.Height);

                //Loop through all player's tower
                for (int k = 0; k < Player.getTowers().Count; k++)
                {
                    Tower tower = Player.getTowers()[k];
                    Rectangle towerRect = new Rectangle((int)(tower.Position.X - tower.getOrigin().X), (int)(tower.Position.Y - tower.getOrigin().Y), tower.getTexture().Width, tower.getTexture().Height);

                    //Check if player stand near a tower
                    if (towerRect.Intersects(playerRect))
                    {
                        tower.setPlayerIsNear(true, player.getPlayerIndex());
                    }
                    else
                    {
                        tower.setPlayerIsNear(false, player.getPlayerIndex());
                    }
                }

                //Loop through all stones
                for (int j = 0; j < stones.Count; j++)
                {
                    Rectangle currentStoneRect = new Rectangle((int)stones[j].Position.X, (int)stones[j].Position.Y, stones[j].Texture.Width, stones[j].Texture.Height);
                    if (playerRect.Intersects(currentStoneRect))
                    {
                        if (stones[j].Type == ElementType.Normal)
                        {
                            Player.addMoney(3);
                        }
                        else
                        {
                            Player.addStoneToInventory(stones[j]);
                        }
                        stones.Remove(stones[j]);
                    }
                }
            }

                //Loop through all enemies
                //Rectangle houseRect = new Rectangle((int)House.Origin.X - (House.Texture.Width / 2) + 30, (int)House.Origin.Y - 40, House.Texture.Width - 30, House.Texture.Height / 4);
                for (int i = 0; i < enemies.Count; i++)
                {
                    //Get current enemy and create collision box
                    Enemy currentEnemy = enemies[i];
                    //Ckeck if current enemy and exit point intersects                   
                    //if (map.TileTypeAt(currentEnemy.Position) == MapTileType.MapExit)
                    if (currentEnemy.boundingCircle(currentEnemy.Position,60,Map.MapToWorld(Map.EndTile,false)))
                    {
                        live--;
                        Sprite.removeList(enemies[i]);
                        enemies.RemoveAt(i);
                    }

                    //Loop through all of current enemies projectiles
                    for (int j = 0; j < currentEnemy.getProjectiles().Count; j++)
                    {
                        Projectile proj = currentEnemy.getProjectiles()[j];
                        Rectangle enemyProjectileRect = new Rectangle((int)(proj.getPosition().X - proj.getOrigin().X), (int)(proj.getPosition().Y - proj.getOrigin().Y), proj.getTexture().Width, proj.getTexture().Height);

                        //Check if player is hit by any of current enemy's current projectile
                       /* if (playerRect.Intersects(enemyProjectileRect))
                        {
                            currentEnemy.getProjectiles().Remove(proj);
                            if (player.isShielded() == false)
                                player.Hit();
                        }*/

                        //Check if player's towers are hit by any of current enemy's current projectile
                        for (int k = 0; k < Player.getTowers().Count; k++)
                        {
                            Tower tower = Player.getTowers()[k];
                            Rectangle towerRect = new Rectangle((int)(tower.Position.X - tower.getOrigin().X), (int)(tower.Position.Y - tower.getOrigin().Y), tower.getTexture().Width, tower.getTexture().Height);
                            if (towerRect.Intersects(enemyProjectileRect))
                            {
                                currentEnemy.getProjectiles().Remove(proj);
                                tower.Hit(currentEnemy.AttackDamage);
                                if (Player.getTowers().Count != 0 && tower.isDead())
                                {
                                    if (Player.getTowers()[k].getTowerName() == "Flame Tower")
                                    {
                                        FlameTower temp = (FlameTower)Player.getTowers()[k];
                                        temp.getSound().Stop();
                                    }
                                    else if (Player.getTowers()[k].getTowerName() == "Ice Tower")
                                    {
                                        IceTower temp = (IceTower)Player.getTowers()[k];
                                        temp.getSound().Stop();
                                    }
                                    Sprite.removeList(tower);
                                    Player.getTowers().RemoveAt(k);

                                }
                            }
                        }
                    }
                }

               /* //Loop through all enemies
                for (int i = 0; i < enemies.Count; i++)
                {
                    //Get current enemy and create collision box
                    Enemy currentEnemy = enemies[i];
                    Rectangle currentEnemyRect = new Rectangle((int)(currentEnemy.Position.X - currentEnemy.getOrigin().X), (int)(currentEnemy.Position.Y - currentEnemy.getOrigin().Y), currentEnemy.Size.Width, currentEnemy.Size.Height);


                    //Check if current enemy is hit by any of player's projectiles
                    for (int j = 0; j < player.getProjectiles().Count; j++)
                    {
                        Projectile proj = player.getProjectiles()[j];
                        Rectangle playerProjectileRect = new Rectangle((int)(proj.getPosition().X - proj.getOrigin().X), (int)(proj.getPosition().Y - proj.getOrigin().Y), proj.getTexture().Width, proj.getTexture().Height);

                        if (currentEnemyRect.Intersects(playerProjectileRect))
                        {            
                            player.getProjectiles().Remove(proj);
                            currentEnemy.Hit(1.0f);
                        }
                    }
                }*/

                //Loop through all enemies
                for (int i = 0; i < enemies.Count; i++)
                {
                    //Get current enemy and create collision box
                    Enemy currentEnemy = enemies[i];
                    Rectangle currentEnemyRect = new Rectangle((int)(currentEnemy.Position.X - currentEnemy.getOrigin().X), (int)(currentEnemy.Position.Y - currentEnemy.getOrigin().Y), currentEnemy.Size.Width, currentEnemy.Size.Height);


                    //Check if current enemy is hit by any of player's tower's projectiles
                    for (int k = 0; k < Player.getTowers().Count; k++)
                    {
                        Tower tower = Player.getTowers()[k];
                        for (int j = 0; j < tower.getProjectiles().Count; j++)
                        {
                            Projectile proj = tower.getProjectiles()[j];
                            Rectangle towerProjectileRect = new Rectangle((int)(proj.getPosition().X - proj.getOrigin().X), (int)(proj.getPosition().Y - proj.getOrigin().Y), proj.getTexture().Width, proj.getTexture().Height);

                            if (currentEnemyRect.Intersects(towerProjectileRect))
                            {
                                if (tower.damage.type != ElementType.Fire)
                                {
                                    explosion.AddParticles(enemies[i].Position);
                                    smoke.AddParticles(enemies[i].Position);
                                }
                                tower.getProjectiles().Remove(proj);
                                currentEnemy.Hit(tower.Damage);
                            }
                        }
                    }
                }
                

                //Ice Tower Range Collision
                for (int i = 0; i < enemies.Count; i++)
                { 
                   // enemies[i].MoveSpeed = .5f;     
                    foreach (Tower tower in Player.getTowers())
                    {   
                        if (tower.getTowerName() == "Ice Tower")
                        {
                            IceTower temp = (IceTower)tower;
                            if (Vector2.Distance(enemies[i].Position, tower.Position) <= tower.getRange())
                            {
                                //temp.getSound().Play();
                                if (enemies[i].EffectStn is EnemyEffectStun) 
                                {
                                   // enemies[i].MoveSpeed = 0f;
                                }
                                else
                                {
                                    enemies[i].MoveSpeed = .2f;
                                }
                            }
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
                    int randNum = rand.Next(0, 12);
                    if (randNum < 4)
                    {
                        Stone newStone = new Stone();
                        newStone.Initialize(this, enemies[i].Position, randNum);
                        stones.Add(newStone);
                    }
                    Sprite.removeList(enemies[i]);
                    explosion.AddParticles(enemies[i].Position);
                    smoke.AddParticles(enemies[i].Position);
                    enemies.RemoveAt(i);
                    Player.addMoney(3);
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
            //if (player1.isShielded())
            //    spriteBatch.DrawString(font, "Shield: " + ((int)player1.getShieldTimeLimit() - (int)player1.getShieldTimer()), new Vector2(player1.Position.X - 9f * 5f, player1.Position.Y + player1.getTurretLength() + 10f), Color.MediumBlue);
            //  if (player2.isShielded())
            //      spriteBatch.DrawString(font, "Shield: " + (3 - (int)player2.getShieldTimer()), new Vector2(player2.getPosition().X - 9f * 5f, player2.getPosition().Y + player2.getTurretLength() + 10f), Color.Purple);

            //spriteBatch.DrawString(font, "Player 1 Kills      : " + player1.getEnemiesDestroyed(), new Vector2(width - 400, 15), Color.White);
            ////    spriteBatch.DrawString(font, "Player 2 Kills      : " + player2.getEnemiesDestroyed(), new Vector2(510, 15), Color.White);
            //spriteBatch.DrawString(font, "Timer      : " + wave.WaveTimer, new Vector2(500, 200), Color.MediumBlue);
            //spriteBatch.DrawString(font, "Player 1 Tower Kills: " + player1.getTowerEnemiesDestroyed(), new Vector2(width - 400, 35), Color.White);
            ////   spriteBatch.DrawString(font, "Player 2 Tower Kills: " + player2.getTowerEnemiesDestroyed(), new Vector2(510, 35), Color.White);
            //
            //spriteBatch.DrawString(font, "Player 1 Deaths     : " + player1.getTimesHit(), new Vector2(width - 400, 55), Color.White);
            ////   spriteBatch.DrawString(font, "Player 2 Deaths     : " + player2.getTimesHit(), new Vector2(510, 55), Color.White);
            if (player1.getMaxTower() == true)
            {
                spriteBatch.DrawString(font, "Tower Limit Reached", new Vector2(500, 35), Color.Black);
            }

            //foreach (Tower tower in player1.getTowers())
            //{
            //    if (tower.getPlayerIsNear() == true)
            //        spriteBatch.DrawString(font, "Player near tower", new Vector2(500, 55), Color.Black);
            //    // spriteBatch.DrawString(font, "" + (tower.getShotsToDestroy() - tower.getShotsTaken()), new Vector2(tower.getPosition().X - 5, tower.getPosition().Y - 60), Color.MediumBlue);
            //    //d spriteBatch.DrawString(font, "P1", new Vector2(tower.getPosition().X - 10, tower.getPosition().Y + 25), Color.MediumBlue);
            //}
            //spriteBatch.DrawString(font, "TILE: " + map.TileString(new Vector2(player1.Position.X, player1.Position.Y)), new Vector2(500, 100), Color.MediumBlue);
            //spriteBatch.DrawString(font, "SPRITE COUNT: " + Sprite.objCount(), new Vector2(500, 150), Color.MediumBlue);
            //  spriteBatch.DrawString(font, "Player 1 Shoot Angle     : " + player1.getShootRotationAngle(), new Vector2(510, 75), Color.Black);
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
                //Enemy currentEnemy = enemies[i];
                Sprite.removeList(enemies[i]);
                enemies.RemoveAt(i);
            }
            //foreach (Player player in players)
            //{
                for (int k = 0; k < Player.getTowers().Count; k++)
                {
                    //Tower tower = player1.getTowers()[k];
                    if (Player.getTowers()[k].getTowerName() == "Flame Tower")
                    {
                        FlameTower temp = (FlameTower)Player.getTowers()[k];
                        temp.getSound().Stop();
                    }
                    else if (Player.getTowers()[k].getTowerName() == "Ice Tower")
                    {
                        IceTower temp = (IceTower)Player.getTowers()[k];
                        temp.getSound().Stop();
                    }
                    Sprite.removeList(Player.getTowers()[k]);
                    Player.getTowers().RemoveAt(k);
                }
            //}

            player1.restartGameLevel1();
            player2.restartGameLevel1();
            //enemies.Clear();
            stones.Clear();
            wave = new Wave(this,userInterface);

            this.ResetElapsedTime();
            live = 100;

            camera.ResetCamera();
        }

        public void resetTower()
        {
          //  foreach (Player player in players)
          //  {
                for (int k = 0; k < Player.getTowers().Count; k++)
                {
                    //Tower tower = player1.getTowers()[k];
                    if (Player.getTowers()[k].getTowerName() == "Flame Tower")
                    {
                        FlameTower temp = (FlameTower)Player.getTowers()[k];
                        temp.getSound().Stop();
                    }
                    else if (Player.getTowers()[k].getTowerName() == "Ice Tower")
                    {
                        IceTower temp = (IceTower)Player.getTowers()[k];
                        temp.getSound().Stop();
                    }
                    Sprite.removeList(Player.getTowers()[k]);
                    Player.getTowers().RemoveAt(k);

                }
          //  }
        }

        public void resetGame()
        {
            /*
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                Enemy currentEnemy = enemies[i];
                Sprite.removeList(enemies[i]);
                enemies.RemoveAt(i);
            }
            */
            resetTower();
            player1.restartGameLevel1();
            player2.restartGameLevel1();
            //enemies.Clear();
            stones.Clear();
            //wave = new Wave(this, userInterface);
            
            this.ResetElapsedTime();

            live = 100;

            camera.ResetCamera();
        }

        private void ResetCamera()
        {
            camera.Zoom = 1f;
            camera.Position = Vector2.Zero;
        }

        public int getLive()
        {
            return live;
        }

        private static Rectangle CalculateFrameRectangle(int width, int height, int columns, int rows, int frame)
        {
            int tileWidth = width / columns;
            int tileHeight = height / rows;
            int x = frame % columns;
            int y = frame / columns;
            return new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
        }

        public void CreateTrees()
        {
            foreach (Tree t in trees)
            {
                Sprite.removeList(t);
            }
            trees.Clear();
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

        public void CreateLamps()
        {
            foreach (Lamp l in lamps)
            {
                Sprite.removeList(l);
            }
            lamps.Clear();
            foreach (Point pt in map.getLamps())
            {
                Vector2 tilePosition = map.MapToWorld(pt.X, pt.Y, true);
                lamps.Add(new Lamp(this, tilePosition));
            }
            foreach (Lamp l in lamps)
            {
                int i = rand.Next(8);
                l.LoadContent();
            }
        }


        public void goNextLevel()
        {
            wave.nextLevel();
            backgroundTexture = mapTextureDict[wave.CurrentLevel];
         //   foreach (Player player in players)
          //  {
            for (int k = 0; k < Player.getTowers().Count; k++)
                {
                    //Tower tower = player1.getTowers()[k];
                    if (Player.getTowers()[k].getTowerName() == "Flame Tower")
                    {
                        FlameTower temp = (FlameTower)Player.getTowers()[k];
                        temp.getSound().Stop();
                    }
                    else if (Player.getTowers()[k].getTowerName() == "Ice Tower")
                    {
                        IceTower temp = (IceTower)Player.getTowers()[k];
                        temp.getSound().Stop();
                    }
                }
          //  }
        }


        public void masterReset()
        {
            map = new Map();
            Initialize();
            LoadContent();
        }


    }
}
