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
        //The "close enough" limit, if the AI is inside this distance to it's
        //destination it's considered at it's destination
        const float atDestinationLimit = 5f;

        private Texture2D AITexture;
        private Vector2 AITextureCenter;
        private Map map;
        private PathFinder path;

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

        /// <summary>
        /// Length 1 vector that represents the tanks’ movement and facing direction
        /// </summary>
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

        /// <summary>
        /// The tanks' movement speed
        /// </summary>
        const float moveSpeed = 100f;
        public static float MoveSpeed
        {
            get { return moveSpeed; }
        }

        //the location of the tanks' current waypoint
        private Vector2 destination;
        public Vector2 Destination
        {
            get { return destination; }
        }

        //the tanks' location on the map
        private Vector2 location;
        public Vector2 Location
        {
            get { return location; }
        }

        /// <summary>
        /// The list of points the tanks will move to in order from first to last
        /// </summary>
        private NodeList waypoints;
        public NodeList Waypoints
        {
            get { return waypoints; }
        }

        /// <summary>
        /// Linear distance to the Tanks' current destination
        /// </summary>
        public float DistanceToDestination
        {
            get { return Vector2.Distance(location, destination); }
        }

        /// <summary>
        /// True when the tank is "close enough" to it's destination
        /// </summary>
        public bool AtDestination
        {
            get { return DistanceToDestination < atDestinationLimit; }
        }
        #endregion

        #region Initialization

        public AI()
        {
            waypoints = new NodeList();
            path = new PathFinder();
        }

        public void Initialize(Map mazeMap)
        {
            location = Vector2.Zero;
            destination = location;
            map = mazeMap;
            path.Initialize(map);

            Reset();
            path.Reset();
            path.IsSearching = !path.IsSearching;
        }

        /// <summary>
        /// Load the tanks' texture resources
        /// </summary>
        /// <param name="content"></param>
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

            if (path.SearchStatus == SearchStatus.PathFound && !Moving)
            {
                foreach (Point point in path.FinalPath())
                {
                    Waypoints.Enqueue(map.MapToWorld(point, true));
                }
                Moving = true;
            }
            path.Update(gameTime);

            if (moving)
            {
                // If we have any waypoints, the first one on the list is where 
                // we want to go
                if (waypoints.Count >= 1)
                {
                    destination = waypoints.Peek();
                }

                // If we’re at the destination and there is at least one waypoint in 
                // the list, get rid of the first one since we’re there now
                if (AtDestination && waypoints.Count >= 1)
                {
                    waypoints.Dequeue();
                }

                if (!AtDestination)
                {
                    direction = -(location - destination);
                    //This scales the vector to 1, we'll use move Speed and elapsed Time 
                    //to find the how far the tank moves
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
