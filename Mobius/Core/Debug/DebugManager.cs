using static SDL2.SDL;
using Engine.Core.Math;

namespace Engine.Core.Debug
{
    public class DebugManager
    {
        public static void RenderBounds(Rectangle bounds, Color? color)
        {
            color ??= Color.Red;
            SDL_Rect rect = new SDL_Rect()
            {
                x = (int)bounds.X,
                y = (int)bounds.Y,
                w = (int)bounds.Width,
                h = (int)bounds.Height
            };

            GraphicsManager.Instance.DrawRectangle(rect, color.Value.R, color.Value.G, color.Value.B, color.Value.A);
        }
    }
}
