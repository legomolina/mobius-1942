using Engine.Components;
using Engine.Core;
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
        protected readonly InputManager input;

        public event EventHandler<MouseEventArgs> MouseDown;
        public event EventHandler<MouseEventArgs> MouseUp;
        public event EventHandler<MouseEventArgs> MouseOver;
        public event EventHandler<MouseEventArgs> MouseOut;

        public bool Focus { get; private set; } = false;

        public bool Hover { get; private set; } = false;

        public UIComponent(GraphicsManager graphics, BatchRenderer renderer)
        {
            this.graphics = graphics;
            this.renderer = renderer;

            input = InputManager.Instance;
        }

        protected void OnMouseDown()
        {
            Focus = true;
            MouseDown?.Invoke(this, new MouseEventArgs(input.MousePosition, MouseButtons.Left));
        }

        protected void OnMouseUp()
        {
            Focus = false;
            MouseUp?.Invoke(this, new MouseEventArgs(input.MousePosition, MouseButtons.Left));
        }

        protected void OnMouseOver()
        {
            Hover = true;
            MouseOver?.Invoke(this, new MouseEventArgs(input.MousePosition, MouseButtons.None));
        }

        protected void OnMouseOut()
        {
            Hover = false;
            MouseOut?.Invoke(this, new MouseEventArgs(input.MousePosition, MouseButtons.None));
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle bounds = new Rectangle(Position.X, Position.Y, Width, Height);

            if (bounds.Contains(input.MousePosition) && input.IsMouseButtonPressed(MouseButtons.Left))
            {
                OnMouseDown();
            }

            if (bounds.Contains(input.MousePosition) && input.IsMouseButtonReleased(MouseButtons.Left))
            {
                OnMouseUp();
            }

            if (!Hover && bounds.Contains(input.MousePosition))
            {
                OnMouseOver();
            }

            if (Hover && !bounds.Contains(input.MousePosition))
            {
                OnMouseOut();
            }
        }
    }
}
