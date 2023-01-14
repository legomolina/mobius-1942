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
    public class Bullet : Entity, IUpdatable
    {
        private readonly Vector2 direction;
        private readonly Sprite bulletSprite;

        public float Speed { get; set; } = 0.5f;
        public Ship[] Targets { get; set; }

        public Bullet(Texture texture, Point initialPosition, Vector2 direction) : base()
        {
            Position = new Point(initialPosition.X - texture.Width / 2, initialPosition.Y);

            bulletSprite = new(texture)
            {
                Position = Position,
            };

            this.direction = direction;
        }

        public bool IsOutsideWindow()
        {
            return Position.Y < 0;
        }

        public override void Render()
        {
            if (!Active)
            {
                return;
            }

            bulletSprite.Rotation = Rotation;
            bulletSprite.Render();
        }

        public override void Update(GameTime gameTime)
        {
            if (!Active)
            {
                return;
            }

            foreach(Ship ship in Targets)
            {
                if (Bounds.Intersects(ship.Bounds))
                {
                    ship.Destroy();
                    Active = false;
                }
            }

            Vector2 velocity = direction.Normalized() * Speed * gameTime.DeltaTime;
            Point endPosition = (bulletSprite.Position.ToVector() + velocity).ToPoint();
            bulletSprite.Position = endPosition;
            Position = endPosition;
        }
    }
}
