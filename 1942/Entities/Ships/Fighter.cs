using Engine.Components;
using Engine.Core;
using Engine.Core.Automation.Tracking;
using Engine.Core.Collisions;
using Engine.Core.Managers;
using Engine.Core.Math;

namespace _1942.Entities.Ships
{
    public class Fighter : Enemy
    {
        private const string TEXTURE_FILENAME = "Assets/Textures/fighter.png";
        private const string BULLET_TEXTURE_FILENAME = "Assets/Textures/bullet.png";
        private const string BULLET_SOUND_FILENAME = "Assets/Effects/bullet_shot.wav";

        private Texture bulletTexture;
        private SoundEffect shootSound;
        private IList<Bullet> bullets;

        public Track Track { get; private set; }

        public Fighter(GraphicsManager graphics, BatchRenderer renderer, Player player, CollisionsContainer collisionsContainer) : base(graphics, renderer, player, collisionsContainer)
        {
            bullets = new List<Bullet>();

            Width = 32;
            Height = 32;
            Health = 50;
            Speed = .2f;
            Tag = "Fighter";
        }

        public override void Initialize()
        {
            base.Initialize();

            CreateTrack();

            Position = Track.Waypoints[0].Position;
        }

        public override void LoadContent(AssetManager assetManager)
        {
            base.LoadContent(assetManager);

            bulletTexture = assetManager.LoadTexture(BULLET_TEXTURE_FILENAME);
            shootSound = assetManager.LoadSoundEffect(BULLET_SOUND_FILENAME);
            shootSound.SetVolume(15);

            texture = assetManager.LoadTexture(TEXTURE_FILENAME);
            sprite = new AnimatedSprite(texture, 4, AnimationDirections.Horizontal)
            {
                AnimationFPS = ANIMATION_FPS,
                Position = Position,
                Rotation = Rotation,
            };
        }

        public override void OnCollision(ICollidable other)
        {
            if (other is Bullet bullet)
            {
                if (bullet.Shooter != this)
                {
                    Health -= bullet.Damage;
                }
            }

            if (other is Player)
            {
                Destroy();
            }
        }

        public override void Render()
        {
            base.Render();

            foreach (Bullet bullet in bullets)
            {
                bullet.Render();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!Active)
            {
                return;
            }

            foreach (Bullet bullet in bullets.ToList())
            {
                bullet.Update(gameTime);

                if (!bullet.Active)
                {
                    bullets.Remove(bullet);
                    collisionsContainer.Remove(bullet);
                }
            }

            if (Track.IsLastWaypoint())
            {
                Active = false;
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

            Vector2 velocity = moveDirection / moveDirection.Magnitude() * Speed * gameTime.DeltaTime;

            if (Health > 0)
            {
                Position += velocity.ToPoint();
                Rotation = new Vector2(0, 1).AngleBetween(moveDirection / moveDirection.Magnitude()) * -1;
            } else
            {
                Destroy();
            }

            base.Update(gameTime);
        }

        protected override void Shoot()
        {
            Vector2 direction = Vector2.FromPoints(Center, player.Center);
            float rotation = new Vector2(0, 1).AngleBetween(direction / direction.Magnitude()) * -1;

            Bullet bullet = new Bullet(graphics, renderer, Position, direction, bulletTexture, this)
            {
                Rotation = rotation,
                Speed = .1f,
            };
            shootSound.Play(1, 0);
            bullets.Add(bullet);
            collisionsContainer.Insert(bullet);
        }

        private void CreateTrack()
        {
            Point startPosition = new Point(CalculateInitX(), -50);
            Point endPosition = new Point(graphics.WindowWidth - startPosition.X, startPosition.Y);
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
