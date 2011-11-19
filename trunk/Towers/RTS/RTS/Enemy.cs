﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RTS
{

    public class Enemy : Sprite
    {
        protected SpriteFont font;

        //different for different enemies
        protected float hp;
        protected int range;
        protected ElementType weakAgainst;
        protected ElementType strongAgainst;
        protected float dmgOffset = 1.0f;

        protected EnemyEffect effect = null;

        //private Texture2D texture;
        private Texture2D turretTexture;
        Random rand = new Random();
        private PathFinder path;

        protected Vector2 origin;

        private List<Projectile> projectileList = new List<Projectile>(5);

        private bool dead = false;
        private Point startTile;
        private SpriteEffects isFlipped;

        private double moveRotationAngle;
        private double shootRotationAngle;
        private double playerRotationAngle;
        private double projectileRotationAngle;

        private List<Vector2> curve = new List<Vector2>();
        private float curveTimer = 0;

        private float elapsedTime;
        private float effectTimer;
        private float shootElapsedTime;
        private float shootTimer = 1.0f;
        private float circle = MathHelper.Pi * 2;

        const float atDestinationLimit = 0.001f;

        #region Properties
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

        protected float moveSpeed = 100f;
        public float MoveSpeed
        {
            set { moveSpeed = value; }
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

        public Vector2 getOrigin()
        {
            return origin;
        }

        //public Texture2D getTexture()
        //{
        //    return texture;
        //}

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

        public bool isDead()
        {
            return dead;
        }

        #endregion

        public virtual void Initialize(Game1 game)
        {
            this.game = game;
            this.contentManager = game.Content;
            this.graphicsDevice = game.GraphicsDevice;

            waypoints = new NodeList();
            path = new PathFinder();

            this.map = game.Map;
            int randomNum = rand.Next(0, map.StartTile.Count());
            startTile = map.StartTile[randomNum]; 

            path.Initialize(map);

            Reset();
            path.Reset(startTile);
            path.IsSearching = !path.IsSearching;
            path.SearchPath();
        }

        public virtual void LoadContent()
        {
            //texture = contentManager.Load<Texture2D>(textureName);
            turretTexture = contentManager.Load<Texture2D>("TurretEnemy");

            origin.X = Size.Width / 2;
            origin.Y = Size.Height / 2;
            font = contentManager.Load<SpriteFont>("font");
        }

        public override void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            spriteBatch.Draw(animation.currentSpriteSheet().texture, Position, animation.currentSpriteSheet().rectangles[animation.FrameIndex], Color.White, 0f, origin, 1.0f, isFlipped, 0f);
            //spriteBatch.Draw(turretTexture, position, null, Color.White, (float)shootRotationAngle, new Vector2(0, turretTexture.Height / 2), map.ScaleB, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, "HP: " + (int)hp, new Vector2(position.X - 40, position.Y + 40), Color.Black);
            foreach (Projectile proj in projectileList)
            {
                proj.Draw(spriteBatch);
            }
        }


        public virtual void Update(GameTime gameTime, List<Tower> towers)//List<Player> players)
        {
            ////Elapsed Time Calculations
            effectTimer += (float)gameTime.ElapsedGameTime.Milliseconds;
            elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            effectTimer += (float)gameTime.ElapsedGameTime.Milliseconds;
            shootElapsedTime += elapsedTime;

            if (effect != null && effectTimer>100)
            {
                //game.fire.setDirection((float)(-Math.PI/2));
                //game.fire.AddParticles(new Vector2(position.X, position.Y));
                if (effectTimer > 100)
                {
                    effect.applyEffects(this);
                    if (!effect.isValid(gameTime))
                    {
                        effect.undoEffect(this);
                        effect = null;
                    }
                    effectTimer = 0f;
                }
            }

            float facingDirection = (float)Math.Atan2(Direction.Y, Direction.X);
            moveRotationAngle = facingDirection;
            shootRotationAngle = facingDirection;

            doPathfinding(gameTime);

            updateAnimation();
            animation.Update(gameTime);
        }

        public void updateAnimation()
        {
            isFlipped = SpriteEffects.None;
            double dir = Math.Atan2(direction.Y, direction.X);
            dir = dir % circle;

            if (Math.Abs(dir) > (Math.PI * (0.75)) && Math.Abs(dir) <= Math.PI) { animation.CurrentSprite = "left"; }
            else if (Math.Abs(dir) >= 0 && Math.Abs(dir) < Math.PI * 0.25)
            {
                animation.CurrentSprite = "left";
                isFlipped = SpriteEffects.FlipHorizontally;
            }
            else if (dir <= Math.PI * (0.75) && dir > Math.PI * 0.25) {animation.CurrentSprite = "front";}
            else if (dir <= -Math.PI * (0.75) && dir > -Math.PI * 0.25) { animation.CurrentSprite = "back"; }
            else{ animation.CurrentSprite = "back"; }

            //if (direction.X < 0 && direction.Y ==0)
            //else if (direction.X < 0 && direction.Y > 0)
            //{
            //    animation.CurrentSprite = "rightUp";
            //    isFlipped = SpriteEffects.FlipHorizontally;
            //}
            //else if (direction.X < 0 && direction.Y < 0)
            //{
            //    animation.CurrentSprite = "rightDown";
            //    isFlipped = SpriteEffects.FlipHorizontally;
            //}

            //else if (direction.X > 0 && direction.Y > 0)
            //{
            //    animation.CurrentSprite = "rightUp";
            //}
            //else if (direction.X > 0 && direction.Y < 0)
            //{
            //    animation.CurrentSprite = "rightDown";
            //}
            //else if (direction.X == 0 && direction.Y > 0)
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
            int xVariation = 0;    
            int yVariation = 0;   
            projectileRotationAngle = Math.Atan2(tower.Position.Y + yVariation - position.Y, tower.Position.X + xVariation - position.X);
        }

        public void updateProjectiles(GameTime gameTime)
        {
            //Update Projectiles
            foreach (Projectile proj in projectileList)
            {
                proj.Update(gameTime);
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


        #region PathFinding
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

        private void addToCurveList()
        {
            for (int i = 0; i < 4 && waypoints.Count >= 4-i; i++)
            {
                if (i == 3)
                {
                    curve.Add(waypoints.Peek());
                }
                else
                {
                    curve.Add(waypoints.Dequeue());
                    waypoints.Dequeue();
                    waypoints.Dequeue();
                    waypoints.Dequeue();
                }
            }
            if (curve.Count != 0) destination = curve[curve.Count - 1];
        }

        public void doPathfinding(GameTime gameTime)
        {
            if (path.SearchStatus == SearchStatus.PathFound && !Moving)
            {
                foreach (Point point in path.FinalPath())
                {
                    Waypoints.Enqueue(map.MapToWorld(point, true));
                }
                Moving = true;
            }
            //path.Update(gameTime);

            if (moving)
            {
                if (curve.Count == 0)
                {
                    addToCurveList();
                }

                if (AtDestination)
                {
                    curve.Clear();
                    addToCurveList();
                    curveTimer = 0;
                }

                if (!AtDestination)
                {
                    List<Vector2> pos = new List<Vector2>();
                    int j = 0;
                    for (int i = 0; i < 4 && curve.Count!=0; i++)
                    {
                        pos.Add(curve[j]);
                        if (j < curve.Count-1) j++;
                    }
                    direction = -(position - destination);

                    direction.Normalize();
                    position = GetPoint(curveTimer, pos[0], pos[1], pos[2], pos[3]);
                    curveTimer += (float)gameTime.ElapsedGameTime.TotalSeconds * 0.3f;
                }
                //if (waypoints.Count >= 1)
                //{
                //    destination = waypoints.Peek();
                //}

                //if (AtDestination && waypoints.Count >= 1)
                //{
                //    waypoints.Dequeue();
                //}

                //if (!AtDestination)
                //{
                //    direction = -(position - destination);
                //
                //    direction.Normalize();
                //    position = position + (Direction *
                //        MoveSpeed * elapsedTime);
                //}
            }
        }

        private Vector2 GetPoint(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
        {
            float cx = 3 * (p1.X - p0.X);
            float cy = 3 * (p1.Y - p0.Y);

            float bx = 3 * (p2.X - p1.X) - cx;
            float by = 3 * (p2.Y - p1.Y) - cy;

            float ax = p3.X - p0.X - cx - bx;
            float ay = p3.Y - p0.Y - cy - by;

            float Cube = t * t * t;
            float Square = t * t;

            float resX = (ax * Cube) + (bx * Square) + (cx * t) + p0.X;
            float resY = (ay * Cube) + (by * Square) + (cy * t) + p0.Y;

            return new Vector2(resX, resY);
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
#endregion

        #region Attack & Take Damage
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

            hp -= damage.amount * dmgOffset;

            if (effect == null && damage.effect != null)
            {
                effect = damage.effect;
                applyOffset(damage);
            }

            if (hp <= 0)
                dead = true;
        }

        public void applyOffset(Damage damage)
        {
            if (effect is EnemyEffectBurn)
            {
                EnemyEffectBurn tmpEffect = (EnemyEffectBurn)effect;
                if (weakAgainst == ElementType.Fire)
                    tmpEffect.Offset = 1.5f;
                else if (strongAgainst == ElementType.Fire)
                    tmpEffect.Offset = 0.5f;
                else tmpEffect.Offset = 1.0f;
            }
            if (effect is EnemyEffectStun)
            {
                EnemyEffectStun tmpEffect = (EnemyEffectStun)effect;
                if (weakAgainst == ElementType.Lightning)
                    tmpEffect.Duration = tmpEffect.Duration+(int)((float)tmpEffect.Duration*0.5f*damage.level);
                else if (strongAgainst == ElementType.Lightning)
                    tmpEffect.Duration = tmpEffect.Duration - (int)((float)tmpEffect.Duration * 0.5f);
            }
        }

        public void effectDamage(float burnDmg)
        {
            hp -= burnDmg*dmgOffset;
            if (hp <= 0)
                dead = true;
            
        }

        public void effectStun()
        {
            moveSpeed = 0;
        }

        public void Attack(List<Tower> towers, GameTime gameTime)
        {
            if (!(effect is EnemyEffectStun))
            {
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
                            projectile.Initialize(contentManager, graphicsDevice, position, (float)projectileRotationAngle, getTurretLength(), 1200f, map);
                            projectile.LoadContent("Projectile");
                            projectileList.Add(projectile);

                            //Add explosion to particle system
                            game.explosion.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength() * map.ScaleB, position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength() * map.ScaleB));
                            game.smoke.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y + (float)Math.Sin(shootRotationAngle) * getTurretLength()));
                        }
                        break;
                    }
                }
            }
            updateProjectiles(gameTime);
        }

        public bool boundingCircle(Vector2 V1, int radius, Vector2 V2)
        {
            Vector2 Distance = V1 - V2;
            if (Distance.Length() < radius)
                return true;
            return false;
        }

        #endregion

    }
}
