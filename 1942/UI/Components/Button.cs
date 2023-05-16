using Engine.Components;
using Engine.Core;
using Engine.Core.Input;
using Engine.Core.Managers;
using Engine.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _1942.UI.Components
{
    public class Button : UIInteractableComponent
    {
        private const string IDLE_TEXTURE = "Assets/Textures/UI/yellow_button_idle.png";
        private const string FOCUS_TEXURE = "Assets/Textures/UI/yellow_button_pressed.png";

        private readonly Label label;
        private readonly Image idleImage;
        private readonly Image focusImage;

        private Image currentImage;

        public Alignment Align { get; set; } = Alignment.Center;

        public string Content { get; set; }

        public Font Font { get; set; }

        public Button(GraphicsManager graphics, BatchRenderer renderer) : base(graphics, renderer)
        {
            label = new Label(graphics, renderer);
            idleImage = new Image(graphics, renderer, IDLE_TEXTURE);
            focusImage = new Image(graphics, renderer, FOCUS_TEXURE);

            currentImage = idleImage;
        }

        public override void LoadContent(AssetManager assetManager)
        {
            idleImage.LoadContent(assetManager);
            focusImage.LoadContent(assetManager);

            Width = idleImage.Width;
            Height = idleImage.Height;
        }

        public override void Render()
        {
            currentImage.Render();
            label.Render();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            GameController? controller = input.GameControllers[0];

            if (controller != null && Focus)
            {
                if (controller.IsButtonReleased(GameControllerButtons.A))
                {
                    OnMouseUp();
                }
            }

            Point labelPosition = Position;

            currentImage = Focus ? focusImage : idleImage;

            switch (Align)
            {
                case Alignment.Center:
                    labelPosition = new Point(Position.X + (Width - label.Width) / 2, Position.Y + (Height - label.Height) / 2);
                break;
                
                case Alignment.Left:
                    labelPosition = new Point(Position.X, Position.Y + (Height - label.Height) / 2);
                break;
                
                case Alignment.Right:
                    labelPosition = new Point(Position.X + Width - label.Width, Position.Y + (Height - label.Height) / 2);
                break;
            }

            currentImage.Position = Position;

            label.Content = Content;
            label.Font = Font;
            label.Position = labelPosition;

            if (currentImage == focusImage)
            {
                currentImage.Position = new Point(Position.X, Position.Y + 5);
                label.Position = new Point(labelPosition.X, labelPosition.Y + 5);
            }

            label.Update(gameTime);
            currentImage.Update(gameTime);
        }
    }
}
