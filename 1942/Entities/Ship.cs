using _1942.Core;
using _1942.Entities.Effects;
using Engine.Components;
using Engine.Core;
using Engine.Core.Managers;
using Engine.Core.Math;

namespace _1942.Entities
{
    public abstract class Ship : Entity, IDrawable, IDisposable
    {
        protected const int ANIMATION_FPS = 24;

        protected readonly Explosion destroyExplosion;
        protected readonly GraphicsManager graphics;
        protected readonly IList<Bullet> bullets;

        protected bool isDestroyed = false;
        protected bool isShooting = false;
        protected Stage stage;
        protected Texture? bulletTexture;
        protected Texture? shipTexture;
        protected Sprite? shipSprite;
        protected SoundEffect? shootSound;
        
        protected float Speed { get; set; }

        protected Ship(GraphicsManager graphics, Stage stage) : base()
        {
            this.destroyExplosion = new Explosion();
            this.graphics = graphics;
            this.stage = stage;
            bullets = new List<Bullet>();

            Order = 1;
        }

        public virtual void LoadContent(AssetManager assetManager)
        {
            destroyExplosion.LoadContent(assetManager);
            stage.Components.Add(destroyExplosion);
        }

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
            if (!Active || Health == 0)
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
            destroyExplosion.Position = Position;
            destroyExplosion.Scale = 0.25f;
            destroyExplosion.Run();
        }

        protected abstract void Shoot();

        // TODO review disposes
        public void Dispose()
        {
        }
    }
}
