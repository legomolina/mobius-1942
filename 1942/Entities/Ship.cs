using Engine.Components;
using Engine.Core;
using Engine.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942.Entities
{
    internal abstract class Ship : Entity, IUpdatable, IDrawable, IDisposable
    {
        protected const int ANIMATION_FPS = 24;

        protected readonly GraphicsManager graphics;
        protected readonly IList<Bullet> bullets;

        protected bool isShooting = false;
        protected Texture? bulletTexture;
        protected Texture? shipTexture;
        protected Sprite? shipSprite;
        protected SoundEffect? shootSound;
        
        protected float Speed { get; set; }

        protected Ship(GraphicsManager graphics) : base()
        {
            this.graphics = graphics;
            bullets = new List<Bullet>();
        }

        public abstract void LoadContent(AssetManager assetManager);

        public override void Update(GameTime gameTime)
        {
            if (!Active)
            {
                return;
            }

            shipSprite!.Update(gameTime);

            foreach (Bullet bullet in bullets.ToList())
            {
                bullet.Update(gameTime);

                if (bullet.IsOutsideWindow())
                {
                    bullets.Remove(bullet);
                }
            }
        }

        public override void Render()
        {
            if (!Active)
            {
                return;
            }

            shipSprite!.Position = Position;
            shipSprite.Rotation = Rotation;
            shipSprite.Render();

            foreach (Bullet bullet in bullets.ToList())
            {
                bullet.Render();
            }
        }

        protected abstract void Shoot();

        public void Dispose()
        {
            bulletTexture?.Dispose();
            shipTexture?.Dispose();
            shipSprite?.Dispose();
        }
    }
}
