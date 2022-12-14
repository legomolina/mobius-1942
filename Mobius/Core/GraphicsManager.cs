using static SDL2.SDL;
using static SDL2.SDL_image;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Exceptions;
using System.Runtime.InteropServices;

namespace Engine.Core
{
    public class GraphicsManager : IDisposable
    {
        private static GraphicsManager? instance;

        private IntPtr renderer = IntPtr.Zero;
        private IntPtr window = IntPtr.Zero;

        private int windowWidth = 640;
        private int windowHeight = 480;

        public static GraphicsManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GraphicsManager();
                }

                return instance;
            }
        }

        public int WindowWidth
        {
            get => windowWidth;
            set
            {
                windowWidth = value;
                
                if (window != IntPtr.Zero)
                {
                    SDL_SetWindowSize(window, windowWidth, windowHeight);
                }
            }
        }

        public int WindowHeight
        {
            get => windowHeight;
            set
            {
                windowHeight = value;

                if (window != IntPtr.Zero)
                {
                    SDL_SetWindowSize(window, windowWidth, windowHeight);
                }
            }
        }

        public bool Initialized { get; private set; } = false;

        private GraphicsManager()
        {
            Initialized = Init();
        }

        private bool Init()
        {
            if (SDL_Init(SDL_INIT_VIDEO) < 0)
            {
                Console.WriteLine($"SDL Video initialization error: {SDL_GetError()}");
                return false;
            }

            window = SDL_CreateWindow("Mobius engine", SDL_WINDOWPOS_UNDEFINED, SDL_WINDOWPOS_UNDEFINED, WindowWidth, WindowHeight, SDL_WindowFlags.SDL_WINDOW_SHOWN);

            if (window == IntPtr.Zero)
            {
                Console.WriteLine($"Window creation error: {SDL_GetError()}");
                return false;
            }

            renderer = SDL_CreateRenderer(window, -1, SDL_RendererFlags.SDL_RENDERER_ACCELERATED);

            if (renderer == IntPtr.Zero)
            {
                Console.WriteLine($"Renderer creation error: {SDL_GetError()}");
                return false;
            }

            // SDL_SetRenderDrawColor(renderer, 0xFF, 0xFF, 0xFF, 0xFF);
            SDL_SetRenderDrawColor(renderer, 0x00, 0x00, 0x00, 0xFF);

            IMG_InitFlags flags = IMG_InitFlags.IMG_INIT_PNG;
            IMG_InitFlags init = (IMG_InitFlags)IMG_Init(flags);

            if (!init.HasFlag(flags))
            {
                Console.WriteLine($"Renderer creation error: {IMG_GetError()}");
                return false;
            }

            return true;
        }

        public void ClearBackBuffer()
        {
            SDL_RenderClear(renderer);
        }

        public void Render()
        {
            SDL_RenderPresent(renderer);
        }

        internal IntPtr LoadTexture(string file)
        {
            IntPtr texture = IMG_LoadTexture(renderer, file);

            if (texture == IntPtr.Zero)
            {
                throw new TextureCreatingException(file);
            }

            return texture;
        }

        internal void DrawTexture(IntPtr texture, SDL_Rect renderRect)
        {
            SDL_RenderCopy(renderer, texture, IntPtr.Zero, ref renderRect);
        }

        internal void DrawTexture(IntPtr texture, SDL_Rect renderRect, SDL_Rect clipRect)
        {
            SDL_RenderCopy(renderer, texture, ref clipRect, ref renderRect);
        }

        internal void DrawTexture(IntPtr texture, SDL_Rect renderRect, SDL_Rect clipRect, double rotation, SDL_Point center)
        {
            SDL_RenderCopyEx(renderer, texture, ref clipRect, ref renderRect, rotation, ref center, SDL_RendererFlip.SDL_FLIP_NONE);
        }

        public void Dispose()
        {
            Initialized = false;

            SDL_DestroyRenderer(renderer);
            SDL_DestroyWindow(window);

            IMG_Quit();
        }
    }
}
