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



        bool skipToGame = true;               // [][][] set this to true to disable all non game screen that I add [][][]
        bool showTitleScreen = false;
        bool showGameScreen = false;
        bool showPauseScreen = false;
        bool showGameOverScreen = false;
        bool restartGame = false;
        

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
            startScreenBackground = contentManager.Load<Texture2D>("ssPlaceHolder");
            font = contentManager.Load<SpriteFont>("font");
        }

        public void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            if (showTitleScreen == true)
            {
                spriteBatch.Draw(startScreenBackground, new Vector2(0, 0), Color.White);
                spriteBatch.DrawString(font, "Press F4 to start the game", new Vector2(500, 250), Color.Tomato);
            }

            if (showGameScreen == false && showPauseScreen == true)
            {
                spriteBatch.DrawString(font, "Game Paused", new Vector2(500, 250), Color.Tomato);
                spriteBatch.DrawString(font, "Press delete to quit", new Vector2(500, 270), Color.Tomato);

            }

            if (showGameOverScreen == true)
            {
                spriteBatch.DrawString(font, "Game Over", new Vector2(500, 250), Color.Tomato);
                spriteBatch.DrawString(font, "Press Enter to retry", new Vector2(500, 270), Color.Tomato);
                spriteBatch.DrawString(font, "Press delete to quit", new Vector2(500, 390), Color.Tomato);
            
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
            }

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
            if (keystate.IsKeyDown(Keys.F4))
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

        public void setRestartGameStatus(bool _status)
        {
            restartGame = _status;
        }

        public bool getRestartGameStatus()
        {
            return restartGame;
        }

        

        








    }
}
