#region File Description
//-----------------------------------------------------------------------------
// ExplosionSmokeParticleSystem.cs
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
    /// ExplosionSmokeParticleSystem is a specialization of ParticleSystem which
    /// creates a circular pattern of smoke. It should be combined with
    /// ExplosionParticleSystem for best effect.
    /// </summary>
    public class FlameTowerSmokeParticleSystem : ParticleSystem
    {
        public FlameTowerSmokeParticleSystem(Game1 game, int howManyEffects, Camera camera)
            : base(game, howManyEffects, camera)
        {            
        }

        /// <summary>
        /// Set up the constants that will give this particle system its behavior and
        /// properties.
        /// </summary>
        protected override void InitializeConstants()
        {
            textureFilename = "smoke";

            // less initial speed than the explosion itself
            minInitialSpeed = 250;
            maxInitialSpeed = 350;

            // acceleration is negative, so particles will accelerate away from the
            // initial velocity.  this will make them slow down, as if from wind
            // resistance. we want the smoke to linger a bit and feel wispy, though,
            // so we don't stop them completely like we do ExplosionParticleSystem
            // particles.
            minAcceleration = 0;
            maxAcceleration = 0;

            // explosion smoke lasts for longer than the explosion itself, but not
            // as long as the plumes do.
            minLifetime = .35f;
            maxLifetime = .45f;

            minScale = .15f;
            maxScale = .2f;

            // we need to reduce the number of particles on Windows Phone in order to keep
            // a good framerate

            minNumParticles = 3;
            maxNumParticles = 4;


            minRotationSpeed =  -MathHelper.PiOver4;
            maxRotationSpeed =   MathHelper.PiOver4;

			blendState = BlendState.AlphaBlend;

            DrawOrder = AlphaBlendDrawOrder;
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
                float scale = p.Scale * 1;// (.75f + .25f * normalizedLifetime);

                game.getSpriteBatch().Draw(texture, p.Position, null, color,
                    p.Rotation, origin, scale, SpriteEffects.None, 0.0f);
            }

            game.getSpriteBatch().End();

           // base.Draw(gameTime);
        }
    }
}
