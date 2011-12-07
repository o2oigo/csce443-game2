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
    public class IceParticleSystem : ParticleSystem
    {
        String textureName;
        // Game1 game;
        PlayerIndex playerIndex;
        public IceParticleSystem(Game1 game, int howManyEffects, Camera camera)
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

            textureFilename = "iceParticle";

            minInitialSpeed = 20;
            maxInitialSpeed = 25;

            minAcceleration = 0;
            maxAcceleration = 0;

            minLifetime = 5.3f;
            maxLifetime = 6.3f;

            minScale = .3f;
            maxScale = .35f;

            minNumParticles = 1;
            maxNumParticles = 2;

            minRotationSpeed =  -MathHelper.PiOver4;
            maxRotationSpeed =   MathHelper.PiOver4;

            // additive blending is very good at creating fiery effects.
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
