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
    public class Camera
    {
        Game1 game;
        ContentManager contentManager;
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;
        private PlayerIndex playerIndex;
        List<Player> players;
        Player player;

        KeyboardState keystate;
        KeyboardState oldKeyState;
        MouseState mousestate;
        MouseState oldMousestate;
        GamePadState currentState;
        GamePadState oldState;




        public Camera(Viewport viewport)
        {
            _viewport = viewport;
            _origin = new Vector2(_viewport.Width / 2.0f, _viewport.Height / 2.0f);
        }


        public void Initialize(Game1 _game, PlayerIndex _playerIndex, Vector2 _startPosition, List<Player> _players)
        {
            this.game = _game;
            contentManager = _game.Content;
            graphicsDevice = _game.GraphicsDevice;
            currentState = GamePad.GetState(_playerIndex);
            Limits = null;
            Limits = new Rectangle(0, 0, 1984, 1536);
            players = _players;
        }

        public void LoadContent()
        {
            
        }


        public void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            keystate = Keyboard.GetState();

            

            if (keystate.IsKeyDown(Keys.Right))
                Position += new Vector2(400.0f * elapsedTime, 0.0f);

            if (keystate.IsKeyDown(Keys.Left))
                Position += new Vector2(-400.0f * elapsedTime, 0.0f);

            if (keystate.IsKeyDown(Keys.Down))
                Position += new Vector2(0.0f, 400.0f * elapsedTime);

            if (keystate.IsKeyDown(Keys.Up))
                Position += new Vector2(0.0f, -400.0f * elapsedTime);

            // override previous Position function
            if (keystate.IsKeyDown(Keys.L))
            {
                Position = new Vector2(players[0].Position.X - (1280/2),players[0].Position.Y - (1024/2));
            }

            // override previous Position function
            Position = new Vector2(players[0].Position.X - (1280 / 2), players[0].Position.Y - (1024 / 2));

            if (keystate.IsKeyDown(Keys.PageUp))
                Zoom += 2.5f * elapsedTime * Zoom;

            if (keystate.IsKeyDown(Keys.PageDown))
            {
                Zoom -= 2.5f * elapsedTime * Zoom;
            }

            oldKeyState = keystate;
        }

        /// <summary>
        /// Gets or sets the position of the camera.
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                ValidatePosition();
            }
        }

        /// <summary>
        /// Gets or sets the zoom of the camera.
        /// </summary>
        public float Zoom
        {
            get
            {
                return _zoom;
            }
            set
            {
                _zoom = MathHelper.Max(value, MinZoom);
                ValidateZoom();
                ValidatePosition();
            }
        }

        /// <summary>
        /// Sets a rectangle that describes which region of the world the camera should
        /// be able to see. Setting it to null removes the limit.
        /// </summary>
        public Rectangle? Limits
        {
            set
            {
                _limits = value;
                ValidateZoom();
                ValidatePosition();
            }
        }

        /// <summary>
        /// Calculates a view matrix for this camera.
        /// </summary>
        public Matrix ViewMatrix
        {
            get
            {
                return Matrix.CreateTranslation(new Vector3(-_position, 0f)) *
                       Matrix.CreateTranslation(new Vector3(-_origin, 0f)) *
                       Matrix.CreateScale(_zoom, _zoom, 1f) *
                       Matrix.CreateTranslation(new Vector3(_origin, 0f));
            }
        }

        /// <summary>
        /// When using limiting, makes sure the camera position is valid.
        /// </summary>
        private void ValidatePosition()
        {
            if (_limits.HasValue)
            {
                Vector2 cameraWorldMin = Vector2.Transform(Vector2.Zero, Matrix.Invert(ViewMatrix));
                Vector2 cameraSize = new Vector2(_viewport.Width, _viewport.Height) / _zoom;
                Vector2 limitWorldMin = new Vector2(_limits.Value.Left, _limits.Value.Top);
                Vector2 limitWorldMax = new Vector2(_limits.Value.Right, _limits.Value.Bottom);
                Vector2 positionOffset = _position - cameraWorldMin;
                _position = Vector2.Clamp(cameraWorldMin, limitWorldMin, limitWorldMax - cameraSize) + positionOffset;
            }
        }

        /// <summary>
        /// When using limiting, makes sure the camera zoom is valid.
        /// </summary>
        private void ValidateZoom()
        {
            if (_limits.HasValue)
            {
                float minZoomX = (float)_viewport.Width / _limits.Value.Width;
                float minZoomY = (float)_viewport.Height / _limits.Value.Height;
                _zoom = MathHelper.Max(_zoom, MathHelper.Max(minZoomX, minZoomY));
            }
        }

        private const float MinZoom = 0.01f;

        private readonly Viewport _viewport;
        private readonly Vector2 _origin;

        private Vector2 _position;
        private float _zoom = 1f;
        private Rectangle? _limits;

        public void ResetCamera()
        {
            Zoom = 1f;
            Position = Vector2.Zero;
        }

        private static Rectangle CalculateFrameRectangle(int width, int height, int columns, int rows, int frame)
        {
            int tileWidth = width / columns;
            int tileHeight = height / rows;
            int x = frame % columns;
            int y = frame / columns;
            return new Rectangle(x * tileWidth, y * tileHeight, tileWidth, tileHeight);
        }
    }
}
