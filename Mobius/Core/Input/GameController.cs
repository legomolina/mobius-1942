using static SDL2.SDL;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace Engine.Core.Input
{
    public enum GameControllerButtons
    {
        A,
        B,
        X,
        Y,
        Back,
        Guide,
        Start,
        DPadUp,
        DPadDown,
        DPadLeft,
        DPadRight,
        LeftStick,
        RightStick,
        LeftShoulder,
        RightShoulder,
    }

    public enum GameControllerAxis
    {
        LeftStick,
        RightStick,
    }

    public class GameController : IDisposable, IUpdatable
    {
        public const int AXIS_MAX_VALUE = 32767;
        public const int AXIS_DEADZONE = 7000;

        private readonly IntPtr controller;

        private GameControllerState currentState;
        private GameControllerState previousState;

        public string Name { get; private set; }
        public GameControllerState State { get => currentState; }

        public GameController(IntPtr controller)
        {
            this.controller = controller;

            Name = SDL_GameControllerName(controller);
        }

        public void Dispose()
        {
            SDL_GameControllerClose(controller);
        }

        public void Update(GameTime gameTime)
        {
            previousState = currentState;
            SetState();
        }

        public bool IsButtonPressed(GameControllerButtons button)
        {
            return currentState.GetButtonByEnum(button);
        }

        public bool IsButtonReleased(GameControllerButtons button)
        {
            return previousState.GetButtonByEnum(button) && !currentState.GetButtonByEnum(button);
        }

        public GameControllerStick GetAxisPosition(GameControllerAxis axis)
        {
            return currentState.GetAxisByEnum(axis);
        }

        private void SetState()
        {
            currentState = new GameControllerState()
            {
                A = SDL_GameControllerGetButton(controller, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_A) > 0,
                B = SDL_GameControllerGetButton(controller, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_B) > 0,
                X = SDL_GameControllerGetButton(controller, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_X) > 0,
                Y = SDL_GameControllerGetButton(controller, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_Y) > 0,
                Back = SDL_GameControllerGetButton(controller, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_BACK) > 0,
                Guide = SDL_GameControllerGetButton(controller, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_GUIDE) > 0,
                Start = SDL_GameControllerGetButton(controller, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_START) > 0,
                LeftShoulder = SDL_GameControllerGetButton(controller, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_LEFTSHOULDER) > 0,
                RightShoulder = SDL_GameControllerGetButton(controller, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_RIGHTSHOULDER) > 0,
                LeftStickButton = SDL_GameControllerGetButton(controller, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_LEFTSTICK) > 0,
                RightStickButton = SDL_GameControllerGetButton(controller, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_RIGHTSTICK) > 0,
                DPadUp = SDL_GameControllerGetButton(controller, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_UP) > 0,
                DPadDown = SDL_GameControllerGetButton(controller, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_DOWN) > 0,
                DPadLeft = SDL_GameControllerGetButton(controller, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_LEFT) > 0,
                DPadRight = SDL_GameControllerGetButton(controller, SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_DPAD_RIGHT) > 0,
                LeftAxis = new GameControllerStick()
                {
                    X = NormalizeAxisInput(SDL_GameControllerGetAxis(controller, SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTX), AXIS_DEADZONE),
                    Y = NormalizeAxisInput(SDL_GameControllerGetAxis(controller, SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_LEFTY), AXIS_DEADZONE),
                    Z = NormalizeAxisInput(SDL_GameControllerGetAxis(controller, SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_TRIGGERLEFT), AXIS_DEADZONE),
                },
                RightAxis = new GameControllerStick()
                {
                    X = NormalizeAxisInput(SDL_GameControllerGetAxis(controller, SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_RIGHTX), AXIS_DEADZONE),
                    Y = NormalizeAxisInput(SDL_GameControllerGetAxis(controller, SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_RIGHTY), AXIS_DEADZONE),
                    Z = NormalizeAxisInput(SDL_GameControllerGetAxis(controller, SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_TRIGGERRIGHT), AXIS_DEADZONE),
                },
            };
        }

        private int NormalizeAxisInput(short value, int deadZone)
        {
            if (value > deadZone)
            {
                return value;
            }
            
            if (value < -deadZone)
            {
                return value;
            }

            return 0;
        }
    }
}
