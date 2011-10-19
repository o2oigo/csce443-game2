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
    class PlayerUserInterface
    {

        Game1 game;

        ContentManager contentManager;
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;

        KeyboardState keystate;
        KeyboardState oldKeyState;
        MouseState mousestate;
        MouseState oldMousestate;
        GamePadState currentState;
        GamePadState oldState;

        private float elapsedTime;

        private Vector2 position;
        private Vector2 origin;
        private PlayerIndex playerIndex;
        
        private double speed = 0;
        private int timesHit = 0;
        private float shootTimer = .8f;
        private float towerRange = 0;
        private float shootElapsedTime = 0;

        private int shotsTaken = 0;
        private int shotsToDestroy = 4;
        private bool dead = false;

        private double moveRotationAngle = 0;
        private double shootRotationAngle = 0;    
        private float xComponent = 0;
        private float yComponent = 0;

        private Texture2D texture;
        private Texture2D turretTexture;

        //vury
        private Texture2D menu1Texture;
        private Texture2D menu2Texture;

        private List<Projectile> projectileList = new List<Projectile>(5);
  
        private float circle = MathHelper.Pi * 2;

        public PlayerUserInterface(Game1 game, PlayerIndex playerIndex, Vector2 startPosition)
        {
            this.playerIndex = playerIndex;
            this.Initialize(game, startPosition);
        }
 
        public void Initialize(Game1 game, Vector2 startPosition)
        {
            this.game = game;
            contentManager = game.Content;
            graphicsDevice = game.GraphicsDevice;
            position = startPosition;
            currentState = GamePad.GetState(PlayerIndex.One);
        }

        

        public void LoadContent(String textureName)
        {
            texture = contentManager.Load<Texture2D>(textureName);
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;

             // vury
            menu1Texture = contentManager.Load<Texture2D>("menu1");
            menu2Texture = contentManager.Load<Texture2D>("menu2");
        }

        public void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
           
            //vury 
            //spriteBatch.Draw(menu1Texture, new Vector2(position.X - 25, position.Y - 25), Color.White);
            //spriteBatch.Draw(menu2Texture, new Vector2(position.X + 25, position.Y - 25), null, Color.White, (float)shootRotationAngle, new Vector2(0, turretTexture.Height / 2), 1.0f, SpriteEffects.None, 0f);
 

            //spriteBatch.End();
        }

        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            //Elapsed Time Calculations
            elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            
        }

        public void updateGamePad()
        {
          
        }

        public void updateKeyboard()
        {
          
        }

        

        

        








    }
}
