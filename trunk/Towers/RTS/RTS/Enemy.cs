﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RTS
{

    class Enemy : Sprite
    {
        //Game1 game;
        SpriteFont font;

        //different for different enemies
        protected float hp;
        protected int range;
        protected ElementType weakAgainst;
        protected ElementType strongAgainst;
        protected float dmgOffset = 1.0f;

        protected EnemyEffect effect = null;

        private Texture2D texture;
        private Texture2D turretTexture;
        Random rand = new Random();
        private PathFinder path;

        private Vector2 origin;

        private List<Projectile> projectileList = new List<Projectile>(5);

        private bool dead = false;
        private Point startTile; 

        private double moveRotationAngle;
        private double shootRotationAngle;
        private double playerRotationAngle;
        private double projectileRotationAngle;

        private float elapsedTime;
        private float shootElapsedTime;
        private float shootTimer = 1.6f;
        private float circle = MathHelper.Pi * 2;

        const float atDestinationLimit = 5f;

        public void Initialize(Game1 game, Vector2 startPosition, Map map)
        {
            this.game = game;
            contentManager = game.Content;
            graphicsDevice = game.GraphicsDevice;

            waypoints = new NodeList();
            path = new PathFinder();

            //change for diff enemies later
            range = 200;
            hp = 100;
            weakAgainst = ElementType.Water;
            strongAgainst = ElementType.Fire;


            this.map = map;
            int randomNum = rand.Next(0, map.StartTile.Count());
            startTile = map.StartTile[randomNum]; 

            path.Initialize(map);

            Reset();
            path.Reset(startTile);
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

        public override void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            spriteBatch.Draw(texture, position, null, Color.White, (float)moveRotationAngle, origin, map.ScaleB, SpriteEffects.None, 0f);
            spriteBatch.Draw(turretTexture, position, null, Color.White, (float)shootRotationAngle, new Vector2(0, turretTexture.Height / 2), map.ScaleB, SpriteEffects.None, 0f);
            foreach (Projectile proj in projectileList)
            {
                proj.Draw(spriteBatch);
            }
            if (effect != null)
            {
                spriteBatch.Draw(effect.Effect, position, null, Color.White,0f, Vector2.Zero, map.ScaleB, SpriteEffects.None, .25f);
            }
        }

        public virtual void Update(GameTime gameTime, List<Tower> towers)//List<Player> players)
        {
            ////Elapsed Time Calculations
            elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            shootElapsedTime += elapsedTime;

            if (effect != null)
            {
                effect.applyEffects(this);
                if (!effect.isValid(gameTime)) effect = null;

            }


            float facingDirection = (float)Math.Atan2(
            Direction.Y, Direction.X);
            moveRotationAngle = facingDirection;
            shootRotationAngle = facingDirection;

            foreach (Tower i in towers)
            {
                if (boundingCircle(this.position, range, i.Position))
                {
                    updateMovement(i);

                    if (shootElapsedTime >= shootTimer)
                    {
                        shootElapsedTime = 0;

                        //Create new projectiles
                        Projectile projectile = new Projectile();
                        projectile.Initialize(contentManager, graphicsDevice, position, (float)projectileRotationAngle, getTurretLength(), 6f);
                        projectile.LoadContent("ProjectileRed");
                        projectileList.Add(projectile);

                        //Add explosion to particle system
                        game.explosion.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));
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
                    direction = -(position - destination);

                    direction.Normalize();
                    position = position + (Direction *
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
            playerRotationAngle = (Math.Atan2(tower.Position.Y - position.Y, tower.Position.X - position.X) + 2 * circle) % circle;
            float difference = WrapAngle((float)playerRotationAngle - (float)moveRotationAngle);
            difference = MathHelper.Clamp(difference, -elapsedTime, elapsedTime);
            moveRotationAngle += difference;
            moveRotationAngle = moveRotationAngle % circle;

            //Shoot angle
            shootRotationAngle = Math.Atan2(tower.Position.Y - position.Y, tower.Position.X - position.X);

            //Adjusted shoot angle with variation for bullet realism
            int xVariation = rand.Next(-100, 100);
            int yVariation = rand.Next(-100, 100);
            projectileRotationAngle = Math.Atan2(tower.Position.Y + yVariation - position.Y, tower.Position.X + xVariation - position.X);
        }

        public void updateProjectiles()
        {
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

        public void Hit(Damage damage)
        {

            if (damage.type == weakAgainst)
            {
                dmgOffset = 1.5f;
            }
            else if (damage.type == strongAgainst)
            {
                dmgOffset = 0.5f;
            }
            else
            {
                dmgOffset = 1.0f;
            }

            hp -= damage.amount*dmgOffset;

            if (effect == null && damage.effect != null)
            {
                effect = damage.effect;
            }

            if (hp <= 0)
                dead = true;
        }

        public void effectDamage(int burnDmg)
        {
            hp -= burnDmg*dmgOffset;
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

        public float DistanceToDestination
        {
            get { return Vector2.Distance(position, destination); }
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
            position = map.MapToWorld(startTile, true);
            destination = position;
        }

        public bool boundingCircle(Vector2 V1, int radius, Vector2 V2)
        {
            Vector2 Distance = V1 - V2; 
            if (Distance.Length() < radius)
                return true;

            return false;
        }
    }
}
