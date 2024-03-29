﻿using Engine.Components;
using Engine.Core;
using Engine.Core.Collisions;
using Engine.Core.Input;
using Engine.Core.Managers;
using Engine.Core.Math;
using System;
using System.Runtime.CompilerServices;

namespace _1942.Entities.Ships
{
    public class Player : Ship
    {
        private const string TEXTURE_FILENAME = "Assets/Textures/player.png";
        private const string BULLET_TEXTURE_FILENAME = "Assets/Textures/bullet.png";
        private const string BULLET_SOUND_FILENAME = "Assets/Effects/bullet_shot.wav";

        private readonly IList<Bullet> bullets;

        private Texture bulletTexture;
        private SoundEffect shootSound;
        private bool isShooting = false;

        public Area Area => new Area(this, 50);

        public Player(GraphicsManager graphics, BatchRenderer renderer, CollisionsContainer collisionsContainer) : base(graphics, renderer, collisionsContainer)
        {
            Width = 32;
            Height = 32;
            Speed = .25f;
            Health = 50;
            Tag = "Player";

            bullets = new List<Bullet>();
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

            if (other is Ship ship && ship.Active && !IsDestroyed)
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
            base.Update(gameTime);

            if (Active)
            {
                HandleInput(gameTime);
            }

            foreach(Bullet bullet in bullets.ToList())
            {
                bullet.Update(gameTime);

                if (!bullet.Active)
                {
                    bullets.Remove(bullet);
                    collisionsContainer.Remove(bullet);
                }
            }

            if (Health <= 0 && !IsDestroyed)
            {
                Destroy();
            }
        }

        protected override void Shoot()
        {
            Point startPosition = new Point(Position.X + Width / 2 - bulletTexture.Width / 2, Position.Y - 5);
            Bullet bullet = new Bullet(graphics, renderer, startPosition, new Vector2(0, -1), bulletTexture, this) 
            { 
                Speed = .5f,
            };
            
            bullets.Add(bullet);
            collisionsContainer.Insert(bullet);

            shootSound.Play(1, 0);
        }

        private void HandleInput(GameTime gameTime)
        {
            // Handle movement
            Vector2 direction = GetMovementDirection();

            // Handle shooting
            HandleShooting();

            if (direction != Vector2.Zero)
            {
                MoveTo(direction * Speed * gameTime.DeltaTime);
            }
        }

        private Vector2 GetMovementDirection()
        {
            Vector2 direction = Vector2.Zero;
            InputManager input = InputManager.Instance;

            if (input.GameControllers[0] != null)
            {
                GameControllerStick leftStick = input.GameControllers[0]!.GetAxisPosition(GameControllerAxis.LeftStick);
                float xPosition = LinearInterpolation.Range(-GameController.AXIS_MAX_VALUE, GameController.AXIS_MAX_VALUE, -1, 1, leftStick.X);
                float yPosition = LinearInterpolation.Range(-GameController.AXIS_MAX_VALUE, GameController.AXIS_MAX_VALUE, -1, 1, leftStick.Y);

                if (xPosition != 0 || yPosition != 0)
                {
                    return new Vector2(xPosition, yPosition);
                }
            }

            if (input.Keyboard.IsKeyPressed(KeyboardKeys.W) || input.Keyboard.IsKeyPressed(KeyboardKeys.Up) || (input.GameControllers[0]?.IsButtonPressed(GameControllerButtons.DPadUp) ?? false))
            {
                direction += new Vector2(0, -1);
            }

            if (input.Keyboard.IsKeyPressed(KeyboardKeys.S) || input.Keyboard.IsKeyPressed(KeyboardKeys.Down) || (input.GameControllers[0]?.IsButtonPressed(GameControllerButtons.DPadDown) ?? false))
            {
                direction += new Vector2(0, 1);
            }

            if (input.Keyboard.IsKeyPressed(KeyboardKeys.A) || input.Keyboard.IsKeyPressed(KeyboardKeys.Left) || (input.GameControllers[0]?.IsButtonPressed(GameControllerButtons.DPadLeft) ?? false))
            {
                direction += new Vector2(-1, 0);
            }

            if (input.Keyboard.IsKeyPressed(KeyboardKeys.D) || input.Keyboard.IsKeyPressed(KeyboardKeys.Right) || (input.GameControllers[0]?.IsButtonPressed(GameControllerButtons.DPadRight) ?? false))
            {
                direction += new Vector2(1, 0);
            }

            return direction.Normalized();
        }

        private void HandleShooting()
        {
            InputManager input = InputManager.Instance;

            if (input.GameControllers[0] != null)
            {
                if (!isShooting && input.GameControllers[0]!.IsButtonPressed(GameControllerButtons.A))
                {
                    Shoot();
                    isShooting = true;
                }
                else if (isShooting && input.GameControllers[0]!.IsButtonReleased(GameControllerButtons.A))
                {
                    isShooting = false;
                }
            }

            if (!isShooting && (input.Keyboard.IsKeyPressed(KeyboardKeys.Space)))
            {
                Shoot();
                isShooting = true;
            }
            else if (isShooting && input.Keyboard.IsKeyReleased(KeyboardKeys.Space))
            {
                isShooting = false;
            }
        }

        private void MoveTo(Vector2 velocity)
        {
            Vector2 clampedPosition = Position.ToVector() + velocity;
            clampedPosition.X = Math.Clamp(clampedPosition.X, 0, graphics.WindowWidth - Width);
            clampedPosition.Y = Math.Clamp(clampedPosition.Y, 0, graphics.WindowHeight - Height);

            Position = clampedPosition.ToPoint();
        }
    }
}
