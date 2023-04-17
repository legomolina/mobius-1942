using Engine.Components;
using Engine.Core;
using Engine.Core.Managers;
using Engine.Core.Math;
using Engine.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _1942.Particles
{
    public class Explosion : GameComponent
    {
        private const string TEXTURE_FILENAME = "Assets/Particles/explosion.png";
        private const string SOUND_FILENAME = "Assets/Effects/explosion.wav";

        private readonly BatchRenderer renderer;
        private readonly ParticleFactory particleFactory;

        private Particle particle;
        private Texture texture;
        private SoundEffect sound;
        private AnimatedSprite sprite;

        public bool IsFinished { get; private set; }

        public Explosion(BatchRenderer renderer)
        {
            this.renderer = renderer;
            particleFactory = new ParticleFactory(renderer);
            Active = false;
            IsFinished = false;
        }

        public override void LoadContent(AssetManager assetManager)
        {
            texture = assetManager.LoadTexture(TEXTURE_FILENAME);
            sound = assetManager.LoadSoundEffect(SOUND_FILENAME);
            sound.SetVolume(15);

            sprite = new AnimatedSprite(texture, 6, AnimationDirections.Horizontal)
            {
                AnimationFPS = 8,
                Loop = false,
                Order = 10,
            };

            sprite.OnAnimationEnd += Sprite_OnAnimationEnd;

            particle = particleFactory
                .Begin(sprite)
                .SetRotation()
                .SetScale(.45f, .5f)
                .Create();
        }

        private void Sprite_OnAnimationEnd(object? sender, EventArgs e)
        {
            Active = false;
            IsFinished = true;
        }

        public void Run()
        {
            particle.Position = Position;
            sound.Play(1, 0);
            Active = true;
        }

        public override void Render()
        {
            if (Active)
            {
                particle.Render();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Active)
            {
                particle.Position = new Point(Position.X - (particle.Width * particle.Scale / 2), Position.Y - (particle.Height * particle.Scale / 2));
                particle.Update(gameTime);
            }
        }
    }
}
