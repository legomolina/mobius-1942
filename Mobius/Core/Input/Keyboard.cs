using static SDL2.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Engine.Core.Input
{
    public enum KeyboardKeys
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

    public class Keyboard : IUpdatable
    {
        private byte[]? currentState;
        private byte[]? previousState;

        public void Update(GameTime gameTime)
        {
            previousState = currentState;

            IntPtr keyboard = SDL_GetKeyboardState(out int arraySize);
            currentState = new byte[arraySize];
            Marshal.Copy(keyboard, currentState, 0, arraySize);
        }

        public bool IsKeyPressed(KeyboardKeys key)
        {
            if (currentState == null)
            {
                throw new NullReferenceException("Update keyboard before getting keyboard state");
            }

            byte keyCode = (byte)SDL_GetScancodeFromKey((SDL_Keycode)key);
            return currentState[keyCode] == 1;
        }

        public bool IsKeyReleased(KeyboardKeys key)
        {
            if (currentState == null || previousState == null)
            {
                throw new NullReferenceException("Update keyboard before getting keyboard state");
            }

            byte keyCode = (byte)SDL_GetScancodeFromKey((SDL_Keycode)key);
            return previousState[keyCode] == 1 && currentState[keyCode] == 0;
        }
    }
}
