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
        private Stack<Stage> stages = new Stack<Stage>();

        public StageManager() 
        {
        }

        public void PushStage(Stage stage)
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

        public Stage PeekStage()
        {
            return stages.Peek();
        }
    }
}
