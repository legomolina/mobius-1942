using _1942.Particles;
using Engine.Components;
using Engine.Core;
using Engine.Core.Collisions;
using Engine.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942.Entities.Ships
{
    public abstract class Ship : PhysicsEntity
    {
        protected const int ANIMATION_FPS = 24;

        protected readonly CollisionsContainer collisionsContainer;
        protected readonly GraphicsManager graphics;
        protected readonly BatchRenderer renderer;
        protected readonly ParticleFactory particleFactory;

        protected Sprite sprite;
        protected Texture texture;
        protected Explosion explosion;

        protected bool IsDestroyed { get; set; }
        protected float Speed { get; set; }

        public Ship(GraphicsManager graphics, BatchRenderer renderer, CollisionsContainer collisionsContainer) : base(renderer)
        {
            this.collisionsContainer = collisionsContainer;
            this.graphics = graphics;
            this.renderer = renderer;
            this.particleFactory = new ParticleFactory(renderer);
            explosion = new Explosion(renderer);

            IsDestroyed = false;
            Order = 5;
        }

        public override void LoadContent(AssetManager assetManager)
        {
            explosion.LoadContent(assetManager);
        }

        public override void Render()
        {
            if (!Active)
            {
                return;
            }

            base.Render();

            if (Health > 0)
            {
                renderer.Render(sprite);
            }
            else
            {
                explosion.Render();
            }

            explosion.Render();
        }

        public override void Update(GameTime gameTime)
        {
            if (!Active)
            {
                return;
            }

            sprite.Position = Position;
            sprite.Rotation = Rotation;
            explosion.Position = Center;

            sprite.Update(gameTime);
            explosion.Update(gameTime);

            if (explosion.IsFinished)
            {
                Active = false;
            }
        }

        protected abstract void Shoot();

        protected void Destroy()
        {
            if (IsDestroyed)
            {
                return;
            }

            Health = 0;
            IsDestroyed = true;
            explosion.Run();
        }
    }
}
