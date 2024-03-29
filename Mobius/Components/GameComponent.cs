﻿using Engine.Core;
using Engine.Core.Managers;
using Engine.Core.Math;

namespace Engine.Components
{
    public abstract class GameComponent : IDrawable, IUpdatable
    {
        private float rotation = 0;

        public bool Active { get; set; } = true;
        public Point Position { get; set; }
        public float Rotation
        {
            get => rotation;
            set
            {
                rotation = value;

                if (rotation > 360)
                {
                    rotation -= 360;
                }

                if (rotation < 0)
                {
                    rotation += 360;
                }
            }
        }
        public int Order { get; set; }
        public float Scale { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public GameComponent(): this(Point.Zero, 0, 1) { }

        public GameComponent(Point position, float rotation) : this(position, rotation, 1) { }

        public GameComponent(Point position, float rotation, float scale)
        {
            Position = position;
            Rotation = rotation;
            Scale = scale;
        }

        public abstract void LoadContent(AssetManager assetManager);

        public abstract void Render();

        public abstract void Update(GameTime gameTime);
    }
}
