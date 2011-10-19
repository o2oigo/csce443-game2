using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RTS
{


    class Enemy
    {
        Game1 game;
        SpriteFont font;
        ContentManager contentManager;
        GraphicsDevice graphicsDevice;
        SpriteBatch spriteBatch;

        private Texture2D texture;
        private Texture2D turretTexture;

        //private Vector2 position;
        //private int shotsTaken = 0;
        //private int shotsToDestroy = 2;
        //private double speed = 1.5;
        private Vector2 origin;

        private List<Projectile> projectileList = new List<Projectile>(5);

        Random rand = new Random();

        private float hp = 100;
        private int range;
        private bool dead = false;
        private float damageAlpha = 1.0f;

        private double moveRotationAngle;
        private double shootRotationAngle;
        private double playerRotationAngle;
        private double projectileRotationAngle;

        private float elapsedTime;
        private float shootElapsedTime;
        private float shootTimer = 1.6f;
        private float circle = MathHelper.Pi * 2;

        private Map map;
        private PathFinder path;
        const float atDestinationLimit = 5f;

        public void Initialize(Game1 game, Vector2 startPosition, Map map)
        {
            this.game = game;
            contentManager = game.Content;
            graphicsDevice = game.GraphicsDevice;

            waypoints = new NodeList();
            path = new PathFinder();
            range = 200;

            this.map = map;
            path.Initialize(map);

            Reset();
            path.Reset();
            path.IsSearching = !path.IsSearching;
        }

        public void LoadContent(String textureName)
        {
            texture = contentManager.Load<Texture2D>(textureName);
            turretTexture = contentManager.Load<Texture2D>("TurretEnemy");
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
            font = contentManager.Load<SpriteFont>("font");
        }

        public void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            spriteBatch.Draw(texture, location, null, Color.White, (float)moveRotationAngle, origin, map.ScaleB, SpriteEffects.None, 0f);
            spriteBatch.Draw(turretTexture, location, null, Color.White, (float)shootRotationAngle, new Vector2(0, turretTexture.Height / 2), map.ScaleB, SpriteEffects.None, 0f);
            foreach (Projectile proj in projectileList)
            {
                proj.Draw(spriteBatch);
            }
        }

        public virtual void Update(GameTime gameTime, List<Tower> towers)//List<Player> players)
        {
            ////Elapsed Time Calculations
            elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            shootElapsedTime += elapsedTime;
            //shootElapsedTime += elapsedTime;
            //
            ////Update movement and angles
            //
            //if (Vector2.Distance(this.position, players[0].getPosition()) < Vector2.Distance(this.position, players[1].getPosition()))
            //    updateMovement(players[0]);
            //else
            //    updateMovement(players[1]);
            //
            //
            ////Create and update projectiles (shoot)
            //updateProjectiles();

            float facingDirection = (float)Math.Atan2(
            Direction.Y, Direction.X);
            moveRotationAngle = facingDirection;
            shootRotationAngle = facingDirection;

            foreach (Tower i in towers)
            {
                //if (Vector2.Distance(this.location, i.getPosition()) < Vector2.Distance(this.location, i.getPosition()))
                if (boundingCircle(this.location, range, i.getPosition()))
                {
                    updateMovement(i);

                    if (shootElapsedTime >= shootTimer)
                    {
                        shootElapsedTime = 0;

                        //Create new projectiles
                        Projectile projectile = new Projectile();
                        projectile.Initialize(contentManager, graphicsDevice, location, (float)projectileRotationAngle, getTurretLength(), 6f);
                        projectile.LoadContent("ProjectileRed");
                        projectileList.Add(projectile);

                        //Add explosion to particle system
                        game.explosion.AddParticles(new Vector2(location.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), location.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));
                        // game.smoke.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));
                    }
                    break;
                }
            }
            updateProjectiles();

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

        public void positionAngle(Tower tower)
        {
        }

        public void updateMovement(Tower tower)
        {
            //Calculate Rotation Angles and Enemy Movement
            playerRotationAngle = (Math.Atan2(tower.getPosition().Y - location.Y, tower.getPosition().X - location.X) + 2 * circle) % circle;
            float difference = WrapAngle((float)playerRotationAngle - (float)moveRotationAngle);
            difference = MathHelper.Clamp(difference, -elapsedTime, elapsedTime);
            moveRotationAngle += difference;
            moveRotationAngle = moveRotationAngle % circle;

            //Update position based on speed and angle
            //position.X += (float)(Math.Cos(moveRotationAngle) * speed);
            //position.Y += (float)(Math.Sin(moveRotationAngle) * speed);

            //Shoot angle
            shootRotationAngle = Math.Atan2(tower.getPosition().Y - location.Y, tower.getPosition().X - location.X);

            //Adjusted shoot angle with variation for bullet realism
            int xVariation = rand.Next(-100, 100);
            int yVariation = rand.Next(-100, 100);
            projectileRotationAngle = Math.Atan2(tower.getPosition().Y + yVariation - location.Y, tower.getPosition().X + xVariation - location.X);
        }

        public void updateProjectiles()
        {

            //Shoot every few seconds / Adds projectiles to screen / Adds particle effects
            //if (shootElapsedTime >= shootTimer)
            //{
            //    shootElapsedTime = 0;
            //
            //    //Create new projectiles
            //    //Projectile projectile = new Projectile();
            //    //projectile.Initialize(contentManager, graphicsDevice, location, (float)projectileRotationAngle, getTurretLength(), 6f);
            //    //projectile.LoadContent("ProjectileRed");
            //    //projectileList.Add(projectile);
            //
            //    //Add explosion to particle system
            //    //game.explosion.AddParticles(new Vector2(location.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), location.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));
            //    // game.smoke.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));
            //}

            //Update Projectiles
            foreach (Projectile proj in projectileList)
            {
                proj.Update();
            }

            //Remove Projectile if it goes off-screen
            for (int i = 0; i < projectileList.Count; i++)
            {
                Projectile proj = projectileList[i];
                if (proj.getPosition().X > graphicsDevice.Viewport.Width || proj.getPosition().X < 0
                    || proj.getPosition().Y > graphicsDevice.Viewport.Height || proj.getPosition().Y < 0)
                {
                    projectileList.RemoveAt(i);
                }
            }
        }


        private static float WrapAngle(float radians)
        {
            while (radians < -MathHelper.Pi)
            {
                radians += MathHelper.TwoPi;
            }
            while (radians > MathHelper.Pi)
            {
                radians -= MathHelper.TwoPi;
            }
            return radians;
        }

        public Texture2D getTexture()
        {
            return texture;
        }

        public List<Projectile> getProjectiles()
        {
            return projectileList;
        }

        public Vector2 getPosition()
        {
            return location;
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

        public double getPlayerRotationAngle()
        {
            return playerRotationAngle;
        }


        public void Hit(float damage)
        {
            hp -= damage;
            if (hp <= 0)
                dead = true;
        }

        public bool isDead()
        {
            return dead;
        }

        private NodeList waypoints;
        public NodeList Waypoints
        {
            get { return waypoints; }
        }

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

        private Vector2 destination;
        public Vector2 Destination
        {
            get { return destination; }
        }

        const float moveSpeed = 100f;
        public static float MoveSpeed
        {
            get { return moveSpeed; }
        }

        private Vector2 location;
        public Vector2 Location
        {
            get { return location; }
        }

        public float DistanceToDestination
        {
            get { return Vector2.Distance(location, destination); }
        }

        public bool AtDestination
        {
            get { return DistanceToDestination < atDestinationLimit; }
        }

        protected bool moving;
        public bool Moving
        {
            get { return moving; }
            set { moving = value; }
        }

        public float getTurretLength()
        {
            return turretTexture.Width;
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

        public bool boundingCircle(Vector2 V1, int radius, Vector2 V2)
        {
            // Vector2 V1 = new Vector2(x1, y1); // reference point 1
            //Vector2 V2 = new Vector2(x2, y2); // reference point 2
            Vector2 Distance = V1 - V2; // get the distance between the two reference points
            if (Distance.Length() < radius) // if the distance is less than the diameter
                return true;

            return false;
        }
    }
}
