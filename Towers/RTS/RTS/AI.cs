using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RTS
{
    class AI
    {

        const float atDestinationLimit = 5f;

        private Texture2D AITexture;
        private Vector2 AITextureCenter;
        private Map map;

        #region Properties
        private float scale = 1f;
        public float Scale
        {
            get { return scale; }
            set
            {
                scale = value;
                //waypoints.Scale = value;
            }
        }

        protected Vector2 direction;
        public Vector2 Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        protected bool moving;
        public bool Moving
        {
            get { return moving; }
            set { moving = value; }
        }

        const float moveSpeed = 100f;
        public static float MoveSpeed
        {
            get { return moveSpeed; }
        }

        private Vector2 destination;
        public Vector2 Destination
        {
            get { return destination; }
        }

        private Vector2 location;
        public Vector2 Location
        {
            get { return location; }
        }

        private NodeList waypoints;
        public NodeList Waypoints
        {
            get { return waypoints; }
        }

        public float DistanceToDestination
        {
            get { return Vector2.Distance(location, destination); }
        }

        public bool AtDestination
        {
            get { return DistanceToDestination < atDestinationLimit; }
        }
        #endregion

        #region Initialization

        public AI()
        {
            waypoints = new NodeList();
        }

        public void Initialize(Map mazeMap)
        {
            location = Vector2.Zero;
            destination = location;
            map = mazeMap;
        }

        public void LoadContent(ContentManager content)
        {
            AITexture = content.Load<Texture2D>("tank");

            AITextureCenter =
                new Vector2(AITexture.Width / 2, AITexture.Height / 2);

            waypoints.LoadContent(content);
        }
        #endregion

        #region Update and Draw

        public void Draw(SpriteBatch spriteBatch)
        {
            float facingDirection = (float)Math.Atan2(
                Direction.Y, Direction.X);

            spriteBatch.Begin();
            spriteBatch.Draw(AITexture, location, null, Color.White, facingDirection,
                AITextureCenter, scale, SpriteEffects.None, 0f);
            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            float elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (moving)
            {
                if (waypoints.Count >= 1)
                {
                    destination = waypoints.Peek();
                }

                if (AtDestination && waypoints.Count >= 1)
                {
                    waypoints.Dequeue();
                }

                if (!AtDestination)
                {
                    direction = -(location - destination);
                    direction.Normalize();
                    location = location + (Direction *
                        MoveSpeed * elapsedTime);
                }
            }
        }

        public void Reset()
        {
            waypoints.Clear();
            direction = Vector2.Zero;
            moving = false;
            Scale = map.Scale;
            location = map.MapToWorld(map.StartTile, true);
            destination = location;
        }
        #endregion
    }
}
