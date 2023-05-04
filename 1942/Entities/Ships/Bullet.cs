using Engine.Components;
using Engine.Core;
using Engine.Core.Collisions;
using Engine.Core.Managers;
using Engine.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace _1942.Entities.Ships
{
    public class Bullet : PhysicsEntity
    {
        private readonly GraphicsManager graphics;
        private readonly Texture texture;
        private readonly Sprite sprite;

        private Vector2 direction;

        public Ship Shooter { get; private set; }
        public int Damage { get; private set; }
        public float Speed { get; set; }

        public Bullet(GraphicsManager graphics, BatchRenderer renderer, Point initialPosition, Vector2 direction, Texture texture, Ship shooter) : base(renderer)
        {
            this.graphics = graphics;
            this.direction = direction;
            this.texture = texture;

            Damage = 30;
            Width = 16;
            Height = 16;
            Health = 1;
            Order = 10;
            Tag = "Bullet";
            Shooter = shooter;
            Position = new Point(initialPosition.X - texture.Width / 2, initialPosition.Y);

            sprite = new Sprite(texture)
            {
                Position = Position,
                Rotation = Rotation,
            };
        }

        public override void LoadContent(AssetManager assetManager)
        {
            // No need to load anything. Parent loads to avoid multiple loads of same asset
        }

        public override void OnCollision(ICollidable other)
        {
            if (other == Shooter)
            {
                return;
            }

            Destroy();
        }

        public override void Render()
        {
            if (!Active)
            {
                return;
            }

            base.Render();

            renderer.Render(sprite);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Active)
            {
                return;
            }

            if (!graphics.WindowBounds.Contains(Position))
            {
                Destroy();
            }

            Vector2 velocity = direction.Normalized() * Speed * gameTime.DeltaTime;
            Position += velocity.ToPoint();

            sprite.Position = Position;
            sprite.Rotation = Rotation;
            sprite.Update(gameTime);
        }

        private void Destroy()
        {
            Health = 0;
            Active = false;
        }
    }
}
