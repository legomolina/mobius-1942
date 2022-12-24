using Engine.Components;
using Engine.Core;
using Engine.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942.Entities.Enemies
{
    internal class Fighter : Enemy
    {
        private const string TextureFilename = "Assets/Textures/fighter.png";
        private const string BulletTextureFilename = "Assets/Textures/bullet.png";

        private readonly Point startPosition;

        private Point? destination = null;
        private bool isReturning = false;
        private bool isShoot = false;

        internal Fighter(GraphicsManager graphics, Player player) : base(graphics, player)
        {
            startPosition = new Point(CalculateInitX(), -50);
            Health = 50;
            Speed = 0.25f;
        }

        public override void LoadContent(AssetManager assetManager)
        {
            bulletTexture = assetManager.LoadTexture(BulletTextureFilename);
            shipTexture = assetManager.LoadTexture(TextureFilename);
            shipSprite = new AnimatedSprite(shipTexture, 4, AnimationDirections.HORIZONTAL)
            {
                AnimationFPS = ANIMATION_FPS
            };

            Width = ((AnimatedSprite)shipSprite).FrameWidth;
            Height = ((AnimatedSprite)shipSprite).FrameHeight;

            Position = startPosition;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Set initial destination
            if (!destination.HasValue)
            {
                Random random = new Random();
                int minPlayerPositionX = (int)player.Position.X - 50;
                int maxPlayerPositionX = (int)player.Position.X + 50;
                int minPlayerPositionY = (int)player.Position.Y - 200;
                int maxPlayerPositionY = (int)player.Position.Y - 50;
                destination = new(random.Next(minPlayerPositionX, maxPlayerPositionX), random.Next(minPlayerPositionY, maxPlayerPositionY));
            }

            Vector2 moveDirection = Vector2.FromPoints(Position, destination.Value);
            float distanceToDestination = (Position.ToVector() - destination.Value.ToVector()).Magnitude();

            Position = (moveDirection / moveDirection.Magnitude() * Speed * gameTime.DeltaTime + Position.ToVector()).ToPoint();
            Rotation = new Vector2(0, 1).AngleBetween(moveDirection / moveDirection.Magnitude()) * -1;

            if (distanceToDestination < 50 && !isShoot)
            {
                isShoot = true;
                Shoot();
            }

            if (distanceToDestination < 10)
            {
                isReturning = true;
                destination = new Point(graphics.WindowWidth - startPosition.X, startPosition.Y);
            }

            if (Position.Y < 0 && isReturning)
            {
                Health = 0;
            }
        }

        public override void Render()
        {
            base.Render();
        }

        protected override void Shoot()
        {
            Vector2 direction = Vector2.FromPoints(Center, player.Center);
            float rotation = new Vector2(0, 1).AngleBetween(direction / direction.Magnitude()) * -1;

            Bullet bullet = new Bullet(bulletTexture, Position, direction)
            {
                Rotation = rotation,
                Speed = 0.2f,
            };

            bullets.Add(bullet);
        }

        private int CalculateInitX()
        {
            Random random = new Random();
            int side = random.Next(0, 1);
            int min, max;

            if (side == 0)
            {
                min = 100;
                max = graphics.WindowWidth / 2 - 100;
            }
            else
            {
                min = graphics.WindowWidth / 2 + 100;
                max = graphics.WindowWidth - 100;
            }

            return random.Next(min, max);
        }
    }
}
