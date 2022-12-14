using Engine.Core;
using Engine.Core.Math;
using Engine.Components;

namespace _1942.Entities
{
    internal class Player : Ship
    {
        private const string BULLET_TEXTURE_FILENAME = "Assets/Textures/bullet.png";
        private const string BULLET_SOUND_FILENAME = "Assets/Effects/bullet_shot.wav";
        private const string PLAYER_TEXTURE_FILENAME = "Assets/Textures/player.png";

        public Player(GraphicsManager graphics) : base(graphics)
        {
            Speed = 0.25f;
        }

        public override void LoadContent(AssetManager assetManager)
        {
            bulletTexture = assetManager.LoadTexture(BULLET_TEXTURE_FILENAME);
            shipTexture = assetManager.LoadTexture(PLAYER_TEXTURE_FILENAME);
            shootSound = assetManager.LoadSoundEffect(BULLET_SOUND_FILENAME);
            shootSound.SetVolume(15);

            shipSprite = new AnimatedSprite(shipTexture, 4, AnimationDirections.HORIZONTAL)
            {
                AnimationFPS = ANIMATION_FPS,
            };

            Width = ((AnimatedSprite)shipSprite).FrameWidth;
            Height = ((AnimatedSprite)shipSprite).FrameHeight;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector2 direction = Vector2.Zero;
            InputManager input = InputManager.Instance;

            if (input.IsKeyPressed(Keyboard.W) || input.IsKeyPressed(Keyboard.UP))
            {
                direction += new Vector2(0, -1);
            }

            if (input.IsKeyPressed(Keyboard.S) || input.IsKeyPressed(Keyboard.DOWN))
            {
                direction += new Vector2(0, 1);
            }

            if (input.IsKeyPressed(Keyboard.A) || input.IsKeyPressed(Keyboard.LEFT))
            {
                direction += new Vector2(-1, 0);
            }

            if (input.IsKeyPressed(Keyboard.D) || input.IsKeyPressed(Keyboard.RIGHT))
            {
                direction += new Vector2(1, 0);
            }

            if (!isShooting && input.IsKeyPressed(Keyboard.SPACE))
            {
                Shoot();
                isShooting = true;
            }
            else if (isShooting && input.IsKeyReleased(Keyboard.SPACE))
            {
                isShooting = false;
            }

            if (direction != Vector2.Zero)
            {
                MoveTo(direction.Normalized() * Speed * gameTime.DeltaTime);
            }
        }

        private void MoveTo(Vector2 velocity)
        {
            Vector2 clampedPosition = Position.ToVector() + velocity;
            clampedPosition.X = Math.Clamp(clampedPosition.X, 0, graphics.WindowWidth - Width);
            clampedPosition.Y = Math.Clamp(clampedPosition.Y, 0, graphics.WindowHeight - Height);

            Position = clampedPosition.ToPoint();
        }

        protected override void Shoot()
        {
            Point startPosition = new(Position.X + Width / 2 - bulletTexture!.Width / 2, Position.Y - 5);
            Bullet bullet = new(bulletTexture!, startPosition, new Vector2(0, -1));

            shootSound!.Play(1, 0);
            bullets.Add(bullet);
        }
    }
}
