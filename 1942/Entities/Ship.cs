﻿using Engine.Components;
using Engine.Core;
using Engine.Core.Managers;


namespace _1942.Entities
{
    public abstract class Ship : Entity, IDrawable, IDisposable
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

        public int Order { get; set; } = 1;

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

                if (bullet.IsOutsideWindow() || !bullet.Active)
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

        public virtual void Destroy()
        {
            Health = 0;
        }

        protected abstract void Shoot();

        // TODO review disposes
        public void Dispose()
        {
        }
    }
}
