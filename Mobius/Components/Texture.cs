using static SDL2.SDL;
using Engine.Core;
using Engine.Core.Math;
using Engine.Core.Managers;

namespace Engine.Components
{
    public class Texture : IDisposable
    {
        private readonly GraphicsManager graphics;
        private readonly IntPtr texture;
        private readonly int width;
        private readonly int height;

        public int Width { get => width; }
        public int Height { get => height; }

        public Texture(GraphicsManager graphics, IntPtr texture)
        {
            this.graphics = graphics;
            this.texture = texture;

            SDL_QueryTexture(texture, out _, out _, out width, out height);
        }

        public void Render()
        {
            Render(new Rectangle(0, 0, Width, Height));
        }

        public void Render(Rectangle renderRectangle)
        {
            graphics.DrawTexture(texture, renderRectangle.ToSDLRect());
        }

        public void Render(Rectangle renderRectangle, Rectangle clipRectangle)
        {
            graphics.DrawTexture(texture, renderRectangle.ToSDLRect(), clipRectangle.ToSDLRect());
        }

        public void Render(Rectangle renderRectangle, Rectangle clipRectangle, double rotation)
        {
            SDL_Point centerPoint = new()
            {
                x = (int)renderRectangle.Width / 2,
                y = (int)renderRectangle.Height / 2,
            };

            graphics.DrawTexture(texture, renderRectangle.ToSDLRect(), clipRectangle.ToSDLRect(), rotation, centerPoint);
        }

        public void Dispose()
        {
            SDL_DestroyTexture(texture);
        }
    }
}
