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

        private Texture2D texture;
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
            this.LoadContent("Tower1");
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
        
        public void LoadContent(String textureName)
        {
            texture = contentManager.Load<Texture2D>(textureName);
            turretTexture = contentManager.Load<Texture2D>("TowerTurret");
            font = contentManager.Load<SpriteFont>("font");
            origin.X = texture.Width / 2;
            origin.Y = texture.Height / 2;
        }

        public override void Draw(SpriteBatch SB)
        {
            spriteBatch = SB;
            spriteBatch.Draw(texture, position, null, Color.White, (float)moveRotationAngle, origin, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(turretTexture, new Vector2(position.X, position.Y - 25), null, Color.White, (float)shootRotationAngle, new Vector2(0, turretTexture.Height / 2), map.ScaleB, SpriteEffects.None, 0f);
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
