#region File Description
//-----------------------------------------------------------------------------
// ExplosionParticleSystem.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace RTS
{
    /// <summary>
    /// ExplosionParticleSystem is a specialization of ParticleSystem which creates a
    /// fiery explosion. It should be combined with ExplosionSmokeParticleSystem for
    /// best effect.
    /// </summary>
    public class FireParticleSystem : ParticleSystem
    {
        String textureName;
        // Game1 game;
        PlayerIndex playerIndex;
        public FireParticleSystem(Game1 game, int howManyEffects, Camera camera)
            : base(game, howManyEffects, camera)
        {
            // this.game = game;
            //this.textureName = textureName;
            this.direction = new Vector2(0, 0);
        }

        /// <summary>
        /// Set up the constants that will give this particle system its behavior and
        /// properties.
        /// </summary>
        protected override void InitializeConstants()
        {

            textureFilename = "fireParticle";

            minInitialSpeed = 200;
            maxInitialSpeed = 300;

            minAcceleration = 0;
            maxAcceleration = 0;

            minLifetime = .3f;
            maxLifetime = .4f;

            minScale = .15f;
            maxScale = .25f;

            minNumParticles = 3;
            maxNumParticles = 4;

            minRotationSpeed =  -MathHelper.PiOver4;
            maxRotationSpeed =   MathHelper.PiOver4;

            blendState = BlendState.Additive;

            DrawOrder = AdditiveDrawOrder;
        }

        //Sets the direction that the particles move
        public override void setDirection(float angle)
        {
            this.direction = new Vector2((float)Math.Cos(angle) + RandomBetween(-.1f, .1f), (float)Math.Sin(angle) + RandomBetween(-.1f, .1f));
        }
    }
}
