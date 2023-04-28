using _1942.Core;
using _1942.UI;
using _1942.UI.Components;
using Engine.Components;
using Engine.Core;
using Engine.Core.Managers;
using Engine.Core.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1942.Stages
{
    public class MainMenu : Stage
    {
        private Button newGameButton;
        private Button quitGameButton;
        private Image backgroundImage;

        public event EventHandler<EventArgs> NewGame;
        public event EventHandler<EventArgs> QuitGame;

        public MainMenu(GraphicsManager graphics, BatchRenderer renderer) : base(graphics, renderer)
        {
        }

        public override void Initialize() 
        {
            backgroundImage = new Image(graphics, renderer, "Assets/Textures/bg_menu.png");
            newGameButton = new Button(graphics, renderer);
            quitGameButton = new Button(graphics, renderer);
        }

        public override void LoadContent(AssetManager assetManager)
        {
            Font font = assetManager.LoadFont("Assets/Fonts/inter.ttf");
            font.Color = Color.White;
            font.Size = 24;

            backgroundImage.LoadContent(assetManager);
            newGameButton.LoadContent(assetManager);
            quitGameButton.LoadContent(assetManager);

            newGameButton.Content = "Nueva partida";
            newGameButton.Font = font;
            newGameButton.Position = new Point(161, 325);
            newGameButton.MouseUp += NewGameButton_MouseUp;

            quitGameButton.Content = "Salir";
            quitGameButton.Font = font;
            quitGameButton.Position = new Point(161, 400);
            quitGameButton.MouseUp += QuitGameButton_MouseUp;
        }

        private void QuitGameButton_MouseUp(object? sender, MouseEventArgs e)
        {
            QuitGame?.Invoke(this, EventArgs.Empty);
        }

        private void NewGameButton_MouseUp(object? sender, MouseEventArgs e)
        {
            NewGame?.Invoke(this, EventArgs.Empty);
        }

        public override void Render()
        {
            renderer.Render(backgroundImage);
            renderer.Render(newGameButton);
            renderer.Render(quitGameButton);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            backgroundImage.Update(gameTime);
            newGameButton.Update(gameTime);
            quitGameButton.Update(gameTime);
        }
    }
}
