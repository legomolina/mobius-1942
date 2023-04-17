using Engine.Components;
using Engine.Core;
using Engine.Core.Managers;
using Engine.Core.Math;

namespace _1942.Entities
{
    public class Background : Entity
    {
        private const float InitialPosition = 800.0f;

        private readonly GraphicsManager graphics;
        private readonly BatchRenderer renderer;
        private readonly string textureFile;

        private float currentPosition;
        private Sprite sprite;

        public float Speed { get; set; } = .1f;

        public Background(string textureFile, GraphicsManager graphics, BatchRenderer renderer)
        {
            currentPosition = InitialPosition;
            this.graphics = graphics;
            this.renderer = renderer;
            this.textureFile = textureFile;

            Order = 1;
        }

        public override void LoadContent(AssetManager assetManager)
        {
            Texture texture = assetManager.LoadTexture(textureFile);
            sprite = new Sprite(texture, new Rectangle(0, currentPosition, graphics.WindowWidth, graphics.WindowHeight))
            {
                Height = graphics.WindowHeight,
            };
        }

        public override void Render()
        {
            renderer.Render(sprite);
        }

        public override void Update(GameTime gameTime)
        {
            float cropHeight = (sprite.Height + graphics.WindowHeight) - currentPosition;

            if (cropHeight > 0)
            {
                currentPosition += gameTime.DeltaTime * Speed;
            }

            sprite.CropRectangle = new Rectangle(0, Math.Max(0, (sprite.Height + graphics.WindowHeight) - currentPosition), sprite.Width, sprite.Height);
        }
    }
}
