using Engine.Core.Managers;
using Engine.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    public class BatchRenderer
    {
        private readonly GraphicsManager graphics;
        private readonly IList<IDrawable> drawables;
        
        private bool isInitialized;

        public BatchRenderer(GraphicsManager graphics) 
        {
            this.graphics = graphics;
            isInitialized = false;
            drawables = new List<IDrawable>();
        }

        public void Start()
        {
            if (isInitialized)
            {
                throw new RendererAlreadyInitializedException();
            }

            isInitialized = true;
            graphics!.ClearBackBuffer();
        }

        public void Render(IDrawable drawable)
        {
            if (!isInitialized)
            {
                throw new RendererNotInitializedException();
            }

            drawables.Add(drawable);
        }

        public void End()
        {
            if (!isInitialized)
            {
                throw new RendererNotInitializedException(); 
            }

            IEnumerable<IDrawable> sortedDrawables = drawables
                .Where(drawable => drawable.Active)
                .OrderBy(drawable => drawable.Order);

            foreach (IDrawable drawable in sortedDrawables)
            {
                drawable.Render();
            }

            graphics.Render();

            drawables.Clear();
            isInitialized = false;
        }
    }
}
