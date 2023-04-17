using Engine.Core;
using Engine.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942.Core
{
    public interface IStage : IDrawable, IUpdatable
    {
        void Initialize();        
    }
}
