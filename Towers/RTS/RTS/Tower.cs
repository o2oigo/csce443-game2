using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;

namespace RTS
{

    public class Tower : Sprite
    {
        GamePadState currentState;

        protected float elapsedTime;
        protected Vector2 origin;
        protected PlayerIndex playerIndex;
        protected Enemy shootAt;

        protected float shootTimer = .8f;
        protected float soundTimer = 1f;
        protected float towerRange = 400;
        protected float shootElapsedTime = 5f;
        protected float soundElapsedTime = 0f;

        protected SpriteFont font;
        //protected int shotsTaken = 0;
        //protected int shotsToDestroy = 100;

        protected float hp = 100;
        public float HP
        {
            get { return hp; }
        }
        protected float maxHP = 100;
        public float MaxHP
        {
            get { return maxHP; }
        }

        private bool dead = false;
        private bool playerIsNear = false;
        protected string level = "level 1";
        protected int ilevel = 1;
        protected string towerName = "Cannon Tower";
        protected SoundEffect shootSound;
        protected bool isShooting = false;
       // protected int attackDamage = 25;

        protected double moveRotationAngle = 0;
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
        protected Texture2D turretTexture;

        protected List<Projectile> projectileList = new List<Projectile>(5);

        public Damage damage = new Damage(10, 1, ElementType.Normal, null);
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
            //damage = new Damage(10, 1, ElementType.Normal, null);

            map = game.Map;
            //damage = new Damage(10, 1,ElementType.Fire, new EnemyEffectBurn(game,5,1));
        }
        
        public virtual void LoadContent()
        {
            shootSound = contentManager.Load<SoundEffect>("Sound/cannon2 - no filter");

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
           // spriteBatch.DrawString(font, towerName, new Vector2(position.X - 50, position.Y - 70), Color.Black);
            //spriteBatch.DrawString(font, level, new Vector2(position.X - 40, position.Y - 50), Color.Black);
            spriteBatch.DrawString(font, getTowerLvl() + ", \nHP: " + hp, new Vector2(position.X - 120, position.Y + 30), Color.Black);
            
            foreach (Projectile proj in projectileList)
            {
                proj.Draw(spriteBatch);
            }
        }

        public virtual void Update(GameTime gameTime, List<Enemy> enemies)
        {
            //Elapsed Time Calculations
            elapsedTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            shootElapsedTime += elapsedTime;
            //soundElapsedTime += elapsedTime;
            //hp = shotsToDestroy - shotsTaken;
            updateTurret(enemies);
            updateProjectiles(gameTime);
        }

        public void updateGamePad()
        {

        }

        public void updateKeyboard()
        {

        }

        public virtual void updateTurret(List<Enemy> enemies)
        {
            shootAt = null;
            isShooting = false;
            if (enemies.Count != 0)
            {
                //shootAt = enemies[0];
                foreach (Enemy enemy in enemies)
                {
                    if (shootAt == null && Vector2.Distance(position, enemy.Position) <= towerRange)
                        shootAt = enemy;
                    if (Vector2.Distance(position, enemy.Position) <= towerRange /*&& (Vector2.Distance(game.House.Position, enemy.Position) < Vector2.Distance(game.House.Position, shootAt.Position))*/)
                    {
                        shootAt = enemy;     
                        break;
                    }
                }

                //Shoot
                if (shootAt != null)
                {
                    isShooting = true;
                    shootRotationAngle = Math.Atan2(shootAt.Position.Y - position.Y, shootAt.Position.X - position.X);
                    if (shootElapsedTime > shootTimer)
                    {
                        this.createProjectile(shootAt);
                        shootElapsedTime = 0;
                    }
                }
            }
        }

        public virtual void createProjectile(Enemy shootAt)
        {
            Projectile projectile = new Projectile();
            projectile.Initialize(contentManager, graphicsDevice, new Vector2(position.X, position.Y - 25), (float)shootRotationAngle, getTurretLength(), 2000f, map);
            projectile.LoadContent("Projectile");
            projectileList.Add(projectile);

            game.explosion.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength() * map.ScaleB, position.Y - 25 + (float)Math.Sin(shootRotationAngle) * getTurretLength() * map.ScaleB));
            game.smoke.AddParticles(new Vector2(position.X + (float)Math.Cos(shootRotationAngle) * getTurretLength(), position.Y - 25 + (float)Math.Sin(shootRotationAngle) * getTurretLength()));

            playShootSound();
        }

        public virtual void playShootSound()
        {
            shootSound.Play();
        }

        public virtual void updateProjectiles(GameTime gameTime)
        {
            for (int i = 0; i < projectileList.Count; i++)
            {
                //Remove Projectile if it goes off-screen
                Projectile proj = projectileList[i];
                if ((proj.getPosition().X > game.getCurrentRectangle().Width) || proj.getPosition().X < 0
                    || (proj.getPosition().Y > game.getCurrentRectangle().Height) || proj.getPosition().Y < 0)
                {
                    projectileList.Remove(proj);
                }
                proj.Update(gameTime);
            }
        }

        public Vector2 getOrigin()
        {
            return origin;
        }

        public float getTurretLength()
        {
            return turretTexture.Width;
        }

        public virtual Texture2D getTexture() // MIGHT CAUSE PROBLEMS
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
        //public void Hit()
        //{
        //    shotsTaken++;
        //    if (shotsTaken >= shotsToDestroy)
        //        dead = true;
        //}

        public void Hit(Damage dmg)
        {
            hp -= dmg.amount;
            if (hp <= 0)
                dead = true;
        }

        public void Hit(float dmg)
        {
            hp -= dmg;
            if (hp <= 0)
                dead = true;
        }

        public bool isDead()
        {
            return dead;
        }

        //public int getShotsTaken()
        //{
        //    return shotsTaken;
        //}
        //
        //public int getShotsToDestroy()
        //{
        //    return shotsToDestroy;
        //}

       // public int getAttackDamage()
       // {
      //      return attackDamage;
      //  }

        public virtual void setToLvlTwo()
        {

            //shotsToDestroy = 150;
            //hp += 50;
            maxHP = hp = 150;
            damage.amount = 20;
            //damage.type = ElementType.Normal;
            level = "level 2";
            ilevel = 2;

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

        public int getLevel()
        {
            return ilevel;
        }
        public void setToFireTower()
        {
            damage.type = ElementType.Fire;
            //damage.effect = new EnemyEffectBurn(game, 5, 0.2f);
            towerName = "Fire Tower";
        }

    }
}
