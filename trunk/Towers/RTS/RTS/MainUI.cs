using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace RTS
{
    class MainUI
    {
        private int totalWave;

        private int currentWave = 1;
        public int Wave
        {
            get { return currentWave; }
        }

        private int health = 20;
        public int Health
        {
            get { return health; }
        }

        private bool defeat = false;
        public bool isDefeat
        {
            get { return defeat; }
            set { defeat = value; }
        }

        public MainUI(int wave)
        {

        }





    }
}
