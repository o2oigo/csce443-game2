using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;


namespace RTS
{
    public struct Damage
    {
        public float amount;
        public int level;
        public ElementType type;
        public EnemyEffect effect;

        public Damage(float amt, int lvl, ElementType t, EnemyEffect e)
        {
            amount = amt;
            level = lvl;
            type = t;
            effect = e;
        }
    }
}