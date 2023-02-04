using Engine.Components;
using Engine.Core;
using Engine.Core.Managers;
using Engine.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942.Entities.Effects
{
    public class Explosion : GameComponent, IDrawable
    {
        private const string SoundFilename = "Assets/Effects/explosion.wav";
        private const string TextureFilename = "Assets/Textures/explosion.png";

        private AnimatedSprite sprite;
        private SoundEffect soundEffect;

        public Explosion()
        {
            Active = false;
        }

        public void Run()
        {
            Active = true;
            soundEffect.Play();
        }

        public void LoadContent(AssetManager assetManager)
        {
            Texture texture = assetManager.LoadTexture(TextureFilename);
            soundEffect = assetManager.LoadSoundEffect(SoundFilename);
            soundEffect.SetVolume(15);

            sprite = new AnimatedSprite(texture, 6, AnimationDirections.Horizontal);
            sprite.AnimationFPS = 8;
            sprite.Loop = false;
            sprite.OnAnimationEnd += Sprite_OnAnimationEnd;
        }

        private void Sprite_OnAnimationEnd(object? sender, EventArgs e)
        {
            Active = false;
        }

        public override void Render()
        {
            sprite.Position = Position;
            sprite.Scale = Scale;

            if (Active)
            {
                base.Render();
                sprite.Render();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Active)
            {
                base.Update(gameTime);
                sprite.Update(gameTime);
            }
        }
    }
}
