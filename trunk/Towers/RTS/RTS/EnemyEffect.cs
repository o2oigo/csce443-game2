using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace RTS
{
    abstract class EnemyEffect
    {
        protected int created = 0;
        protected int duration;
        protected int life;

        protected Texture2D effect;
        public Texture2D Effect
        {
            get { return effect; }
        }

        public abstract void applyEffects(Enemy enemy);
        public abstract void undoEffect(Enemy enemy);

        public Boolean isValid(GameTime time)
        {
            life += time.ElapsedGameTime.Milliseconds;
            if (life < duration * 1000)
                return true;
            else return false;
        }
    }

    class EnemyEffectBurn : EnemyEffect
    {
        protected float burnDmg;
        protected float offset = 1.0f;
        public float Offset
        {
            get { return offset; }
            set { offset = value; }
        }

        public EnemyEffectBurn(Game1 game, int duration, int burnDmg)
        {
            this.duration = duration;
            this.burnDmg = burnDmg;
            effect = game.Content.Load<Texture2D>("burn");
        }

        public override void applyEffects(Enemy enemy)
        {
            enemy.effectDamage(burnDmg*offset);
        }
        public override void undoEffect(Enemy enemy)
        {

        }



    }



}


