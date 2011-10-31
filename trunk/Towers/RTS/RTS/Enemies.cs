using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RTS
{
    class HPEnemy : Enemy
    {
        public override void Initialize(Game1 game, Vector2 startPosition, Map map)
        {
            base.Initialize(game, startPosition, map);

            range = 200;
            hp = 300;
            weakAgainst = ElementType.Fire;
            strongAgainst = ElementType.Water;

        }

        public virtual void LoadContent()
        {
            base.LoadContent("TankEnemy");
        }
    }

    class FastEnemy : Enemy
    {
        public override void Initialize(Game1 game, Vector2 startPosition, Map map)
        {
            base.Initialize(game, startPosition, map);

            range = 200;
            hp = 100;
            weakAgainst = ElementType.Water;
            strongAgainst = ElementType.Fire;
            moveSpeed = 200.0f;
        }

        public virtual void LoadContent()
        {
            base.LoadContent("TankPurple");
        }
    }


}
