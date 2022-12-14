using static SDL2.SDL;
using Engine.Core;
using Engine.Core.Math;

namespace Engine.Components
{
    public class Texture
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
            SDL_Rect renderRect = new()
            {
                x = (int)renderRectangle.X,
                y = (int)renderRectangle.Y,
                w = (int)renderRectangle.Width,
                h = (int)renderRectangle.Height
            };

            graphics.DrawTexture(texture, renderRect);
        }

        public void Render(Rectangle renderRectangle, Rectangle clipRectangle)
        {
            SDL_Rect renderRect = new()
            {
                x = (int)renderRectangle.X,
                y = (int)renderRectangle.Y,
                w = (int)renderRectangle.Width,
                h = (int)renderRectangle.Height
            };
            SDL_Rect clipRect = new()
            {
                x = (int)clipRectangle.X,
                y = (int)clipRectangle.Y,
                w = (int)clipRectangle.Width,
                h = (int)clipRectangle.Height,
            };

            graphics.DrawTexture(texture, renderRect, clipRect);
        }

        public void Render(Rectangle renderRectangle, Rectangle clipRectangle, double rotation)
        {
            SDL_Rect renderRect = new()
            {
                x = (int)renderRectangle.X,
                y = (int)renderRectangle.Y,
                w = (int)renderRectangle.Width,
                h = (int)renderRectangle.Height
            };
            SDL_Rect clipRect = new()
            {
                x = (int)clipRectangle.X,
                y = (int)clipRectangle.Y,
                w = (int)clipRectangle.Width,
                h = (int)clipRectangle.Height,
            };
            SDL_Point centerPoint = new()
            {
                x = (int)clipRectangle.Width / 2,
                y = (int)clipRectangle.Height / 2,
            };

            graphics.DrawTexture(texture, renderRect, clipRect, rotation, centerPoint);
        }
    }
}
