using Engine.Core.Math;

namespace Engine.Components
{
    public class Sprite : GameComponent, IDisposable
    {
        protected readonly Texture texture;

        protected Rectangle cropRectangle;

        public Sprite(Texture texture) : this(texture, new Rectangle(0, 0, texture.Width, texture.Height)) { }

        public Sprite(Texture texture, Rectangle cropRectangle)
        {
            this.cropRectangle = cropRectangle;
            this.texture = texture;

            Width = texture.Width;
            Height = texture.Height;
        }

        public void Dispose()
        {
            texture.Dispose();
        }

        public override void Render()
        {
            if (!Active)
            {
                return;
            }

            Rectangle renderRectangle = new()
            {
                X = Position.X,
                Y = Position.Y,
                Width = Width * Scale,
                Height = Height * Scale
            };

            texture.Render(renderRectangle, cropRectangle, Rotation);
        }
    }
}
