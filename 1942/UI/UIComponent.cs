using Engine.Components;
using Engine.Core;
using Engine.Core.Input;
using Engine.Core.Managers;
using Engine.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942.UI
{
    public abstract class UIComponent : GameComponent
    {
        protected readonly GraphicsManager graphics;
        protected readonly BatchRenderer renderer;
        
        public UIComponent(GraphicsManager graphics, BatchRenderer renderer)
        {
            this.graphics = graphics;
            this.renderer = renderer;
        }

        public override void Update(GameTime gameTime) { }
    }
}
