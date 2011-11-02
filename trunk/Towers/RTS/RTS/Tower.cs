using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace RTS
{
    public struct Damage
    {
        public float amount;
        public int level;
        public ElementType type;
        public EnemyEffect effect;

        public Damage(float amt, int lvl, ElementType t,EnemyEffect e)
        {
            amount = amt;
            level = lvl;
            type = t;
            effect = e;
        }
    }

    public class Tower : Sprite
    {
        GamePadState currentState;

        private float elapsedTime;
        private Vector2 origin;
        protected PlayerIndex playerIndex;
        protected Enemy shootAt;

        private float shootTimer = .8f;
        private float towerRange = 400;
        private float shootElapsedTime = 0;

        SpriteFont font;
        private int shotsTaken = 0;
        private int shotsToDestroy = 100;
        private int hp;
        private bool dead = false;
        private bool playerIsNear = false;
        private string level = "level 1";
        private string towerName = "Arrow Tower";
        private int attackDamage = 25;

        private double moveRotationAngle = 0;
        protected double shootRotationAngle = 0;

        //private Texture2D texture;
        private Texture2D cannon1Texture;
        private Texture2D cannon2Texture;
        private Texture2D cannon3Texture;
        private Texture2D cannon4Texture;
        private Texture2D cannon5Texture;
        private Texture2D cannon6Texture;
        private Texture2D cannon7Texture;
        private Texture2D cannon8Texture;
        private Texture2D cannon9Texture;
        private Texture2D cannon10Texture;
        private Texture2D cannon11Texture;
        private Texture2D cannon12Texture;
        private Texture2D turretTexture;

        protected List<Projectile> projectileList = new List<Projectile>(5);

        private Damage damage;
        public Damage Damage
        {
            get { return damage; }
        }

        public Tower(Game1 game, PlayerIndex playerIndex, Vector2 startPosition)
        {
            this.playerIndex = playerIndex;
            this.Initialize(game, startPosition);
            this.LoadContent();
        }

        public void Initialize(Game1 game, Vector2 startPosition)
        {
            this.game = game;
            contentManager = game.Content;
            graphicsDevice = game.GraphicsDevice;
            position = startPosition;
            currentState = GamePad.GetState(PlayerIndex.One);
            //towerRange = 50;

            //damage = new Damage(10, 1,ElementType.Normal, new EnemyEffectBurn(game,5,1));
            damage = new Damage(10, 1, ElementType.Normal, null);

            map = game.Map;
            //damage = new Damage(10, 1,ElementType.Fire, new EnemyEffectBurn(game,5,1));
        }
        
        public void LoadContent()
        {
            cannon1Texture = contentManager.Load<Texture2D>("cannon1");
            cannon2Texture = contentManager.Load<Texture2D>("cannon2");
            cannon3Texture = contentManager.Load<Texture2D>("cannon3");
            cannon4Texture = contentManager.Load<Texture2D>("cannon4");
            cannon5Texture = contentManager.Load<Texture2D>("cannon5");
            cannon6Texture = contentManager.Load<Texture2D>("cannon6");
            cannon7Texture = contentManager.Load<Texture2D>("cannon7");
            cannon8Texture = contentManager.Load<Texture2D>("cannon8");
            cannon9Texture = contentManager.Load<Texture2D>("cannon9");
            cannon10Texture = contentManager.Load<Texture2D>("cannon10");
            cannon11Texture = contentManager.Load<Texture2D>("cannon11");
            cannon12Texture = contentManager.Load<Texture2D>("cannon12");
            turretTexture = contentManager.Load<Texture2D>("TowerTurret");
            font = contentManager.Load<SpriteFont>("font");
            origin.X = cannon1Texture.Width / 2;
            origin.Y = cannon1Texture.Height / 2;
        }

        public override void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            if (shootRotationAngle <= -3.5*Math.PI / 6 && shootRotationAngle > -4.5 * Math.PI / 6)
                spriteBatch.Draw(cannon1Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= -4.5 * Math.PI / 6 && shootRotationAngle > -5.5 * Math.PI / 6)
                spriteBatch.Draw(cannon2Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= -5.5 * Math.PI / 6 || shootRotationAngle > 5.5* Math.PI / 6)
                spriteBatch.Draw(cannon3Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= 5.5*Math.PI / 6 && shootRotationAngle > 4.5 * Math.PI / 6)
                spriteBatch.Draw(cannon4Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= 4.5 * Math.PI / 6 && shootRotationAngle > 3.5 * Math.PI / 6)
                spriteBatch.Draw(cannon5Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= 3.5 * Math.PI / 6 && shootRotationAngle > 2.5 * Math.PI / 6)
                spriteBatch.Draw(cannon6Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= 2.5 * Math.PI / 6 && shootRotationAngle > 1.5 * Math.PI / 6)
                spriteBatch.Draw(cannon7Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= 1.5 * Math.PI / 6 && shootRotationAngle > .5 * Math.PI / 6)
                spriteBatch.Draw(cannon8Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= .5 * Math.PI / 6 && shootRotationAngle > -.5)
                spriteBatch.Draw(cannon9Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= -.5 && shootRotationAngle > -1.5 * Math.PI / 6)
                spriteBatch.Draw(cannon10Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= -1.5 * Math.PI / 6 && shootRotationAngle > -2.5 * Math.PI / 6)
                spriteBatch.Draw(cannon11Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            else if (shootRotationAngle <= -2.5 * Math.PI / 6 && shootRotationAngle > -3.5 * Math.PI / 6)
                spriteBatch.Draw(cannon12Texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            //spriteBatch.Draw(turretTexture, new Vector2(position.X, position.Y - 25), null, Color.White, (float)shootRotationAngle, new Vector2(0, turretTexture.Height / 2), map.ScaleB, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, towerName, new Vector2(position.X - 50, position.Y - 70), Color.Black);
            spriteBatch.DrawString(font, level, new Vector2(position.X - 40, position.Y - 50), Color.Black);
            spriteBatch.DrawString(font, "HP: " + hp, new Vector2(position.X - 40, position.Y + 30), Color.Black);
            
            foreach (Projectile proj in projectileList)
            {
                proj.Draw(spriteBatch);
            }
        }

        public void Update(GameTime gameTime, List<Enemy> enemies)
        {
            //Elapsed Time Calculations
            elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            shootElapsedTime += elapsedTime;
            hp = shotsToDestroy - shotsTaken;
            updateTurret(enemies);
            updateProjectiles();
        }

        public void updateGamePad()
        {

        }

        public void updateKeyboard()
        {

        }

        public void updateTurret(List<Enemy> enemies)
        {
            shootAt = null;
            if (enemies.Count != 0)
            {
               // shootAt = enemies[0];
                foreach (Enemy enemy in enemies)
                {
                    if (Vector2.Distance(position, enemy.Position) <= towerRange)
                    {
                        shootAt = enemy;
                        break;
                    }
                }

                //Shoot
                if (shootAt != null)
                {
                    shootRotationAngle = Math.Atan2(shootAt.Position.Y - position.Y, shootAt.Position.X - position.X);
                    if (shootElapsedTime > shootTimer)
                    {
                        this.createProjectile();
                        shootElapsedTime = 0;
                    }
                }
            }
        }

        public virtual void createProjectile()
        {
            Projectile projectile = new Projectile();
            projectile.Initialize(contentManager, graphicsDevice, new Vector2(position.X, position.Y - 25), (float)shootRotationAngle, getTurretLength(), 20f, map);
            if (playerIndex == PlayerIndex.One)
                projectile.LoadContent("ProjectileBlue");
            else
                projectile.LoadContent("ProjectilePurple");
            projectileList.Add(projectile);

            game.explosion.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength() * map.ScaleB, position.Y - 25 + (float)Math.Sin(shootRotationAngle) * getTurretLength() * map.ScaleB));
            // game.smoke.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y - 25 + (float)Math.Sin(shootRotationAngle) * getTurretLength()));         
        }

        public void updateProjectiles()
        {
            for (int i = 0; i < projectileList.Count; i++)
            {
                //Remove Projectile if it goes off-screen
                Projectile proj = projectileList[i];
                if (proj.getPosition().X > graphicsDevice.Viewport.Width || proj.getPosition().X < 0
                    || proj.getPosition().Y > graphicsDevice.Viewport.Height || proj.getPosition().Y < 0)
                {
                    projectileList.Remove(proj);
                }
                proj.Update();
            }
        }

        public float getTurretLength()
        {
            return turretTexture.Width;
        }

        public Texture2D getTexture() // MIGHT CAUSE PROBLEMS
        {
            return cannon1Texture;
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

        //If the tower was hit by an enemy or projectile
        public void Hit()
        {
            shotsTaken++;
            if (shotsTaken >= shotsToDestroy)
                dead = true;
        }

        public bool isDead()
        {
            return dead;
        }

        public int getShotsTaken()
        {
            return shotsTaken;
        }

        public int getShotsToDestroy()
        {
            return shotsToDestroy;
        }

        public int getAttackDamage()
        {
            return attackDamage;
        }

        public void setToLvlTwo()
        {

            shotsToDestroy = 150;
            damage.amount = 20;
            damage.type = ElementType.Normal;
            level = "level 2";

        }

        public void setPlayerIsNear(bool _playerIsNear)
        {
            playerIsNear = _playerIsNear;
        }

        public bool getPlayerIsNear()
        {
            return playerIsNear;
        }

        public void setTowerName(string _towerName)
        {
            towerName = _towerName;
        }

        public string getTowerName()
        {
            return towerName;
        }

        public void setTowerLvl(string _level)
        {
            level = _level;
        }

        public string getTowerLvl()
        {
            return level;
        }

        public void setToFireTower()
        {
            damage.type = ElementType.Fire;
            damage.effect = new EnemyEffectBurn(game, 5, 1);
            towerName = "Fire Tower";
        }

    }
}
