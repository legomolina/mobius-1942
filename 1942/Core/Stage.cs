﻿using Engine.Core;
using Engine.Core.Collisions;
using Engine.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942.Core
{
    public abstract class Stage : IStage
    {
        protected readonly GraphicsManager graphics;
        protected readonly BatchRenderer renderer;

        public bool Active { get; set; }
        protected CollisionsContainer CollisionsContainer { get; private set; }
        public int Order { get; set; }

        public Stage(GraphicsManager graphics, BatchRenderer renderer)
        {
            this.graphics = graphics;
            this.renderer = renderer;

            Active = true;
            CollisionsContainer = new CollisionsContainer();
            Order = 0;
        }

        public abstract void Initialize();

        public abstract void LoadContent(AssetManager assetManager);

        public abstract void Render();

        public virtual void Update(GameTime gameTime)
        {
            CollisionsContainer.Update(gameTime);
        }
    }
}
