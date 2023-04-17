using _1942.Exceptions;
using Engine.Components;
using Engine.Core;
using Engine.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace _1942.Particles
{
    public class ParticleFactory
    {
        private readonly BatchRenderer renderer;
        private readonly Random random;

        private Particle particle;

        public ParticleFactory(BatchRenderer renderer)
        {
            random = new Random();
            this.renderer = renderer;
        }

        public ParticleFactory Begin(Sprite sprite)
        {
            particle = new Particle(renderer, sprite)
            {
                Width = sprite.Width,
                Height = sprite.Height,
            };

            if (sprite is AnimatedSprite animatedSprite)
            {
                particle.Width = animatedSprite.FrameWidth;
                particle.Height = animatedSprite.FrameHeight;
            }

            return this;
        }

        public ParticleFactory SetPosition(Point position)
        {
            if (particle == null)
            {
                throw new ParticleNotInitializedException("SetPosition");
            }

            particle.Position = position;

            return this;
        }

        public ParticleFactory SetPosition(Point position, int padding)
        {
            if (particle == null)
            {
                throw new ParticleNotInitializedException("SetPosition");
            }

            Area area = new Area(position, padding);
            particle.Position = area.GetRandomPoint();

            return this;
        }

        public ParticleFactory SetRotation(int rotation)
        {
            if (particle == null)
            {
                throw new ParticleNotInitializedException("SetRotation");
            }

            particle.Rotation = rotation;
            return this;
        }

        public ParticleFactory SetRotation(int min = 0, int max = 360)
        {
            if (particle == null)
            {
                throw new ParticleNotInitializedException("SetRotation");
            }

            if (min > max)
            {
                throw new ArgumentException("Min value must be less than max");
            }

            if (max < min)
            {
                throw new ArgumentException("Max value must be greater than min");
            }

            if (min < 0)
            {
                throw new ArgumentException("Min value must be greater or equal than 0");
            }

            if (max > 360)
            {
                throw new ArgumentException("Max value must be less or equal than 360");
            }

            particle.Rotation = random.Next(min, max);

            return this;
        }

        public ParticleFactory SetScale(float scale)
        {
            if (particle == null)
            {
                throw new ParticleNotInitializedException("SetScale");
            }

            particle.Scale = scale;

            return this;
        }

        public ParticleFactory SetScale(float min = .0f, float max = 1f)
        {
            if (particle == null)
            {
                throw new ParticleNotInitializedException("SetScale");
            }

            if (min > max)
            {
                throw new ArgumentException("Min value must be less than max");
            }

            if (max < min)
            {
                throw new ArgumentException("Max value must be greater than min");
            }

            if (min < 0)
            {
                throw new ArgumentException("Min value must be greater or equal than 0");
            }

            double scale = random.NextDouble() * (max - min) + min;

            particle.Scale = (float)scale;

            return this;
        }

        public Particle Create()
        {
            if (particle == null)
            {
                throw new ParticleNotInitializedException("Create");
            }

            return particle;
        }
    }
}
