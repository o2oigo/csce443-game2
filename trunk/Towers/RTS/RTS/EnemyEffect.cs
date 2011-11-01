using System;
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
        protected int life;


        public abstract void applyEffects(Enemy enemy);
        public abstract void undoEffect(Enemy enemy);

        public Boolean isValid(GameTime time)
        {
            life += time.ElapsedGameTime.Milliseconds;
            if (life < duration * 1000)
            {
                life = 0;
                return true;
            }
            else return false;
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

        public EnemyEffectBurn(Game1 game, int duration, int burnDmg)
        {
            this.duration = duration;
            this.burnDmg = burnDmg;
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


