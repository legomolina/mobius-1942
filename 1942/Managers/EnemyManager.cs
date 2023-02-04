using _1942.Entities.Enemies;
using Engine.Components;
using Engine.Core;
using Engine.Core.Managers;
using System.Collections.Generic;

namespace _1942.Managers
{
    public class EnemyManager : GameComponent, IDrawable
    {
        private readonly Dictionary<Enemy, int> enemies = new Dictionary<Enemy, int>();
        private readonly Engine.Core.Timer timer = new();

        public Enemy[] Enemies => enemies.Keys.ToArray();
        public int Order { get; set; } = 1;

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

        public override void Render()
        {
            foreach (KeyValuePair<Enemy, int> entry in enemies)
            {
                if (entry.Key.Health > 0)
                {
                    entry.Key.Render();
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<Enemy, int> entry in enemies)
            {
                if ((timer.ElapsedTime / 1000) >= entry.Value && !entry.Key.Initialized)
                {
                    entry.Key.Initialize();
                }

                if (entry.Key.Health > 0)
                {
                    entry.Key.Update(gameTime);
                }
                else
                {
                    enemies.Remove(entry.Key);
                    entry.Key.Dispose();
                }
            }
        }
    }
}
