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
    public class UserInterface
    {

        Game1 game;

        ContentManager contentManager;
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        private PlayerIndex playerIndex;
        List<Player> players;
        Player player1;
        Player player2;
        Texture2D statusBar;
        
        public bool runTestChecking = true;  // true to show the test Status on every screen, false to show nothing
        SpriteFont font;



        private bool skipToGame = false;               
        private bool showTitleScreen = false;
        private bool showGameScreen = false;
        private bool showPauseScreen = false;
        private bool showGameOverScreen = false;
        private bool showWinScreen = false;
        private bool showLevel1Screen = false;
        private bool showLevel2Screen = false;
        private bool showLevel3Screen = false;
        private bool loadingGameScreen1 = false;
        private bool loadingGameScreen2 = false;
        private bool loadingGameScreen3 = false;
        private bool showEncyclopediaScreen = false;
        private bool showTower1EncyclopediaScreen = false;
        private bool showEnemy1EncyclopediaScreen = false;
        private bool showTower2EncyclopediaScreen = false;
        private bool showEnemy2EncyclopediaScreen = false;
        private bool showTower3EncyclopediaScreen = false;
        private bool showEnemy3EncyclopediaScreen = false;
        private bool restartGame = false;
        private bool firstRun = true;
        
        

        private int wave = 0;
        private int nextWave = 1;
        private int xPos1 = 0;
        private int yPos1 = 0;
        private int xPos2 = 0;
        private int yPos2 = 0;
        private int xPosMax = 0;
        private int yPosMax = 0;
        private int currentLevel = 1;
        private int towerLevelEncyclopedia = 1;



        private bool finishMenu1 = false;
        private bool finishMenu2 = false;


        private Vector2 uiMoneyPosition = new Vector2(100, 23);
        private Vector2 uiLifePosition= new Vector2(180, 23);
        //private Vector2 uiStonePosition = new Vector2(50, 43);
        private Vector2 uiWavePosition = new Vector2(155, 95);
        private Vector2 uiFirePosition = new Vector2(85, 57);
        private Vector2 uiLightningPosition = new Vector2(145, 57);
        private Vector2 uiIcePosition = new Vector2(210, 57);


        private Vector2 uiPosition1;
        private Vector2 uiPosition2;
        private Vector2 uiPosition3;
        private Vector2 uiPosition4;
        private Vector2 uiPositionMiddle;
        private Vector2 encyclopediaTowerPosition;
        private Vector2 encyclopediaEnemyPosition;
        private Vector2 encyclopediaTower1Position;
        /*
        private Vector2 encyclopediaTower1Position;
        private Vector2 encyclopediaTower1Position;
        private Vector2 encyclopediaTower1Position;
        private Vector2 encyclopediaTower1Position;
        private Vector2 encyclopediaTower1Position;
        private Vector2 encyclopediaTower1Position;
        private Vector2 encyclopediaTower1Position;
        private Vector2 encyclopediaTower1Position;
        private Vector2 encyclopediaTower1Position;
        */
        
        

        KeyboardState keystate;
        KeyboardState oldKeyState;
        MouseState mousestate;
        MouseState oldMousestate;
        GamePadState currentState;
        GamePadState oldState;
        private Vector2 mousePos;
        private Vector2 origin;


        // fredy code for User Interface (tower menu)
        private Texture2D mouseTexture;
        private Texture2D menu1Texture;
        private Texture2D menu2Texture;
        private Texture2D menu3Texture;
        private Texture2D menu4Texture;
        private Texture2D startScreenBackground;
        private Texture2D winScreenBackground;
        private Texture2D startMenuTexture;
        private Texture2D gameoverScreenBackground;
        private Texture2D level1MapTexture;
        private Texture2D level2MapTexture;
        private Texture2D level3MapTexture;
        private Texture2D startLevelButtonTexture;
        private Texture2D startLevelButtonTextureSelect;
        private Texture2D encyclopediaButtonTexture;
        private Texture2D encyclopediaBackgroundTexture;
        private Texture2D encyclopediaButtonTextureSelect;


        #region Declare Encyclopedia Texture
        // Encyclopedia main screen
        private Texture2D mainEncyclopediaTexture;
        private Texture2D mainTowerEncyclopediaTexture;
        private Texture2D mainEnemyEncyclopediaTexture;

        #region tower
        // Tower part level 1
        private Texture2D towerEncyclopediaTexture;
        private Texture2D arrowTowerEncyclopediaTexture;
        private Texture2D canonTowerEncyclopediaTexture;
        private Texture2D missileTowerEncyclopediaTexture;
        private Texture2D magicTowerEncyclopediaTexture;
        private Texture2D iceTowerEncyclopediaTexture;
        private Texture2D flameTowerEncyclopediaTexture;
        private Texture2D shockTowerEncyclopediaTexture;
        // Tower part level 2
        private Texture2D arrowTower2EncyclopediaTexture;
        private Texture2D canonTower2EncyclopediaTexture;
        private Texture2D missileTower2EncyclopediaTexture;
        private Texture2D magicTower2EncyclopediaTexture;
        private Texture2D iceTower2EncyclopediaTexture;
        private Texture2D flameTower2EncyclopediaTexture;
        private Texture2D shockTower2EncyclopediaTexture;
        // Tower part level 3
        private Texture2D arrowTower3EncyclopediaTexture;
        private Texture2D canonTower3EncyclopediaTexture;
        private Texture2D missileTower3EncyclopediaTexture;
        private Texture2D magicTower3EncyclopediaTexture;
        private Texture2D iceTower3EncyclopediaTexture;
        private Texture2D flameTower3EncyclopediaTexture;
        private Texture2D shockTower3EncyclopediaTexture;

        // Tower Description part 1
        private Texture2D arrowTowerEncyclopediaLargeTexture;
        private Texture2D canonTowerEncyclopediaLargeTexture;
        private Texture2D missileTowerEncyclopediaLargeTexture;
        private Texture2D magicTowerEncyclopediaLargeTexture;
        private Texture2D iceTowerEncyclopediaLargeTexture;
        private Texture2D flameTowerEncyclopediaLargeTexture;
        private Texture2D shockTowerEncyclopediaLargeTexture;

        // Tower Description part 2
        private Texture2D arrowTower2EncyclopediaLargeTexture;
        private Texture2D canonTower2EncyclopediaLargeTexture;
        private Texture2D missileTower2EncyclopediaLargeTexture;
        private Texture2D magicTower2EncyclopediaLargeTexture;
        private Texture2D iceTower2EncyclopediaLargeTexture;
        private Texture2D flameTower2EncyclopediaLargeTexture;
        private Texture2D shockTower2EncyclopediaLargeTexture;

        // Tower Description part 3
        private Texture2D arrowTower3EncyclopediaLargeTexture;
        private Texture2D canonTower3EncyclopediaLargeTexture;
        private Texture2D missileTower3EncyclopediaLargeTexture;
        private Texture2D magicTower3EncyclopediaLargeTexture;
        private Texture2D iceTower3EncyclopediaLargeTexture;
        private Texture2D flameTower3EncyclopediaLargeTexture;
        private Texture2D shockTower3EncyclopediaLargeTexture;
        #endregion

        #region enemy
        // Enemy part 1
        private Texture2D enemyEncyclopediaTexture;
        private Texture2D enemy1EncyclopediaTexture;
        private Texture2D enemy2EncyclopediaTexture;
        private Texture2D enemy3EncyclopediaTexture;
        private Texture2D enemy4EncyclopediaTexture;
        private Texture2D enemy5EncyclopediaTexture;
        private Texture2D enemy6EncyclopediaTexture;
        private Texture2D enemy7EncyclopediaTexture;

        // Enemy Description
        private Texture2D enemy1EncyclopediaLargeTexture;
        private Texture2D enemy2EncyclopediaLargeTexture;
        private Texture2D enemy3EncyclopediaLargeTexture;
        private Texture2D enemy4EncyclopediaLargeTexture;
        private Texture2D enemy5EncyclopediaLargeTexture;
        private Texture2D enemy6EncyclopediaLargeTexture;
        private Texture2D enemy7EncyclopediaLargeTexture;
        #endregion

        #endregion



        public void Initialize(Game1 game, PlayerIndex index, Vector2 startPosition, List<Player> _players)
        {
            this.game = game;
            players = _players;
            contentManager = game.Content;
            graphicsDevice = game.GraphicsDevice;
            currentState = GamePad.GetState(playerIndex);
            showTitleScreen = true;
            showGameScreen = false;
            showPauseScreen = false;
            showGameOverScreen = false;
            showLevel1Screen = false;
            showLevel2Screen = false;
            showLevel3Screen = false;
            loadingGameScreen1 = false;
            loadingGameScreen2 = false;
            loadingGameScreen3 = false;
            showEncyclopediaScreen = false;
            showEnemy1EncyclopediaScreen = false;
            showTower1EncyclopediaScreen = false;
            showEnemy2EncyclopediaScreen = false;
            showTower2EncyclopediaScreen = false;
            showEnemy3EncyclopediaScreen = false;
            showTower3EncyclopediaScreen = false;
            restartGame = false;
            player1 = players[0];
            //player2 = players[1];
        }
       

        public UserInterface()
        {
        }

        public void LoadContent()
        {
            startScreenBackground = contentManager.Load<Texture2D>("startMenuBackground");
            winScreenBackground = contentManager.Load<Texture2D>("EndingScreen");
            gameoverScreenBackground = contentManager.Load<Texture2D>("gameoverNew");
            startMenuTexture = contentManager.Load<Texture2D>("start");
            font = contentManager.Load<SpriteFont>("font");
            level1MapTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\level1Screen");
            level2MapTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\level2Screen");
            level3MapTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\level3Screen");
            startLevelButtonTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\start");
            encyclopediaButtonTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\encyclopediaButton");
            encyclopediaBackgroundTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\background");
            startLevelButtonTextureSelect = contentManager.Load<Texture2D>(".\\encyclopedia\\start");
            encyclopediaButtonTextureSelect = contentManager.Load<Texture2D>(".\\encyclopedia\\encyclopediaButton");

            #region encyclopedia load content
            // main encyclopedia
            mainEncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\encyclopediaWindow");
            mainTowerEncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\encyclopedia_tower_yellow");
            mainEnemyEncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\encyclopedia_enemy_yellow");

            #region tower encyclopedia
            // tower part
            towerEncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\towerTemplate");
            arrowTowerEncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\arrow");
            canonTowerEncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\cannon1b");
            missileTowerEncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\missile");
            magicTowerEncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\magic");
            iceTowerEncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\ice");
            flameTowerEncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\flame");
            shockTowerEncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\lightning1");

            arrowTower2EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\arrow");
            canonTower2EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\cannon3");
            missileTower2EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\missile");
            magicTower2EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\magic");
            iceTower2EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\ice");
            flameTower2EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\flame");
            shockTower2EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\lightning3");

            arrowTower3EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\arrow");
            canonTower3EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\cannon5");
            missileTower3EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\missile");
            magicTower3EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\magic");
            iceTower3EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\ice");
            flameTower3EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\flame");
            shockTower3EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\lightning5");



            // tower description part
            arrowTowerEncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\arrowL");
            canonTowerEncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\cannon1L");
            missileTowerEncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\missileL");
            magicTowerEncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\magicL");
            iceTowerEncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\iceL");
            flameTowerEncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\flameL");
            shockTowerEncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\lightning1L");

            arrowTower2EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\arrowL");
            canonTower2EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\cannon3L");
            missileTower2EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\missileL");
            magicTower2EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\magicL");
            iceTower2EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\iceL");
            flameTower2EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\flameL");
            shockTower2EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\lightning3L");

            arrowTower3EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\arrowL");
            canonTower3EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\cannon5L");
            missileTower3EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\missileL");
            magicTower3EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\magicL");
            iceTower3EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\iceL");
            flameTower3EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\flameL");
            shockTower3EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\lightning5L");
            #endregion

            #region enemy encyclopedia
            // enemy part
            enemyEncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\enemyTemp");
            enemy1EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\stock");
            enemy2EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\muffin");
            enemy3EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\stick");
            enemy4EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\puddin");
            enemy5EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\snow");
            enemy6EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\muffin");
            enemy7EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\puddin");

            // enemy description
            enemy1EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\stockingL");
            enemy2EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\muffinL");
            enemy3EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\stickL");
            enemy4EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\puddinL");
            enemy5EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\snowL");
            enemy6EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\muffinL");
            enemy7EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\puddinL");
            #endregion

            #endregion

            statusBar = contentManager.Load<Texture2D>("statusBar");
        }

        public void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            int width = game.GraphicsDevice.Viewport.Width;
            int height = game.GraphicsDevice.Viewport.Height;
            //uiPosition1 = new Vector2(100, 30);

            //uiPosition1 = new Vector2(80, game.GraphicsDevice.Viewport.Height - 50);
            uiPosition2 = new Vector2(250, game.GraphicsDevice.Viewport.Height - 70);
            uiPosition3 = new Vector2(500, game.GraphicsDevice.Viewport.Height - 70);
            uiPosition4 = new Vector2(700, game.GraphicsDevice.Viewport.Height - 70);
            uiPositionMiddle = new Vector2(400, game.GraphicsDevice.Viewport.Height - 600);

            #region screen for game pad user
            if (currentState.IsConnected)//Game Pad
            {
                if (showTitleScreen == true)
                {
                    spriteBatch.Draw(startScreenBackground, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(startMenuTexture, new Vector2(400, 650), Color.White);
                    spriteBatch.DrawString(font, "Ver. Beta 1.3.0", new Vector2(width - 200, height - 50), Color.Black);
                }

                if (showGameScreen == false && showPauseScreen == true)
                {
                    spriteBatch.DrawString(font, "Game Paused", new Vector2(500, 450), Color.Black);
                    spriteBatch.DrawString(font, "Press Y to go back to title menu to quit", new Vector2(500, 470), Color.Black);
                    spriteBatch.DrawString(font, "Press A to re-play the game", new Vector2(500, 490), Color.Black);
                }

                if (showGameOverScreen == true)
                {
                    spriteBatch.Draw(gameoverScreenBackground, new Vector2(0, 0), Color.White);
                    //spriteBatch.DrawString(font, "Game Over", new Vector2(500, 600), Color.Black);
                    spriteBatch.DrawString(font, "Press A to retry", new Vector2(500, 700), Color.Black);
                    spriteBatch.DrawString(font, "Press Start to go back to title menu", new Vector2(500, 740), Color.Black);
                    spriteBatch.DrawString(font, "Ver. Beta 1.3.0", new Vector2(width - 200, height - 50), Color.Tomato);
                }

                if (showWinScreen == true)
                {
                    spriteBatch.Draw(startScreenBackground, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(font, "You Win", new Vector2(500, 580), Color.Black);
                    spriteBatch.DrawString(font, "Thank you for playing", new Vector2(500, 600), Color.Black);
                    spriteBatch.DrawString(font, "Press A to re-play the game", new Vector2(500, 700), Color.Black);
                    spriteBatch.DrawString(font, "Press Start to go back to title menu", new Vector2(500, 740), Color.Black);
                    spriteBatch.DrawString(font, "Ver. Beta 1.3.0", new Vector2(width - 200, height - 50), Color.Tomato);
                }

                if (showGameScreen == true)
                {
                    spriteBatch.Draw(statusBar, new Vector2(10, -10), Color.White);

                    spriteBatch.DrawString(font, "Resources: ", uiPosition2, Color.Black);
                    if (nextWave == 1)
                    {
                        spriteBatch.DrawString(font, "Next Wave: Normal", uiPosition1, Color.Black);
                    }
                    else if (nextWave == 2)
                    {
                        spriteBatch.DrawString(font, "Next Wave: Normal", uiPosition1, Color.Black);
                    }
                    else if (nextWave == 3)
                    {
                        spriteBatch.DrawString(font, "Next Wave: High HP", uiPosition1, Color.Black);
                    }
                    else if (nextWave == 4)
                    {
                        spriteBatch.DrawString(font, "Next Wave: High HP", uiPosition1, Color.Black);
                    }
                    else if (nextWave == 5)
                    {
                        spriteBatch.DrawString(font, "Next Wave: Fast", uiPosition1, Color.Black);
                    }
                    else if (nextWave == 6)
                    {
                        spriteBatch.DrawString(font, "Next Wave: ???", uiPosition1, Color.Black);
                    }
                }
                if (loadingGameScreen1 == true && showGameScreen == true)
                {
                    spriteBatch.Draw(encyclopediaBackgroundTexture, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(startLevelButtonTexture, new Vector2(500, 900), Color.White);
                    spriteBatch.Draw(encyclopediaButtonTexture, new Vector2(650, 900), Color.White);
                    spriteBatch.DrawString(font, "Loading...", uiPositionMiddle, Color.Blue);
                }
                if (loadingGameScreen2 == true && showGameScreen == true)
                {
                    spriteBatch.Draw(encyclopediaBackgroundTexture, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(startLevelButtonTexture, new Vector2(500, 900), Color.White);
                    spriteBatch.Draw(encyclopediaButtonTexture, new Vector2(650, 900), Color.White);
                    spriteBatch.DrawString(font, "Loading...", uiPositionMiddle, Color.Blue);
                }
                if (loadingGameScreen3 == true && showGameScreen == true)
                {
                    spriteBatch.Draw(encyclopediaBackgroundTexture, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(startLevelButtonTexture, new Vector2(500, 900), Color.White);
                    spriteBatch.Draw(encyclopediaButtonTexture, new Vector2(650, 900), Color.White);
                    spriteBatch.DrawString(font, "Loading...", uiPositionMiddle, Color.Blue);
                }


            }

            #endregion



            #region screen for keyboard user
            else
            {

                #region title screen for keyboard user
                if (showTitleScreen == true)
                {
                    spriteBatch.Draw(startScreenBackground, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(startMenuTexture, new Vector2(400, 650), Color.White);
                    spriteBatch.DrawString(font, "Ver. Beta 1.3.0", new Vector2(width - 200, height - 50), Color.Tomato);
                }
                #endregion

                #region pause screen for keyboard user
                if (showGameScreen == false && showPauseScreen == true)
                {
                    spriteBatch.DrawString(font, "Game Paused", new Vector2(500, 250), Color.Black);
                    spriteBatch.DrawString(font, "Press delete to quit", new Vector2(500, 270), Color.Black);
                    spriteBatch.Draw(statusBar, new Vector2(10, -10), Color.White);
                }
                #endregion

                #region gameover screen for keyboard user
                if (showGameOverScreen == true)
                {
                    spriteBatch.Draw(startScreenBackground, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(font, "Game Over", new Vector2(500, 600), Color.Black);
                    spriteBatch.DrawString(font, "Press Enter to retry", new Vector2(500, 700), Color.Black);
                    spriteBatch.DrawString(font, "Press delete to quit", new Vector2(500, 740), Color.Black);
                    spriteBatch.DrawString(font, "Ver. Beta 1.3.0", new Vector2(width - 200, height - 50), Color.Tomato);
                }
                #endregion

                #region win screen for keyboard user
                if (showWinScreen == true)
                {
                    spriteBatch.Draw(winScreenBackground, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(font, "Thank you for playing", new Vector2(500, 600), Color.Black);
                    spriteBatch.DrawString(font, "Press Enter to restart", new Vector2(500, 700), Color.Black);
                    spriteBatch.DrawString(font, "Press delete to quit", new Vector2(500, 740), Color.Black);
                    spriteBatch.DrawString(font, "Ver. Beta 1.3.0", new Vector2(width - 200, height - 50), Color.Tomato);
                }
                #endregion

                #region game screen for keyboard user
                if (showGameScreen == true)
                {
                    spriteBatch.DrawString(font, player1.getFireStoneInInventory() + " Fire Stone", new Vector2(uiPosition2.X, uiPosition2.Y + 40), Color.Black);
                    spriteBatch.DrawString(font, player1.getWaterStoneInInventory() + " Thunder Stone", new Vector2(uiPosition2.X, uiPosition2.Y + 20), Color.Black);
                    spriteBatch.DrawString(font, player1.getHealStoneInInventory() + " Ice Stone", uiPosition2, Color.Black);
                    //spriteBatch.DrawString(font, "Resources: " + player1.getMoney(), uiPosition1, Color.Black);
                    spriteBatch.DrawString(font, "Lives: " + game.getLive(), uiPosition3, Color.Black);
                    spriteBatch.Draw(statusBar, new Vector2(10, -10), Color.White);
                    spriteBatch.DrawString(font, "" + player1.getMoney(), uiMoneyPosition, Color.White);
                    spriteBatch.DrawString(font, "" + game.getLive(), uiLifePosition, Color.White);
                    spriteBatch.DrawString(font, "" + game.Wave.CurrentWave + " / " + game.Wave.totalWave(), uiWavePosition, Color.White);
                    spriteBatch.DrawString(font, "" + player1.getFireStoneInInventory(), uiFirePosition, Color.White);
                    spriteBatch.DrawString(font, "" + player1.getHealStoneInInventory(), uiIcePosition, Color.White);
                    spriteBatch.DrawString(font, "" + player1.getWaterStoneInInventory(), uiLightningPosition, Color.White);

                    if (nextWave == 1)
                    {
                        spriteBatch.DrawString(font, "Next Wave: Normal", uiPosition4, Color.Black);
                    }
                    else if (nextWave == 2)
                    {
                        spriteBatch.DrawString(font, "Next Wave: Normal", uiPosition4, Color.Black);
                    }
                    else if (nextWave == 3)
                    {
                        spriteBatch.DrawString(font, "Next Wave: High HP", uiPosition4, Color.Black);
                    }
                    else if (nextWave == 4)
                    {
                        spriteBatch.DrawString(font, "Next Wave: High HP", uiPosition4, Color.Black);
                    }
                    else if (nextWave == 5)
                    {
                        spriteBatch.DrawString(font, "Next Wave: Fast", uiPosition4, Color.Black);
                    }
                    else if (nextWave == 6)
                    {
                        spriteBatch.DrawString(font, "Next Wave: ???", uiPosition4, Color.Black);
                    }
                }
                #endregion

                #region level one loading screen for keyboard user
                if (loadingGameScreen1 == true && showGameScreen == true)
                {
                    spriteBatch.Draw(level1MapTexture, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(startLevelButtonTexture, new Vector2(500, 900), Color.White);
                    spriteBatch.Draw(encyclopediaButtonTexture, new Vector2(650, 900), Color.White);
                    spriteBatch.DrawString(font, "Loading...", uiPositionMiddle, Color.Blue);
                }
                #endregion

                #region level two loading screen for keyboard user
                if (loadingGameScreen2 == true && showGameScreen == true)
                {
                    spriteBatch.Draw(level2MapTexture, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(startLevelButtonTexture, new Vector2(500, 900), Color.White);
                    spriteBatch.Draw(encyclopediaButtonTexture, new Vector2(650, 900), Color.White);
                    spriteBatch.DrawString(font, "Loading...", uiPositionMiddle, Color.Blue);
                }
                #endregion

                #region level three loading screen for keyboard user
                if (loadingGameScreen3 == true && showGameScreen == true)
                {
                    spriteBatch.Draw(level3MapTexture, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(startLevelButtonTexture, new Vector2(500, 900), Color.White);
                    spriteBatch.Draw(encyclopediaButtonTexture, new Vector2(650, 900), Color.White);
                    spriteBatch.DrawString(font, "Loading...", uiPositionMiddle, Color.Blue);
                }
                #endregion

                #region pre-level one game screen for keyboard user
                if (showLevel1Screen == true)
                {
                    spriteBatch.Draw(level1MapTexture, new Vector2(0, 0), Color.White);
                    if (xPos1 == 1 && yPos1 == 0)
                    {
                        spriteBatch.Draw(startLevelButtonTexture, new Vector2(500, 900), Color.LightBlue);
                        spriteBatch.Draw(encyclopediaButtonTexture, new Vector2(650, 900), Color.White);
                    }
                    if (xPos1 == 0 && yPos1 == 0)
                    {
                        spriteBatch.Draw(startLevelButtonTexture, new Vector2(500, 900), Color.White);
                        spriteBatch.Draw(encyclopediaButtonTexture, new Vector2(650, 900), Color.LightBlue);
                    }
                }
                #endregion

                #region pre-level two game screen for keyboard user
                else if (showLevel2Screen == true)
                {
                    spriteBatch.Draw(level2MapTexture, new Vector2(0, 0), Color.White);
                    if (xPos1 == 1 && yPos1 == 0)
                    {
                        spriteBatch.Draw(startLevelButtonTexture, new Vector2(500, 900), Color.LightBlue);
                        spriteBatch.Draw(encyclopediaButtonTexture, new Vector2(650, 900), Color.White);
                    }
                    if (xPos1 == 0 && yPos1 == 0)
                    {
                        spriteBatch.Draw(startLevelButtonTexture, new Vector2(500, 900), Color.White);
                        spriteBatch.Draw(encyclopediaButtonTexture, new Vector2(650, 900), Color.LightBlue);
                    }
                }
                #endregion

                #region pre-level three game screen for keyboard user
                else if (showLevel3Screen == true)
                {
                    spriteBatch.Draw(level3MapTexture, new Vector2(0, 0), Color.White);
                    if (xPos1 == 1 && yPos1 == 0)
                    {
                        spriteBatch.Draw(startLevelButtonTexture, new Vector2(500, 900), Color.LightBlue);
                        spriteBatch.Draw(encyclopediaButtonTexture, new Vector2(650, 900), Color.White);
                    }
                    if (xPos1 == 0 && yPos1 == 0)
                    {
                        spriteBatch.Draw(startLevelButtonTexture, new Vector2(500, 900), Color.White);
                        spriteBatch.Draw(encyclopediaButtonTexture, new Vector2(650, 900), Color.LightBlue);
                    }
                }
                #endregion

                #region main encyclopedia screen for keyboard user
                if (showEncyclopediaScreen == true)
                {

                    // background image 
                    if (currentLevel == 1)
                        spriteBatch.Draw(level1MapTexture, new Vector2(0, 0), Color.White);
                    else if (currentLevel == 2)
                        spriteBatch.Draw(level1MapTexture, new Vector2(0, 0), Color.White);
                    else if (currentLevel == 3)
                        spriteBatch.Draw(level1MapTexture, new Vector2(0, 0), Color.White);

                    // negate previous background image
                    spriteBatch.Draw(encyclopediaBackgroundTexture, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(mainEncyclopediaTexture, new Vector2(230, 130), Color.White);


                    // left option
                    if (xPos1 == 0)
                    {
                        spriteBatch.Draw(mainTowerEncyclopediaTexture, new Vector2(300, 300), Color.White);
                        spriteBatch.Draw(mainEnemyEncyclopediaTexture, new Vector2(670, 300), Color.LightBlue);
                    }
                    // right option
                    if (xPos1 == 1)
                    {
                        spriteBatch.Draw(mainTowerEncyclopediaTexture, new Vector2(300, 300), Color.LightBlue);
                        spriteBatch.Draw(mainEnemyEncyclopediaTexture, new Vector2(670, 300), Color.White);
                    }
                }
                #endregion

                #region tower encyclopedia screen for keyboard use
                else if (showTower1EncyclopediaScreen == true)
                {

                    // background image
                    if (currentLevel == 1)
                        spriteBatch.Draw(encyclopediaBackgroundTexture, new Vector2(0, 0), Color.White);
                    else if (currentLevel == 2)
                        spriteBatch.Draw(encyclopediaBackgroundTexture, new Vector2(0, 0), Color.White);
                    else if (currentLevel == 3)
                        spriteBatch.Draw(encyclopediaBackgroundTexture, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(towerEncyclopediaTexture, new Vector2(0, 0), Color.White);

                    if (currentLevel == 1)
                    {
                        spriteBatch.Draw(arrowTowerEncyclopediaTexture, new Vector2(350, 320), Color.LightBlue);
                        spriteBatch.Draw(canonTowerEncyclopediaTexture, new Vector2(450, 320), Color.LightBlue);
                        spriteBatch.Draw(missileTowerEncyclopediaTexture, new Vector2(450, 435), Color.LightBlue);
                        spriteBatch.Draw(magicTowerEncyclopediaTexture, new Vector2(350, 435), Color.LightBlue);
                        spriteBatch.Draw(flameTowerEncyclopediaTexture, new Vector2(350, 600), Color.LightBlue);
                        spriteBatch.Draw(iceTowerEncyclopediaTexture, new Vector2(450, 600), Color.LightBlue);
                        spriteBatch.Draw(shockTowerEncyclopediaTexture, new Vector2(350, 715), Color.LightBlue);
                    }

                    if (currentLevel == 2)
                    {
                        spriteBatch.Draw(arrowTowerEncyclopediaTexture, new Vector2(350, 320), Color.LightBlue);
                        spriteBatch.Draw(canonTowerEncyclopediaTexture, new Vector2(450, 320), Color.LightBlue);
                        spriteBatch.Draw(missileTowerEncyclopediaTexture, new Vector2(450, 435), Color.LightBlue);
                        spriteBatch.Draw(magicTowerEncyclopediaTexture, new Vector2(350, 435), Color.LightBlue);
                        spriteBatch.Draw(flameTowerEncyclopediaTexture, new Vector2(350, 600), Color.LightBlue);
                        spriteBatch.Draw(iceTowerEncyclopediaTexture, new Vector2(450, 600), Color.LightBlue);
                        spriteBatch.Draw(shockTowerEncyclopediaTexture, new Vector2(350, 715), Color.LightBlue);
                    }

                    if (currentLevel == 3)
                    {
                        spriteBatch.Draw(arrowTowerEncyclopediaTexture, new Vector2(350, 320), Color.LightBlue);
                        spriteBatch.Draw(canonTowerEncyclopediaTexture, new Vector2(450, 320), Color.LightBlue);
                        spriteBatch.Draw(missileTowerEncyclopediaTexture, new Vector2(450, 435), Color.LightBlue);
                        spriteBatch.Draw(magicTowerEncyclopediaTexture, new Vector2(350, 435), Color.LightBlue);
                        spriteBatch.Draw(flameTowerEncyclopediaTexture, new Vector2(350, 600), Color.LightBlue);
                        spriteBatch.Draw(iceTowerEncyclopediaTexture, new Vector2(450, 600), Color.LightBlue);
                        spriteBatch.Draw(shockTowerEncyclopediaTexture, new Vector2(350, 715), Color.LightBlue);
                    }


                    #region level1 tower encyclopedia
                    if (currentLevel == 1)
                    {
                        if (xPos2 == 0 && yPos2 == 0)
                        {
                            spriteBatch.Draw(arrowTowerEncyclopediaTexture, new Vector2(350, 320), Color.White);
                            spriteBatch.Draw(arrowTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(arrowTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(arrowTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(arrowTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(arrowTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(arrowTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(arrowTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(arrowTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(arrowTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                        if (xPos2 == 1 && yPos2 == 0)
                        {
                            spriteBatch.Draw(canonTowerEncyclopediaTexture, new Vector2(450, 320), Color.White);
                            spriteBatch.Draw(canonTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(canonTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(canonTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(canonTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(canonTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(canonTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(canonTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(canonTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(canonTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                        if (xPos2 == 0 && yPos2 == 1)
                        {
                            spriteBatch.Draw(magicTowerEncyclopediaTexture, new Vector2(350, 435), Color.White);
                            spriteBatch.Draw(magicTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(magicTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(magicTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(magicTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(magicTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(magicTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(magicTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(magicTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(magicTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                        if (xPos2 == 1 && yPos2 == 1)
                        {
                            spriteBatch.Draw(missileTowerEncyclopediaTexture, new Vector2(450, 435), Color.White);
                            spriteBatch.Draw(missileTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(missileTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(missileTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(missileTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(missileTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(missileTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(missileTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(missileTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(missileTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                        if (xPos2 == 0 && yPos2 == 2)
                        {
                            spriteBatch.Draw(flameTowerEncyclopediaTexture, new Vector2(350, 600), Color.White);
                            spriteBatch.Draw(flameTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(flameTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(flameTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(flameTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(flameTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(flameTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(flameTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(flameTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(flameTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                        if (xPos2 == 1 && yPos2 == 2)
                        {
                            spriteBatch.Draw(iceTowerEncyclopediaTexture, new Vector2(450, 600), Color.White);
                            spriteBatch.Draw(iceTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(iceTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(iceTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(iceTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(iceTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(iceTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(iceTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(iceTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(iceTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                        if (xPos2 == 0 && yPos2 == 3)
                        {
                            spriteBatch.Draw(shockTowerEncyclopediaTexture, new Vector2(350, 715), Color.White);
                            spriteBatch.Draw(shockTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(shockTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(shockTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(shockTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(shockTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(shockTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(shockTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(shockTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(shockTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                    }
                    #endregion


                    #region level2 tower encyclopedia
                    if (currentLevel == 2)
                    {
                        if (xPos2 == 0 && yPos2 == 0)
                        {
                            spriteBatch.Draw(arrowTowerEncyclopediaTexture, new Vector2(350, 320), Color.White);
                            spriteBatch.Draw(arrowTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(arrowTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(arrowTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(arrowTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(arrowTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(arrowTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(arrowTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(arrowTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(arrowTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                        if (xPos2 == 1 && yPos2 == 0)
                        {
                            spriteBatch.Draw(canonTowerEncyclopediaTexture, new Vector2(450, 320), Color.White);
                            spriteBatch.Draw(canonTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(canonTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(canonTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(canonTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(canonTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(canonTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(canonTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(canonTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(canonTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                        if (xPos2 == 0 && yPos2 == 1)
                        {
                            spriteBatch.Draw(magicTowerEncyclopediaTexture, new Vector2(350, 435), Color.White);
                            spriteBatch.Draw(magicTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(magicTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(magicTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(magicTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(magicTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(magicTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(magicTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(magicTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(magicTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                        if (xPos2 == 1 && yPos2 == 1)
                        {
                            spriteBatch.Draw(missileTowerEncyclopediaTexture, new Vector2(450, 435), Color.White);
                            spriteBatch.Draw(missileTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(missileTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(missileTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(missileTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(missileTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(missileTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(missileTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(missileTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(missileTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                        if (xPos2 == 0 && yPos2 == 2)
                        {
                            spriteBatch.Draw(flameTowerEncyclopediaTexture, new Vector2(350, 600), Color.White);
                            spriteBatch.Draw(flameTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(flameTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(flameTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(flameTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(flameTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(flameTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(flameTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(flameTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(flameTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                        if (xPos2 == 1 && yPos2 == 2)
                        {
                            spriteBatch.Draw(iceTowerEncyclopediaTexture, new Vector2(450, 600), Color.White);
                            spriteBatch.Draw(iceTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(iceTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(iceTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(iceTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(iceTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(iceTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(iceTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(iceTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(iceTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                        if (xPos2 == 0 && yPos2 == 3)
                        {
                            spriteBatch.Draw(shockTowerEncyclopediaTexture, new Vector2(350, 715), Color.White);
                            spriteBatch.Draw(shockTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(shockTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(shockTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(shockTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(shockTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(shockTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(shockTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(shockTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(shockTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                    }
                    #endregion


                    #region level3 tower encyclopedia
                    if (currentLevel == 3)
                    {
                        if (xPos2 == 0 && yPos2 == 0)
                        {
                            spriteBatch.Draw(arrowTowerEncyclopediaTexture, new Vector2(350, 320), Color.White);
                            spriteBatch.Draw(arrowTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(arrowTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(arrowTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(arrowTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(arrowTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(arrowTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(arrowTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(arrowTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(arrowTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                        if (xPos2 == 1 && yPos2 == 0)
                        {
                            spriteBatch.Draw(canonTowerEncyclopediaTexture, new Vector2(450, 320), Color.White);
                            spriteBatch.Draw(canonTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(canonTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(canonTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(canonTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(canonTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(canonTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(canonTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(canonTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(canonTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                        if (xPos2 == 0 && yPos2 == 1)
                        {
                            spriteBatch.Draw(magicTowerEncyclopediaTexture, new Vector2(350, 435), Color.White);
                            spriteBatch.Draw(magicTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(magicTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(magicTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(magicTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(magicTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(magicTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(magicTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(magicTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(magicTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                        if (xPos2 == 1 && yPos2 == 1)
                        {
                            spriteBatch.Draw(missileTowerEncyclopediaTexture, new Vector2(450, 435), Color.White);
                            spriteBatch.Draw(missileTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(missileTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(missileTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(missileTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(missileTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(missileTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(missileTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(missileTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(missileTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                        if (xPos2 == 0 && yPos2 == 2)
                        {
                            spriteBatch.Draw(flameTowerEncyclopediaTexture, new Vector2(350, 600), Color.White);
                            spriteBatch.Draw(flameTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(flameTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(flameTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(flameTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(flameTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(flameTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(flameTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(flameTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(flameTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                        if (xPos2 == 1 && yPos2 == 2)
                        {
                            spriteBatch.Draw(iceTowerEncyclopediaTexture, new Vector2(450, 600), Color.White);
                            spriteBatch.Draw(iceTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(iceTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(iceTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(iceTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(iceTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(iceTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(iceTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(iceTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(iceTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                        if (xPos2 == 0 && yPos2 == 3)
                        {
                            spriteBatch.Draw(shockTowerEncyclopediaTexture, new Vector2(350, 715), Color.White);
                            spriteBatch.Draw(shockTowerEncyclopediaTexture, new Vector2(900, 260), Color.LightBlue);
                            spriteBatch.Draw(shockTower2EncyclopediaTexture, new Vector2(900, 360), Color.LightBlue);
                            spriteBatch.Draw(shockTower3EncyclopediaTexture, new Vector2(900, 460), Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(shockTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(shockTowerEncyclopediaTexture, new Vector2(900, 260), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(shockTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(shockTower2EncyclopediaTexture, new Vector2(900, 360), Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(shockTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(shockTower3EncyclopediaTexture, new Vector2(900, 460), Color.White);
                            }
                        }
                    }
                    #endregion

                }
                #endregion

                #region enemy encyclopedia screen for keyboard use
                else if (showEnemy1EncyclopediaScreen == true)
                {
                    // background image
                    if (currentLevel == 1)
                        spriteBatch.Draw(level1MapTexture, new Vector2(0, 0), Color.White);
                    else if (currentLevel == 2)
                        spriteBatch.Draw(level1MapTexture, new Vector2(0, 0), Color.White);
                    else if (currentLevel == 3)
                        spriteBatch.Draw(level1MapTexture, new Vector2(0, 0), Color.White);
                    // negate previous background imaga
                    spriteBatch.Draw(encyclopediaBackgroundTexture, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(enemyEncyclopediaTexture, new Vector2(230, 130), Color.LightBlue);
                    //spriteBatch.Draw(enemyEncyclopediaTexture, new Vector2(0, 0), Color.LightBlue);

                    if (currentLevel == 1)
                    {
                        spriteBatch.Draw(enemy1EncyclopediaTexture, new Vector2(300, 300), Color.LightBlue);
                        spriteBatch.Draw(enemy2EncyclopediaTexture, new Vector2(400, 300), Color.LightBlue);
                        spriteBatch.Draw(enemy3EncyclopediaTexture, new Vector2(300, 430), Color.LightBlue);
                        spriteBatch.Draw(enemy4EncyclopediaTexture, new Vector2(400, 430), Color.LightBlue);
                        spriteBatch.Draw(enemy5EncyclopediaTexture, new Vector2(300, 560), Color.LightBlue);
                        spriteBatch.Draw(enemy6EncyclopediaTexture, new Vector2(400, 560), Color.LightBlue);
                        spriteBatch.Draw(enemy7EncyclopediaTexture, new Vector2(300, 690), Color.LightBlue);
                    }
                    if (xPos2 == 0 && yPos2 == 0)
                    {
                        spriteBatch.Draw(enemy1EncyclopediaTexture, new Vector2(300, 300), Color.White);
                        spriteBatch.Draw(enemy1EncyclopediaLargeTexture, new Vector2(620, 300), Color.White);
                    }
                    if (xPos2 == 1 && yPos2 == 0)
                    {
                        spriteBatch.Draw(enemy2EncyclopediaTexture, new Vector2(400, 300), Color.White);
                        spriteBatch.Draw(enemy2EncyclopediaLargeTexture, new Vector2(620, 300), Color.White);
                    }
                    if (xPos2 == 0 && yPos2 == 1)
                    {
                        spriteBatch.Draw(enemy3EncyclopediaTexture, new Vector2(300, 430), Color.White);
                        spriteBatch.Draw(enemy3EncyclopediaLargeTexture, new Vector2(620, 300), Color.White);
                    }
                    if (xPos2 == 1 && yPos2 == 1)
                    {
                        spriteBatch.Draw(enemy4EncyclopediaTexture, new Vector2(400, 430), Color.White);
                        spriteBatch.Draw(enemy4EncyclopediaLargeTexture, new Vector2(620, 300), Color.White);
                    }
                    if (xPos2 == 0 && yPos2 == 2)
                    {
                        spriteBatch.Draw(enemy5EncyclopediaTexture, new Vector2(300, 560), Color.White);
                        spriteBatch.Draw(enemy5EncyclopediaLargeTexture, new Vector2(620, 300), Color.White);
                    }
                    if (xPos2 == 1 && yPos2 == 2)
                    {
                        spriteBatch.Draw(enemy6EncyclopediaTexture, new Vector2(400, 560), Color.White);
                        spriteBatch.Draw(enemy6EncyclopediaLargeTexture, new Vector2(620, 300), Color.White);
                    }
                    if (xPos2 == 0 && yPos2 == 3)
                    {
                        spriteBatch.Draw(enemy7EncyclopediaTexture, new Vector2(300, 690), Color.White);
                        spriteBatch.Draw(enemy7EncyclopediaLargeTexture, new Vector2(620, 300), Color.White);
                    }
                }
                #endregion

                // testing purpose only, set runTestChecking = true to enable this
                if (runTestChecking)
                {

                    /// checking the buildMenu boolean, need to change all of these from private to public at player.cs class
                    /*
                    if (player1.upgradeBuildMode == true)
                        spriteBatch.DrawString(font, "upgradeBuildMode == true", new Vector2(200, 400), Color.Green);
                    else
                        spriteBatch.DrawString(font, "upgradeBuildMode == false", new Vector2(200, 400), Color.Green);

                    if (player1.upgradeBuildMagicMode == true)
                        spriteBatch.DrawString(font, "upgradeBuildMagicMode == true", new Vector2(200, 350), Color.Green);
                    else
                        spriteBatch.DrawString(font, "upgradeBuildMagicMode == false", new Vector2(200, 350), Color.Green);

                    if (player1.buildMode == true)
                        spriteBatch.DrawString(font, "buildMode == true", new Vector2(200, 450), Color.Green);
                    else
                        spriteBatch.DrawString(font, "buildMode == false", new Vector2(200, 450), Color.Green);

                    if (player1.mainBuildMode == true)
                        spriteBatch.DrawString(font, "mainBuildMode == true", new Vector2(200, 500), Color.Green);
                    else
                        spriteBatch.DrawString(font, "mainBuildMode == false", new Vector2(200, 500), Color.Green);
                     */
                }

            }
            #endregion


            //vury 
            //spriteBatch.Draw(menu1Texture, new Vector2(position.X - 25, position.Y - 25), Color.White);
            //spriteBatch.Draw(menu2Texture, new Vector2(position.X + 25, position.Y - 25), null, Color.White, (float)shootRotationAngle, new Vector2(0, turretTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);
 

            //spriteBatch.End();
        }

        public void Update()
        {
            if (skipToGame == true)
            {
                showGameScreen = true;
                showPauseScreen = false;
                showTitleScreen = false;
                showGameOverScreen = false;
                skipToGame = false;

            }

            if (restartGame == true)
            {
                showGameScreen = true;
                showPauseScreen = false;
                showTitleScreen = false;
                showGameOverScreen = false;
                
            }

            if (showGameOverScreen == true)
            {
                showGameScreen = false;
                showPauseScreen = false;
                showTitleScreen = false;
                showWinScreen = false;
            }

            if (showWinScreen == true)
            {
                showGameScreen = false;
                showPauseScreen = false;
                showTitleScreen = false;
                showGameOverScreen = false;
            }

            // Get the game pad state.
            currentState = GamePad.GetState(playerIndex);

            //Get Input
            if (currentState.IsConnected)//Game Pad
                updateGamePad();
            else //Keyboard and Mouse
                updateKeyboard();

            

            oldMousestate = mousestate;
            oldKeyState = keystate;
            oldState = currentState;
        }

        #region game pad update
        public void updateGamePad()
        {

            updateGamePadForAllScreen();


            if (showTitleScreen == true)
                updateGamePadForTitleScreen();

            else if (showLevel1Screen == true)
                updateGamePadForLevel1Screen();

            else if (showLevel2Screen == true)
                updateGamePadForLevel2Screen();

            else if (showLevel3Screen == true)
                updateGamePadForLevel3Screen();

            else if (showGameScreen == true)
                updateGamePadForGameScreen();

            else if (showPauseScreen == true)
                updateGamePadForPauseScreen();

            else if (showGameOverScreen == true)
                updateGamePadForGameOverScreen();

            else if (showEncyclopediaScreen == true)
                updateGamePadForEncyclopediaScreen();

            else if (showTower1EncyclopediaScreen == true)
                updateGamePadForTower1EncyclopediaScreen();



        }

        private void updateGamePadForAllScreen()
        {
            if (currentState.IsButtonDown(Buttons.Back) && oldState.IsButtonUp(Buttons.Back))
            {
                game.Exit();
            }


            if (currentState.IsButtonDown(Buttons.RightShoulder) && currentState.IsButtonDown(Buttons.LeftShoulder) && currentState.IsButtonDown(Buttons.RightTrigger) && currentState.IsButtonDown(Buttons.LeftTrigger) && currentState.IsButtonDown(Buttons.Y))
            {
                showWinScreen = true;
            }

        }

        private void updateGamePadForTitleScreen()
        {
            if (currentState.IsButtonDown(Buttons.Start) && oldState.IsButtonUp(Buttons.Start))
            {
                //showGameScreen = true;
                showGameScreen = false;
                showTitleScreen = false;
                showPauseScreen = false;
                showLevel1Screen = true;
                //restartGame = true;
                firstRun = false;
            }

            if (currentState.IsButtonDown(Buttons.A) && oldState.IsButtonUp(Buttons.A))
            {
                //showGameScreen = true;
                showGameScreen = false;
                showTitleScreen = false;
                showPauseScreen = false;
                showLevel1Screen = true;
                //restartGame = true;
                firstRun = false;
            }
        }

        private void updateGamePadForLevel1Screen()
        {
            xPosMax = 1;
            yPosMax = 0;

            if (currentState.IsButtonDown(Buttons.DPadRight) && oldState.IsButtonUp(Buttons.DPadRight) && xPos1 < xPosMax)
            {
                xPos1++;
            }
            if (currentState.IsButtonDown(Buttons.DPadLeft) && oldState.IsButtonUp(Buttons.DPadLeft) && xPos1 > 0)
            {
                xPos1--;
            }
            if (currentState.IsButtonDown(Buttons.A) && oldState.IsButtonUp(Buttons.A) && xPos1 == 0 && yPos1 == 0)
            {
                showGameScreen = true;
                loadingGameScreen1 = true;
                showTitleScreen = false;
                showPauseScreen = false;
                showLevel1Screen = false;
                restartGame = true;
            }

            if (currentState.IsButtonDown(Buttons.A) && oldState.IsButtonUp(Buttons.A) && xPos1 == 1 && yPos1 == 0)
            {
                showLevel1Screen = false;
                showEncyclopediaScreen = true;
                xPos1 = 0;
                yPos1 = 0;
            }


        }

        private void updateGamePadForLevel2Screen()
        {
            if (currentState.IsButtonDown(Buttons.A) && oldState.IsButtonUp(Buttons.A))
            {
                showGameScreen = true;
                loadingGameScreen2 = true;
                showTitleScreen = false;
                showPauseScreen = false;
                showLevel2Screen = false;
                //restartGame = true;
                game.goNextLevel();
            }
        }

        private void updateGamePadForLevel3Screen()
        {
            if (currentState.IsButtonDown(Buttons.A) && oldState.IsButtonUp(Buttons.A))
            {
                showGameScreen = true;
                loadingGameScreen3 = true;
                showTitleScreen = false;
                showPauseScreen = false;
                showLevel3Screen = false;
                //restartGame = true;
                game.goNextLevel();
            }
        }

        private void updateGamePadForPauseScreen()
        {
            if (currentState.IsButtonDown(Buttons.Start) && oldState.IsButtonUp(Buttons.Start))
            {
                showGameScreen = true;
                showPauseScreen = false;
            }

            if (currentState.IsButtonDown(Buttons.Y) && oldState.IsButtonUp(Buttons.Y))
            {
                showTitleScreen = true;
                showGameOverScreen = false;
                showGameScreen = false;
                showPauseScreen = false;
            }

            if (currentState.IsButtonDown(Buttons.A) && oldState.IsButtonUp(Buttons.A))
            {
                showGameScreen = true;
                showTitleScreen = false;
                showPauseScreen = false;
                restartGame = true;
            }

        }

        private void updateGamePadForGameScreen()
        {
            if (currentState.IsButtonDown(Buttons.Start) && oldState.IsButtonUp(Buttons.Start))
            {
                showGameScreen = false;
                showPauseScreen = true;
            }
        }

        private void updateGamePadForGameOverScreen()
        {
            if (currentState.IsButtonDown(Buttons.A) && oldState.IsButtonUp(Buttons.A))
            {
                showGameScreen = true;
                showGameOverScreen = false;
                showPauseScreen = false;
                restartGame = true;
            }

            if (currentState.IsButtonDown(Buttons.Start) && oldState.IsButtonUp(Buttons.Start))
            {
                showTitleScreen = true;
                showGameOverScreen = false;
                showGameScreen = false;
                showPauseScreen = false;
            }

        }

        private void updateGamePadForWinScreen()
        {
            if (currentState.IsButtonDown(Buttons.A) && oldState.IsButtonUp(Buttons.A))
            {
                showGameScreen = true;
                showGameOverScreen = false;
                showPauseScreen = false;
                restartGame = true;
                showWinScreen = false;
            }

            if (currentState.IsButtonDown(Buttons.Start) && oldState.IsButtonUp(Buttons.Start))
            {
                showTitleScreen = true;
                showGameOverScreen = false;
                showGameScreen = false;
                showPauseScreen = false;
                showWinScreen = false;
            }
        }

        private void updateGamePadForEncyclopediaScreen()
        {
            xPosMax = 1;
            yPosMax = 0;

            if (currentState.IsButtonDown(Buttons.Y) && oldState.IsButtonUp(Buttons.Y))
            {
                showEncyclopediaScreen = false;
                showLevel1Screen = true;
            }

            if (currentState.IsButtonDown(Buttons.DPadRight) && oldState.IsButtonUp(Buttons.DPadRight) && xPos1 < xPosMax)
            {
                xPos1++;
            }
            if (currentState.IsButtonDown(Buttons.DPadLeft) && oldState.IsButtonUp(Buttons.DPadLeft) && xPos1 > 0)
            {
                xPos1--;
            }
            if (currentState.IsButtonDown(Buttons.A) && oldState.IsButtonUp(Buttons.A) && xPos1 == 0 && yPos1 == 0)
            {
                showTower1EncyclopediaScreen = true;
                showEncyclopediaScreen = false;
                xPos2 = 0;
                yPos2 = 0;
            }

            if (currentState.IsButtonDown(Buttons.A) && oldState.IsButtonUp(Buttons.A) && xPos1 == 1 && yPos1 == 0)
            {
                showEnemy1EncyclopediaScreen = true;
                showEncyclopediaScreen = false;
                xPos2 = 0;
                yPos2 = 0;
            }

        }

        private void updateGamePadForTower1EncyclopediaScreen()
        {
            if (showTower1EncyclopediaScreen == true)
            {
                xPosMax = 2;
                yPosMax = 0;
            }
            else if (showTower2EncyclopediaScreen == true)
            {
                xPosMax = 2;
                yPosMax = 1;
            }
            else if (showTower3EncyclopediaScreen == true)
            {
                xPosMax = 2;
                yPosMax = 0;
            }
            if (currentState.IsButtonDown(Buttons.Y) && oldState.IsButtonUp(Buttons.Y))
            {
                showEncyclopediaScreen = true;
                showTower1EncyclopediaScreen = false;
            }

            if (currentState.IsButtonDown(Buttons.DPadRight) && oldState.IsButtonUp(Buttons.DPadRight) && xPos2 < xPosMax)
            {
                xPos2++;
            }
            if (currentState.IsButtonDown(Buttons.DPadLeft) && oldState.IsButtonUp(Buttons.DPadLeft) && xPos2 > 0)
            {
                xPos2--;
            }

        }
        #endregion

        #region keyboard update
        public void updateKeyboard()
        {
            keystate = Keyboard.GetState();
            mousestate = Mouse.GetState();
            mousePos.X = mousestate.X;
            mousePos.Y = mousestate.Y;

            updateKeyboardForAllScreen();

            if (showTitleScreen == true)
                updateKeyboardForTitleScreen();

            else if (showLevel1Screen == true)
                updateKeyboardForLevel1Screen();

            else if (showLevel2Screen == true)
                updateKeyboardForLevel2Screen();

            else if (showLevel3Screen == true)
                updateKeyboardForLevel3Screen();

            else if (showGameScreen == true)
                updateKeyboardForGameScreen();

            else if (showPauseScreen == true)
                updateKeyboardForPauseScreen();

            else if (showGameOverScreen == true)
                updateKeyboardForGameOverScreen();

            else if (showEncyclopediaScreen == true)
                updateKeyboardForEncyclopediaScreen();

            else if (showTower1EncyclopediaScreen == true)
                updateKeyboardForTower1EncyclopediaScreen();

            else if (showEnemy1EncyclopediaScreen == true)
                updateKeyboardForEnemy1EncyclopediaScreen();

        }

        private void updateKeyboardForAllScreen()
        {
            if (keystate.IsKeyUp(Keys.Delete) && oldKeyState.IsKeyDown(Keys.Delete))
            {
                game.Exit();
            }

            if (keystate.IsKeyUp(Keys.P) && oldKeyState.IsKeyDown(Keys.P))
            {
                game.Exit();
            }

            if (keystate.IsKeyUp(Keys.F10) && oldKeyState.IsKeyDown(Keys.F10))
            {
                showGameOverScreen = true;
            }

        }

        private void updateKeyboardForTitleScreen()
        {
            if (keystate.IsKeyDown(Keys.Enter) && oldKeyState.IsKeyUp(Keys.Enter))
            {
                //showGameScreen = true;
                showGameScreen = false;
                showTitleScreen = false;
                showPauseScreen = false;
                showLevel1Screen = true;
                //restartGame = true;
                firstRun = false;
            }
        }

        private void updateKeyboardForLevel1Screen()
        {
            currentLevel = 1;
            xPosMax = 1;
            yPosMax = 0;

            if (keystate.IsKeyDown(Keys.Right) && oldKeyState.IsKeyUp(Keys.Right) && xPos1 < xPosMax)
            {
                xPos1++;
            }
            if (keystate.IsKeyDown(Keys.Left) && oldKeyState.IsKeyUp(Keys.Left) && xPos1 > 0)
            {
                xPos1--;
            }
            if (keystate.IsKeyDown(Keys.Enter) && oldKeyState.IsKeyUp(Keys.Enter) && xPos1 == 0 && yPos1 == 0)
            {
                showGameScreen = true;
                loadingGameScreen1 = true;
                showTitleScreen = false;
                showPauseScreen = false;
                showLevel1Screen = false;
                restartGame = true;
            }

            if (keystate.IsKeyDown(Keys.Enter) && oldKeyState.IsKeyUp(Keys.Enter) && xPos1 == 1 && yPos1 == 0)
            {
                showLevel1Screen = false;
                showEncyclopediaScreen = true;
                xPos1 = 0;
                yPos1 = 0;
            }

        }

        private void updateKeyboardForLevel2Screen()
        {
            currentLevel = 2;
            xPosMax = 1;
            yPosMax = 0;
            if (keystate.IsKeyDown(Keys.Right) && oldKeyState.IsKeyUp(Keys.Right) && xPos1 < xPosMax)
            {
                xPos1++;
            }
            if (keystate.IsKeyDown(Keys.Left) && oldKeyState.IsKeyUp(Keys.Left) && xPos1 > 0)
            {
                xPos1--;
            }
            if (keystate.IsKeyDown(Keys.Enter) && oldKeyState.IsKeyUp(Keys.Enter) && xPos1 == 0 && yPos1 == 0)
            {
                showGameScreen = true;
                loadingGameScreen2 = true;
                showTitleScreen = false;
                showPauseScreen = false;
                showLevel2Screen = false;
                //restartGame = true;
                game.goNextLevel();
            }

            if (keystate.IsKeyDown(Keys.Enter) && oldKeyState.IsKeyUp(Keys.Enter) && xPos1 == 1 && yPos1 == 0)
            {
                showLevel2Screen = false;
                showEncyclopediaScreen = true;
                xPos1 = 0;
                yPos1 = 0;
            }
        }

        private void updateKeyboardForLevel3Screen()
        {
            currentLevel = 3;
            xPosMax = 1;
            yPosMax = 0;
            if (keystate.IsKeyDown(Keys.Right) && oldKeyState.IsKeyUp(Keys.Right) && xPos1 < xPosMax)
            {
                xPos1++;
            }
            if (keystate.IsKeyDown(Keys.Left) && oldKeyState.IsKeyUp(Keys.Left) && xPos1 > 0)
            {
                xPos1--;
            }
            if (keystate.IsKeyDown(Keys.Enter) && oldKeyState.IsKeyUp(Keys.Enter) && xPos1 == 0 && yPos1 == 0)
            {
                showGameScreen = true;
                loadingGameScreen3 = true;
                showTitleScreen = false;
                showPauseScreen = false;
                showLevel3Screen = false;
                //restartGame = true;
                game.goNextLevel();
            }

            if (keystate.IsKeyDown(Keys.Enter) && oldKeyState.IsKeyUp(Keys.Enter) && xPos1 == 1 && yPos1 == 0)
            {
                showLevel3Screen = false;
                showEncyclopediaScreen = true;
                xPos1 = 0;
                yPos1 = 0;
            }
        }

        private void updateKeyboardForPauseScreen()
        {
            if (keystate.IsKeyUp(Keys.Escape) && oldKeyState.IsKeyDown(Keys.Escape))
            {
                showGameScreen = true;
                showPauseScreen = false;
            }
            if (keystate.IsKeyUp(Keys.F1) && oldKeyState.IsKeyDown(Keys.F1))
            {
                showTitleScreen = true;
                showGameOverScreen = false;
                showGameScreen = false;
                showPauseScreen = false;
            }
        }

        private void updateKeyboardForGameScreen()
        {
            if (keystate.IsKeyUp(Keys.Escape) && oldKeyState.IsKeyDown(Keys.Escape))
            {
                showGameScreen = false;
                showPauseScreen = true;
            }
        }

        private void updateKeyboardForGameOverScreen()
        {
            if (keystate.IsKeyUp(Keys.Enter) && oldKeyState.IsKeyDown(Keys.Enter))
            {
                showGameScreen = true;
                showGameOverScreen = false;
                showPauseScreen = false;
                restartGame = true;
            }
        }

        private void updateKeyboardForWinScreen()
        {
            if (keystate.IsKeyUp(Keys.Enter) && oldKeyState.IsKeyDown(Keys.Enter))
            {
                showGameScreen = true;
                showGameOverScreen = false;
                showPauseScreen = false;
                restartGame = true;
            }
        }

        private void updateKeyboardForEncyclopediaScreen()
        {
            xPosMax = 1;
            yPosMax = 0;

            if (keystate.IsKeyUp(Keys.Escape) && oldKeyState.IsKeyDown(Keys.Escape))
            {
                showEncyclopediaScreen = false;
                showLevel1Screen = true;
            }

            if (keystate.IsKeyDown(Keys.Right) && oldKeyState.IsKeyUp(Keys.Right) && xPos1 < xPosMax)
            {
                xPos1++;
            }
            if (keystate.IsKeyDown(Keys.Left) && oldKeyState.IsKeyUp(Keys.Left) && xPos1 > 0)
            {
                xPos1--;
            }
            if (keystate.IsKeyDown(Keys.Enter) && oldKeyState.IsKeyUp(Keys.Enter) && xPos1 == 0 && yPos1 == 0)
            {
                showTower1EncyclopediaScreen = true;
                showEncyclopediaScreen = false;
                xPos2 = 0;
                yPos2 = 0;
            }

            if (keystate.IsKeyDown(Keys.Enter) && oldKeyState.IsKeyUp(Keys.Enter) && xPos1 == 1 && yPos1 == 0)
            {
                showEnemy1EncyclopediaScreen = true;
                showEncyclopediaScreen = false;
                xPos2 = 0;
                yPos2 = 0;
            }

        }

        private void updateKeyboardForTower1EncyclopediaScreen()
        {
            if (showTower1EncyclopediaScreen == true)
            {
                xPosMax = 1;
                yPosMax = 0;
            }
            else if (showTower2EncyclopediaScreen == true)
            {
                xPosMax = 1;
                yPosMax = 2;
            }
            else if (showTower3EncyclopediaScreen == true)
            {
                xPosMax = 1;
                yPosMax = 3;
            }
            if (keystate.IsKeyUp(Keys.Escape) && oldKeyState.IsKeyDown(Keys.Escape))
            {
                showEncyclopediaScreen = true;
                showTower1EncyclopediaScreen = false;
            }

            if (keystate.IsKeyDown(Keys.Right) && oldKeyState.IsKeyUp(Keys.Right) && xPos2 < xPosMax)
            {
                xPos2++;
                towerLevelEncyclopedia = 1;
            }
            if (keystate.IsKeyDown(Keys.Left) && oldKeyState.IsKeyUp(Keys.Left) && xPos2 > 0)
            {
                xPos2--;
                towerLevelEncyclopedia = 1;
            }
            if (keystate.IsKeyDown(Keys.Up) && oldKeyState.IsKeyUp(Keys.Up) && yPos2 > 0)
            {
                yPos2--;
                towerLevelEncyclopedia = 1;
            }
            if (keystate.IsKeyDown(Keys.Down) && oldKeyState.IsKeyUp(Keys.Down) && yPos2 < yPosMax)
            {
                yPos2++;
                towerLevelEncyclopedia = 1;
            }

            if (yPos2 == 3)
            {
                xPos2 = 0;
            }

            if (mousestate.ScrollWheelValue > oldMousestate.ScrollWheelValue && towerLevelEncyclopedia > 1)
            {
                towerLevelEncyclopedia--;
            }

            if (mousestate.ScrollWheelValue < oldMousestate.ScrollWheelValue && towerLevelEncyclopedia < 3)
            {
                towerLevelEncyclopedia++;
            }

        }

        private void updateKeyboardForEnemy1EncyclopediaScreen()
        {
            if (showEnemy1EncyclopediaScreen == true)
            {
                xPosMax = 1;
                yPosMax = 3;
            }
            else if (showEnemy2EncyclopediaScreen == true)
            {
                xPosMax = 2;
                yPosMax = 1;
            }
            else if (showEnemy3EncyclopediaScreen == true)
            {
                xPosMax = 1;
                yPosMax = 3;
            }
            if (keystate.IsKeyUp(Keys.Escape) && oldKeyState.IsKeyDown(Keys.Escape))
            {
                showEncyclopediaScreen = true;
                showEnemy1EncyclopediaScreen = false;
            }

            if (keystate.IsKeyDown(Keys.Right) && oldKeyState.IsKeyUp(Keys.Right) && xPos2 < xPosMax)
            {
                xPos2++;
            }
            if (keystate.IsKeyDown(Keys.Left) && oldKeyState.IsKeyUp(Keys.Left) && xPos2 > 0)
            {
                xPos2--;
            }
            if (keystate.IsKeyDown(Keys.Up) && oldKeyState.IsKeyUp(Keys.Up) && yPos2 > 0)
            {
                yPos2--;
            }
            if (keystate.IsKeyDown(Keys.Down) && oldKeyState.IsKeyUp(Keys.Down) && yPos2 < yPosMax)
            {
                yPos2++;
            }

            if (yPos2 == 3)
            {
                xPos2 = 0;
            }

        }
        #endregion

        public bool getScreen(string _screenName)
        {
            if (_screenName == "showTitleScreen")
                return showTitleScreen;
            else if (_screenName == "loadingGameScreen1")
                return loadingGameScreen1;
            else if (_screenName == "loadingGameScreen2")
                return loadingGameScreen2;
            else if (_screenName == "loadingGameScreen3")
                return loadingGameScreen3;
            else if (_screenName == "showEncyclopediaScreen")
                return showEncyclopediaScreen;
            else if (_screenName == "showGameOverScreen")
                return showGameOverScreen;
            else if (_screenName == "showGameScreen")
                return showGameScreen;
            else if (_screenName == "showLevel1Screen")
                return showLevel1Screen;
            else if (_screenName == "showLevel2Screen")
                return showLevel2Screen;
            else if (_screenName == "showLevel3Screen")
                return showLevel3Screen;
            else if (_screenName == "showPauseScreen")
                return showPauseScreen;
            else if (_screenName == "showWinScreen")
                return showWinScreen;
            else if (_screenName == "restartGame")
                return restartGame;
            else if (_screenName == "firstRun")
                return firstRun;
            else
                return false;
        }

        public void setScreenStatus(string _screenName, bool _status)
        {
            if (_screenName == "showGameScreen")
                showGameScreen = _status;
            else if (_screenName == "showTitleScreen")
                showTitleScreen = _status;
            else if (_screenName == "showGameOverScreen")
                showGameOverScreen = _status;
            else if (_screenName == "showWinScreen")
                showWinScreen = _status;
            else if (_screenName == "restartGame")
                restartGame = _status;
            else if (_screenName == "loadingGameScreen1")
                loadingGameScreen1 = _status;
            else if (_screenName == "loadingGameScreen2")
                loadingGameScreen2 = _status;
            else if (_screenName == "loadingGameScreen3")
                loadingGameScreen3 = _status;
            else if (_screenName == "showLevel1Screen")
                showLevel1Screen = _status;
            else if (_screenName == "showLevel2Screen")
                showLevel2Screen = _status;
            else if (_screenName == "showLevel3Screen")
                showLevel3Screen = _status;
            else if (_screenName == "firstRun")
                firstRun = _status;
        }

        public void setWavesNumber(int _wave)
        {
            nextWave = _wave + 1;
        }

        






    }
}
