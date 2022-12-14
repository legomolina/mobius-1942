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
    internal class Bullet : Entity, IUpdatable
    {
        private readonly Vector2 direction;
        private readonly Sprite bulletSprite;

        internal float Speed { get; set; } = 0.5f;

        internal Bullet(Texture texture, Point initialPosition, Vector2 direction) : base()
        {
            Position = new Point(initialPosition.X - texture.Width / 2, initialPosition.Y);

            bulletSprite = new(texture)
            {
                Position = Position,
            };

            this.direction = direction;
        }

        internal bool IsOutsideWindow()
        {
            return Position.Y < 0;
        }

        public override void Render()
        {
            bulletSprite.Rotation = Rotation;
            bulletSprite.Render();
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 velocity = direction.Normalized() * Speed * gameTime.DeltaTime;
            Point endPosition = (bulletSprite.Position.ToVector() + velocity).ToPoint();
            bulletSprite.Position = endPosition;
            Position = endPosition;
        }
    }
}
