using _1942.Core;
using Engine;
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
        private readonly Game game;
        private readonly Stack<IStage> stages = new Stack<IStage>();

        public StageManager(Game game)
        {
            this.game = game;
        }

        public void PushStage(IStage stage)
        {
            stage.Initialize();
            stage.LoadContent(game.AssetManager);

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
