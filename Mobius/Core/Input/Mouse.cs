using static SDL2.SDL;
using Engine.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Core.Math;

namespace Engine.Core.Input
{
    public enum MouseButtons
    {
        None = 0,
        Left = 1,
        Middle = 2,
        Right = 4,
        Button_4 = 8,
        Button_5 = 16,
    }

    public class Mouse : IUpdatable
    {
        private MouseState currentState;
        private MouseState previousState;

        public void Update(GameTime gameTime)
        {
            previousState = currentState;
            SetState();
        }

        public bool IsButtonPressed(MouseButtons buttons)
        {
            return currentState.GetButtonByEnum(buttons);
        }

        public bool IsButtonReleased(MouseButtons buttons)
        {
            return previousState.GetButtonByEnum(buttons) && !currentState.GetButtonByEnum(buttons);
        }

        public Point GetMousePosition()
        {
            return currentState.Position;
        }

        private void SetState()
        {
            uint state = SDL_GetMouseState(out int mouseX, out int mouseY);

            currentState = new MouseState()
            {
                Position = new Point(mouseX, mouseY),
                LeftButton = ((MouseButtons)state).HasFlag(MouseButtons.Left),
                RightButton = ((MouseButtons)state).HasFlag(MouseButtons.Right),
                MiddleButton = ((MouseButtons)state).HasFlag(MouseButtons.Middle),
                Mouse4Button = ((MouseButtons)state).HasFlag(MouseButtons.Button_4),
                Mouse5Button = ((MouseButtons)state).HasFlag(MouseButtons.Button_5),
            };
        }
    }
}
