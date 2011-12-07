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
        Texture2D timerSprite;
        Texture2D timerBar;
        
        public bool runTestChecking = false;  // true to show the test Status on every screen, false to show nothing
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


        private Vector2 uiEncyclopediaPosition1;
        private Vector2 uiEncyclopediaPosition2;
        private Vector2 uiEncyclopediaPosition3;
        private Vector2 uiEncyclopediaPosition4;
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

        private Texture2D arrowTowerEncyclopediaLarge1Texture;
        private Texture2D canonTowerEncyclopediaLarge1Texture;
        private Texture2D missileTowerEncyclopediaLarge1Texture;
        private Texture2D magicTowerEncyclopediaLarge1Texture;
        private Texture2D iceTowerEncyclopediaLarge1Texture;
        private Texture2D flameTowerEncyclopediaLarge1Texture;
        private Texture2D shockTowerEncyclopediaLarge1Texture;

        // Tower Description part 2
        private Texture2D arrowTower2EncyclopediaLargeTexture;
        private Texture2D canonTower2EncyclopediaLargeTexture;
        private Texture2D missileTower2EncyclopediaLargeTexture;
        private Texture2D magicTower2EncyclopediaLargeTexture;
        private Texture2D iceTower2EncyclopediaLargeTexture;
        private Texture2D flameTower2EncyclopediaLargeTexture;
        private Texture2D shockTower2EncyclopediaLargeTexture;

        private Texture2D arrowTower2EncyclopediaLarge1Texture;
        private Texture2D canonTower2EncyclopediaLarge1Texture;
        private Texture2D missileTower2EncyclopediaLarge1Texture;
        private Texture2D magicTower2EncyclopediaLarge1Texture;
        private Texture2D iceTower2EncyclopediaLarge1Texture;
        private Texture2D flameTower2EncyclopediaLarge1Texture;
        private Texture2D shockTower2EncyclopediaLarge1Texture;

        // Tower Description part 3
        private Texture2D arrowTower3EncyclopediaLargeTexture;
        private Texture2D canonTower3EncyclopediaLargeTexture;
        private Texture2D missileTower3EncyclopediaLargeTexture;
        private Texture2D magicTower3EncyclopediaLargeTexture;
        private Texture2D iceTower3EncyclopediaLargeTexture;
        private Texture2D flameTower3EncyclopediaLargeTexture;
        private Texture2D shockTower3EncyclopediaLargeTexture;

        private Texture2D arrowTower3EncyclopediaLarge1Texture;
        private Texture2D canonTower3EncyclopediaLarge1Texture;
        private Texture2D missileTower3EncyclopediaLarge1Texture;
        private Texture2D magicTower3EncyclopediaLarge1Texture;
        private Texture2D iceTower3EncyclopediaLarge1Texture;
        private Texture2D flameTower3EncyclopediaLarge1Texture;
        private Texture2D shockTower3EncyclopediaLarge1Texture;
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
            canonTowerEncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\cannon1");
            missileTowerEncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\missile");
            magicTowerEncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\magic");
            iceTowerEncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\ice1");
            flameTowerEncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\flame");
            shockTowerEncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\lightning1");

            arrowTower2EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\arrow2");
            canonTower2EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\cannon3");
            missileTower2EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\missile2");
            magicTower2EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\magic");
            iceTower2EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\ice2");
            flameTower2EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\flame2");
            shockTower2EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\lightning3");

            arrowTower3EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\arrow3");
            canonTower3EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\cannon5");
            missileTower3EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\missile3");
            magicTower3EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\magic");
            iceTower3EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\ice3");
            flameTower3EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\flame3");
            shockTower3EncyclopediaTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\lightning5");



            // tower description part
            arrowTowerEncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\arrowL");
            canonTowerEncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\cannon1L");
            missileTowerEncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\missileL");
            magicTowerEncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\magicL");
            iceTowerEncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\ice1L");
            flameTowerEncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\flameL");
            shockTowerEncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\lightning1L");

            arrowTowerEncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\arrowLv1");
            canonTowerEncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\cannonLv1");
            missileTowerEncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\missileLv1");
            magicTowerEncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\magicDescription");
            iceTowerEncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\iceLv1");
            flameTowerEncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\flameLv1");
            shockTowerEncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\telsaLv1");



            arrowTower2EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\arrow2L");
            canonTower2EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\cannon3L");
            missileTower2EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\missile2L");
            magicTower2EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\magicL");
            iceTower2EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\ice2L");
            flameTower2EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\flame2L");
            shockTower2EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\lightning3L");

            arrowTower2EncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\arrowLv2");
            canonTower2EncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\cannonLv2");
            missileTower2EncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\missileLv2");
            magicTower2EncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\magicDescription");
            iceTower2EncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\iceLv2");
            flameTower2EncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\flameLv2");
            shockTower2EncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\telsaLv2");



            arrowTower3EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\arrow3L");
            canonTower3EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\cannon5L");
            missileTower3EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\missle3L");
            magicTower3EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\magicL");
            iceTower3EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\ice3L");
            flameTower3EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\flame3L");
            shockTower3EncyclopediaLargeTexture = contentManager.Load<Texture2D>(".\\encyclopedia\\lightning5L");

            arrowTower3EncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\arrowLv3");
            canonTower3EncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\cannonLv3");
            missileTower3EncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\missileLv3");
            magicTower3EncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\magicDescription");
            iceTower3EncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\iceLv3");
            flameTower3EncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\flameLv3");
            shockTower3EncyclopediaLarge1Texture = contentManager.Load<Texture2D>(".\\encyclopedia\\telsaLv3");
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
            timerSprite = contentManager.Load<Texture2D>("enemystockRight");
            timerBar = contentManager.Load<Texture2D>("timer");

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

                #region title screen for gamepad user
                if (showTitleScreen == true)
                {
                    spriteBatch.Draw(startScreenBackground, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(startMenuTexture, new Vector2(400, 650), Color.White);
                    spriteBatch.DrawString(font, "Ver. Beta 1.3.0", new Vector2(width - 200, height - 50), Color.Tomato);
                }
                #endregion

                #region pause screen for gamepad user
                if (showGameScreen == false && showPauseScreen == true)
                {
                    spriteBatch.DrawString(font, "Game Paused", new Vector2(500, 250), Color.Black);
                    spriteBatch.DrawString(font, "Press delete to quit", new Vector2(500, 270), Color.Black);
                    spriteBatch.Draw(statusBar, new Vector2(10, -10), Color.White);
                }
                #endregion

                #region gameover screen for gamepad user
                if (showGameOverScreen == true)
                {
                    spriteBatch.Draw(startScreenBackground, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(font, "Game Over", new Vector2(500, 600), Color.Black);
                    spriteBatch.DrawString(font, "Press Enter to retry", new Vector2(500, 700), Color.Black);
                    spriteBatch.DrawString(font, "Press delete to quit", new Vector2(500, 740), Color.Black);
                    spriteBatch.DrawString(font, "Ver. Beta 1.3.0", new Vector2(width - 200, height - 50), Color.Tomato);
                }
                #endregion

                #region win screen for gamepad user
                if (showWinScreen == true)
                {
                    spriteBatch.Draw(winScreenBackground, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(font, "Thank you for playing", new Vector2(500, 600), Color.Black);
                    spriteBatch.DrawString(font, "Press Enter to restart", new Vector2(500, 700), Color.Black);
                    spriteBatch.DrawString(font, "Press delete to quit", new Vector2(500, 740), Color.Black);
                    spriteBatch.DrawString(font, "Ver. Beta 1.3.0", new Vector2(width - 200, height - 50), Color.Tomato);
                }
                #endregion

                #region game screen for gamepad user
                if (showGameScreen == true)
                {
                    spriteBatch.DrawString(font, Player.getFireStoneInInventory() + " Fire Stone", new Vector2(uiPosition2.X, uiPosition2.Y + 40), Color.Black);
                    spriteBatch.DrawString(font, Player.getWaterStoneInInventory() + " Thunder Stone", new Vector2(uiPosition2.X, uiPosition2.Y + 20), Color.Black);
                    spriteBatch.DrawString(font, Player.getHealStoneInInventory() + " Ice Stone", uiPosition2, Color.Black);
                    //spriteBatch.DrawString(font, "Resources: " + player1.getMoney(), uiPosition1, Color.Black);
                    spriteBatch.DrawString(font, "Lives: " + game.getLive(), uiPosition3, Color.Black);
                    spriteBatch.Draw(statusBar, new Vector2(10, -10), Color.White);
                    spriteBatch.DrawString(font, "" + Player.getMoney(), uiMoneyPosition, Color.White);
                    spriteBatch.DrawString(font, "" + game.getLive(), uiLifePosition, Color.White);
                    spriteBatch.DrawString(font, "" + game.Wave.CurrentWave + " / " + game.Wave.totalWave(), uiWavePosition, Color.White);
                    spriteBatch.DrawString(font, "" + Player.getFireStoneInInventory(), uiFirePosition, Color.White);
                    spriteBatch.DrawString(font, "" + Player.getHealStoneInInventory(), uiIcePosition, Color.White);
                    spriteBatch.DrawString(font, "" + Player.getWaterStoneInInventory(), uiLightningPosition, Color.White);

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

                #region level one loading screen for gamepad user
                if (loadingGameScreen1 == true && showGameScreen == true)
                {
                    spriteBatch.Draw(level1MapTexture, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(startLevelButtonTexture, new Vector2(500, 900), Color.White);
                    spriteBatch.Draw(encyclopediaButtonTexture, new Vector2(650, 900), Color.White);
                    spriteBatch.DrawString(font, "Loading...", uiPositionMiddle, Color.Blue);
                }
                #endregion

                #region level two loading screen for gamepad user
                if (loadingGameScreen2 == true && showGameScreen == true)
                {
                    spriteBatch.Draw(level2MapTexture, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(startLevelButtonTexture, new Vector2(500, 900), Color.White);
                    spriteBatch.Draw(encyclopediaButtonTexture, new Vector2(650, 900), Color.White);
                    spriteBatch.DrawString(font, "Loading...", uiPositionMiddle, Color.Blue);
                }
                #endregion

                #region level three loading screen for gamepad user
                if (loadingGameScreen3 == true && showGameScreen == true)
                {
                    spriteBatch.Draw(level3MapTexture, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(startLevelButtonTexture, new Vector2(500, 900), Color.White);
                    spriteBatch.Draw(encyclopediaButtonTexture, new Vector2(650, 900), Color.White);
                    spriteBatch.DrawString(font, "Loading...", uiPositionMiddle, Color.Blue);
                }
                #endregion

                #region pre-level one game screen for gamepad user
                if (showLevel1Screen == true)
                {
                    game.menuSoundInstance.Stop();
                    game.level2SoundInstance.Stop();
                    game.level3SoundInstance.Stop();
                    game.level1SoundInstance.Play();

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

                #region pre-level two game screen for gamepad user
                else if (showLevel2Screen == true)
                {
                    game.menuSoundInstance.Stop();
                    game.level2SoundInstance.Play();
                    game.level3SoundInstance.Stop();
                    game.level1SoundInstance.Stop();

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

                #region pre-level three game screen for gamepad user
                else if (showLevel3Screen == true)
                {
                    game.menuSoundInstance.Stop();
                    game.level2SoundInstance.Stop();
                    game.level3SoundInstance.Play();
                    game.level1SoundInstance.Stop();
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

                #region main encyclopedia screen for gamepad user
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

                #region tower encyclopedia screen for gamepad use
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
                    if (currentLevel == 1 || currentLevel == 2 || currentLevel == 3)
                    {
                        uiEncyclopediaPosition1 = new Vector2(900, 240);
                        uiEncyclopediaPosition2 = new Vector2(900, 350);
                        uiEncyclopediaPosition3 = new Vector2(900, 460);
                        uiEncyclopediaPosition4 = new Vector2(900, 260);

                        if (xPos2 == 0 && yPos2 == 0)
                        {
                            spriteBatch.Draw(arrowTowerEncyclopediaTexture, new Vector2(350, 320), Color.White);
                            spriteBatch.Draw(arrowTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.LightBlue);
                            spriteBatch.Draw(arrowTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.LightBlue);
                            spriteBatch.Draw(arrowTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(arrowTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(arrowTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(arrowTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(arrowTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(arrowTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(arrowTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.White);
                            }
                        }
                        if (xPos2 == 1 && yPos2 == 0)
                        {
                            spriteBatch.Draw(canonTowerEncyclopediaTexture, new Vector2(450, 320), Color.White);
                            spriteBatch.Draw(canonTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.LightBlue);
                            spriteBatch.Draw(canonTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.LightBlue);
                            spriteBatch.Draw(canonTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(canonTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(canonTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(canonTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(canonTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(canonTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(canonTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.White);
                            }
                        }
                        if (xPos2 == 0 && yPos2 == 1)
                        {
                            spriteBatch.Draw(magicTowerEncyclopediaTexture, new Vector2(350, 435), Color.White);
                            spriteBatch.Draw(magicTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.LightBlue);
                            spriteBatch.Draw(magicTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.LightBlue);
                            spriteBatch.Draw(magicTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(magicTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(magicTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(magicTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(magicTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(magicTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(magicTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.White);
                            }
                        }
                        if (xPos2 == 1 && yPos2 == 1)
                        {
                            spriteBatch.Draw(missileTowerEncyclopediaTexture, new Vector2(450, 435), Color.White);
                            spriteBatch.Draw(missileTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.LightBlue);
                            spriteBatch.Draw(missileTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.LightBlue);
                            spriteBatch.Draw(missileTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(missileTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(missileTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(missileTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(missileTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(missileTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(missileTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.White);
                            }
                        }
                        if (xPos2 == 0 && yPos2 == 2)
                        {
                            spriteBatch.Draw(flameTowerEncyclopediaTexture, new Vector2(350, 600), Color.White);
                            spriteBatch.Draw(flameTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.LightBlue);
                            spriteBatch.Draw(flameTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.LightBlue);
                            spriteBatch.Draw(flameTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(flameTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(flameTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(flameTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(flameTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(flameTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(flameTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.White);
                            }
                        }
                        if (xPos2 == 1 && yPos2 == 2)
                        {
                            spriteBatch.Draw(iceTowerEncyclopediaTexture, new Vector2(450, 600), Color.White);
                            spriteBatch.Draw(iceTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.LightBlue);
                            spriteBatch.Draw(iceTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.LightBlue);
                            spriteBatch.Draw(iceTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(iceTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(iceTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(iceTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(iceTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(iceTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(iceTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.White);
                            }
                        }
                        if (xPos2 == 0 && yPos2 == 3)
                        {
                            spriteBatch.Draw(shockTowerEncyclopediaTexture, new Vector2(350, 715), Color.White);
                            spriteBatch.Draw(shockTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.LightBlue);
                            spriteBatch.Draw(shockTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.LightBlue);
                            spriteBatch.Draw(shockTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(shockTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(shockTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(shockTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(shockTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(shockTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(shockTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.White);
                            }
                        }
                    }
                    #endregion


                    #region level2 tower encyclopedia
                    /*
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
                     * */
                    #endregion


                    #region level3 tower encyclopedia
                    /*
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
                     * */
                    #endregion

                }
                #endregion

                #region enemy encyclopedia screen for gamepad use
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


                #region testing boolean
                // testing purpose only, set runTestChecking = true to enable this
                if (runTestChecking)
                {
                    //spriteBatch.Draw(mainEncyclopediaTexture, new Vector2(50, 140), Color.White);
                    /// checking the buildMenu boolean, need to change all of these from private to public at player.cs class


                    if (player1.upgradeBuildMode == true)
                        spriteBatch.DrawString(font, "upgradeBuildMode == true", new Vector2(100, 320), Color.Red);
                    else
                        spriteBatch.DrawString(font, "upgradeBuildMode == false", new Vector2(100, 320), Color.Yellow);

                    if (player1.upgradeBuildMagicMode == true)
                        spriteBatch.DrawString(font, "upgradeBuildMagicMode == true", new Vector2(100, 300), Color.Red);
                    else
                        spriteBatch.DrawString(font, "upgradeBuildMagicMode == false", new Vector2(100, 300), Color.Yellow);

                    if (player1.buildMode == true)
                        spriteBatch.DrawString(font, "buildMode == true", new Vector2(100, 340), Color.Red);
                    else
                        spriteBatch.DrawString(font, "buildMode == false", new Vector2(100, 340), Color.Yellow);

                    if (player1.mainBuildMode == true)
                        spriteBatch.DrawString(font, "mainBuildMode == true", new Vector2(100, 360), Color.Red);
                    else
                        spriteBatch.DrawString(font, "mainBuildMode == false", new Vector2(100, 360), Color.Yellow);

                    /// checking screen boolean
                    if (showEncyclopediaScreen == true)
                        spriteBatch.DrawString(font, "showEncyclopediaScreen == true", new Vector2(100, 400), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showEncyclopediaScreen == false", new Vector2(100, 400), Color.Yellow);

                    if (showEnemy1EncyclopediaScreen == true)
                        spriteBatch.DrawString(font, "showEnemy1EncyclopediaScreen == true", new Vector2(100, 420), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showEnemy1EncyclopediaScreen == false", new Vector2(100, 420), Color.Yellow);

                    if (showEnemy2EncyclopediaScreen == true)
                        spriteBatch.DrawString(font, "showEnemy2EncyclopediaScreen == true", new Vector2(100, 440), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showEnemy2EncyclopediaScreen == false", new Vector2(100, 440), Color.Yellow);

                    if (showEnemy3EncyclopediaScreen == true)
                        spriteBatch.DrawString(font, "showEnemy3EncyclopediaScreen == true", new Vector2(100, 460), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showEnemy3EncyclopediaScreen == false", new Vector2(100, 460), Color.Yellow);

                    if (showGameOverScreen == true)
                        spriteBatch.DrawString(font, "showGameOverScreen == true", new Vector2(100, 480), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showGameOverScreen == false", new Vector2(100, 480), Color.Yellow);

                    if (showGameScreen == true)
                        spriteBatch.DrawString(font, "showGameScreen == true", new Vector2(100, 500), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showGameScreen == false", new Vector2(100, 500), Color.Yellow);

                    if (showLevel1Screen == true)
                        spriteBatch.DrawString(font, "showLevel1Screen == true", new Vector2(100, 520), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showLevel1Screen == false", new Vector2(100, 520), Color.Yellow);

                    if (showLevel2Screen == true)
                        spriteBatch.DrawString(font, "showLevel2Screen == true", new Vector2(100, 540), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showLevel2Screen == false", new Vector2(100, 540), Color.Yellow);

                    if (showLevel3Screen == true)
                        spriteBatch.DrawString(font, "showLevel3Screen == true", new Vector2(100, 560), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showLevel3Screen == false", new Vector2(100, 560), Color.Yellow);

                    if (showPauseScreen == true)
                        spriteBatch.DrawString(font, "showPauseScreen == true", new Vector2(100, 580), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showPauseScreen == false", new Vector2(100, 580), Color.Yellow);

                    if (showTitleScreen == true)
                        spriteBatch.DrawString(font, "showTitleScreen == true", new Vector2(100, 600), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showTitleScreen == false", new Vector2(100, 600), Color.Yellow);

                    if (showTower1EncyclopediaScreen == true)
                        spriteBatch.DrawString(font, "showTower1EncyclopediaScreen == true", new Vector2(100, 620), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showTower1EncyclopediaScreen == false", new Vector2(100, 620), Color.Yellow);

                    if (showTower2EncyclopediaScreen == true)
                        spriteBatch.DrawString(font, "showTower2EncyclopediaScreen == true", new Vector2(100, 640), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showTower2EncyclopediaScreen == false", new Vector2(100, 640), Color.Yellow);

                    if (showTower3EncyclopediaScreen == true)
                        spriteBatch.DrawString(font, "showTower3EncyclopediaScreen == true", new Vector2(100, 660), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showTower3EncyclopediaScreen == false", new Vector2(100, 660), Color.Yellow);

                    if (showWinScreen == true)
                        spriteBatch.DrawString(font, "showWinScreen == true", new Vector2(100, 680), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showWinScreen == false", new Vector2(100, 680), Color.Yellow);

                    spriteBatch.DrawString(font, "currentLevel == " + currentLevel, new Vector2(100, 700), Color.Yellow);
                }
                #endregion

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
                    spriteBatch.DrawString(font, Player.getFireStoneInInventory() + " Fire Stone", new Vector2(uiPosition2.X, uiPosition2.Y + 40), Color.Black);
                    spriteBatch.DrawString(font, Player.getWaterStoneInInventory() + " Thunder Stone", new Vector2(uiPosition2.X, uiPosition2.Y + 20), Color.Black);
                    spriteBatch.DrawString(font, Player.getHealStoneInInventory() + " Ice Stone", uiPosition2, Color.Black);
                    //spriteBatch.DrawString(font, "Resources: " + player1.getMoney(), uiPosition1, Color.Black);
                    spriteBatch.DrawString(font, "Lives: " + game.getLive(), uiPosition3, Color.Black);

                    int tmp2 = (int)((game.Wave.WaveTimer / game.Wave.IntervalWave) * 140);

                    if (game.Wave.WaveTimer > 0)
                    {
                        spriteBatch.Draw(timerBar, new Vector2((width / 2) - timerBar.Width / 2, 10), Color.White);
                        spriteBatch.Draw(timerSprite, new Vector2(((width / 2) + 35 - tmp2), 3), new Rectangle(0, 0, timerSprite.Width, timerSprite.Height), Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 1.0f);
                    }
                    spriteBatch.Draw(statusBar, new Vector2(10, -10), Color.White);
                    spriteBatch.DrawString(font, "" + Player.getMoney(), uiMoneyPosition, Color.White);
                    spriteBatch.DrawString(font, "" + game.getLive(), uiLifePosition, Color.White);
                    spriteBatch.DrawString(font, "" + game.Wave.CurrentWave + " / " + game.Wave.totalWave(), uiWavePosition, Color.White);
                    spriteBatch.DrawString(font, "" + Player.getFireStoneInInventory(), uiFirePosition, Color.White);
                    spriteBatch.DrawString(font, "" + Player.getHealStoneInInventory(), uiIcePosition, Color.White);
                    spriteBatch.DrawString(font, "" + Player.getWaterStoneInInventory(), uiLightningPosition, Color.White);

                    //Rectangle newRect = new Rectangle((int)pos.X, (int)pos.Y, (int)((((Enemy)this).HP / ((Enemy)this).MaxHP) * hpBarInside.Width), (int)hpBarInside.Height);

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
                    game.menuSoundInstance.Stop();
                    game.level2SoundInstance.Stop();
                    game.level3SoundInstance.Stop();
                    game.level1SoundInstance.Play();
     
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
                    game.menuSoundInstance.Stop();
                    game.level2SoundInstance.Play();
                    game.level3SoundInstance.Stop();
                    game.level1SoundInstance.Stop();

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
                    game.menuSoundInstance.Stop();
                    game.level2SoundInstance.Stop();
                    game.level3SoundInstance.Play();
                    game.level1SoundInstance.Stop();
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
                    if (currentLevel == 1 || currentLevel == 2 || currentLevel == 3)
                    {
                        uiEncyclopediaPosition1 = new Vector2(900, 240);
                        uiEncyclopediaPosition2 = new Vector2(900, 350);
                        uiEncyclopediaPosition3 = new Vector2(900, 460);
                        uiEncyclopediaPosition4 = new Vector2(900, 260);

                        if (xPos2 == 0 && yPos2 == 0)
                        {
                            spriteBatch.Draw(arrowTowerEncyclopediaTexture, new Vector2(350, 320), Color.White);
                            spriteBatch.Draw(arrowTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.LightBlue);
                            spriteBatch.Draw(arrowTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.LightBlue);
                            spriteBatch.Draw(arrowTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(arrowTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(arrowTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(arrowTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(arrowTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(arrowTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(arrowTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.White);
                            }
                        }
                        if (xPos2 == 1 && yPos2 == 0)
                        {
                            spriteBatch.Draw(canonTowerEncyclopediaTexture, new Vector2(450, 320), Color.White);
                            spriteBatch.Draw(canonTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.LightBlue);
                            spriteBatch.Draw(canonTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.LightBlue);
                            spriteBatch.Draw(canonTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(canonTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(canonTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(canonTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(canonTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(canonTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(canonTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.White);
                            }
                        }
                        if (xPos2 == 0 && yPos2 == 1)
                        {
                            spriteBatch.Draw(magicTowerEncyclopediaTexture, new Vector2(350, 435), Color.White);
                            spriteBatch.Draw(magicTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.LightBlue);
                            spriteBatch.Draw(magicTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.LightBlue);
                            spriteBatch.Draw(magicTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(magicTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(magicTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(magicTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(magicTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(magicTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(magicTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.White);
                            }
                        }
                        if (xPos2 == 1 && yPos2 == 1)
                        {
                            spriteBatch.Draw(missileTowerEncyclopediaTexture, new Vector2(450, 435), Color.White);
                            spriteBatch.Draw(missileTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.LightBlue);
                            spriteBatch.Draw(missileTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.LightBlue);
                            spriteBatch.Draw(missileTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(missileTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(missileTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(missileTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(missileTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(missileTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(missileTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.White);
                            }
                        }
                        if (xPos2 == 0 && yPos2 == 2)
                        {
                            spriteBatch.Draw(flameTowerEncyclopediaTexture, new Vector2(350, 600), Color.White);
                            spriteBatch.Draw(flameTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.LightBlue);
                            spriteBatch.Draw(flameTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.LightBlue);
                            spriteBatch.Draw(flameTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(flameTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(flameTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(flameTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(flameTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(flameTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(flameTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.White);
                            }
                        }
                        if (xPos2 == 1 && yPos2 == 2)
                        {
                            spriteBatch.Draw(iceTowerEncyclopediaTexture, new Vector2(450, 600), Color.White);
                            spriteBatch.Draw(iceTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.LightBlue);
                            spriteBatch.Draw(iceTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.LightBlue);
                            spriteBatch.Draw(iceTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(iceTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(iceTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(iceTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(iceTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(iceTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(iceTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.White);
                            }
                        }
                        if (xPos2 == 0 && yPos2 == 3)
                        {
                            spriteBatch.Draw(shockTowerEncyclopediaTexture, new Vector2(350, 715), Color.White);
                            spriteBatch.Draw(shockTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.LightBlue);
                            spriteBatch.Draw(shockTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.LightBlue);
                            spriteBatch.Draw(shockTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.LightBlue);
                            if (towerLevelEncyclopedia == 1)
                            {
                                spriteBatch.Draw(shockTowerEncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(shockTowerEncyclopediaTexture, uiEncyclopediaPosition1, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 2)
                            {
                                spriteBatch.Draw(shockTower2EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(shockTower2EncyclopediaTexture, uiEncyclopediaPosition2, Color.White);
                            }
                            else if (towerLevelEncyclopedia == 3)
                            {
                                spriteBatch.Draw(shockTower3EncyclopediaLargeTexture, new Vector2(620, 270), Color.White);
                                spriteBatch.Draw(shockTower3EncyclopediaTexture, uiEncyclopediaPosition3, Color.White);
                            }
                        }
                    }
                    #endregion


                    #region level2 tower encyclopedia
                    /*
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
                     * */
                    #endregion


                    #region level3 tower encyclopedia
                    /*
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
                     * */
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


                #region testing boolean
                // testing purpose only, set runTestChecking = true to enable this
                if (runTestChecking)
                {
                    spriteBatch.Draw(mainEncyclopediaTexture, new Vector2(50, 140), Color.White);
                    /// checking the buildMenu boolean, need to change all of these from private to public at player.cs class
                    

                    if (player1.upgradeBuildMode == true)
                        spriteBatch.DrawString(font, "upgradeBuildMode == true", new Vector2(100, 320), Color.Red);
                    else
                        spriteBatch.DrawString(font, "upgradeBuildMode == false", new Vector2(100, 320), Color.Yellow);

                    if (player1.upgradeBuildMagicMode == true)
                        spriteBatch.DrawString(font, "upgradeBuildMagicMode == true", new Vector2(100, 300), Color.Red);
                    else
                        spriteBatch.DrawString(font, "upgradeBuildMagicMode == false", new Vector2(100, 300), Color.Yellow);

                    if (player1.buildMode == true)
                        spriteBatch.DrawString(font, "buildMode == true", new Vector2(100, 340), Color.Red);
                    else
                        spriteBatch.DrawString(font, "buildMode == false", new Vector2(100, 340), Color.Yellow);

                    if (player1.mainBuildMode == true)
                        spriteBatch.DrawString(font, "mainBuildMode == true", new Vector2(100, 360), Color.Red);
                    else
                        spriteBatch.DrawString(font, "mainBuildMode == false", new Vector2(100, 360), Color.Yellow);

                    /// checking screen boolean
                    if (showEncyclopediaScreen == true)
                        spriteBatch.DrawString(font, "showEncyclopediaScreen == true", new Vector2(100, 400), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showEncyclopediaScreen == false", new Vector2(100, 400), Color.Yellow);

                    if (showEnemy1EncyclopediaScreen == true)
                        spriteBatch.DrawString(font, "showEnemy1EncyclopediaScreen == true", new Vector2(100, 420), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showEnemy1EncyclopediaScreen == false", new Vector2(100, 420), Color.Yellow);

                    if (showEnemy2EncyclopediaScreen == true)
                        spriteBatch.DrawString(font, "showEnemy2EncyclopediaScreen == true", new Vector2(100, 440), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showEnemy2EncyclopediaScreen == false", new Vector2(100, 440), Color.Yellow);

                    if (showEnemy3EncyclopediaScreen == true)
                        spriteBatch.DrawString(font, "showEnemy3EncyclopediaScreen == true", new Vector2(100, 460), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showEnemy3EncyclopediaScreen == false", new Vector2(100, 460), Color.Yellow);

                    if (showGameOverScreen == true)
                        spriteBatch.DrawString(font, "showGameOverScreen == true", new Vector2(100, 480), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showGameOverScreen == false", new Vector2(100, 480), Color.Yellow);

                    if (showGameScreen == true)
                        spriteBatch.DrawString(font, "showGameScreen == true", new Vector2(100, 500), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showGameScreen == false", new Vector2(100, 500), Color.Yellow);

                    if (showLevel1Screen == true)
                        spriteBatch.DrawString(font, "showLevel1Screen == true", new Vector2(100, 520), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showLevel1Screen == false", new Vector2(100, 520), Color.Yellow);

                    if (showLevel2Screen == true)
                        spriteBatch.DrawString(font, "showLevel2Screen == true", new Vector2(100, 540), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showLevel2Screen == false", new Vector2(100, 540), Color.Yellow);

                    if (showLevel3Screen == true)
                        spriteBatch.DrawString(font, "showLevel3Screen == true", new Vector2(100, 560), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showLevel3Screen == false", new Vector2(100, 560), Color.Yellow);

                    if (showPauseScreen == true)
                        spriteBatch.DrawString(font, "showPauseScreen == true", new Vector2(100, 580), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showPauseScreen == false", new Vector2(100, 580), Color.Yellow);

                    if (showTitleScreen == true)
                        spriteBatch.DrawString(font, "showTitleScreen == true", new Vector2(100, 600), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showTitleScreen == false", new Vector2(100, 600), Color.Yellow);

                    if (showTower1EncyclopediaScreen == true)
                        spriteBatch.DrawString(font, "showTower1EncyclopediaScreen == true", new Vector2(100, 620), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showTower1EncyclopediaScreen == false", new Vector2(100, 620), Color.Yellow);

                    if (showTower2EncyclopediaScreen == true)
                        spriteBatch.DrawString(font, "showTower2EncyclopediaScreen == true", new Vector2(100, 640), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showTower2EncyclopediaScreen == false", new Vector2(100, 640), Color.Yellow);

                    if (showTower3EncyclopediaScreen == true)
                        spriteBatch.DrawString(font, "showTower3EncyclopediaScreen == true", new Vector2(100, 660), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showTower3EncyclopediaScreen == false", new Vector2(100, 660), Color.Yellow);

                    if (showWinScreen == true)
                        spriteBatch.DrawString(font, "showWinScreen == true", new Vector2(100, 680), Color.Red);
                    else
                        spriteBatch.DrawString(font, "showWinScreen == false", new Vector2(100, 680), Color.Yellow);

                    spriteBatch.DrawString(font, "currentLevel == " + currentLevel, new Vector2(100, 700), Color.Yellow);
                }
                #endregion


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

            else if (showEnemy1EncyclopediaScreen == true)
                updateGamePadForEnemy1EncyclopediaScreen();

            else if (showWinScreen == true)
                updateGamePadForWinScreen();



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

            if ((currentState.IsButtonDown(Buttons.DPadRight) && oldState.IsButtonUp(Buttons.DPadRight) ||
                currentState.ThumbSticks.Left.X >= 0.5 && oldState.ThumbSticks.Left.X < 0.5) && xPos1 < xPosMax)
            {
                xPos1++;
            }
            if ((currentState.IsButtonDown(Buttons.DPadLeft) && oldState.IsButtonUp(Buttons.DPadLeft) ||
                currentState.ThumbSticks.Left.X <= -0.5 && oldState.ThumbSticks.Left.X > -0.5) && xPos1 > 0)
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
            currentLevel = 2;
            xPosMax = 1;
            yPosMax = 0;
            if ((currentState.IsButtonDown(Buttons.DPadRight) && oldState.IsButtonUp(Buttons.DPadRight) ||
                currentState.ThumbSticks.Left.X >= 0.5 && oldState.ThumbSticks.Left.X < 0.5) && xPos1 < xPosMax)
            {
                xPos1++;
            }
            if ((currentState.IsButtonDown(Buttons.DPadLeft) && oldState.IsButtonUp(Buttons.DPadLeft) ||
                currentState.ThumbSticks.Left.X <= -0.5 && oldState.ThumbSticks.Left.X > -0.5) && xPos1 > 0)
            {
                xPos1--;
            }
            if (currentState.IsButtonDown(Buttons.A) && oldState.IsButtonUp(Buttons.A) && xPos1 == 0 && yPos1 == 0)
            {
                showGameScreen = true;
                loadingGameScreen2 = true;
                showTitleScreen = false;
                showPauseScreen = false;
                showLevel2Screen = false;
                //restartGame = true;
                game.goNextLevel();
            }

            if (currentState.IsButtonDown(Buttons.A) && oldState.IsButtonUp(Buttons.A) && xPos1 == 1 && yPos1 == 0)
            {
                showLevel2Screen = false;
                showEncyclopediaScreen = true;
                xPos1 = 0;
                yPos1 = 0;
            }
        }

        private void updateGamePadForLevel3Screen()
        {
            currentLevel = 3;
            xPosMax = 1;
            yPosMax = 0;
            if ((currentState.IsButtonDown(Buttons.DPadRight) && oldState.IsButtonUp(Buttons.DPadRight) ||
                currentState.ThumbSticks.Left.X >= 0.5 && oldState.ThumbSticks.Left.X < 0.5) && xPos1 < xPosMax)
            {
                xPos1++;
            }
            if ((currentState.IsButtonDown(Buttons.DPadLeft) && oldState.IsButtonUp(Buttons.DPadLeft) ||
                currentState.ThumbSticks.Left.X <= -0.5 && oldState.ThumbSticks.Left.X > -0.5) && xPos1 > 0)
            {
                xPos1--;
            }
            if (currentState.IsButtonDown(Buttons.A) && oldState.IsButtonUp(Buttons.A) && xPos1 == 0 && yPos1 == 0)
            {
                showGameScreen = true;
                loadingGameScreen3 = true;
                showTitleScreen = false;
                showPauseScreen = false;
                showLevel3Screen = false;
                //restartGame = true;
                game.goNextLevel();
            }

            if (currentState.IsButtonDown(Buttons.A) && oldState.IsButtonUp(Buttons.A) && xPos1 == 1 && yPos1 == 0)
            {
                showLevel3Screen = false;
                showEncyclopediaScreen = true;
                xPos1 = 0;
                yPos1 = 0;
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

            if (currentState.IsButtonDown(Buttons.B) && oldState.IsButtonUp(Buttons.B))
            {
                showEncyclopediaScreen = false;
                if (currentLevel == 1)
                    showLevel1Screen = true;
                if (currentLevel == 2)
                    showLevel2Screen = true;
                if (currentLevel == 3)
                    showLevel3Screen = true;
            }

            if ((currentState.IsButtonDown(Buttons.DPadRight) && oldState.IsButtonUp(Buttons.DPadRight) ||
                currentState.ThumbSticks.Left.X >= 0.5 && oldState.ThumbSticks.Left.X < 0.5) && xPos1 < xPosMax)
            {
                xPos1++;
            }
            if ((currentState.IsButtonDown(Buttons.DPadLeft) && oldState.IsButtonUp(Buttons.DPadLeft) ||
                currentState.ThumbSticks.Left.X <= -0.5 && oldState.ThumbSticks.Left.X > -0.5) && xPos1 > 0)
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
            xPosMax = 1;
            yPosMax = 3;

            if (currentState.IsButtonDown(Buttons.B) && oldState.IsButtonUp(Buttons.B))
            {
                showEncyclopediaScreen = true;
                showTower1EncyclopediaScreen = false;
            }

            if ((currentState.IsButtonDown(Buttons.DPadRight) && oldState.IsButtonUp(Buttons.DPadRight) || 
                currentState.ThumbSticks.Left.X >= 0.5 && oldState.ThumbSticks.Left.X < 0.5) && xPos2 < xPosMax)
            {
                xPos2++;
                towerLevelEncyclopedia = 1;
            }
            if ((currentState.IsButtonDown(Buttons.DPadLeft) && oldState.IsButtonUp(Buttons.DPadLeft) ||
                currentState.ThumbSticks.Left.X <= -0.5 && oldState.ThumbSticks.Left.X > -0.5) && xPos2 > 0)
            {
                xPos2--;
                towerLevelEncyclopedia = 1;
            }
            if ((currentState.IsButtonDown(Buttons.DPadUp) && oldState.IsButtonUp(Buttons.DPadUp) ||
                currentState.ThumbSticks.Left.Y >= 0.5 && oldState.ThumbSticks.Left.Y < 0.5) && yPos2 > 0)
            {
                yPos2--;
                towerLevelEncyclopedia = 1;
            }
            if ((currentState.IsButtonDown(Buttons.DPadDown) && oldState.IsButtonUp(Buttons.DPadDown) ||
                currentState.ThumbSticks.Left.Y <= -0.5 && oldState.ThumbSticks.Left.Y > -0.5) && yPos2 < yPosMax)
            {
                yPos2++;
                towerLevelEncyclopedia = 1;
            }

            if (yPos2 == 3)
            {
                xPos2 = 0;
            }

            if (currentState.ThumbSticks.Right.Y >= 0.5 && oldState.ThumbSticks.Right.Y < 0.5 && towerLevelEncyclopedia > 1)
            {
                towerLevelEncyclopedia--;
            }

            if (currentState.ThumbSticks.Right.Y <= -0.5 && oldState.ThumbSticks.Right.Y > -0.5 && towerLevelEncyclopedia < 3)
            {
                towerLevelEncyclopedia++;
            }

        }

        private void updateGamePadForEnemy1EncyclopediaScreen()
        {
            if (showEnemy1EncyclopediaScreen == true)
            {
                xPosMax = 1;
                yPosMax = 1;
            }
            else if (showEnemy2EncyclopediaScreen == true)
            {
                xPosMax = 1;
                yPosMax = 2;
            }
            else if (showEnemy3EncyclopediaScreen == true)
            {
                xPosMax = 1;
                yPosMax = 3;
            }
            if (currentState.IsButtonDown(Buttons.B) && oldState.IsButtonUp(Buttons.B))
            {
                showEncyclopediaScreen = true;
                showEnemy1EncyclopediaScreen = false;
            }

            if ((currentState.IsButtonDown(Buttons.DPadRight) && oldState.IsButtonUp(Buttons.DPadRight) ||
                currentState.ThumbSticks.Left.X >= 0.5 && oldState.ThumbSticks.Left.X < 0.5) && xPos2 < xPosMax)
            {
                xPos2++;
            }
            if ((currentState.IsButtonDown(Buttons.DPadLeft) && oldState.IsButtonUp(Buttons.DPadLeft) ||
                currentState.ThumbSticks.Left.X <= -0.5 && oldState.ThumbSticks.Left.X > -0.5) && xPos2 > 0)
            {
                xPos2--;
            }
            if ((currentState.IsButtonDown(Buttons.DPadUp) && oldState.IsButtonUp(Buttons.DPadUp) ||
                currentState.ThumbSticks.Left.Y >= 0.5 && oldState.ThumbSticks.Left.Y < 0.5) && yPos2 > 0)
            {
                yPos2--;
            }
            if ((currentState.IsButtonDown(Buttons.DPadDown) && oldState.IsButtonUp(Buttons.DPadDown) ||
                currentState.ThumbSticks.Left.Y <= -0.5 && oldState.ThumbSticks.Left.Y > -0.5) && yPos2 < yPosMax)
            {
                yPos2++;
            }

            if (currentLevel == 1)
            {
                if (yPos2 == 1)
                {
                    xPos2 = 0;
                }
            }

            else if (currentLevel == 2)
            {
                if (yPos2 == 2)
                {
                    xPos2 = 0;
                }
            }

            else if (currentLevel == 3)
            {
                if (yPos2 == 3)
                {
                    xPos2 = 0;
                }
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

            else if (showWinScreen == true)
                updateKeyboardForWinScreen();

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
            if (keystate.IsKeyDown(Keys.Enter) && oldKeyState.IsKeyUp(Keys.Enter))
            {
                showGameScreen = false;
                showGameOverScreen = false;
                showPauseScreen = false;
                showWinScreen = false;
                //restartGame = true;
                showTitleScreen = true;
            }
        }

        private void updateKeyboardForEncyclopediaScreen()
        {
            xPosMax = 1;
            yPosMax = 0;

            if (keystate.IsKeyUp(Keys.Escape) && oldKeyState.IsKeyDown(Keys.Escape))
            {
                showEncyclopediaScreen = false;
                if (currentLevel == 1)
                    showLevel1Screen = true;
                if (currentLevel == 2)
                    showLevel2Screen = true;
                if (currentLevel == 3)
                    showLevel3Screen = true;
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
            xPosMax = 1;
            yPosMax = 3;
            
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
