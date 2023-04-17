using Engine.Core.Managers;

namespace Engine.Core
{
    public interface IDrawable
    {
        bool Active { get; }
        int Order { get; }

        void LoadContent(AssetManager assetManager);
        void Render();
    }
}
