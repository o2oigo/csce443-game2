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
    class UserInterface
    {

        Game1 game;

        ContentManager contentManager;
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        private PlayerIndex playerIndex;

        public bool booltest = false;
        SpriteFont font;



        private bool skipToGame = false;               // [][][] set this to true to disable all non game screen that I add [][][]
        private bool showTitleScreen = false;
        private bool showGameScreen = false;
        private bool showPauseScreen = false;
        private bool showGameOverScreen = false;
        private bool showWinScreen = false;
        private bool restartGame = false;

        private int wave = 0;
        private int nextWave = 1;


        private bool finishMenu1 = false;
        private bool finishMenu2 = false;

        private Vector2 uiPosition1;
        private Vector2 uiPosition2;
        private Vector2 uiPosition3;
        private Vector2 uiPosition4;
        

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
        private Texture2D startMenuTexture;
        private Texture2D gameoverScreenBackground;

        public void Initialize(Game1 game, PlayerIndex index, Vector2 startPosition)
        {
            this.game = game;
            contentManager = game.Content;
            graphicsDevice = game.GraphicsDevice;
            currentState = GamePad.GetState(playerIndex);
            showTitleScreen = true;
            showGameScreen = false;
            showPauseScreen = false;
            showGameOverScreen = false;
            restartGame = false;          
        }
       

        public UserInterface()
        {
        }

        public void LoadContent()
        {
            startScreenBackground = contentManager.Load<Texture2D>("startMenuBackground");
            gameoverScreenBackground = contentManager.Load<Texture2D>("gameoverNew");
            startMenuTexture = contentManager.Load<Texture2D>("start");
            font = contentManager.Load<SpriteFont>("font");
        }

        public void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            int width = game.GraphicsDevice.Viewport.Width;
            int height = game.GraphicsDevice.Viewport.Height;
            uiPosition1 = new Vector2(width - 600, height - 50);
            if (currentState.IsConnected)//Game Pad
            {
                if (showTitleScreen == true)
                {
                    spriteBatch.Draw(startScreenBackground, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(startMenuTexture, new Vector2(400, 650), Color.White);
                    spriteBatch.DrawString(font, "Ver. Beta 1.0.0", new Vector2(width - 200, height - 50), Color.Black);
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
                    spriteBatch.DrawString(font, "Ver. Beta 1.0.0", new Vector2(width - 200, height - 50), Color.Tomato);
                }

                if (showWinScreen == true)
                {
                    spriteBatch.Draw(startScreenBackground, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(font, "You Win", new Vector2(500, 580), Color.Black);
                    spriteBatch.DrawString(font, "Thank you for playing", new Vector2(500, 600), Color.Black);
                    spriteBatch.DrawString(font, "Press A to re-play the game", new Vector2(500, 700), Color.Black);
                    spriteBatch.DrawString(font, "Press Start to go back to title menu", new Vector2(500, 740), Color.Black);
                    spriteBatch.DrawString(font, "Ver. Beta 1.0.0", new Vector2(width - 200, height - 50), Color.Tomato);
                }

                if (showGameScreen == true)
                {
                    if (nextWave == 1)
                    {
                        spriteBatch.DrawString(font, "Next Wave: Normal", uiPosition1, Color.Black);
                    }
                    else if (nextWave == 2)
                    {
                        spriteBatch.DrawString(font, "Next Wave: Normal" , uiPosition1, Color.Black);
                    }
                    else if (nextWave == 3)
                    {
                        spriteBatch.DrawString(font, "Next Wave: High HP" , uiPosition1, Color.Black);
                    }
                    else if (nextWave == 4)
                    {
                        spriteBatch.DrawString(font, "Next Wave: High HP" , uiPosition1, Color.Black);
                    }
                    else if (nextWave == 5)
                    {
                        spriteBatch.DrawString(font, "Next Wave: Fast" , uiPosition1, Color.Black);
                    }
                    else if (nextWave == 6)
                    {
                        spriteBatch.DrawString(font, "Next Wave: ???" , uiPosition1, Color.Black);
                    }
                }
            }


            // screen for keyboard user
            else
            {
                if (showTitleScreen == true)
                {
                    spriteBatch.Draw(startScreenBackground, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(startMenuTexture, new Vector2(400, 650), Color.White);
                    spriteBatch.DrawString(font, "Ver. Beta 1.0.0", new Vector2(width - 200, height - 50), Color.Tomato);
                }

                if (showGameScreen == false && showPauseScreen == true)
                {
                    spriteBatch.DrawString(font, "Game Paused", new Vector2(500, 250), Color.Black);
                    spriteBatch.DrawString(font, "Press delete to quit", new Vector2(500, 270), Color.Black);

                }

                if (showGameOverScreen == true)
                {
                    spriteBatch.Draw(startScreenBackground, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(font, "Game Over", new Vector2(500, 600), Color.Black);
                    spriteBatch.DrawString(font, "Press Enter to retry", new Vector2(500, 700), Color.Black);
                    spriteBatch.DrawString(font, "Press delete to quit", new Vector2(500, 740), Color.Black);
                    spriteBatch.DrawString(font, "Ver. Beta 1.0.0", new Vector2(width - 200, height - 50), Color.Tomato);
                }

                if (showWinScreen == true)
                {
                    spriteBatch.Draw(startScreenBackground, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(font, "Thank you for playing", new Vector2(500, 600), Color.Black);
                    spriteBatch.DrawString(font, "Press Enter to restart", new Vector2(500, 700), Color.Black);
                    spriteBatch.DrawString(font, "Press delete to quit", new Vector2(500, 740), Color.Black);
                    spriteBatch.DrawString(font, "Ver. Beta 1.0.0", new Vector2(width - 200, height - 50), Color.Tomato);
                }
                if (showTitleScreen == true)
                {
                    spriteBatch.Draw(startScreenBackground, new Vector2(0, 0), Color.White);
                    spriteBatch.Draw(startMenuTexture, new Vector2(400, 650), Color.White);
                    spriteBatch.DrawString(font, "Ver. Beta 1.0.0", new Vector2(width - 200, height - 50), Color.Tomato);
                }

                if (showGameScreen == false && showPauseScreen == true)
                {
                    spriteBatch.DrawString(font, "Game Paused", new Vector2(500, 250), Color.Black);
                    spriteBatch.DrawString(font, "Press delete to quit", new Vector2(500, 270), Color.Black);

                }

                if (showGameOverScreen == true)
                {
                    spriteBatch.Draw(startScreenBackground, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(font, "Game Over", new Vector2(500, 600), Color.Black);
                    spriteBatch.DrawString(font, "Press Enter to retry", new Vector2(500, 700), Color.Black);
                    spriteBatch.DrawString(font, "Press delete to quit", new Vector2(500, 740), Color.Black);
                    spriteBatch.DrawString(font, "Ver. Beta 1.0.0", new Vector2(width - 200, height - 50), Color.Tomato);
                }

                if (showWinScreen == true)
                {
                    spriteBatch.Draw(startScreenBackground, new Vector2(0, 0), Color.White);
                    spriteBatch.DrawString(font, "You Win", new Vector2(500, 580), Color.Black);
                    spriteBatch.DrawString(font, "Thank you for playing", new Vector2(500, 600), Color.Black);
                    spriteBatch.DrawString(font, "Press Enter to restart", new Vector2(500, 700), Color.Black);
                    spriteBatch.DrawString(font, "Press delete to quit", new Vector2(500, 740), Color.Black);
                    spriteBatch.DrawString(font, "Ver. Beta 1.0.0", new Vector2(width - 200, height - 50), Color.Tomato);
                }

                if (showGameScreen == true)
                {
                    spriteBatch.DrawString(font, "Next Wave ", uiPosition1, Color.White);
                }
            }

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

        public void updateGamePad()
        {

            updateGamePadForAllScreen();

            if (showTitleScreen == true)
                updateGamePadForTitleScreen();
            else if (showGameScreen == true)
            {
                updateGamePadForGameScreen();
            }
            else if (showPauseScreen == true)
                updateGamePadForPauseScreen();
            else if (showGameOverScreen == true)
                updateGamePadForGameOverScreen();
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
                showGameScreen = true;
                showTitleScreen = false;
                showPauseScreen = false;
                restartGame = true;
            }

            if (currentState.IsButtonDown(Buttons.A) && oldState.IsButtonUp(Buttons.A))
            {
                showGameScreen = true;
                showTitleScreen = false;
                showPauseScreen = false;
                restartGame = true;
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


        public void updateKeyboard()
        {
            keystate = Keyboard.GetState();
            mousestate = Mouse.GetState();
            mousePos.X = mousestate.X;
            mousePos.Y = mousestate.Y;

            updateKeyboardForAllScreen();

            if (showTitleScreen == true)
                updateKeyboardForTitleScreen();
            else if (showGameScreen == true)
            {
                updateKeyboardForGameScreen();
            }
            else if (showPauseScreen == true)
                updateKeyboardForPauseScreen();
            else if (showGameOverScreen == true)
                updateKeyboardForGameOverScreen();
            

            

          
        }

        private void updateKeyboardForAllScreen()
        {
            if (keystate.IsKeyUp(Keys.Delete) && oldKeyState.IsKeyDown(Keys.Delete))
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
            if (keystate.IsKeyDown(Keys.Enter))
            {
                showGameScreen = true;
                showTitleScreen = false;
                showPauseScreen = false;
                restartGame = true;
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

        public bool getShowGameScreen()
        {
            return showGameScreen;
        }

        public bool getShowTitleScreen()
        {
            return showTitleScreen;
        }

        public bool getShowPauseScreen()
        {
            return showPauseScreen;
        }

        public void setShowGameScreen(bool _status)
        {
            showGameScreen = _status;
        }

        public void setShowTitleScreen(bool _status)
        {
            showTitleScreen = _status;
        }

        public void setShowGameoverScreen(bool _status)
        {
            showGameOverScreen = _status;
        }

        public void setShowWinScreen(bool _status)
        {
            showWinScreen = _status;
        }

        public void setRestartGameStatus(bool _status)
        {
            restartGame = _status;
        }

        public bool getRestartGameStatus()
        {
            return restartGame;
        }



        

        public void setWavesNumber(int _wave)
        {
            nextWave = _wave + 1;
        }

        






    }
}
