using Engine.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Debug
{
    public abstract class DebugDrawable : IDrawable
    {
        protected readonly GraphicsManager graphics;

        public bool Active => true;

        public int Order => int.MaxValue;

        public DebugDrawable(GraphicsManager graphics)
        {
            this.graphics = graphics;
        }

        public void LoadContent(AssetManager assetManager) { }

        public abstract void Render();
    }
}
