using static SDL2.SDL;
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
    internal class Enemy : Ship
    {
        private const string BULLET_TEXTURE_FILENAME = "Assets/Textures/bullet.png";
        private const string BULLET_SOUND_FILENAME = "Assets/Effects/bullet_shot.wav";
        private const string ENEMY_TEXTURE_FILENAME = "Assets/Textures/enemy1.png";
        private const int SHOT_WAIT_TIME = 500; // milliseconds

        private readonly Player player;

        private uint lastTimeStartShot = SDL_GetTicks();
        private uint elapsedTimeFromLastShot = 0;

        public Enemy(GraphicsManager graphics, Player player) : base(graphics)
        {
            this.player = player;
            Rotation = 180;
        }

        public override void LoadContent(AssetManager assetManager)
        {
            bulletTexture = assetManager.LoadTexture(BULLET_TEXTURE_FILENAME);
            shipTexture = assetManager.LoadTexture(ENEMY_TEXTURE_FILENAME);
            shootSound = assetManager.LoadSoundEffect(BULLET_SOUND_FILENAME);
            shootSound.SetVolume(15);

            shipSprite = new AnimatedSprite(shipTexture, 4, AnimationDirections.HORIZONTAL)
            {
                AnimationFPS = ANIMATION_FPS,
            };

            Width = ((AnimatedSprite)shipSprite).FrameWidth;
            Height = ((AnimatedSprite)shipSprite).FrameHeight;
        }

        protected override void Shoot()
        {
            if (player.Center.Y > Position.Y)
            {
                Vector2 direction = Vector2.FromPoints(Center, player.Center);
                float rotation = (float)(Math.Atan2(player.Center.X - Position.X, player.Center.Y - Position.Y) * (180 / Math.PI));

                Bullet bullet = new Bullet(bulletTexture, Center, direction)
                {
                    Rotation = -rotation,
                    Speed = 0.5f
                };

                shootSound!.Play(1, 1);
                bullets.Add(bullet);
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            elapsedTimeFromLastShot = SDL_GetTicks() - lastTimeStartShot;

            if (elapsedTimeFromLastShot >= SHOT_WAIT_TIME)
            {
                Shoot();
                lastTimeStartShot = SDL_GetTicks();
            }
        }
    }
}
