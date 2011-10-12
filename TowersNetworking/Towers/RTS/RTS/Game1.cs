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
using Microsoft.Xna.Framework.Net;


namespace RTS
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        NetworkSession networkSession;
        AvailableNetworkSessionCollection availableSessions;
        int selectedSessionIndex;
        PacketReader packetReader = new PacketReader();
        PacketWriter packetWriter = new PacketWriter();

        GamePadState currentState;
        GamePadState lastState;

        //Player localPlayer;
        //Player player1;
        //Player player2;
        Player player;

        List<Enemy> enemies;
        List<Player> players;
        float enemyTimer = 0;

        Random rand = new Random();
        public ExplosionParticleSystem explosion;
        public ExplosionParticleSystem explosion2;
        public ExplosionSmokeParticleSystem smoke;

        SpriteFont font;

        String localPlayerTag;
        bool firstTime = true;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            this.graphics.PreferredBackBufferWidth = 768;
            this.graphics.PreferredBackBufferWidth = 1024;
            //this.graphics.IsFullScreen = true;

            // Add Gamer Services
            Components.Add(new GamerServicesComponent(this));

            // Respond to the SignedInGamer event
            SignedInGamer.SignedIn +=
                new EventHandler<SignedInEventArgs>(SignedInGamer_SignedIn);

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

            //player1 = new Player();
            //player1.Initialize(this, new Vector2(100,100));
            //player1.LoadContent("TankPlayer");

            //player2 = new Player();
           // player2.Initialize(this, new Vector2(200, 200));
            //player2.LoadContent("TankPlayer");

            enemies = new List<Enemy>(5);
            players = new List<Player>(4);

            //players.Add(localPlayer);
           // players.Add(player2);

           
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
            
            if (!Guide.IsVisible)
            {
                foreach (SignedInGamer signedInGamer in
                    SignedInGamer.SignedInGamers)
                {               
                    Player player = signedInGamer.Tag as Player;
                    lastState = player.oldState;
                    currentState = GamePad.GetState(signedInGamer.PlayerIndex);

                    if (networkSession != null)
                    {
                        if (networkSession.SessionState ==
                            NetworkSessionState.Lobby)
                            HandleLobbyInput();
                        else
                        {
                            if (signedInGamer.Gamertag == localPlayerTag)
                            {
                                this.player = player;
                                HandleGameplayInput(player, gameTime);
                            }
                        }
                    }
                    else if (availableSessions != null)
                    {
                        HandleAvailableSessionsInput();
                    }
                    else
                    {
                        HandleTitleScreenInput();
                    }
                    player.oldState = currentState;
                }
            }
           /* //Creates Enemies
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
            detectCollisions();*/
         
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);
            if (networkSession != null)
            {
                // Draw the Lobby
                if (networkSession.SessionState == NetworkSessionState.Lobby)
                    DrawLobby();
                else
                    DrawGameplay(gameTime);
            }
            else if (availableSessions != null)
            {
                DrawAvailableSessions();
            }
            else
            {
                DrawTitleScreen();
            }

           // player1.Draw(spriteBatch);
           //player2.Draw(spriteBatch);
           // for(int i = 0; i < enemies.Count; i++)
            //    enemies[i].Draw(spriteBatch);
           // drawText();
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
            Rectangle playerRect = new Rectangle((int)player.getPosition().X, (int)player.getPosition().Y, player.getTexture().Width, player.getTexture().Height);
            //Rectangle player2Rect = new Rectangle((int)player2.getPosition().X, (int)player2.getPosition().Y, player2.getTexture().Width, player2.getTexture().Height);

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
                        player.Hit();
                    }
                    if (playerRect.Intersects(enemyProjectileRect))
                    {
                        currentEnemy.getProjectiles().Remove(proj);
                        player.Hit();
                    }
                }

                //Check if current enemy is hit by any of player's projectiles
                for (int j = 0; j < player.getProjectiles().Count; j++)
                {
                    Projectile proj = player.getProjectiles()[j];
                    Rectangle playerProjectileRect = new Rectangle((int)proj.getPosition().X, (int)proj.getPosition().Y, proj.getTexture().Width, proj.getTexture().Height);
                    
                    if (currentEnemyRect.Intersects(playerProjectileRect))
                    {
                        player.getProjectiles().Remove(proj);
                        currentEnemy.Hit();
                        if (enemies[i].isDead())
                        {
                            enemies.RemoveAt(i);
                            player.enemyDestroyed();
                        }
                    }
                }
               /* for (int j = 0; j < player2.getProjectiles().Count; j++)
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
                }*/
            }
        }

        //Draw Text method for debugging / displaying
        public void drawText()
        {
            spriteBatch.Begin();
            foreach (LocalNetworkGamer gamer in networkSession.LocalGamers)
            {
                Player player = gamer.Tag as Player;
                spriteBatch.DrawString(font, localPlayerTag, new Vector2(player.getPosition().X - localPlayerTag.Length * 4.5f, player.getPosition().Y - player.getTurretLength() - 30), Color.White);
            }
            //spriteBatch.DrawString(font, "Player 1 Enemies Destroyed: " + player1.getEnemiesDestroyed(), new Vector2(30, 45), Color.White);
            //spriteBatch.DrawString(font, "Player 2 Enemies Destroyed: " + player2.getEnemiesDestroyed(), new Vector2(530, 45), Color.White);
       
            //spriteBatch.DrawString(font, "Player 1 Deaths: " + player1.getTimesHit(), new Vector2(30, 75), Color.White);
            //spriteBatch.DrawString(font, "Player 2 Deaths: " + player2.getTimesHit(), new Vector2(530, 75), Color.White);        
            spriteBatch.End();
        }

        private void HandleGameplayInput(Player player, GameTime gameTime)
        {
            if (IsButtonPressed(Buttons.Back))
                this.Exit();

                // change UpdateInput to take a Player
                UpdateInput(player, gameTime);

                player.Update(gameTime);
          
                networkSession.Update();

                //base.Update(gameTime);
        }

        private void UpdateInput(Player player, GameTime gameTime)
        {
            foreach (LocalNetworkGamer gamer in networkSession.LocalGamers)
            {
                ReceiveNetworkData(gamer, gameTime);

                // this code is the same code we have been 
                // using to update the player input
                if (currentState.IsConnected)
                {

                }
                packetWriter.Write((Int32)1);
                packetWriter.Write(player.getPosition());
                packetWriter.Write(player.getMoveRotationAngle());
                packetWriter.Write(player.getShootRotationAngle());
               /* packetWriter.Write((Int32)player.getProjectiles().Count);
                for (int i = 0; i < player.getProjectiles().Count; i++)
                {
                    packetWriter.Write(player.getProjectiles()[i].getPosition());
                    packetWriter.Write(player.getProjectiles()[i].getShootRotationAngle());
                    packetWriter.Write((double)player.getTurretLength());
                }
                packetWriter.Write(player.ship.isActive);
                packetWriter.Write(player.ship.Position);
                packetWriter.Write(player.ship.Rotation);
                packetWriter.Write(player.score);
                for (int i = 0; i < GameConstants.NumAsteroids; i++)
                {
                    packetWriter.Write(player.asteroidList[i].isActive);
                    packetWriter.Write(player.asteroidList[i].position);
                }*/

                gamer.SendData(packetWriter, SendDataOptions.None);
            }
        }

        void ReceiveNetworkData(LocalNetworkGamer gamer, GameTime gameTime)
        {
            while (gamer.IsDataAvailable)
            {
                NetworkGamer sender;
                gamer.ReceiveData(packetReader, out sender);

                if (!sender.IsLocal)
                {
                    Player player = sender.Tag as Player;
                    //Identity Int (Tells what data comes after)
                    int data = packetReader.ReadInt32();
                    switch (data)
                    {
                        case 0:

                            break;
                        case 1:
                            player.setPosition(packetReader.ReadVector2());
                            player.setMoveRotationAngle(packetReader.ReadDouble());
                            player.setShootRotationAngle(packetReader.ReadDouble());
                            break;
                        default:
                            break;
                    }
                    

                  /*  Int32 projectilesCount = packetReader.ReadInt32();
                    List<Projectile> projectiles = new List<Projectile>();
                    for (int i = 0; i < projectilesCount; i++)
                    {
                        Projectile proj = new Projectile();
                        proj.Initialize(this.Content,this.GraphicsDevice,packetReader.ReadVector2(),(float)packetReader.ReadDouble(),(float)packetReader.ReadDouble(),30f);
                        proj.LoadContent("Projectile");
                        projectiles.Add(proj);
                    }
                    player.setProjectiles(projectiles);*/    
                    //packetWriter.Write(player.getProjectiles()[i].getPosition());
                    //    packetWriter.Write(player.getProjectiles()[i].getShootRotationAngle());*/
                    
                  /*  player.ship.isActive = packetReader.ReadBoolean();
                    player.ship.Position = packetReader.ReadVector3();
                    player.ship.Rotation = packetReader.ReadSingle();
                    player.score = packetReader.ReadInt32();
                    for (int i = 0; i < GameConstants.NumAsteroids; i++)
                    {
                        player.asteroidList[i].isActive =
                            packetReader.ReadBoolean();
                        player.asteroidList[i].position =
                            packetReader.ReadVector3();
                    }*/
                   // player.Update(gameTime);
                }
            }
        }

        bool IsButtonPressed(Buttons button)
        {
            switch (button)
            {
                case Buttons.A:
                    return (currentState.Buttons.A == ButtonState.Pressed &&
                        lastState.Buttons.A == ButtonState.Released);
                case Buttons.B:
                    return (currentState.Buttons.B == ButtonState.Pressed &&
                        lastState.Buttons.B == ButtonState.Released);
                case Buttons.X:
                    return (currentState.Buttons.X == ButtonState.Pressed &&
                        lastState.Buttons.X == ButtonState.Released);
                case Buttons.Back:
                    return (currentState.Buttons.Back == ButtonState.Pressed &&
                        lastState.Buttons.Back == ButtonState.Released);
                case Buttons.DPadDown:
                    return (currentState.DPad.Down == ButtonState.Pressed &&
                        lastState.DPad.Down == ButtonState.Released);
                case Buttons.DPadUp:
                    return (currentState.DPad.Up == ButtonState.Pressed &&
                        lastState.DPad.Down == ButtonState.Released);
            }
            return false;
        }

        protected void HandleAvailableSessionsInput()
        {
            KeyboardState keystate = Keyboard.GetState();
            if (IsButtonPressed(Buttons.A) || keystate.IsKeyDown(Keys.A))
            {
                // Join the selected session.
                if (availableSessions.Count > 0)
                {
                    networkSession = NetworkSession.Join(
                        availableSessions[selectedSessionIndex]);
                    HookSessionEvents();

                    availableSessions.Dispose();
                    availableSessions = null;
                }
            }
            else if (IsButtonPressed(Buttons.DPadUp) || keystate.IsKeyDown(Keys.Up))
            {
                // Select the previous session from the list.
                if (selectedSessionIndex > 0)
                    selectedSessionIndex--;
            }
            else if (IsButtonPressed(Buttons.DPadDown) || keystate.IsKeyDown(Keys.Down))
            {
                // Select the next session from the list.
                if (selectedSessionIndex < availableSessions.Count - 1)
                    selectedSessionIndex++;
            }
            else if (IsButtonPressed(Buttons.B) || keystate.IsKeyDown(Keys.B))
            {
                // Go back to the title screen.
                availableSessions.Dispose();
                availableSessions = null;
            }

        }

        private void DrawLobby()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            float y = 100;

            spriteBatch.DrawString(font, "Lobby (A=ready, B=leave)",
                new Vector2(101, y + 1), Color.Black);
            spriteBatch.DrawString(font, "Lobby (A=ready, B=leave)",
                new Vector2(101, y), Color.White);

            y += font.LineSpacing * 2;

            foreach (NetworkGamer gamer in networkSession.AllGamers)
            {
                string text = gamer.Gamertag;

                Player player = gamer.Tag as Player;

                if (player.picture == null)
                {
                    GamerProfile gamerProfile = gamer.GetProfile();
                    player.picture = Texture2D.FromStream(this.GraphicsDevice, gamerProfile.GetGamerPicture());
               }

                if (gamer.IsReady)
                    text += " - ready!";

                spriteBatch.Draw(player.picture,
                    new Vector2(100, y), Color.White);
                spriteBatch.DrawString(font, text,
                    new Vector2(170, y), Color.White);

                y += font.LineSpacing + 64;
            }
            spriteBatch.End();

        }

        protected void HandleLobbyInput()
        {
            // Signal I'm ready to play!
            KeyboardState keystate = Keyboard.GetState();
            if (IsButtonPressed(Buttons.A) || keystate.IsKeyDown(Keys.A))
            {
                foreach (LocalNetworkGamer gamer in networkSession.LocalGamers)
                    gamer.IsReady = true;
            }

            if (IsButtonPressed(Buttons.B) || keystate.IsKeyDown(Keys.B))
            {
                networkSession.Dispose();
                networkSession = null;
                availableSessions = null;
                
            }

            // The host checks if everyone is ready, 
            // and moves to game play if true.
            if (networkSession!=null && networkSession.IsHost)
            {
                if (networkSession.IsEveryoneReady)
                    networkSession.StartGame();
            }

            // Pump the underlying session object.
            if(networkSession != null )
                networkSession.Update();

        }

        private void DrawAvailableSessions()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            float y = 100;

            spriteBatch.DrawString(font,
                "Available sessions (A=join, B=back)",
                new Vector2(101, y + 1), Color.Black);
            spriteBatch.DrawString(font,
                "Available sessions (A=join, B=back)",
                new Vector2(100, y), Color.White);

            y += font.LineSpacing * 2;

            int selectedSessionIndex = 0;

            for (
                int sessionIndex = 0;
                sessionIndex < availableSessions.Count;
                sessionIndex++)
            {
                Color color = Color.Black;

                if (sessionIndex == selectedSessionIndex)
                    color = Color.Yellow;

                spriteBatch.DrawString(font,
                    availableSessions[sessionIndex].HostGamertag,
                    new Vector2(100, y), color);

                y += font.LineSpacing;
            }
            spriteBatch.End();
        }

        private void DrawTitleScreen()
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            string message = "";

            if (SignedInGamer.SignedInGamers.Count == 0)
            {
                message = "No profile signed in!  \n" +
                    "Press the Home key on the keyboard or \n" +
                    "the Xbox Guide Button on the controller to sign in.";
            }
            else
            {
                message += "Press A to create a new session\n" +
                    "X to search for sessions\nB or ESC to quit\n\n";
            }
            spriteBatch.Begin();
            spriteBatch.DrawString(font, message,
                new Vector2(101, 101), Color.Black);
            spriteBatch.DrawString(font, message,
                new Vector2(100, 100), Color.White);
            spriteBatch.End();
        }

        protected void HandleTitleScreenInput()
        {
            KeyboardState keystate = Keyboard.GetState();
            if (IsButtonPressed(Buttons.A) || keystate.IsKeyDown(Keys.A))
            {
                CreateSession();
            }
            else if (IsButtonPressed(Buttons.X) || keystate.IsKeyDown(Keys.X))
            {
                availableSessions = NetworkSession.Find(
                    NetworkSessionType.PlayerMatch, 1, null);

                selectedSessionIndex = 0;
            }
            else if (IsButtonPressed(Buttons.B) || keystate.IsKeyDown(Keys.Escape))
            {
                Exit();
            }
        }

        private void DrawGameplay(GameTime gameTime)
        {
            Player player;
            if (networkSession != null)
            {
                foreach (NetworkGamer networkGamer in networkSession.AllGamers)
                {
                    player = networkGamer.Tag as Player;
                    player.Draw(spriteBatch);
                    for (int i = 0; i < enemies.Count; i++)
                        enemies[i].Draw(spriteBatch);
                    drawText();
                }
            }
        }

        void CreateSession()
        {
            networkSession = NetworkSession.Create(
                NetworkSessionType.PlayerMatch,
                1, 8, 2,
                null);

            networkSession.AllowHostMigration = true;
            networkSession.AllowJoinInProgress = true;

            HookSessionEvents();
        }

        private void HookSessionEvents()
        {
            networkSession.GamerJoined +=
                new EventHandler<GamerJoinedEventArgs>(
                    networkSession_GamerJoined);
        }

        void SignedInGamer_SignedIn(object sender, SignedInEventArgs e)
        {
            e.Gamer.Tag = new Player(this, new Vector2(100,100), "TankPlayer");
            localPlayerTag = e.Gamer.Gamertag;
        }

        void networkSession_GamerJoined(object sender, GamerJoinedEventArgs e)
        {
            if (!e.Gamer.IsLocal)
            {
                e.Gamer.Tag = new Player(this, new Vector2(100, 100), "TankPlayer");
            }
            else
            {
                e.Gamer.Tag = GetPlayer(e.Gamer.Gamertag);
            }
        }

        Player GetPlayer(String gamertag)
        {
            foreach (SignedInGamer signedInGamer in
                SignedInGamer.SignedInGamers)
            {
                if (signedInGamer.Gamertag == gamertag)
                {
                    return signedInGamer.Tag as Player;
                }
            }

            return new Player(this, new Vector2(100, 100), "TankPlayer");
        }
    }
}
