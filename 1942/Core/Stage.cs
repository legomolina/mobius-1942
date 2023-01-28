using Engine.Components;
using Engine.Core;
using Engine.Core.Managers;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace _1942.Core
{
    public abstract class Stage : IUpdatable
    {
        public ObservableCollection<GameComponent> Components { get; private set; }

        protected IList<GameComponent> sortedComponents;

        public Stage()
        {
            Components = new ObservableCollection<GameComponent>();
            Components.CollectionChanged += Components_CollectionChanged;
            sortedComponents = new List<GameComponent>();
        }

        public abstract void Initialize();

        private void Components_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            sortedComponents = Components.OrderBy((GameComponent component) => component.Order).ToList();
        }

        public abstract void LoadContent(AssetManager assetManager);

        public virtual void Render()
        {
            foreach (GameComponent component in sortedComponents)
            {
                if (component.Active)
                {
                    component.Render();
                }
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (GameComponent component in sortedComponents)
            {
                if (component.Active)
                {
                    component.Update(gameTime);
                }
            }
        }
    }
}
