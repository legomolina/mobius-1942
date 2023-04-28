using static SDL2.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Engine.Core.Math;

namespace Engine.Core.Managers
{
    public enum Keyboard
    {
        Space = 32,
        Num_0 = 48,
        Num_1,
        Num_2,
        Num_3,
        Num_4,
        Num_5,
        Num_6,
        Num_7,
        Num_8,
        Num_9,
        A = 97,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K,
        L,
        M,
        N,
        O,
        P,
        Q,
        R,
        S,
        T,
        U,
        V,
        W,
        X,
        Y,
        Z,
        Right = 1073741903,
        Left,
        Down,
        Up,
    }

    [Flags]
    public enum MouseButtons
    {
        None = 0,
        Left = 1,
        Middle = 2,
        Right = 4,
        Button_4 = 8,
        Button_5 = 16,
    }

    public class InputManager : IUpdatable
    {
        private static InputManager? instance;

        private byte[]? keyboardState;
        private byte[]? previousKeyboardState;
        private MouseButtons mouseState = MouseButtons.None;
        private MouseButtons previousMouseState = MouseButtons.None;

        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputManager();
                }

                return instance;
            }
        }

        public Point MousePosition { get; private set; }

        private InputManager()
        {

        }

        public bool IsKeyPressed(Keyboard key)
        {
            if (keyboardState == null)
            {
                throw new NullReferenceException("Update input manager before getting keyboard state");
            }

            byte keyCode = (byte)SDL_GetScancodeFromKey((SDL_Keycode)key);
            return keyboardState[keyCode] == 1;
        }

        public bool IsKeyReleased(Keyboard key)
        {
            if (keyboardState == null || previousKeyboardState == null)
            {
                throw new NullReferenceException("Update input manager before getting keyboard state");
            }

            byte keyCode = (byte)SDL_GetScancodeFromKey((SDL_Keycode)key);

            return previousKeyboardState[keyCode] == 1 && keyboardState[keyCode] == 0;
        }

        public bool IsMouseButtonPressed(MouseButtons button)
        {
            return mouseState.HasFlag(button);
        }

        public bool IsMouseButtonReleased(MouseButtons button)
        {
            return previousMouseState.HasFlag(button) && !mouseState.HasFlag(button);
        }

        public void Update(GameTime gameTime)
        {
            previousKeyboardState = keyboardState;
            previousMouseState = mouseState;

            IntPtr keyboard = SDL_GetKeyboardState(out int arraySize);
            keyboardState = new byte[arraySize];
            Marshal.Copy(keyboard, keyboardState, 0, arraySize);

            uint currentMouseState = SDL_GetMouseState(out int mouseX, out int mouseY);
            MousePosition = new Point(mouseX, mouseY);
            mouseState = (MouseButtons)currentMouseState;
        }
    }
}
