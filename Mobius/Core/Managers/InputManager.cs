using static SDL2.SDL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Engine.Core.Math;
using System.Runtime.CompilerServices;
using Engine.Core.Input;

namespace Engine.Core.Managers
{
    public class InputManager : IUpdatable
    {
        private const int MAX_CONTROLLERS = 2;

        private static InputManager? instance;

        private readonly Mouse mouse = new();
        private readonly Keyboard keyboard = new();
        private readonly GameController?[] controllers = new GameController[MAX_CONTROLLERS];

        public Mouse Mouse { get => mouse; }
        public Keyboard Keyboard { get => keyboard; }
        public GameController?[] GameControllers { get => controllers; }

        public static InputManager Instance
        {
            get
            {
                instance ??= new InputManager();
                return instance;
            }
        }
        
        public bool Initialized { get; private set; }

        private InputManager()
        {
            Initialized = Init();
        }

        private bool Init()
        {
            if (SDL_Init(SDL_INIT_GAMECONTROLLER) < 0)
            {
                Console.WriteLine($"SDL Controller initialization error: {SDL_GetError()}");
                return false;
            }

            return true;
        }

        internal void HandleInputEvents(SDL_Event ev)
        {
            if (ev.type == SDL_EventType.SDL_CONTROLLERDEVICEADDED && ev.cdevice.which < MAX_CONTROLLERS)
            {
                int deviceId = ev.cdevice.which;
                IntPtr controller = SDL_GameControllerOpen(deviceId);

                if (controller == IntPtr.Zero)
                {
                    Console.WriteLine($"SDL Game controller initialization error: {SDL_GetError()}");
                    return;
                }

                controllers[deviceId]?.Dispose();
                controllers[deviceId] = new GameController(controller);
            }

            if (ev.type == SDL_EventType.SDL_CONTROLLERDEVICEREMOVED && ev.cdevice.which < MAX_CONTROLLERS)
            {
                int deviceId = ev.cdevice.which;
                GameController? controller = controllers[deviceId];

                if (controller == null)
                {
                    Console.WriteLine($"SDL Game controller removing error: {SDL_GetError()}");
                    return;
                }

                controller.Dispose();
                controllers[deviceId] = null;
            }
        }

        public void Update(GameTime gameTime)
        {
            mouse.Update(gameTime);
            keyboard.Update(gameTime);

            foreach (GameController? controller in controllers)
            {
                controller?.Update(gameTime);
            }
        }
    }
}
