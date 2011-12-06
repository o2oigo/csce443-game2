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
        //float rotation;
        public LightningParticleSystem(Game1 game, int howManyEffects, Camera camera)
            : base(game, howManyEffects, camera)
        {
        }

        /// <summary>
        /// Set up the constants that will give this particle system its behavior and
        /// properties.
        /// </summary>
        protected override void InitializeConstants()
        {
            textureFilename = "lightningParticle";
           // textureFilename = "fireParticle";

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

            minNumParticles = 1;
            maxNumParticles = 2;

            /*minInitialSpeed = 0;
            maxInitialSpeed = 0;

            // doesn't matter what these values are set to, acceleration is tweaked in
            // the override of InitializeParticle.
            minAcceleration = 0;
            maxAcceleration = 0;

            // explosions should be relatively short lived
            minLifetime = 1f;
            maxLifetime = 1f;

            minScale = .1f;
            maxScale = .2f;

            minNumParticles = 1;
            maxNumParticles = 1;*/

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
            //Vector2 direction = PickRandomDirection();

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

            public override void Draw(GameTime gameTime)
            {
                // tell sprite batch to begin, using the spriteBlendMode specified in
                // initializeConstants
                game.getSpriteBatch().Begin(SpriteSortMode.Deferred, blendState, SamplerState.PointClamp, null, null, null, camera.ViewMatrix);

                foreach (Particle p in particles)
                {
                    // skip inactive particles
                    if (!p.Active)
                        continue;

                    // normalized lifetime is a value from 0 to 1 and represents how far
                    // a particle is through its life. 0 means it just started, .5 is half
                    // way through, and 1.0 means it's just about to be finished.
                    // this value will be used to calculate alpha and scale, to avoid 
                    // having particles suddenly appear or disappear.
                    float normalizedLifetime = p.TimeSinceStart / p.Lifetime;

                    // we want particles to fade in and fade out, so we'll calculate alpha
                    // to be (normalizedLifetime) * (1-normalizedLifetime). this way, when
                    // normalizedLifetime is 0 or 1, alpha is 0. the maximum value is at
                    // normalizedLifetime = .5, and is
                    // (normalizedLifetime) * (1-normalizedLifetime)
                    // (.5)                 * (1-.5)
                    // .25
                    // since we want the maximum alpha to be 1, not .25, we'll scale the 
                    // entire equation by 4.
                    float alpha = 4 * normalizedLifetime * (1 - normalizedLifetime);
                    Color color = Color.White * alpha;

                    // make particles grow as they age. they'll start at 75% of their size,
                    // and increase to 100% once they're finished.
                    float scale = p.Scale * (1f);

                    game.getSpriteBatch().Draw(texture, p.Position, null, color,
                        p.Rotation, origin, scale, SpriteEffects.None, 0.0f);
                }

                game.getSpriteBatch().End();

                //base.Draw(gameTime);
            }
            
    }
}
