using Engine.Components;
using Engine.Core;
using Engine.Core.Automation.Tracking;
using Engine.Core.Managers;
using Engine.Core.Math;

namespace _1942.Entities.Enemies
{
    public class Fighter : Enemy
    {
        private const string TextureFilename = "Assets/Textures/fighter.png";
        private const string BulletTextureFilename = "Assets/Textures/bullet.png";
        private const string BulletSoundFilename = "Assets/Effects/bullet_shot.wav";

        public Track Track { get; private set; }

        public Fighter(GraphicsManager graphics, Player player) : base(graphics, player)
        {
            Health = 50;
            Speed = 0.2f;
        }

        public override void Initialize()
        {
            base.Initialize();

            CreateTrack();

            Position = Track.Waypoints[0].Position;
        }

        public override void LoadContent(AssetManager assetManager)
        {
            bulletTexture = assetManager.LoadTexture(BulletTextureFilename);
            shipTexture = assetManager.LoadTexture(TextureFilename);
            shootSound = assetManager.LoadSoundEffect(BulletSoundFilename);
            shootSound.SetVolume(15);
            shipSprite = new AnimatedSprite(shipTexture, 4, AnimationDirections.HORIZONTAL)
            {
                AnimationFPS = ANIMATION_FPS
            };

            Width = ((AnimatedSprite)shipSprite).FrameWidth;
            Height = ((AnimatedSprite)shipSprite).FrameHeight;
        }

        public override void Update(GameTime gameTime)
        {
            if (!Active)
            {
                return;
            }

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
            if (!Active)
            {
                return;
            }

            base.Render();

#if DEBUG
            DebugManager.DrawTrack(Track);
#endif
        }

        protected override void Shoot()
        {
            Vector2 direction = Vector2.FromPoints(Center, player.Center);
            float rotation = new Vector2(0, 1).AngleBetween(direction / direction.Magnitude()) * -1;

            Bullet bullet = new(bulletTexture, Position, direction)
            {
                Rotation = rotation,
                Speed = 0.1f,
                Targets = new Ship[] { player },
            };

            shootSound!.Play(1, 0);
            bullets.Add(bullet);
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

        private int CalculateInitX()
        {
            Random random = new();
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
