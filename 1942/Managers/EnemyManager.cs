using _1942.Entities;
using _1942.Entities.Ships;
using Engine.Components;
using Engine.Core;
using Engine.Core.Collisions;
using Engine.Core.Managers;

namespace _1942.Managers
{
    public class EnemyManager : IUpdatable, IDrawable
    {
        private readonly Dictionary<Enemy, int> enemies = new Dictionary<Enemy, int>();
        private readonly Engine.Core.Timer timer = new();
        private readonly BatchRenderer renderer;
        private readonly CollisionsContainer collisionsContainer;

        public bool Active { get; set; }
        public Enemy[] Enemies => enemies.Keys.ToArray();
        public int Order { get; set; }

        public EnemyManager(BatchRenderer renderer, CollisionsContainer collisionsContainer)
        {
            this.renderer = renderer;
            this.collisionsContainer = collisionsContainer;

            Active = true;
            Order = 0;
        }

        public EnemyManager(Dictionary<Enemy, int> enemies)
        {
            this.enemies = enemies;
        }

        public void AddEnemy(Enemy enemy, int time = 0)
        {
            enemies.Add(enemy, time);
        }

        public void AddEnemyRelative(Enemy enemy, int time = 0)
        {
            if (enemies.Count == 0)
            {
                throw new MethodAccessException("Can't add relative enemy to non existent previous enemy.\nUse `AddEnemy()` first.");
            }

            int lastTime = enemies.Last().Value;
            enemies.Add(enemy, lastTime + time);
        }

        public void Start()
        {
            timer.Start();
        }

        public void LoadContent(AssetManager assetManager)
        {
            foreach (KeyValuePair<Enemy, int> entry in enemies)
            {
                entry.Key.LoadContent(assetManager);
            }
        }

        public void Render()
        {
            foreach (KeyValuePair<Enemy, int> entry in enemies)
            {
                if (entry.Key.Active)
                {
                    entry.Key.Render();
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<Enemy, int> entry in enemies)
            {
                if ((timer.ElapsedTime / 1000) >= entry.Value && !entry.Key.Initialized)
                {
                    entry.Key.Initialize();
                    collisionsContainer.Insert(entry.Key);
                }

                if (entry.Key.Active)
                {
                    entry.Key.Update(gameTime);
                }
                else if (!entry.Key.Active && entry.Key.Initialized)
                {
                    enemies.Remove(entry.Key);
                    collisionsContainer.Remove(entry.Key);
                }
            }
        }
    }
}
