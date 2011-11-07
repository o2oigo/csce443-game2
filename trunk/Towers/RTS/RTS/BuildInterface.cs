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
    class BuildInterface
    {
        private Texture2D mouseTexture;
        private Texture2D menu1Texture;
        private Texture2D menu2Texture;
        private Texture2D menu3Texture;
        private Texture2D menu4Texture;
        private Texture2D buildTexture;
        private Texture2D cancelTexture;
        private Texture2D upgradeTexture;
        private Texture2D sellTexture;
        private Texture2D enhanceTexture;
        private Texture2D fireTowerBuildTexture;
        private Texture2D lightningTowerBuildTexture;

        private bool buildMode = false;
        private bool mainBuildMode = false;
        private bool upgradeBuildMode = false;

        private int fireStoneInInventory = 1;
        private int waterStoneInInventory = 1;
        private int healStoneInInventory = 1;

        private int money = 35;

        Game1 game;
        ContentManager contentManager;

        public void Initialize(Game1 game, PlayerIndex index, Vector2 startPosition)
        {
            this.game = game;
            contentManager = game.Content;
        }

        public void LoadContent()
        {
            mouseTexture = contentManager.Load<Texture2D>("CrossHair1");
            menu1Texture = contentManager.Load<Texture2D>("buildTowerMenu");
            menu2Texture = contentManager.Load<Texture2D>("cancelMenu");
            menu3Texture = contentManager.Load<Texture2D>("buildTowerMenuSelect");
            menu4Texture = contentManager.Load<Texture2D>("cancelMenuSelect");
            buildTexture = contentManager.Load<Texture2D>("buildSmall");
            cancelTexture = contentManager.Load<Texture2D>("cancelSmall");
            upgradeTexture = contentManager.Load<Texture2D>("upgradeSmall");
            enhanceTexture = contentManager.Load<Texture2D>("enhanceSmall");
            sellTexture = contentManager.Load<Texture2D>("sellSmall");
            lightningTowerBuildTexture = contentManager.Load<Texture2D>("lightningTowerSmall");
            fireTowerBuildTexture = contentManager.Load<Texture2D>("fireTowerSmall");
        }
    }
}
