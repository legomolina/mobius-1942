using Engine.Core;
using Engine.Core.Input;
using Engine.Core.Managers;
using Engine.Core.Math;

namespace _1942.UI
{
    public abstract class UIInteractableComponent : UIComponent, IInteractable
    {
        protected readonly InputManager input;

        public event EventHandler<MouseEventArgs>? MouseDown;
        public event EventHandler<MouseEventArgs>? MouseUp;
        public event EventHandler<MouseEventArgs>? MouseOver;
        public event EventHandler<MouseEventArgs>? MouseOut;

        public bool Focus { get; protected set; } = false;

        public bool Hover { get; protected set; } = false;

        public int TabIndex { get; set; } = 0;

        public UIInteractableComponent(GraphicsManager graphics, BatchRenderer renderer) : base(graphics, renderer)
        {
            input = InputManager.Instance;
        }

        protected void OnMouseDown()
        {
            Focus = true;
            MouseDown?.Invoke(this, new MouseEventArgs(input.Mouse.GetMousePosition(), MouseButtons.Left));
        }

        protected void OnMouseUp()
        {
            Focus = false;
            MouseUp?.Invoke(this, new MouseEventArgs(input.Mouse.GetMousePosition(), MouseButtons.Left));
        }

        protected void OnMouseOver()
        {
            Hover = true;
            MouseOver?.Invoke(this, new MouseEventArgs(input.Mouse.GetMousePosition(), MouseButtons.None));
        }

        protected void OnMouseOut()
        {
            Hover = false;
            MouseOut?.Invoke(this, new MouseEventArgs(input.Mouse.GetMousePosition(), MouseButtons.None));
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle bounds = new Rectangle(Position.X, Position.Y, Width, Height);

            if (bounds.Contains(input.Mouse.GetMousePosition()) && input.Mouse.IsButtonPressed(MouseButtons.Left))
            {
                OnMouseDown();
            }

            if (bounds.Contains(input.Mouse.GetMousePosition()) && input.Mouse.IsButtonReleased(MouseButtons.Left))
            {
                OnMouseUp();
            }

            if (!Hover && bounds.Contains(input.Mouse.GetMousePosition()))
            {
                OnMouseOver();
            }

            if (Hover && !bounds.Contains(input.Mouse.GetMousePosition()))
            {
                OnMouseOut();
            }
        }

        public void SetFocus(bool hasFocus)
        {
            Focus = hasFocus;
        }
    }
}
