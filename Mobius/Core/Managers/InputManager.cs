using static SDL2.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Engine.Core.Managers
{
    public enum Keyboard
    {
        SPACE = 32,
        NUM_0 = 48,
        NUM_1,
        NUM_2,
        NUM_3,
        NUM_4,
        NUM_5,
        NUM_6,
        NUM_7,
        NUM_8,
        NUM_9,
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
        RIGHT = 1073741903,
        LEFT,
        DOWN,
        UP,
    }

    public class InputManager : IUpdatable
    {
        private static InputManager? instance;

        private byte[]? keyboardState;
        private byte[]? previousKeyboardState;

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

        public void Update(GameTime gameTime)
        {
            previousKeyboardState = keyboardState;

            IntPtr keyboard = SDL_GetKeyboardState(out int arraySize);

            keyboardState = new byte[arraySize];
            Marshal.Copy(keyboard, keyboardState, 0, arraySize);
        }
    }
}
