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
    public class LightningParticleSystem : ParticleSystem
    {
        float rotation;
        public LightningParticleSystem(Game1 game, int howManyEffects)
            : base(game, howManyEffects)
        {
        }

        /// <summary>
        /// Set up the constants that will give this particle system its behavior and
        /// properties.
        /// </summary>
        protected override void InitializeConstants()
        {
            textureFilename = "lightningParticle";

            // high initial speed with lots of variance.  make the values closer
            // together to have more consistently circular explosions.
            minInitialSpeed = 20;
            maxInitialSpeed = 30;

            // doesn't matter what these values are set to, acceleration is tweaked in
            // the override of InitializeParticle.
            minAcceleration = 0;
            maxAcceleration = 0;

            // explosions should be relatively short lived
            minLifetime = .2f;
            maxLifetime = .3f;

            minScale = 1f;
            maxScale = 1f;

            minNumParticles = 10;
            maxNumParticles = 15;

            minRotationSpeed = 0; //-MathHelper.PiOver4;
            maxRotationSpeed = 0;// MathHelper.PiOver4;

            // additive blending is very good at creating fiery effects.
			blendState = BlendState.Additive;

            DrawOrder = AdditiveDrawOrder;
        }

            protected override void InitializeParticle(Particle p, Vector2 where)
        {
            // first, call PickRandomDirection to figure out which way the particle
            // will be moving. velocity and acceleration's values will come from this.
            Vector2 direction = PickDirection();

            // pick some random values for our particle
            float velocity = 
                RandomBetween(minInitialSpeed, maxInitialSpeed);
            float acceleration =
                RandomBetween(minAcceleration, maxAcceleration);
            float lifetime =
                RandomBetween(minLifetime, maxLifetime);
            float scale =
                RandomBetween(minScale, maxScale);
            float rotationSpeed =
                RandomBetween(minRotationSpeed, maxRotationSpeed);

            // then initialize it with those random values. initialize will save those,
            // and make sure it is marked as active.

            p.Initialize(
                where, velocity * direction, acceleration * direction,
                lifetime, scale, rotationSpeed, rotation);

            p.Acceleration = -p.Velocity / p.Lifetime;
        }

            public void setRotation(float shootRotationAngle)
            {
                this.rotation = shootRotationAngle;
            }

            public void setScale(float scale)
            {
                minScale = scale;
                maxScale = scale;
            }
            
    }
}
