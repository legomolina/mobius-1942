using Engine.Components;
using Engine.Core;
using Engine.Core.Automation.Tracking;
using Engine.Core.Debug;
using Engine.Core.Math;

namespace _1942.Entities.Enemies
{
    public class Fighter : Enemy
    {
        private const string TextureFilename = "Assets/Textures/fighter.png";
        private const string BulletTextureFilename = "Assets/Textures/bullet.png";

        public Track Track { get; set; }

        public Fighter(GraphicsManager graphics, Player player) : base(graphics, player)
        {
            CreateTrack();
            Health = 50;
            Speed = 0.25f;
        }

        private void CreateTrack()
        {
            Point startPosition = new(CalculateInitX(), -50);
            Point endPosition = new(graphics.WindowWidth - startPosition.X, startPosition.Y);
            Point playerPosition = player.Area.GetRandomPoint(AreaQuadrants.TopLeft | AreaQuadrants.TopRight);

            Track = new TrackFactory()
                .Begin(startPosition)
                .AddWaypoint(playerPosition)
                .ShootPlayer()
                .AddWaypoint(endPosition)
                .Build();
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

            Position = Track.CurrentWaypoint.Position + new Point(-Width / 2, -Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Track.IsLastWaypoint())
            {
                Health = 0;
                return;
            }

            Vector2 moveDirection = Vector2.FromPoints(Center, Track.NextWaypoint.Position);
            float distanceToDestination = (Center.ToVector() - Track.NextWaypoint.Position.ToVector()).Magnitude();

            if (distanceToDestination < 20)
            {
                if (Track.NextWaypoint.Shoot)
                {
                    Shoot();
                }

                Track.MoveNextWaypoint();
            }

            Position = (moveDirection / moveDirection.Magnitude() * Speed * gameTime.DeltaTime + Position.ToVector()).ToPoint();
            Rotation = new Vector2(0, 1).AngleBetween(moveDirection / moveDirection.Magnitude()) * -1;
        }

        public override void Render()
        {
            base.Render();

            DebugManager.RenderTrack(Track);
        }

        protected override void Shoot()
        {
            Vector2 direction = Vector2.FromPoints(Center, player.Center);
            float rotation = new Vector2(0, 1).AngleBetween(direction / direction.Magnitude()) * -1;

            Bullet bullet = new Bullet(bulletTexture, Position, direction)
            {
                Rotation = rotation,
                Speed = 0.05f,
            };

            bullets.Add(bullet);
        }

        private int CalculateInitX()
        {
            Random random = new Random();
            int side = random.Next(0, 2);
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
