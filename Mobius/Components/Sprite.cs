using Engine.Core;
using Engine.Core.Managers;
using Engine.Core.Math;

namespace Engine.Components
{
    public class Sprite : GameComponent
    {
        protected readonly Texture texture;

        public Rectangle CropRectangle { get; set; }

        public Sprite(Texture texture) : this(texture, new Rectangle(0, 0, texture.Width, texture.Height)) { }

        public Sprite(Texture texture, Rectangle cropRectangle)
        {
            this.texture = texture;

            CropRectangle = cropRectangle;
            Width = texture.Width;
            Height = texture.Height;
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

            texture.Render(renderRectangle, CropRectangle, Rotation);
        }

        public override void LoadContent(AssetManager assetManager) { }

        public override void Update(GameTime gameTime) { }
    }
}
