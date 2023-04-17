using _1942.Core;
using Engine.Core;
using Engine.Core.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942.Managers
{
    public class StageManager
    {
        private Stack<IStage> stages = new Stack<IStage>();

        public StageManager() 
        {
        }

        public void PushStage(IStage stage)
        {
            stages.Push(stage);
        }

        public void PopStage() 
        {
            stages.Pop();
        }

        public void Clear()
        {
            stages.Clear();
        }

        public IStage PeekStage()
        {
            return stages.Peek();
        }
    }
}
