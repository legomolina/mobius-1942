using Engine.Core.Managers;

namespace Engine.Core
{
    public interface IDrawable
    {
        void LoadContent(AssetManager assetManager);
        void Render();
    }
}
