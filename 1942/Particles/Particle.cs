using Engine.Components;
using Engine.Core;
using Engine.Core.Managers;
using Engine.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942.Particles
{
    public class Particle : GameComponent
    {
        private readonly BatchRenderer renderer;

        private Sprite sprite;

        public Particle(BatchRenderer renderer, Sprite sprite)
        {
            this.renderer = renderer;
            this.sprite = sprite;
        }

        public override void LoadContent(AssetManager assetManager) { }

        public override void Render()
        {
            if (!Active)
            {
                return;
            }

            renderer.Render(sprite);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Active)
            {
                return;
            }

            sprite.Position = Position;
            sprite.Rotation = Rotation;
            sprite.Scale = Scale;

            sprite.Update(gameTime);
        }
    }
}
