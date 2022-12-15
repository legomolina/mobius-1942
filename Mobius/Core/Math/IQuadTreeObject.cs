using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core.Math
{
    public interface IQuadTreeObject<in T>
    {
        float GetTop(T obj);
        float GetBottom(T obj);
        float GetLeft(T obj);
        float GetRight(T obj);
    }
}
