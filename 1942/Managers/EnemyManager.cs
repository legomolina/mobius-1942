using _1942.Entities.Enemies;
using Engine.Core;
using System.Collections.Generic;

namespace _1942.Managers
{
    public class EnemyManager : IDrawable, IUpdatable
    {
        private readonly Dictionary<Enemy, int> enemies = new Dictionary<Enemy, int>();
        private readonly Engine.Core.Timer timer = new();

        public EnemyManager()
        {
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
            int lastTime = enemies.Last().Value;
            enemies.Add(enemy, lastTime + time);
        }

        public void Initialize()
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
                entry.Key.Render();
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<Enemy, int> entry in enemies)
            {
                if ((timer.ElapsedTime / 1000) >= entry.Value && !entry.Key.Initialized)
                {
                    entry.Key.Initialize();
                }

                entry.Key.Update(gameTime);
            }
        }
    }
}
