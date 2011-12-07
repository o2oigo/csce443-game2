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
    public class Player : Sprite
    {
        PlayerIndex playerIndex;

        KeyboardState keystate;
        KeyboardState oldKeyState;
        MouseState mousestate;
        MouseState oldMousestate;
        GamePadState currentState;
        GamePadState oldState;

        // tower cost
        int magicTowerCost = 10;
        int arrowTowerCost = 10;
        int cannonTowerCost = 15;
        int missileTowerCost = 20;

        private float elapsedTime;

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

        private Texture2D turretTexture;
        bool isMove;
        SpriteEffects isFlipped = SpriteEffects.None;

        // fredy code for User Interface (tower menu)
        public bool boolForTest = false;
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
        private Texture2D fireTowerBuildTexture;
        private Texture2D lightningTowerBuildTexture;
        private Texture2D arrowTowerBuildTexture;
        private Texture2D flameTowerBuildTexture;
        private Texture2D missileTowerBuildTexture;
        private Texture2D canonTowerBuildTexture;
        private Texture2D magicTowerBuildTexture;

        private Texture2D fireTowerBuildSelectTexture;
        private Texture2D lightningTowerBuildSelectTexture;
        private Texture2D arrowTowerBuildSelectTexture;
        private Texture2D flameTowerBuildSelectTexture;
        private Texture2D missileTowerBuildSelectTexture;
        private Texture2D canonTowerBuildSelectTexture;
        private Texture2D magicTowerBuildSelectTexture;

        private Texture2D fireTowerBuildInactiveTexture;
        private Texture2D lightningTowerBuildInactiveTexture;
        private Texture2D arrowTowerBuildInactiveTexture;
        private Texture2D flameTowerBuildInactiveTexture;
        private Texture2D missileTowerBuildInactiveTexture;
        private Texture2D canonTowerBuildInactiveTexture;
        private Texture2D magicTowerBuildInactiveTexture;
        //private Texture2D backgroundTexture;
        //private Texture2D startMenuTexture;
        //private Texture2D quitMenuTexture;

        public bool buildMode = false;
        public bool mainBuildMode = false;
        public bool upgradeBuildMode = false;
        public bool upgradeBuildMagicMode = false;
        //private bool upgradeSubBuildMode = false;
        private bool maxCapacityTower = false;
        SpriteFont font;
        private int fireStoneInInventory = 1;
        private int waterStoneInInventory = 1;
        private int healStoneInInventory = 1;
        static int money = 35;
        private float timeForResources = 0;
        private Vector2 uiPosition1;
        private Vector2 uiPosition2;
        private Vector2 uiPosition3;
        private Vector2 uiPosition4;

        private List<Projectile> projectileList = new List<Projectile>(5);
        private List<Missile> missileList = new List<Missile>(5);
        private List<Tower> towerList = new List<Tower>(20);
        private static List<Stone> stoneList = new List<Stone>(20);
        private List<bool> buildableTowers = new List<bool>(7);

        private List<Enemy> enemyList = new List<Enemy>(30);

        private int enemiesDestroyed = 0;
        private int towerEnemiesDestroyed = 0;
        private static int maxTowerCount = 10;

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
            this.buildableTowers.Add(false); //[0] Arrow Tower
            this.buildableTowers.Add(false); //[1] Cannon Tower
            this.buildableTowers.Add(false); //[2] Missile Tower
            this.buildableTowers.Add(true);  //[3] Magic Tower
            this.buildableTowers.Add(false); //[4] Lightning Tower
            this.buildableTowers.Add(false); //[5] Flame Tower
            this.buildableTowers.Add(false); //[6] Ice Tower
        }

        public void LoadContent(String textureName)
        {
           // texture = contentManager.Load<Texture2D>("elfFront1");
            Texture2D tFront = contentManager.Load<Texture2D>("elfFront");
            Texture2D tBack = contentManager.Load<Texture2D>("elfBack");
            Texture2D tRight = contentManager.Load<Texture2D>("elfRight");
            Texture2D tRightDown = contentManager.Load<Texture2D>("elfRightDown");
            Texture2D tRightUp = contentManager.Load<Texture2D>("elfRightUp");
            Dictionary<String, SpriteSheet> txtMap = new Dictionary<string, SpriteSheet>();
            txtMap["front"] = new SpriteSheet(tFront, 22);
            txtMap["back"] = new SpriteSheet(tBack, 22);
            txtMap["right"] = new SpriteSheet(tRight, 22);
            txtMap["rightUp"] = new SpriteSheet(tRightUp, 22);
            txtMap["rightDown"] = new SpriteSheet(tRightDown, 22);
            //txtMap["left"] = new SpriteSheet(tLeft, 16);

            animation = new SpriteAnimation(txtMap, true);
            animation.CurrentSprite = "front";
            //

            if (playerIndex == PlayerIndex.One)
                turretTexture = contentManager.Load<Texture2D>("TurretPlayer");
            else
                turretTexture = contentManager.Load<Texture2D>("TurretPurple");

            mouseTexture = contentManager.Load<Texture2D>("CrossHair1");
            //menu1Texture = contentManager.Load<Texture2D>("buildTowerMenu");
            //menu2Texture = contentManager.Load<Texture2D>("cancelMenu");
            //menu3Texture = contentManager.Load<Texture2D>("buildTowerMenuSelect");
            //menu4Texture = contentManager.Load<Texture2D>("cancelMenuSelect");
            buildTexture = contentManager.Load<Texture2D>("buildSmall");
            cancelTexture = contentManager.Load<Texture2D>("cancelSmall");
            upgradeTexture = contentManager.Load<Texture2D>("upgradeSmall");
            enhanceTexture = contentManager.Load<Texture2D>("enhanceSmall");
            sellTexture = contentManager.Load<Texture2D>("sellSmall");

            lightningTowerBuildTexture = contentManager.Load<Texture2D>("lightningTowerSmall");
            fireTowerBuildTexture = contentManager.Load<Texture2D>("fireTowerSmall");
            arrowTowerBuildTexture = contentManager.Load<Texture2D>("arrowTowerMenu");
            flameTowerBuildTexture = contentManager.Load<Texture2D>("flameTowerMenu");
            missileTowerBuildTexture = contentManager.Load<Texture2D>("missileTowerMenu");
            canonTowerBuildTexture = contentManager.Load<Texture2D>("cannonTowerSmall");
            magicTowerBuildTexture = contentManager.Load<Texture2D>("magicTowerMenu");
            
            lightningTowerBuildSelectTexture = contentManager.Load<Texture2D>("lightningTowerSmallSelected");
            //fireTowerBuildSelectTexture = contentManager.Load<Texture2D>("fireTowerMenuSelected");
            arrowTowerBuildSelectTexture = contentManager.Load<Texture2D>("arrowTowerMenuSelected");
            flameTowerBuildSelectTexture = contentManager.Load<Texture2D>("flameTowerMenuSelected");
            missileTowerBuildSelectTexture = contentManager.Load<Texture2D>("missileTowerMenuSelected");
            canonTowerBuildSelectTexture = contentManager.Load<Texture2D>("connonTowerSmallSelected");
            magicTowerBuildSelectTexture = contentManager.Load<Texture2D>("magicTowerMenuSelected");

            lightningTowerBuildInactiveTexture = contentManager.Load<Texture2D>("lightningTowerSmallInactive");
            //fireTowerBuildInactiveTexture = contentManager.Load<Texture2D>("fireTowerMenuInactive");
            arrowTowerBuildInactiveTexture = contentManager.Load<Texture2D>("arrowTowerMenuInactive");
            flameTowerBuildInactiveTexture = contentManager.Load<Texture2D>("flameTowerMenuInactive");
            missileTowerBuildInactiveTexture = contentManager.Load<Texture2D>("missileTowerMenuInactive");
            canonTowerBuildInactiveTexture = contentManager.Load<Texture2D>("connonTowerSmallInactive");
            magicTowerBuildInactiveTexture = contentManager.Load<Texture2D>("magicTowerMenuInactive");
            
            font = contentManager.Load<SpriteFont>("font");

            origin.X = Size.Width / 2;
            origin.Y = Size.Height / 2;

            lowerLeft.X = position.X;
            lowerLeft.Y = position.Y + Size.Height;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            uiPosition1 = new Vector2(80, game.GraphicsDevice.Viewport.Height - 50);
            uiPosition2 = new Vector2(250, game.GraphicsDevice.Viewport.Height - 70);
            uiPosition3 = new Vector2(500, game.GraphicsDevice.Viewport.Height - 70);
            uiPosition4 = new Vector2(700, game.GraphicsDevice.Viewport.Height - 70);

            if (!currentState.IsConnected)
            {
                spriteBatch.Draw(mouseTexture, mousePos, null, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0f);
            }
            if (!isMove) { animation.Loop = false; }
            else
            {
                isFlipped = SpriteEffects.None;
                animation.Loop = true; 
                if (moveRotationAngle == 0)
                {
                    animation.CurrentSprite = "right";
                }
                else if (moveRotationAngle == Math.PI) //for keyboard
                //else if (moveRotationAngle == -Math.PI) //for game pad
                {
                    animation.CurrentSprite = "right";
                    isFlipped = SpriteEffects.FlipHorizontally;
                }
                else if (moveRotationAngle == -Math.PI / 2)
                {
                    animation.CurrentSprite = "back";
                }
                else if (moveRotationAngle == Math.PI / 2)
                {
                    animation.CurrentSprite = "front";
                }
                else if (moveRotationAngle > 0 && moveRotationAngle < Math.PI / 2)
                {
                    animation.CurrentSprite = "rightDown";
                }
                else if (moveRotationAngle > Math.PI / 2 && moveRotationAngle < Math.PI)
                {
                    animation.CurrentSprite = "rightDown";
                    isFlipped = SpriteEffects.FlipHorizontally;
                }
                else if (moveRotationAngle < 0 && moveRotationAngle > -Math.PI / 2)
                {
                    animation.CurrentSprite = "rightUp";
                }
                else if (moveRotationAngle < -(Math.PI / 2) && moveRotationAngle > -Math.PI)
                {
                    animation.CurrentSprite = "rightUp";
                    isFlipped = SpriteEffects.FlipHorizontally;
                }
            }
            spriteBatch.Draw(animation.currentSpriteSheet().texture, Position, animation.currentSpriteSheet().rectangles[animation.FrameIndex], Color.White, 0f, origin, 1.0f, isFlipped, 0f);
            
            foreach (Projectile proj in projectileList)
            {
                proj.Draw(spriteBatch);
            }

            foreach (Missile mis in missileList)
            {
                mis.Draw(spriteBatch);
            }

            //foreach (Tower tower in towerList)
            //{
            //    tower.Draw(spriteBatch);
            //}

            //foreach (Tower tower in towerList)
            //{
            //    tower.Draw(spriteBatch);
            //}
   

/*
                else if (upgradeBuildMode == true && (shootRotationAngle < -0.93 || shootRotationAngle > (float)Math.Sqrt(2) / 2f))
                {
                    spriteBatch.Draw(upgradeTexture, new Vector2(position.X - 40, position.Y - 110), Color.White);
                    spriteBatch.Draw(cancelTexture, new Vector2(position.X - 40, position.Y + 50), Color.White);
                    spriteBatch.Draw(sellTexture, new Vector2(position.X - 100, position.Y - 30), Color.White);
                    spriteBatch.Draw(enhanceTexture, new Vector2(position.X + 20, position.Y - 30), Color.White);
 
                }
 */
 
            }

        public void DrawBuildMenu(SpriteBatch spriteBatch)
        {
            if (buildMode == true)
            {
                // if player aim up, basic tower will showed up
                if (mainBuildMode == true && upgradeBuildMode == false && upgradeBuildMagicMode == false && (shootRotationAngle >= -3 * (float)Math.PI / 4 && shootRotationAngle < -(float)Math.PI / 4))
                {
                    // default texture (not greyed nor selected)
                    spriteBatch.Draw(buildTexture, new Vector2(position.X - 40, position.Y - 110), Color.White);
                    spriteBatch.Draw(cancelTexture, new Vector2(position.X - 40, position.Y + 50), Color.White);
                    spriteBatch.Draw(arrowTowerBuildTexture, new Vector2(position.X - 100, position.Y - 170), Color.White);
                    spriteBatch.Draw(canonTowerBuildTexture, new Vector2(position.X - 40, position.Y - 195), Color.White);
                    spriteBatch.Draw(magicTowerBuildTexture, new Vector2(position.X + 25, position.Y - 170), Color.White);


                    if (shootRotationAngle > -3 * (float)Math.PI / 4 && shootRotationAngle < -7 * (float)Math.PI / 12 && money >= 10 && (map.TileTypeAt(position) == MapTileType.MapGrass))
                        spriteBatch.Draw(arrowTowerBuildSelectTexture, new Vector2(position.X - 100, position.Y - 170), Color.White);

                    if (shootRotationAngle >= -7 * (float)Math.PI / 12 && shootRotationAngle < -5 * (float)Math.PI / 12 && money >= 15 && (map.TileTypeAt(position) == MapTileType.MapGrass))
                        spriteBatch.Draw(canonTowerBuildSelectTexture, new Vector2(position.X - 40, position.Y - 195), Color.White);

                    if (shootRotationAngle >= -5 * (float)Math.PI / 12 && shootRotationAngle <= -(float)Math.PI / 4 && money >= 20 && (map.TileTypeAt(position) == MapTileType.MapGrass))
                        spriteBatch.Draw(magicTowerBuildSelectTexture, new Vector2(position.X + 25, position.Y - 170), Color.White);

                    if (money < arrowTowerCost || (map.TileTypeAt(position) != MapTileType.MapGrass))
                    {
                        spriteBatch.Draw(arrowTowerBuildInactiveTexture, new Vector2(position.X - 100, position.Y - 170), Color.White);
                        spriteBatch.Draw(canonTowerBuildInactiveTexture, new Vector2(position.X - 40, position.Y - 195), Color.White);
                        spriteBatch.Draw(magicTowerBuildInactiveTexture, new Vector2(position.X + 25, position.Y - 170), Color.White);
                    }

                    if (money < cannonTowerCost)
                    {
                        spriteBatch.Draw(canonTowerBuildInactiveTexture, new Vector2(position.X - 40, position.Y - 195), Color.White);
                        spriteBatch.Draw(magicTowerBuildInactiveTexture, new Vector2(position.X + 25, position.Y - 170), Color.White);
                    }

                    if (money < missileTowerCost)
                    {
                        spriteBatch.Draw(magicTowerBuildInactiveTexture, new Vector2(position.X + 25, position.Y - 170), Color.White);
                    }

                }

                // If player aim other place, only show the base texture, not extension
                else if (mainBuildMode == true && upgradeBuildMagicMode == false && upgradeBuildMode == false)
                {
                    spriteBatch.Draw(buildTexture, new Vector2(position.X - 40, position.Y - 110), Color.White);
                    spriteBatch.Draw(cancelTexture, new Vector2(position.X - 40, position.Y + 50), Color.White);
                }

                else if (upgradeBuildMode == true && upgradeBuildMagicMode == false)
                {
                    spriteBatch.Draw(upgradeTexture, new Vector2(position.X - 40, position.Y - 110), Color.White);
                    spriteBatch.Draw(cancelTexture, new Vector2(position.X - 40, position.Y + 50), Color.White);
                    spriteBatch.Draw(sellTexture, new Vector2(position.X - 100, position.Y - 30), Color.White);
                    spriteBatch.Draw(enhanceTexture, new Vector2(position.X + 20, position.Y - 30), Color.White);

                    if (shootRotationAngle >= -(float)Math.PI / 4 && shootRotationAngle < (float)Math.PI / 4)
                    {
                    }
                }

                else if (upgradeBuildMagicMode == true)
                {
                    spriteBatch.Draw(upgradeTexture, new Vector2(position.X - 40, position.Y - 110), Color.White);
                    spriteBatch.Draw(cancelTexture, new Vector2(position.X - 40, position.Y + 50), Color.White);
                    spriteBatch.Draw(sellTexture, new Vector2(position.X - 100, position.Y - 30), Color.White);
                    spriteBatch.Draw(enhanceTexture, new Vector2(position.X + 20, position.Y - 30), Color.White);

                    if (shootRotationAngle >= -(float)Math.PI / 4 && shootRotationAngle < (float)Math.PI / 4)
                    {
                        spriteBatch.Draw(flameTowerBuildTexture, new Vector2(position.X + 80, position.Y + 20), Color.White);
                        spriteBatch.Draw(lightningTowerBuildTexture, new Vector2(position.X + 80, position.Y - 80), Color.White);

                        if (fireStoneInInventory < 1)
                        {
                            spriteBatch.Draw(flameTowerBuildInactiveTexture, new Vector2(position.X + 80, position.Y + 20), Color.White);
                        }
                        if (waterStoneInInventory < 1)
                        {
                            spriteBatch.Draw(lightningTowerBuildInactiveTexture, new Vector2(position.X + 80, position.Y - 80), Color.White);
                        }
                        if (fireStoneInInventory > 0 && shootRotationAngle >= -1 * (float)Math.PI / 12 && shootRotationAngle < 1 * (float)Math.PI / 12)
                        {
                            spriteBatch.Draw(flameTowerBuildSelectTexture, new Vector2(position.X + 80, position.Y + 20), Color.White);
                        }
                        if (waterStoneInInventory > 0 && shootRotationAngle >= -(float)Math.PI / 4 && shootRotationAngle < -1 * (float)Math.PI / 12)
                        {
                            spriteBatch.Draw(lightningTowerBuildSelectTexture, new Vector2(position.X + 80, position.Y - 80), Color.White);
                        }
                    }
                }
            }
        }

        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            enemyList = enemies;

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

            // to reset everything equal false before checking
            upgradeBuildMode = false;
            upgradeBuildMagicMode = false;
            foreach (Tower tower in towerList)
            {
                //Set upgrade mode if near tower
                if (tower.getPlayerIsNear() == true)
                {
                    if (tower is MagicTower)
                    {
                        upgradeBuildMagicMode = true;
                        upgradeBuildMode = false;
                        //mainBuildMode = false;
                    }

                    else
                    {
                        upgradeBuildMagicMode = false;
                        upgradeBuildMode = true; 
                        //mainBuildMode = false;
                    }
                    
                    break;
                }
                
                // check if it is the last tower in tower list and no tower is near player
                if (tower == towerList[towerList.Count()-1])
                {
                    upgradeBuildMagicMode = false;
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
            animation.Update(gameTime);

            //Update Projectiles
            updateProjectiles(gameTime);

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
            isMove = false;
            if (Math.Abs(currentState.ThumbSticks.Left.X) > 0 || Math.Abs(currentState.ThumbSticks.Left.Y) > 0)
            {
                isMove = true;

                moveRotationAngle = Math.Atan2(-currentState.ThumbSticks.Left.Y, currentState.ThumbSticks.Left.X);
                moveRotationAngle = moveRotationAngle % circle;
            }

            //Update Speed
            if (currentState.ThumbSticks.Left.X != 0 || currentState.ThumbSticks.Left.Y != 0)
                speed = 3;
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
            if (oldState.IsButtonUp(Buttons.LeftShoulder) && currentState.IsButtonDown(Buttons.LeftShoulder) && buildMode == false)
            {
                buildMode = true;
                mainBuildMode = true;
                upgradeBuildMode = false;
            }
            else if (buildMode == true && oldState.IsButtonUp(Buttons.LeftShoulder) && currentState.IsButtonDown(Buttons.LeftShoulder))
            {
                if (upgradeBuildMode == false)
                {
                    mainBuildMode = true;
                }
                else
                    mainBuildMode = false;

                if (shootRotationAngle > -3 * (float)Math.PI / 4 && shootRotationAngle <= (float)Math.PI / 4 && buildMode == true && towerList.Count < maxTowerCount && upgradeBuildMode == false && upgradeBuildMagicMode == false)
                {
                    if (mainBuildMode == true && upgradeBuildMagicMode == false && upgradeBuildMode == false)
                    {
                        if (map.TileTypeAt(position) == MapTileType.MapGrass)
                        {
                            // for arrow tower
                            if (shootRotationAngle > -3 * (float)Math.PI / 4 && shootRotationAngle < -7 * (float)Math.PI / 12)
                            {
                                if (money >= arrowTowerCost)
                                {
                                    removeMoney(arrowTowerCost);
                                    createArrowTower();
                                    buildMode = false;
                                    mainBuildMode = false;
                                }
                            }

                            // for cannon tower
                            else if (shootRotationAngle >= -7 * (float)Math.PI / 12 && shootRotationAngle < -5 * (float)Math.PI / 12)
                            {
                                if (money >= cannonTowerCost)
                                {
                                    removeMoney(cannonTowerCost);
                                    removeStoneFromInventory(0);
                                    int level = 1;
                                    bool isFire = true;
                                    //createMissileTower(Position, level, isFire);
                                    //createArrowTower(Position, level, isFire);
                                    createTower();
                                    buildMode = false;
                                    mainBuildMode = false;
                                }
                            }

                            // for missile tower
                            else if (shootRotationAngle >= -5 * (float)Math.PI / 12 && shootRotationAngle <= -(float)Math.PI / 4)
                            {
                                if (money >= missileTowerCost)
                                {
                                    removeMoney(missileTowerCost);
                                    int level = 1;
                                    bool isFire = true;
                                    createMissileTower(Position, level, isFire);
                                    //createMagicTower();
                                    buildMode = false;
                                    mainBuildMode = false;
                                }
                            }

                            // for magic tower
                            else if (shootRotationAngle > -(float)Math.PI / 4 && shootRotationAngle <= (float)Math.PI / 4)
                            {
                                if (money >= magicTowerCost)
                                {
                                    removeMoney(magicTowerCost);
                                    int level = 1;
                                    bool isFire = true;
                                    //createMissileTower(Position, level, isFire);
                                    createMagicTower();
                                    buildMode = false;
                                    mainBuildMode = false;
                                }
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
                            if (towerList[i].getPlayerIsNear() == true && towerList[i].getTowerIntLevel() == 1)
                            {
                                if (money >= 10)
                                {
                                    removeMoney(10);

                                    towerList[i].setToLvlTwo();
                                    buildMode = false;
                                    upgradeBuildMode = false;
                                }
                            }

                            else if (towerList[i].getPlayerIsNear() == true && towerList[i].getTowerIntLevel() == 2)
                            {
                                if (money >= 10)
                                {
                                    removeMoney(10);
                                    towerList[i].setToLvlThree();
                                    buildMode = false;
                                    upgradeBuildMode = false;
                                }
                            }

                        }
                    }
                }
                else if (shootRotationAngle > (float)Math.PI / 4 && shootRotationAngle <= 3 * (float)Math.PI / 4)
                {
                    buildMode = false;
                    mainBuildMode = false;
                    upgradeBuildMode = false;
                }
                else if (shootRotationAngle > 1 * (float)Math.PI / 12 && shootRotationAngle <= 1 * (float)Math.PI / 4 && (upgradeBuildMode == true || upgradeBuildMagicMode == true))
                {

                    for (int i = 0; i < towerList.Count(); i++)
                    {
                        if (towerList[i].getPlayerIsNear() == true)
                        {
                            if (healStoneInInventory >= 1 && towerList[i] is MagicTower)
                            {
                                removeStoneFromInventory(2);
                                //towerList[i].setToFireTower();
                                int level = towerList[i].getLevel();
                                bool isFire = false;
                                if (towerList[i].damage.type == ElementType.Fire)
                                    isFire = true;

                                //createMissileTower(towerList[i].Position, level, isFire);
                                //createFlameTower(towerList[i].Position, level, isFire);
                                createIceTower(towerList[i].Position, level, isFire);
                                Sprite.removeList(towerList[i]);
                                towerList.RemoveAt(i);
                                buildMode = false;
                                mainBuildMode = false;
                                upgradeBuildMode = false;
                            }
                        }

                    }
                }
                else if (shootRotationAngle > -1 * (float)Math.PI / 12 && shootRotationAngle <= 1 * (float)Math.PI / 12 && (upgradeBuildMode == true || upgradeBuildMagicMode == true))
                {

                    for (int i = 0; i < towerList.Count(); i++)
                    {
                        if (towerList[i].getPlayerIsNear() == true)
                        {
                            if (fireStoneInInventory >= 1 && towerList[i] is MagicTower)
                            {
                                removeStoneFromInventory(0);
                                //towerList[i].setToFireTower();
                                int level = towerList[i].getLevel();
                                bool isFire = false;
                                if (towerList[i].damage.type == ElementType.Fire)
                                    isFire = true;

                                //createMissileTower(towerList[i].Position, level, isFire);
                                createFlameTower(towerList[i].Position, level, isFire);             
                                //createIceTower(towerList[i].Position, level, isFire);
                                Sprite.removeList(towerList[i]);
                                towerList.RemoveAt(i);
                                buildMode = false;
                                mainBuildMode = false;
                                upgradeBuildMode = false;
                            }
                        }

                    }
                }

                else if (shootRotationAngle > -(float)Math.PI / 4 && shootRotationAngle <= 1 * (float)Math.PI / 12 && (upgradeBuildMode == true || upgradeBuildMagicMode == true))
                {

                    for (int i = 0; i < towerList.Count(); i++)
                    {
                        if (towerList[i].getPlayerIsNear() == true)
                        {
                            if (waterStoneInInventory >= 1 && towerList[i] is MagicTower)
                            {
                                removeStoneFromInventory(1);
                                int level = towerList[i].getLevel();
                                bool isFire = false;
                                if (towerList[i].damage.type == ElementType.Fire)
                                    isFire = true;
                                createLightningTower(towerList[i].Position, level, isFire);
                                Sprite.removeList(towerList[i]);
                                towerList.RemoveAt(i);
                                buildMode = false;
                                mainBuildMode = false;
                                upgradeBuildMode = false;
                            }
                        }

                    }
                }

                else if ((shootRotationAngle <= -3 * (float)Math.PI / 4 && shootRotationAngle >= -(float)Math.PI) || (shootRotationAngle <= (float)Math.PI && shootRotationAngle > 3 * (float)Math.PI / 4))
                {
                    for (int i = 0; i < towerList.Count(); i++)
                    {
                        if (towerList[i].getPlayerIsNear() == true)
                        {
                            if (towerList[i].getTowerName() == "Flame Tower")
                            {

                                FlameTower temp = (FlameTower)towerList[i];
                                temp.getSound().Stop();

                            }
                            if (towerList[i].getTowerName() == "Ice Tower")
                            {

                                IceTower temp = (IceTower)towerList[i];
                                temp.getSound().Stop();

                            }
                            if (towerList[i].getTowerLvl() == "level 1")
                            {
                                addMoney(5);
                                Sprite.removeList(towerList[i]);
                                towerList.RemoveAt(i);
                                buildMode = false;
                                mainBuildMode = false;
                                upgradeBuildMode = false;
                            }
                            else if (towerList[i].getTowerLvl() == "level 2")
                            {
                                addMoney(10);
                                Sprite.removeList(towerList[i]);
                                towerList.RemoveAt(i);
                                buildMode = false;
                                mainBuildMode = false;
                                upgradeBuildMode = false;
                            }
                            else if (towerList[i].getTowerLvl() == "level 3")
                            {
                                addMoney(10);
                                Sprite.removeList(towerList[i]);
                                towerList.RemoveAt(i);
                                buildMode = false;
                                mainBuildMode = false;
                                upgradeBuildMode = false;
                            }

                        }
                    }
                }
            }
            if (currentState.IsButtonDown(Buttons.RightShoulder) && currentState.IsButtonDown(Buttons.LeftShoulder) && currentState.IsButtonDown(Buttons.RightTrigger) && currentState.IsButtonDown(Buttons.LeftTrigger) && currentState.IsButtonDown(Buttons.X))
            {
                money = 777;
                Stone fStone = new Stone();
                fStone.Initialize(game, position, 0);
                addStoneToInventory(fStone);
                Stone wStone = new Stone();
                wStone.Initialize(game, position, 1);
                addStoneToInventory(wStone);
                Stone hStone = new Stone();
                hStone.Initialize(game, position, 2);
                addStoneToInventory(hStone);

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

            isMove = false;
            if (keystate.IsKeyDown(Keys.A) && buildMode == false)
            {
                xComponent = -1;
                speed = 2;
                isMove = true;
            }
            if (keystate.IsKeyDown(Keys.D) && buildMode == false)
            {
                xComponent = 1;
                speed = 2;
                isMove = true;
            }
            if (keystate.IsKeyDown(Keys.W) && buildMode == false)
            {
                yComponent = -1;
                speed = 2;
                isMove = true;
            }
            else if (keystate.IsKeyDown(Keys.S) && buildMode == false)
            {
                yComponent = 1;
                speed = 2;
                isMove = true;
            }

            if (keystate.IsKeyDown(Keys.Enter))
            {
                position = new Vector2(300, 400);
            }

            if (keystate.IsKeyDown(Keys.NumPad1))
            {
                Stone fStone = new Stone();
                fStone.Initialize(game, position, 0);
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

            if (keystate.IsKeyDown(Keys.LeftShift))
            {
                game.Wave.endTimer();
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

                if (oldMousestate.RightButton == ButtonState.Pressed && mousestate.RightButton == ButtonState.Released)
                {
                    createMissile();
                }
            }

            //Open Main Build Menu
            if (keystate.IsKeyUp(Keys.Space) && oldKeyState.IsKeyDown(Keys.Space))
            {
                buildMode = true;

                if (upgradeBuildMode == false && upgradeBuildMagicMode == false)
                {
                    mainBuildMode = true;
                }
                else
                    mainBuildMode = false;
            }
            
            if (buildMode == true && oldMousestate.LeftButton == ButtonState.Pressed && mousestate.LeftButton == ButtonState.Released)
            {

                if (shootRotationAngle > -3 * (float)Math.PI / 4 && shootRotationAngle <= -(float)Math.PI / 4 && buildMode == true && towerList.Count < maxTowerCount)
                {
                    if (mainBuildMode == true)
                    {
                        if (map.TileTypeAt(position) == MapTileType.MapGrass)
                        {
                            // for arrow tower
                            if (shootRotationAngle > -3 * (float)Math.PI / 4 && shootRotationAngle < -7 * (float)Math.PI / 12)
                            {
                                if (money >= 10)
                                {
                                    removeMoney(10);
                                    createArrowTower();
                                    buildMode = false;
                                    mainBuildMode = false;
                                }
                            }

                            // for cannon tower
                            else if (shootRotationAngle >= -7 * (float)Math.PI / 12 && shootRotationAngle < -5 * (float)Math.PI / 12)
                            {
                                if (money >= 15)
                                {
                                    removeMoney(15);
                                    int level = 1;
                                    bool isFire = true;
                                    createMissileTower(Position, level, isFire);
                                    //createArrowTower(Position, level, isFire);
                                    //createTower();
                                    buildMode = false;
                                    mainBuildMode = false;
                                }
                            }

                            // for magic tower
                            else if (shootRotationAngle >= - 5 * (float)Math.PI / 12 && shootRotationAngle <= -(float)Math.PI / 4)
                            {
                                if (money >= 20)
                                {
                                    removeMoney(20);
                                    createMagicTower();
                                    buildMode = false;
                                    mainBuildMode = false;
                                }
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
                            if (towerList[i].getPlayerIsNear() == true && towerList[i].getTowerIntLevel() == 1)
                            {
                                if (money >= 10)
                                {
                                    removeMoney(10);

                                    towerList[i].setToLvlTwo();
                                    buildMode = false;
                                    upgradeBuildMode = false;
                                }
                            }
                            else if (towerList[i].getPlayerIsNear() == true && towerList[i].getTowerIntLevel() == 2)
                            {
                                if (money >= 10)
                                {
                                    removeMoney(10);
                                    towerList[i].setToLvlThree();
                                    buildMode = false;
                                    upgradeBuildMode = false;
                                }
                            }

                        }
                    }
                }

                // Exit from build menu
                else if (shootRotationAngle > (float)Math.PI / 4 && shootRotationAngle <= 3 * (float)Math.PI / 4)
                {
                    buildMode = false;
                    mainBuildMode = false;
                    upgradeBuildMode = false;
                    upgradeBuildMagicMode = false;
                }
                else if (shootRotationAngle > 1 * (float)Math.PI / 12 && shootRotationAngle <= 1 * (float)Math.PI / 4 && (upgradeBuildMode == true || upgradeBuildMagicMode == true))
                {

                    for (int i = 0; i < towerList.Count(); i++)
                    {
                        if (towerList[i].getPlayerIsNear() == true)
                        {
                            if (healStoneInInventory >= 1 && towerList[i] is MagicTower)
                            {
                                removeStoneFromInventory(2);
                                //towerList[i].setToFireTower();
                                int level = towerList[i].getLevel();
                                bool isFire = false;
                                if (towerList[i].damage.type == ElementType.Fire)
                                    isFire = true;

                                //createMissileTower(towerList[i].Position, level, isFire);
                                //createFlameTower(towerList[i].Position, level, isFire);
                                createIceTower(towerList[i].Position, level, isFire);
                                Sprite.removeList(towerList[i]);
                                towerList.RemoveAt(i);
                                buildMode = false;
                                mainBuildMode = false;
                                upgradeBuildMode = false;
                            }
                        }

                    }
                }
                else if (shootRotationAngle > -1 * (float)Math.PI / 12 && shootRotationAngle <= 1 * (float)Math.PI / 12 && upgradeBuildMagicMode == true)
                {

                    for (int i = 0; i < towerList.Count(); i++)
                    {
                        if (towerList[i].getPlayerIsNear() == true)
                        {
                            if (fireStoneInInventory >= 1 && towerList[i] is MagicTower)
                            {
                                removeStoneFromInventory(0);
                                //towerList[i].setToFireTower();
                                int level = towerList[i].getLevel();
                                bool isFire = false;
                                if (towerList[i].damage.type == ElementType.Fire)
                                    isFire = true;

                                //createMissileTower(towerList[i].Position, level, isFire);
                                createFlameTower(towerList[i].Position, level, isFire);
                                //createIceTower(towerList[i].Position, level, isFire);

                                Sprite.removeList(towerList[i]);
                                towerList.RemoveAt(i);
                                buildMode = false;
                                mainBuildMode = false;
                                upgradeBuildMode = false;
                            }
                        }

                    }
                }

                else if (shootRotationAngle > -(float)Math.PI / 4 && shootRotationAngle <= -1 * (float)Math.PI / 12 && upgradeBuildMagicMode == true)
                {

                    for (int i = 0; i < towerList.Count(); i++)
                    {
                        if (towerList[i].getPlayerIsNear() == true)
                        {
                            if (waterStoneInInventory >= 1 && towerList[i] is MagicTower)
                            {
                                removeStoneFromInventory(1);
                                int level = towerList[i].getLevel();
                                bool isFire = false;
                                if (towerList[i].damage.type == ElementType.Fire)
                                    isFire = true;
                                createLightningTower(towerList[i].Position, level, isFire);
                                Sprite.removeList(towerList[i]);
                                towerList.RemoveAt(i);
                                buildMode = false;
                                mainBuildMode = false;
                                upgradeBuildMode = false;
                            }
                        }

                    }
                }


                // selling tower
                else if ((shootRotationAngle <= -3 * Math.PI / 4 && shootRotationAngle >= -Math.PI) || (shootRotationAngle <= Math.PI && shootRotationAngle > 3 * Math.PI / 4))
                {
                    for (int i = 0; i < towerList.Count(); i++)
                    {
                        if (towerList[i].getPlayerIsNear() == true)
                        {
                            if (towerList[i].getTowerName() == "Flame Tower")
                            {

                                FlameTower temp = (FlameTower)towerList[i];
                                temp.getSound().Stop();

                            }
                            if (towerList[i].getTowerName() == "Ice Tower")
                            {

                                IceTower temp = (IceTower)towerList[i];
                                temp.getSound().Stop();

                            }
                            if (towerList[i].getTowerLvl() == "level 1")
                            {
                                addMoney(5);
                                Sprite.removeList(towerList[i]);
                                towerList.RemoveAt(i);
                                buildMode = false;
                                mainBuildMode = false;
                                upgradeBuildMode = false;
                            }
                            else if (towerList[i].getTowerLvl() == "level 2")
                            {
                                addMoney(10);
                                Sprite.removeList(towerList[i]);
                                towerList.RemoveAt(i);
                                buildMode = false;
                                mainBuildMode = false;
                                upgradeBuildMode = false;
                            }
                            else if (towerList[i].getTowerLvl() == "level 3")
                            {
                                addMoney(10);
                                Sprite.removeList(towerList[i]);
                                towerList.RemoveAt(i);
                                buildMode = false;
                                mainBuildMode = false;
                                upgradeBuildMode = false;
                            }
                        }
                    }
                }
            }

            if (keystate.IsKeyUp(Keys.Delete) && oldKeyState.IsKeyDown(Keys.Delete))
            {
                game.Exit();
            }
        }

        public void updateMovement()
        {
            bool collision = false;
            Vector2 tmpPos;
            tmpPos.X = position.X + (float)(Math.Cos(moveRotationAngle) * speed);
            tmpPos.Y = position.Y + (float)(Math.Sin(moveRotationAngle) * speed);
            Rectangle player = new Rectangle((int)tmpPos.X - 5, (int)tmpPos.Y, animation.currentSpriteSheet().size.Width, animation.currentSpriteSheet().size.Height);

            //TODO
            if (map.TileTypeAt(tmpPos) == MapTileType.MapBarrier) collision = true;


            foreach (Tree t in game.Trees)
            {
                //Rectangle treeRect = new Rectangle((int)t.Origin.X, (int)t.Origin.Y, t.Texture.Width / 3, t.Texture.Height / 3);
                Rectangle treeRect = new Rectangle((int)t.Origin.X, (int)t.Origin.Y-10, 40, 5);
                if (treeRect.Intersects(player))
                {
                    collision = true;
                    break;
                }
            }
            foreach (Lamp l in game.Lamps)
            {
                //Rectangle treeRect = new Rectangle((int)t.Origin.X, (int)t.Origin.Y, t.Texture.Width / 3, t.Texture.Height / 3);
                Rectangle lampRect = new Rectangle((int)l.Origin.X+15, (int)l.Origin.Y+5, 1, 1);
                if (lampRect.Intersects(new Rectangle((int)tmpPos.X - 5, (int)tmpPos.Y, (int)(animation.currentSpriteSheet().size.Width*0.75), (int)(animation.currentSpriteSheet().size.Height * 0.75))))
                {
                    collision = true;
                    break;
                }
            }

            //Rectangle houseRect = new Rectangle((int)game.House.Origin.X - (game.House.Texture.Width / 2) + 30, (int)game.House.Origin.Y - 40, game.House.Texture.Width - 30, game.House.Texture.Height / 6);
            //if (houseRect.Intersects(player))
            if (game.Map.endRectangle().Intersects(player))
            {
                collision = true;
            }


            if (!collision)
            {
                position.X += (float)(Math.Cos(moveRotationAngle) * speed);
                position.Y += (float)(Math.Sin(moveRotationAngle) * speed);

                //Movement boundaries for player (keeps player on screen)
                if (position.X > game.getCurrentRectangle().Width)
                    position.X = game.getCurrentRectangle().Width;
                else if (position.X < 0)
                    position.X = 0;
                if (position.Y > game.getCurrentRectangle().Height)
                    position.Y = game.getCurrentRectangle().Height;
                else if (position.Y < 0)
                    position.Y = 0;
            }
        }

        public void createProjectile()
        {
            Projectile projectile = new Projectile();
            projectile.Initialize(contentManager, graphicsDevice, position, (float)shootRotationAngle, getTurretLength(), 2000f, map);
            projectile.LoadContent("Projectile");
            projectileList.Add(projectile);

            game.explosion.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength() * map.ScaleB, position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength() * map.ScaleB));
            game.smoke.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));         
        }

        public void createMissile()
        {
            if (enemyList.Count != 0)
            {
                Enemy target = enemyList[0];
                Missile missile = new Missile(target);
                missile.Initialize(contentManager, graphicsDevice, position, (float)shootRotationAngle, getTurretLength(), 30f, map);
                missile.LoadContent("ProjectileBlue");
                missileList.Add(missile);
                game.explosion.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength() * map.ScaleB, position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength() * map.ScaleB));
                game.smoke.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));         
            }
        }

        public void updateProjectiles(GameTime gameTime)
        {
            for (int i = 0; i < projectileList.Count; i++)
            {
                //Remove Projectile if it goes off-screen
                Projectile proj = projectileList[i];
                if ((proj.getPosition().X > game.getCurrentRectangle().Width) || proj.getPosition().X < 0
                    || (proj.getPosition().Y > game.getCurrentRectangle().Height) || proj.getPosition().Y < 0)
                {
                    projectileList.Remove(proj);
                }
                proj.Update(gameTime);
            }

            for (int i = 0; i < missileList.Count; i++)
            {
                //Remove Projectile if it goes off-screen
                Missile mis = missileList[i];
                if (mis.getPosition().X > game.getCurrentRectangle().Width || mis.getPosition().X < 0
                    || mis.getPosition().Y > game.getCurrentRectangle().Height || mis.getPosition().Y < 0)
                {
                    missileList.Remove(mis);
                }
                if (mis.getTarget() == null || mis.getTarget().isDead())
                    missileList.Remove(mis);
                else
                    mis.Update(gameTime);
            }
        }

        public void createTower()
        {
            Tower tower = new Tower(game, playerIndex, this.position);
            towerList.Add(tower);
        }

        public void createArrowTower()
        {
            ArrowTower tower = new ArrowTower(game, playerIndex, this.position);
            towerList.Add(tower);
        }

        public void createLightningTower(Vector2 oldPosition, int level, bool isFire)
        {
            LightningTower tower = new LightningTower(game, playerIndex, oldPosition, level, isFire);
            towerList.Add(tower);
        }

        public void createFlameTower(Vector2 oldPosition, int level, bool isFire)
        {
            FlameTower tower = new FlameTower(game, playerIndex, oldPosition, level, isFire);
            towerList.Add(tower);
        }

        public void createIceTower(Vector2 oldPosition, int level, bool isFire)
        {
            IceTower tower = new IceTower(game, playerIndex, oldPosition, level, isFire);
            towerList.Add(tower);
        }

        public void createMissileTower(Vector2 oldPosition, int level, bool isFire)
        {
            MissileTower tower = new MissileTower(game, playerIndex, oldPosition, level, isFire);
            towerList.Add(tower);
        }

        public void createMagicTower()
        {
            MagicTower tower = new MagicTower(game, playerIndex, this.position);
            towerList.Add(tower);
        }

        public void updateTowers(GameTime gameTime, List<Enemy> enemies)
        {
           // for (int i = 0; i < towerList.Count; i++)
            foreach (Tower t in towerList)
            {
                //towerList[i].Update(gameTime, enemies);
                t.Update(gameTime, enemies);
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
                else if (_stone.Type == ElementType.Lightning)
                {
                    waterStoneInInventory++;
                }
                else if (_stone.Type == ElementType.Ice)
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

        public List<bool> getBuildableTowers()
        {
            return buildableTowers;
        }

        static public void addMoney(int _money)
        {
            money += _money;
        }

        static public void removeMoney(int _money)
        {
            money -= _money;
        }

        public float getTurretLength()
        {
            return turretTexture.Width;
        }

        public Vector2 getOrigin()
        {
            return origin;
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

        public float getShieldTimeLimit()
        {
            return shieldTimeLimit;
        }

        public bool getMaxTower()
        {
            return maxCapacityTower;
        }

        public int getMoney()
        {
            return money;
        }


        public int getFireStoneInInventory()
        {
            return fireStoneInInventory;
        }
        public int getWaterStoneInInventory()
        {
            return waterStoneInInventory;
        }
        public int getHealStoneInInventory()
        {
            return healStoneInInventory;
        }

        //If the player was hit by an enemy or projectile
        //public void Hit()
        //{
        //    timesHit++;
        //    if (timesHit % 4 == 0)
        //        Position = new Vector2(100, 100);
        //    if (timesHit % 4 == 1)
        //        Position = new Vector2(graphicsDevice.Viewport.Width - 100, 100);
        //    if (timesHit % 4 == 2)
        //        Position = new Vector2(100, graphicsDevice.Viewport.Height - 100);
        //    if (timesHit % 4 == 3)
        //        Position = new Vector2(graphicsDevice.Viewport.Width - 100, graphicsDevice.Viewport.Height - 100);
        //    buildMode = false;
        //    mainBuildMode = false;
        //    spawnShield = true;
        //
        //}


        public void restartGameLevel1()
        {
            position = new Vector2(300, 400);
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
            foreach (Tower t in towerList)
            {
                Sprite.removeList(t);
            }
            towerList.Clear();
            stoneList.Clear();

            enemiesDestroyed = 0;
            towerEnemiesDestroyed = 0;
        }



    }
}
