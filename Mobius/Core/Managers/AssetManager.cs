using Engine.Components;
using Engine.Exceptions;
using System.Runtime.InteropServices;

namespace Engine.Core.Managers
{
    public class AssetManager
    {
        private readonly GraphicsManager graphicsManager;
        private readonly AudioManager audio;
        private readonly Dictionary<string, Font> fonts = new();
        private readonly Dictionary<string, SoundEffect> soundEffects = new();
        private readonly Dictionary<string, Texture> textures = new();

        public AssetManager(GraphicsManager graphicsManager, AudioManager audioManager)
        {
            this.graphicsManager = graphicsManager;
            audio = audioManager;
        }

        public Font LoadFont(string file)
        {
            if (!fonts.ContainsKey(file) || fonts[file] == null)
            {
                IntPtr fontPointer = graphicsManager.LoadFont(file, Font.DEFAULT_FONT_SIZE);
                fonts[file] = new Font(graphicsManager, fontPointer, file);
            }

            return fonts[file];
        }

        public Texture LoadTexture(string file)
        {
            if (!textures.ContainsKey(file) || textures[file] == null)
            {
                IntPtr texturePointer = graphicsManager.LoadTexture(file);
                textures[file] = new Texture(graphicsManager, texturePointer);
            }

            return textures[file];
        }

        public SoundEffect LoadSoundEffect(string file)
        {
            if (!soundEffects.ContainsKey(file) || soundEffects[file] == null)
            {
                IntPtr soundPointer = audio.LoadSoundEffect(file);
                soundEffects[file] = new SoundEffect(soundPointer);
            }

            return soundEffects[file];
        }
    }
}
