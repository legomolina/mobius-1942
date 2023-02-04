using Engine.Core;
using Engine.Core.Math;

namespace Engine.Components
{
    public enum AnimationDirections
    {
        Horizontal,
        Vertical
    }

    public class AnimatedSprite : Sprite, IUpdatable, IDisposable
    {
        private readonly AnimationDirections direction;
        private readonly Rectangle[] frames;

        private int lastFrame = 0;
        private float lastUpdate = 0;

        public int AnimationFPS { get; set; } = 30;
        public int FrameWidth { get; private set; } = 0;
        public int FrameHeight { get; private set; } = 0;
        public bool Loop { get; set; } = true;

        public event EventHandler<EventArgs> OnAnimationEnd;

        public AnimatedSprite(Texture texture, int framesCount, AnimationDirections direction) : base(texture)
        {
            frames = new Rectangle[framesCount];
            this.direction = direction;

            CropTexture();
        }

        private void CropTexture()
        {
            switch (direction)
            {
                case AnimationDirections.Horizontal:
                    FrameWidth = texture.Width / frames.Length;
                    FrameHeight = texture.Height;
                    break;

                case AnimationDirections.Vertical:
                    FrameWidth = texture.Width;
                    FrameHeight = texture.Height / frames.Length;
                    break;
            }

            for (int i = 0; i < frames.Length; i++)
            {
                Rectangle frame = new();

                switch (direction)
                {
                    case AnimationDirections.Horizontal:
                        frame = new Rectangle(i * FrameWidth, 0, FrameWidth, FrameHeight);
                        break;

                    case AnimationDirections.Vertical:
                        frame = new Rectangle(0, i * FrameHeight, FrameWidth, FrameHeight);
                        break;
                }

                frames[i] = frame;
            }
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
                Width = FrameWidth * Scale,
                Height = FrameHeight * Scale
            };

            texture.Render(renderRectangle, frames[lastFrame], Rotation);
        }

        public override void Update(GameTime gameTime)
        {
            if (!Active)
            {
                return;
            }

            float delta = (gameTime.CurrentTime - lastUpdate) / 1000;
            int framesToUpdate = (int)Math.Floor(delta / (1.0f / AnimationFPS));

            if (framesToUpdate > 0)
            {
                lastFrame += framesToUpdate;
                lastFrame %= frames.Length;
                lastUpdate = gameTime.CurrentTime;
            }

            if (!Loop && lastFrame == frames.Length - 1)
            {
                Active = false;
                OnAnimationEnd?.Invoke(this, new EventArgs());
            }
        }
    }
}
