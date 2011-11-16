﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS
{
    public abstract class EnemyEffect
    {
        protected int created = 0;
        protected int duration;
        public int Duration
        {
            get { return duration; }
            set { duration= value; }
        }
        protected int life;
        protected Game1 game;


        public abstract void applyEffects(Enemy enemy);
        public abstract void undoEffect(Enemy enemy);

        public Boolean isValid(GameTime time)
        {
            life += time.ElapsedGameTime.Milliseconds;
            if (life < duration * 100)
            {
                return true;
            }
            else
            {
                life = 0;
                return false;
            }
        }
    }

    public class EnemyEffectBurn : EnemyEffect
    {
        protected float burnDmg;
        protected float offset = 1.0f;
        public float Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public EnemyEffectBurn(Game1 game, int duration, float burnDmg)
        {
            this.duration = duration;
            this.burnDmg = burnDmg;

            this.game = game;
        }

        public override void applyEffects(Enemy enemy)
        {
            game.fire.setDirection((float)(-Math.PI / 2));
            game.fire.AddParticles(new Vector2(enemy.Position.X, enemy.Position.Y));
            enemy.effectDamage(burnDmg * offset);
        }
        public override void undoEffect(Enemy enemy)
        {
        }
    }

    public class EnemyEffectStun : EnemyEffect
    {
        protected float prevSpeed;
        protected float offset = 1.0f;
        public float Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public EnemyEffectStun(Game1 game, int duration)
        {
            this.duration = duration;
        }

        public override void applyEffects(Enemy enemy)
        {
            if (enemy.MoveSpeed != 0) prevSpeed = enemy.MoveSpeed;
            enemy.effectStun();
        }

        public override void undoEffect(Enemy enemy)
        {
            enemy.MoveSpeed = prevSpeed;
        }
    }


}

