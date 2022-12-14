using Engine.Components;
using Engine.Exceptions;

namespace Engine.Core
{
    public class AssetManager
    {
        private readonly GraphicsManager graphics;
        private readonly Dictionary<string, Texture> textures = new();

        public AssetManager(GraphicsManager graphicsManager)
        {
            graphics = graphicsManager;
        }

        public Texture LoadTexture(string file, bool isTransparent = false)
        {
            if (!textures.ContainsKey(file) || textures[file] == null)
            {
                IntPtr texturePointer = graphics.LoadTexture(file, isTransparent);
                textures[file] = new Texture(graphics, texturePointer);
            }

            return textures[file];
        }
    }
}
