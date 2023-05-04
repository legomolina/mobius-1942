using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Math
{
    /// <summary>
    /// See https://www.trysmudford.com/blog/linear-interpolation-functions/
    /// </summary>
    public class LinearInterpolation
    {
        public static float Lerp(float x, float y, float value)
        {
            return x * (1 - value) + y * value;
        }

        public static float InvLerp(float x, float y, float value)
        {
            return System.Math.Clamp((value - x) / (y - x), 0, 1);
        }

        public static float Range(float x1, float y1, float x2, float y2, float value)
        {
            return Lerp(x2, y2, InvLerp(x1, y1, value));
        }
    }
}
