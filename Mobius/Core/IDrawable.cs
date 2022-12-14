using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    public interface IDrawable
    {
        void LoadContent(AssetManager assetManager);
        void Render();
    }
}
